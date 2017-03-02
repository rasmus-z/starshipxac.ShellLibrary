﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace starshipxac.Shell.PropertySystem.Interop
{
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal struct PROPARRAY
    {
        internal UInt32 cElems;
        internal IntPtr pElems;
    }
}