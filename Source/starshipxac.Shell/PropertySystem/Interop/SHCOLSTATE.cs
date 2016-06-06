using System;
using System.Diagnostics.CodeAnalysis;

namespace starshipxac.Shell.PropertySystem.Interop
{
    /// <summary>
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/bb762538(v=vs.85).aspx
    /// </remarks>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal enum SHCOLSTATE : uint
    {
        SHCOLSTATE_DEFAULT = 0,
        SHCOLSTATE_TYPE_STR = 0x1,
        SHCOLSTATE_TYPE_INT = 0x2,
        SHCOLSTATE_TYPE_DATE = 0x3,
        SHCOLSTATE_TYPEMASK = 0xf,
        SHCOLSTATE_ONBYDEFAULT = 0x10,
        SHCOLSTATE_SLOW = 0x20,
        SHCOLSTATE_EXTENDED = 0x40,
        SHCOLSTATE_SECONDARYUI = 0x80,
        SHCOLSTATE_HIDDEN = 0x100,
        SHCOLSTATE_PREFER_VARCMP = 0x200,
        SHCOLSTATE_PREFER_FMTCMP = 0x400,
        SHCOLSTATE_NOSORTBYFOLDERNESS = 0x800,
        SHCOLSTATE_VIEWONLY = 0x10000,
        SHCOLSTATE_BATCHREAD = 0x20000,
        SHCOLSTATE_NO_GROUPBY = 0x40000,
        SHCOLSTATE_FIXED_WIDTH = 0x1000,
        SHCOLSTATE_NODPISCALE = 0x2000,
        SHCOLSTATE_FIXED_RATIO = 0x4000,
        SHCOLSTATE_DISPLAYMASK = 0xf000
    }
}