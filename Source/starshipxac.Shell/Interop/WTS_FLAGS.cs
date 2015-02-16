﻿using System;

namespace starshipxac.Shell.Interop
{
    /// <summary>
    /// サムネイルイメージフラグを定義します。
    /// </summary>
    /// <remarks>
    /// http://msdn.microsoft.com/en-us/library/windows/desktop/hh707152(v=vs.85).aspx
    /// </remarks>
    [Flags]
    internal enum WTS_FLAGS
    {
        WTS_NONE = 0,
        WTS_EXTRACT = 0,
        WTS_INCACHEONLY = 0x1,
        WTS_FASTEXTRACT = 0x2,
        WTS_FORCEEXTRACTION = 0x4,
        WTS_SLOWRECLAIM = 0x8,
        WTS_EXTRACTDONOTCACHE = 0x20,
        WTS_SCALETOREQUESTEDSIZE = 0x40,
        WTS_SKIPFASTEXTRACT = 0x80,
        WTS_EXTRACTINPROC = 0x100,
        WTS_CROPTOSQUARE = 0x200,
        WTS_INSTANCESURROGATE = 0x400,
        WTS_REQUIRESURROGATE = 0x800,
        WTS_APPSTYLE = 0x2000,
        WTS_WIDETHUMBNAILS = 0x4000,
        WTS_IDEALCACHESIZEONLY = 0x8000,
        WTS_SCALEUP = 0x10000
    }
}