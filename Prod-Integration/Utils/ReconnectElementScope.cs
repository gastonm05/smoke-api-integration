using Coypu;
using System;
using OpenQA.Selenium;

namespace Prod_Integration.Utils
{
    /// <summary>
    /// There is a bug in Coypu/Selenium that causes the WebDriver to get disconnected when the grid is unstable.
    /// This class wraps an ElementScope and provides a way to reconnect to the WebDriver via a workaround.
    /// First, javascript is executed to stop the current browser session (in the case of an infinite load or non-ready state).
    /// Optionally, specified by the caller in the ExtendedOptions object, an opportunity to refresh the current page
    /// and/or retry the last action performed is available via the RefreshToRecover and RepeatImmediately flags.
    /// </summary>
    /// <seealso cref="CCC_Utils.CustomElementScope" />
    /// <seealso cref="CCC_Utils.ExtendedOptions" />
    public class ReconnectElementScope : CustomElementScope
    {
        /// <summary>
        /// Constructor and wraps ElementScope instance
        /// </summary>
        public ReconnectElementScope(ElementScope e) : base(e) { }

        /// <summary>
        /// Handles the retry options for actions.
        /// </summary>
        /// <param name="e">The original exception.</param>
        /// <param name="options">ExtendedOptions</param>
        /// <param name="repeat">The ElementScope action to repeat.</param>
        protected void HandleRetryOptions(Exception e, ExtendedOptions options, Action repeat)
        {
            if (options == null) throw new Exception("Options cannot be null.");
            // find the browser session
            DriverScope cursor = OuterScope;
            while (cursor != null && !(cursor is BrowserSession))
            {
                cursor = OuterScope.OuterScope;
            }
            if (cursor is BrowserSession) // can only stop or refresh browser session
            {
                var bs = (BrowserSession)cursor;
                bs.ExecuteScript("window.stop();");
                if (options.RefreshToRecover && _elementScope.Missing(new Options() { Timeout = TimeSpan.FromSeconds(3) }))
                {
                    bs.Refresh();
                }
                if (options.RepeatImmediately && _elementScope.Exists(new Options() { Timeout = TimeSpan.FromSeconds(3) }))
                {
                    repeat.Invoke();
                }
            }
            else // just fail like we normally would since we don't have a browser session to work with
            {
                throw e;
            }
        }

        /// <summary>
        /// Handles the retry options for boolean methods.
        /// </summary>
        /// <param name="e">The original exception.</param>
        /// <param name="options">ExtendedOptions, only RefreshToRecover supported</param>
        /// <param name="repeat">The ElementScope boolean method to repeat.</param>
        /// <returns></returns>
        protected bool HandleRetryOptions(Exception e, ExtendedOptions options, Func<bool> repeat)
        {
            if (options == null) throw new Exception("Options cannot be null.");
            // find the browser session
            DriverScope cursor = OuterScope;
            while (cursor != null && !(cursor is BrowserSession))
            {
                cursor = OuterScope.OuterScope;
            }
            if (cursor is BrowserSession) // can only stop or refresh browser session
            {
                var bs = (BrowserSession)cursor;
                bs.ExecuteScript("window.stop();");
                if (options.RefreshToRecover)
                {
                    bs.Refresh();
                }
                return repeat.Invoke();
            }
            else // just fail like we normally would since we don't have a browser session to work with
            {
                throw e;
            }

        }

        /// <summary>
        /// Calls Missing() on ElementScope and supports ExtendedOptions.RefreshToRecover
        /// </summary>
        /// <param name="options">ExtendedOptions, only RefreshToRecover supported</param>
        public virtual bool Missing(ExtendedOptions options = null)
        {
            var localOptions = options ?? new ExtendedOptions();
            try
            {
                return _elementScope.Missing(localOptions);
            }
            catch (WebDriverTimeoutException e)
            {
                return HandleRetryOptions(e, localOptions, () => _elementScope.Missing(localOptions));
            }
        }

        /// <summary>
        /// Calls Exists() on ElementScope and supports ExtendedOptions.RefreshToRecover
        /// </summary>
        /// <param name="options">ExtendedOptions, only RefreshToRecover supported</param>

        public virtual bool Exists(ExtendedOptions options = null)
        {
            var localOptions = options ?? new ExtendedOptions();
            try
            {
                return _elementScope.Exists(localOptions);
            }
            catch (WebDriverTimeoutException e)
            {
                return HandleRetryOptions(e, localOptions, () => _elementScope.Exists(localOptions));
            }
        }

        /// <summary>
        /// Calls Click() on ElementScope and supports ExtendedOptions
        /// </summary>
        /// <param name="options">ExtendedOptions</param>
        public virtual void Click(ExtendedOptions options = null)
        {
            var localOptions = options ?? new ExtendedOptions();
            try
            {
                _elementScope.Click(localOptions);
            }
            catch (WebDriverTimeoutException e)
            {
                HandleRetryOptions(e, localOptions, () => _elementScope.Click(localOptions));
            }
        }

        /// <summary>
        /// Calls SendKeys() on ElementScope and supports ExtendedOptions
        /// </summary>
        /// <param name="options">ExtendedOptions</param>
        public virtual void SendKeys(string value, ExtendedOptions options = null)
        {
            var localOptions = options ?? new ExtendedOptions();
            try
            {
                _elementScope.SendKeys(value, localOptions);
            }
            catch (WebDriverTimeoutException e)
            {
                HandleRetryOptions(e, localOptions, () =>
                {
                    ((IWebElement)_elementScope.Native).Clear();
                    _elementScope.SendKeys(value, localOptions);
                });
            }
        }

        /// <summary>
        /// Calls FillInWith() on ElementScope and supports ExtendedOptions
        /// </summary>
        /// <param name="options">ExtendedOptions</param>
        public virtual void FillInWith(string value, ExtendedOptions options = null)
        {
            var localOptions = options ?? new ExtendedOptions();
            try
            {
                _elementScope.FillInWith(value, localOptions);
            }
            catch (WebDriverTimeoutException e)
            {
                HandleRetryOptions(e, localOptions, () => _elementScope.FillInWith(value, localOptions));
            }
        }
    }
}
