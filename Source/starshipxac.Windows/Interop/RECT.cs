﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace starshipxac.Windows.Interop
{
    /// <summary>
    ///     <c>RECT</c>構造体を定義します。
    /// </summary>
    /// <remarks>
    ///     <code>http://msdn.microsoft.com/en-us/library/windows/desktop/dd162897(v=vs.85).aspx</code>
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal struct RECT
    {
        public RECT(int left, int top, int right, int bottom)
            : this()
        {
            this.Left = left;
            this.Top = top;
            this.Right = right;
            this.Bottom = bottom;
        }

        public static RECT Empty()
        {
            return new RECT(0, 0, 0, 0);
        }

        public static RECT Create(int left, int top, int right, int bottom)
        {
            return new RECT(left, top, right, bottom);
        }

        public static RECT Create(double left, double top, double right, double bottom)
        {
            return new RECT((int)left, (int)top, (int)right, (int)bottom);
        }

        public int Left { get; set; }
        public int Top { get; set; }
        public int Right { get; set; }
        public int Bottom { get; set; }

        public int Width => this.Right - this.Left;

        public int Height => this.Bottom - this.Top;
    }
}