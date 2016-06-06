using System;
using System.Diagnostics.CodeAnalysis;

namespace starshipxac.Windows.Shell.Interop
{
    /// <summary>
    ///     メニューアイテム種別を定義します。
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/ms647578(v=vs.85).aspx
    /// </remarks>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal static class MFT
    {
        public const UInt32 MFT_BITMAP = 0x00000004;
        public const UInt32 MFT_MENUBARBREAK = 0x00000020;
        public const UInt32 MFT_MENUBREAK = 0x00000040;
        public const UInt32 MFT_OWNERDRAW = 0x00000100;
        public const UInt32 MFT_RADIOCHECK = 0x00000200;
        public const UInt32 MFT_RIGHTJUSTIFY = 0x00004000;
        public const UInt32 MFT_RIGHTORDER = 0x00002000;
        public const UInt32 MFT_SEPARATOR = 0x00000800;
        public const UInt32 MFT_STRING = 0x00000000;
    }
}