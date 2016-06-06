using System;
using System.Diagnostics.CodeAnalysis;

namespace starshipxac.Shell.Interop
{
    /// <summary>
    ///     シェルイメージ取得フラグを定義します。
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/bb761082(v=vs.85).aspx
    /// </remarks>
    [Flags]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal enum SIIGBF
    {
        SIIGBF_RESIZETOFIT = 0x00000000,
        SIIGBF_BIGGERSIZEOK = 0x00000001,
        SIIGBF_MEMORYONLY = 0x00000002,
        SIIGBF_ICONONLY = 0x00000004,
        SIIGBF_THUMBNAILONLY = 0x00000008,
        SIIGBF_INCACHEONLY = 0x00000010,
        SIIGBF_CROPTOSQUARE = 0x00000020,
        SIIGBF_WIDETHUMBNAILS = 0x00000040,
        SIIGBF_ICONBACKGROUND = 0x00000080,
        SIIGBF_SCALEUP = 0x00000100,
    }
}