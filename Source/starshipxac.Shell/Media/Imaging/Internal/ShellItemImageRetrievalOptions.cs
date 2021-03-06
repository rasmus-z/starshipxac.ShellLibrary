﻿using System;
using starshipxac.Shell.Interop;

namespace starshipxac.Shell.Media.Imaging.Internal
{
    public enum ShellItemImageRetrievalOptions
    {
        Default,

        CacheOnly = SIIGBF.SIIGBF_INCACHEONLY,

        MemoryOnly = SIIGBF.SIIGBF_MEMORYONLY
    }
}