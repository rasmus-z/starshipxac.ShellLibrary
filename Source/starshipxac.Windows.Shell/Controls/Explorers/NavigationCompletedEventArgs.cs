using System;
using starshipxac.Shell;

namespace starshipxac.Windows.Shell.Controls.Explorers
{
    public class NavigationCompletedEventArgs : ExplorerEventArgs
    {
        public NavigationCompletedEventArgs(ShellObject newLocation)
        {
            this.NewLocation = newLocation;
        }

        public ShellObject NewLocation { get; }
    }
}