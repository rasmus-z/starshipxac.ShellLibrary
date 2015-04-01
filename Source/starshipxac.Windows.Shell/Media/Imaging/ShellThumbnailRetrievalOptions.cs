using System;
using starshipxac.Shell.Interop;

namespace starshipxac.Windows.Shell.Media.Imaging
{
    public enum ShellThumbnailRetrievalOptions
    {
        Default,

        CacheOnly = SIIGBF.SIIGBF_INCACHEONLY,

        MemoryOnly = SIIGBF.SIIGBF_MEMORYONLY,
    }
}