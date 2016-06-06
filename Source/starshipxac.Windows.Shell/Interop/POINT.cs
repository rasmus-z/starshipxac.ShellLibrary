using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace starshipxac.Windows.Shell.Interop
{
    /// <summary>
    ///     <c>POINT</c>構造体を定義します。
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/dd162805(v=vs.85).aspx
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal struct POINT
    {
        public POINT(int x, int y)
            : this()
        {
            this.X = x;
            this.Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
    }
}