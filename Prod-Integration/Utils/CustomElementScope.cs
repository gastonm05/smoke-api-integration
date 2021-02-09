using Coypu;
using Coypu.Actions;
using Coypu.Drivers;
using Coypu.Queries;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Prod_Integration.Utils
{
    public abstract class CustomElementScope : Scope, Element
    {
        protected ElementScope _elementScope;

        public CustomElementScope(ElementScope e)
        {
            _elementScope = e;
        }

        #region Visual Studio Auto-Generated this passthru code using Scope and Element interfaces
        public string this[string attributeName]
        {
            get
            {
                return ((Element)_elementScope)[attributeName];
            }
        }

        public Browser Browser
        {
            get
            {
                return ((Scope)_elementScope).Browser;
            }
        }

        public bool Disabled
        {
            get
            {
                return ((Element)_elementScope).Disabled;
            }
        }

        public string Id
        {
            get
            {
                return ((Element)_elementScope).Id;
            }
        }

        public string InnerHTML
        {
            get
            {
                return ((Element)_elementScope).InnerHTML;
            }
        }

        public Uri Location
        {
            get
            {
                return ((Scope)_elementScope).Location;
            }
        }

        public string Name
        {
            get
            {
                return ((Element)_elementScope).Name;
            }
        }

        public object Native
        {
            get
            {
                return ((Element)_elementScope).Native;
            }
        }

        public string OuterHTML
        {
            get
            {
                return ((Element)_elementScope).OuterHTML;
            }
        }

        public DriverScope OuterScope
        {
            get
            {
                return ((Scope)_elementScope).OuterScope;
            }
        }

        public bool Selected
        {
            get
            {
                return ((Element)_elementScope).Selected;
            }
        }

        public string SelectedOption
        {
            get
            {
                return ((Element)_elementScope).SelectedOption;
            }
        }

        public string Text
        {
            get
            {
                return ((Element)_elementScope).Text;
            }
        }

        public string Title
        {
            get
            {
                return ((Element)_elementScope).Title;
            }
        }

        public string Value
        {
            get
            {
                return ((Element)_elementScope).Value;
            }
        }

        public void Check(string locator, Options options = null)
        {
            ((Scope)_elementScope).Check(locator, options);
        }

        public void Choose(string locator, Options options = null)
        {
            ((Scope)_elementScope).Choose(locator, options);
        }

        public void ClickButton(string locator, Options options = null)
        {
            ((Scope)_elementScope).ClickButton(locator, options);
        }

        public Scope ClickButton(string locator, PredicateQuery until, Options options = null)
        {
            return ((Scope)_elementScope).ClickButton(locator, until, options);
        }

        public void ClickLink(string locator, Options options = null)
        {
            ((Scope)_elementScope).ClickLink(locator, options);
        }

        public Scope ClickLink(string locator, PredicateQuery until, Options options = null)
        {
            return ((Scope)_elementScope).ClickLink(locator, until, options);
        }

        public FillInWith FillIn(string locator, Options options = null)
        {
            return ((Scope)_elementScope).FillIn(locator, options);
        }

        public IEnumerable<SnapshotElementScope> FindAllCss(string cssSelector, Func<IEnumerable<SnapshotElementScope>, bool> predicate = null, Options options = null)
        {
            return ((Scope)_elementScope).FindAllCss(cssSelector, predicate, options);
        }

        public IEnumerable<SnapshotElementScope> FindAllXPath(string xpath, Func<IEnumerable<SnapshotElementScope>, bool> predicate = null, Options options = null)
        {
            return ((Scope)_elementScope).FindAllXPath(xpath, predicate, options);
        }

        public ElementScope FindButton(string locator, Options options = null)
        {
            return ((Scope)_elementScope).FindButton(locator, options);
        }

        public ElementScope FindCss(string cssSelector, Options options = null)
        {
            return ((Scope)_elementScope).FindCss(cssSelector, options);
        }

        public ElementScope FindCss(string cssSelector, Regex text, Options options = null)
        {
            return ((Scope)_elementScope).FindCss(cssSelector, text, options);
        }

        public ElementScope FindCss(string cssSelector, string text, Options options = null)
        {
            return ((Scope)_elementScope).FindCss(cssSelector, text, options);
        }

        public ElementScope FindField(string locator, Options options = null)
        {
            return ((Scope)_elementScope).FindField(locator, options);
        }

        public ElementScope FindFieldset(string locator, Options options = null)
        {
            return ((Scope)_elementScope).FindFieldset(locator, options);
        }

        public ElementScope FindFrame(string locator, Options options = null)
        {
            return ((Scope)_elementScope).FindFrame(locator, options);
        }

        public ElementScope FindId(string id, Options options = null)
        {
            return ((Scope)_elementScope).FindId(id, options);
        }

        public ElementScope FindIdEndingWith(string endsWith, Options options = null)
        {
            return ((Scope)_elementScope).FindIdEndingWith(endsWith, options);
        }

        public ElementScope FindLink(string locator, Options options = null)
        {
            return ((Scope)_elementScope).FindLink(locator, options);
        }

        public ElementScope FindSection(string locator, Options options = null)
        {
            return ((Scope)_elementScope).FindSection(locator, options);
        }

        public State FindState(params State[] states)
        {
            return ((Scope)_elementScope).FindState(states);
        }

        public State FindState(State[] states, Options options = null)
        {
            return ((Scope)_elementScope).FindState(states, options);
        }

        public ElementScope FindXPath(string xpath, Options options = null)
        {
            return ((Scope)_elementScope).FindXPath(xpath, options);
        }

        public ElementScope FindXPath(string idInspectingcontentUlIdCsstestLi, Regex text, Options options = null)
        {
            return ((Scope)_elementScope).FindXPath(idInspectingcontentUlIdCsstestLi, text, options);
        }

        public ElementScope FindXPath(string idInspectingcontentUlIdCsstestLi, string text, Options options = null)
        {
            return ((Scope)_elementScope).FindXPath(idInspectingcontentUlIdCsstestLi, text, options);
        }

        public bool HasContent(string text, Options options = null)
        {
            return ((Scope)_elementScope).HasContent(text, options);
        }

        public bool HasContentMatch(Regex pattern, Options options = null)
        {
            return ((Scope)_elementScope).HasContentMatch(pattern, options);
        }

        public bool HasCss(string cssSelector, Regex text, Options options = null)
        {
            return ((Scope)_elementScope).HasCss(cssSelector, text, options);
        }

        public bool HasCss(string cssSelector, string text, Options options = null)
        {
            return ((Scope)_elementScope).HasCss(cssSelector, text, options);
        }

        public bool HasNoContent(string text, Options options = null)
        {
            return ((Scope)_elementScope).HasNoContent(text, options);
        }

        public bool HasNoContentMatch(Regex pattern, Options options = null)
        {
            return ((Scope)_elementScope).HasNoContentMatch(pattern, options);
        }

        public bool HasNoCss(string cssSelector, Regex text, Options options = null)
        {
            return ((Scope)_elementScope).HasNoCss(cssSelector, text, options);
        }

        public bool HasNoCss(string cssSelector, string text, Options options = null)
        {
            return ((Scope)_elementScope).HasNoCss(cssSelector, text, options);
        }

        public bool HasNoXPath(string xpath, Options options = null)
        {
            return ((Scope)_elementScope).HasNoXPath(xpath, options);
        }

        public bool HasXPath(string xpath, Options options = null)
        {
            return ((Scope)_elementScope).HasXPath(xpath, options);
        }

        public Element Now()
        {
            return ((Scope)_elementScope).Now();
        }

        public T Query<T>(Query<T> query)
        {
            return ((Scope)_elementScope).Query<T>(query);
        }

        public T Query<T>(Func<T> query, T expecting, Options options = null)
        {
            return ((Scope)_elementScope).Query<T>(query, expecting, options);
        }

        public void RetryUntilTimeout(BrowserAction action)
        {
            ((Scope)_elementScope).RetryUntilTimeout(action);
        }

        public void RetryUntilTimeout(Action action, Options options = null)
        {
            ((Scope)_elementScope).RetryUntilTimeout(action, options);
        }

        public TResult RetryUntilTimeout<TResult>(Func<TResult> function, Options options = null)
        {
            return ((Scope)_elementScope).RetryUntilTimeout<TResult>(function, options);
        }

        public SelectFrom Select(string option, Options options = null)
        {
            return ((Scope)_elementScope).Select(option, options);
        }

        public void TryUntil(BrowserAction tryThis, PredicateQuery until, Options options = null)
        {
            ((Scope)_elementScope).TryUntil(tryThis, until, options);
        }

        public void TryUntil(Action tryThis, Func<bool> until, TimeSpan waitBeforeRetry, Options options = null)
        {
            ((Scope)_elementScope).TryUntil(tryThis, until, waitBeforeRetry, options);
        }

        public void Uncheck(string locator, Options options = null)
        {
            ((Scope)_elementScope).Uncheck(locator, options);
        }
        #endregion
    }
}
