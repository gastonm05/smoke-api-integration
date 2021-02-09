using Coypu;

namespace Prod_Integration.Utils
{
    /// <summary>
    /// Support retry logic for when Coypu or WebDriver get disconnected.
    /// </summary>
    /// <seealso cref="Coypu.Options" />
    public class ExtendedOptions : Options
    {
        /// <summary>
        /// Will refresh the browser on disconnect/timeout
        /// </summary>
        public bool RefreshToRecover { get; set; }
        /// <summary>
        /// For CisionElementScope, this option will immediately retry the current action.
        /// Not recommended if multiple actions were performed in a transaction
        /// such as filling out a page to submit data.
        /// For TryUntil, this option is not yet supported.
        /// </summary>
        public bool RepeatImmediately { get; set; }
    }
}
