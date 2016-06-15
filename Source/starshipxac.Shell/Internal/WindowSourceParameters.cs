using System;
using System.Diagnostics.Contracts;

namespace starshipxac.Shell.Internal
{
    internal class WindowSourceParameters
    {
        public WindowSourceParameters(string windowName)
        {
            Contract.Requires<ArgumentNullException>(windowName != null);

            this.WindowName = windowName;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.WindowName != null);
        }

        public string WindowName { get; }
        public UInt32 WindowClassStyle { get; set; }
        internal UInt32 WindowStyle { get; set; }
        internal UInt32 ExtendedWindowStyle { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }

        public IntPtr ParentWindow { get; set; }
        public WindowSourceHook WindowSourceHook { get; set; }
    }
}