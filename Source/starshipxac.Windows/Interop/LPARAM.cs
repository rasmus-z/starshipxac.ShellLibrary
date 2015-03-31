using System;
using System.Runtime.InteropServices;

namespace starshipxac.Windows.Interop
{
    /// <summary>
    /// <c>LPARAM</c>を定義します。
    /// </summary>
    public static class LPARAM
    {
        public static IntPtr MakeLPARAM(int low, int high)
        {
            return new IntPtr((high << 16) + low);
        }

        public static IntPtr FormStructure<T>(T s) where T : struct
        {
            var result = IntPtr.Zero;
            Marshal.StructureToPtr(s, result, true);
            return result;
        }

        public static readonly IntPtr Zero = IntPtr.Zero;
    }
}