using System;

namespace MultiScreenSample.Views
{
    public class CustomWindowStateEventArgs : EventArgs
    {
        public CustomWindowStateEventArgs(CustomWindowStates newWindowState, CustomWindowStates oldWindowState)
        {
            this.NewWindowState = newWindowState;
            this.OldWindowState = oldWindowState;
        }

        public CustomWindowStates NewWindowState { get; }

        public CustomWindowStates OldWindowState { get; }
    }
}