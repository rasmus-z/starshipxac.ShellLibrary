using System;
using starshipxac.Shell;

namespace starshipxac.Windows.Shell.Controls.Explorers
{
    public class NavigationPendingEventArgs : ExplorerEventArgs
    {
        public NavigationPendingEventArgs(ShellObject pendingLocation)
        {
            this.PendingLocation = pendingLocation;
        }

        public ShellObject PendingLocation { get; private set; }

        public bool Cancel { get; set; }
    }
}