﻿using System;
// ReSharper disable InconsistentNaming

namespace starshipxac.Windows.Shell.Controls.Explorers.Interop
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// http://msdn.microsoft.com/en-us/library/windows/desktop/bb762508(v=vs.85).aspx
    /// </remarks>
    [Flags]
    internal enum FOLDERFLAGS : uint
    {
        FWF_NONE = 0x00000000,
        FWF_AUTOARRANGE = 0x00000001,
        FWF_ABBREVIATEDNAMES = 0x00000002,
        FWF_SNAPTOGRID = 0x00000004,
        FWF_OWNERDATA = 0x00000008,
        FWF_BESTFITWINDOW = 0x00000010,
        FWF_DESKTOP = 0x00000020,
        FWF_SINGLESEL = 0x00000040,
        FWF_NOSUBFOLDERS = 0x00000080,
        FWF_TRANSPARENT = 0x00000100,
        FWF_NOCLIENTEDGE = 0x00000200,
        FWF_NOSCROLL = 0x00000400,
        FWF_ALIGNLEFT = 0x00000800,
        FWF_NOICONS = 0x00001000,
        FWF_SHOWSELALWAYS = 0x00002000,
        FWF_NOVISIBLE = 0x00004000,
        FWF_SINGLECLICKACTIVATE = 0x00008000,
        FWF_NOWEBVIEW = 0x00010000,
        FWF_HIDEFILENAMES = 0x00020000,
        FWF_CHECKSELECT = 0x00040000,
        FWF_NOENUMREFRESH = 0x00080000,
        FWF_NOGROUPING = 0x00100000,
        FWF_FULLROWSELECT = 0x00200000,
        FWF_NOFILTERS = 0x00400000,
        FWF_NOCOLUMNHEADER = 0x00800000,
        FWF_NOHEADERINALLVIEWS = 0x01000000,
        FWF_EXTENDEDTILES = 0x02000000,
        FWF_TRICHECKSELECT = 0x04000000,
        FWF_AUTOCHECKSELECT = 0x08000000,
        FWF_NOBROWSERVIEWSTATE = 0x10000000,
        FWF_SUBSETGROUPS = 0x20000000,
        FWF_USESEARCHFOLDER = 0x40000000,
        FWF_ALLOWRTLREADING = 0x80000000
    }
}