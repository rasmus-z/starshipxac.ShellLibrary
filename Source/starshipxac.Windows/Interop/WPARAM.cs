using System;

namespace starshipxac.Windows.Interop
{
    /// <summary>
    /// <c>WPARAM</c>を定義します。
    /// </summary>
    public static class WPARAM
    {
        public static IntPtr MakeWPARAM(int low, int high)
        {
            return new IntPtr((uint)((low << 16) + high));
        }

        public static readonly IntPtr Zero = IntPtr.Zero;
    }
}