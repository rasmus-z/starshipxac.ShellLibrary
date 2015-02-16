using System;

namespace starshipxac.Windows.Shell.Interop
{
    /// <summary>
    /// メニューアイテムマスクを定義します。
    /// </summary>
    /// <remarks>
    /// http://msdn.microsoft.com/en-us/library/windows/desktop/ms647578(v=vs.85).aspx
    /// </remarks>
    internal static class MIIM
    {
        public const UInt32 MIIM_BITMAP = 0x00000080;
        public const UInt32 MIIM_CHECKMARKS = 0x00000008;
        public const UInt32 MIIM_DATA = 0x00000020;
        public const UInt32 MIIM_FTYPE = 0x00000100;
        public const UInt32 MIIM_ID = 0x00000002;
        public const UInt32 MIIM_STATE = 0x00000001;
        public const UInt32 MIIM_STRING = 0x00000004;
        public const UInt32 MIIM_TYPE = 0x00000010;
    }
}