using System;
using System.Diagnostics.Contracts;
using starshipxac.Shell.Components.Internal;

namespace starshipxac.Shell.Components
{
    /// <summary>
    ///     Define shell change event argments.
    /// </summary>
    public class ShellChangedEventArgs : ShellNotificationEventArgs
    {
        /// <summary>
        ///     Initialize a instance of the <see cref="ShellChangedEventArgs"/> class.
        /// </summary>
        /// <param name="changeNotify">Shell change notification information.</param>
        internal ShellChangedEventArgs(ShellChangeNotify changeNotify)
            : base(changeNotify)
        {
            Contract.Requires<ArgumentNullException>(changeNotify != null);
            Contract.Requires<ArgumentException>(changeNotify.ShellObject != null);

            this.ShellObject = changeNotify.ShellObject;
        }

        /// <summary>
        ///     Get the <see cref="ShellObject"/>.
        /// </summary>
        public ShellObject ShellObject { get; }
    }
}