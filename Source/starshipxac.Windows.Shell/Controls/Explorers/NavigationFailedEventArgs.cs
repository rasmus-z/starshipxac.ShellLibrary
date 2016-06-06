using System;
using starshipxac.Shell;

namespace starshipxac.Windows.Shell.Controls.Explorers
{
    public class NavigationFailedEventArgs : ExplorerEventArgs
    {
        public NavigationFailedEventArgs(ShellObject failedLocation)
        {
            this.FailedLocation = failedLocation;
        }

        public ShellObject FailedLocation { get; }
    }
}