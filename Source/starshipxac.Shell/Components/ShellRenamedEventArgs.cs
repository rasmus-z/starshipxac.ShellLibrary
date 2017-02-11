using System;
using System.Diagnostics.Contracts;
using starshipxac.Shell.Components.Internal;

namespace starshipxac.Shell.Components
{
    /// <summary>
    ///     Define shell rename event arguments.
    /// </summary>
    public class ShellRenamedEventArgs : ShellNotificationEventArgs
    {
        /// <summary>
        ///     Initialize a instance of the <see cref="ShellRenamedEventArgs" /> class.
        /// </summary>
        /// <param name="changeNotify"></param>
        internal ShellRenamedEventArgs(ShellChangeNotify changeNotify)
            : base(changeNotify)
        {
            Contract.Requires<ArgumentNullException>(changeNotify != null);
            Contract.Requires<ArgumentException>(changeNotify.ShellObject != null);
            Contract.Requires<ArgumentException>(changeNotify.ShellObject2 != null);

            this.ShellObject = changeNotify.ShellObject;
            this.NewShellObject = changeNotify.ShellObject2;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.ShellObject != null);
            Contract.Invariant(this.NewShellObject != null);
        }

        /// <summary>
        ///     Get the <see cref="ShellObject" />.
        /// </summary>
        public ShellObject ShellObject { get; }

        /// <summary>
        ///     Get the new <see cref="ShellObject" />.
        /// </summary>
        public ShellObject NewShellObject { get; }
    }
}