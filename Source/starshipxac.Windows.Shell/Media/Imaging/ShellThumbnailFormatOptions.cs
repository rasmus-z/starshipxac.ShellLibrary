using System;
using starshipxac.Shell.Interop;

namespace starshipxac.Windows.Shell.Media.Imaging
{
    public enum ShellThumbnailFormatOptions
    {
        Default,

        ThumbnailOnly = SIIGBF.SIIGBF_THUMBNAILONLY,

        IconOnly = SIIGBF.SIIGBF_ICONONLY
    }
}