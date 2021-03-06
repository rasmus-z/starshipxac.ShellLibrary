﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using starshipxac.Windows.Shell.Interop;

namespace starshipxac.Windows.Shell.Controls.Interop
{
    [StructLayout(LayoutKind.Sequential)]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal struct IMAGEINFO
    {
        public IntPtr hbmImage;
        public IntPtr hbmMask;
        public int Unused1;
        public int Unused2;
        public RECT rcImage;
    }
}