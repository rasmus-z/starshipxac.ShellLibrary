using System;
using System.Diagnostics.Contracts;
using starshipxac.Shell.Components.Internal;

namespace starshipxac.Shell.Components
{
    /// <summary>
    ///     Define shell notificaiton event arguments.
    /// </summary>
    public class ShellNotificationEventArgs : EventArgs
    {
        /// <summary>
        ///     Initialize a new instance of the <see cref="ShellNotificationEventArgs" /> class.
        /// </summary>
        /// <param name="changeNotify">Shell change notification information.</param>
        internal ShellNotificationEventArgs(ShellChangeNotify changeNotify)
        {
            Contract.Requires<ArgumentNullException>(changeNotify != null);

            ChangeType = changeNotify.ChangeType;
            FromSystemInterrupt = changeNotify.FromSystemInterrupt;
        }

        /// <summary>
        ///     Get the <see cref="ShellObject" />.
        /// </summary>
        public ShellChangeTypes ChangeType { get; }

        /// <summary>
        ///     Get a value that determines whether the event that occurred is a system event.
        /// </summary>
        public bool FromSystemInterrupt { get; }
    }
}