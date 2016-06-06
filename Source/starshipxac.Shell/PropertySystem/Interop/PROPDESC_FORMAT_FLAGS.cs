﻿using System;
using System.Diagnostics.CodeAnalysis;

namespace starshipxac.Shell.PropertySystem.Interop
{
    /// <summary>
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/bb762525(v=vs.85).aspx
    /// </remarks>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal enum PROPDESC_FORMAT_FLAGS
    {
        PDFF_DEFAULT = 0,
        PDFF_PREFIXNAME = 0x1,
        PDFF_FILENAME = 0x2,
        PDFF_ALWAYSKB = 0x4,
        PDFF_RESERVED_RIGHTTOLEFT = 0x8,
        PDFF_SHORTTIME = 0x10,
        PDFF_LONGTIME = 0x20,
        PDFF_HIDETIME = 0x40,
        PDFF_SHORTDATE = 0x80,
        PDFF_LONGDATE = 0x100,
        PDFF_HIDEDATE = 0x200,
        PDFF_RELATIVEDATE = 0x400,
        PDFF_USEEDITINVITATION = 0x800,
        PDFF_READONLY = 0x1000,
        PDFF_NOAUTOREADINGORDER = 0x2000
    }
}