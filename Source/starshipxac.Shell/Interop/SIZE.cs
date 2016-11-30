using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace starshipxac.Shell.Interop
{
    /// <summary>
    ///     The <c>SIZE</c> structure specifies the width and height of a rectangle.
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/dd145106(v=vs.85).aspx
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
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
        ///     Specifies the rectangle's width. The units depend on which function uses this.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        ///     Specifies the rectangle's height. The units depend on which function uses this.
        /// </summary>
        public int Height { get; set; }
    }
}