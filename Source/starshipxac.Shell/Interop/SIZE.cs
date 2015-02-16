using System;
using System.Runtime.InteropServices;

namespace starshipxac.Shell.Interop
{
    /// <summary>
    /// <c>SIZE</c>構造体を定義します。
    /// </summary>
    /// <remarks>
    /// http://msdn.microsoft.com/en-us/library/windows/desktop/dd145106(v=vs.85).aspx
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal struct SIZE
    {
        public SIZE(int width, int height)
            : this()
        {
            this.Width = width;
            this.Height = height;
        }

        public SIZE(double width, double height)
            : this()
        {
            this.Width = Convert.ToInt32(width);
            this.Height = Convert.ToInt32(height);
        }

        /// <summary>
        /// 幅
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// 高さ
        /// </summary>
        public int Height { get; set; }
    }
}