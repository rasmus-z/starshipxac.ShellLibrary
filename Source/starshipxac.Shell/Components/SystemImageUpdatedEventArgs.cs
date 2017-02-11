using System;
using System.Diagnostics.Contracts;
using starshipxac.Shell.Components.Internal;

namespace starshipxac.Shell.Components
{
    /// <summary>
    ///     Define system image update event arguments.
    /// </summary>
    public class SystemImageUpdatedEventArgs : ShellNotificationEventArgs
    {
        /// <summary>
        ///     Initialize a new instance of the <see cref="SystemImageUpdatedEventArgs" /> class.
        /// </summary>
        /// <param name="changeNotify">Change notificaiton information.</param>
        internal SystemImageUpdatedEventArgs(ShellChangeNotify changeNotify)
            : base(changeNotify)
        {
            Contract.Requires<ArgumentNullException>(changeNotify != null);

            ImageIndex = changeNotify.ImageIndex;
        }

        /// <summary>
        ///     Get the image index.
        /// </summary>
        public int ImageIndex { get; }
    }
}