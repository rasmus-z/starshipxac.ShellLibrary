using System;

// ReSharper disable InconsistentNaming

namespace starshipxac.Windows.Shell.Interop
{
    /// <summary>
    ///     ポップアップメニュー表示位置フラグを定義します。
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/ms648002(v=vs.85).aspx
    /// </remarks>
    internal static class TPM
    {
        public const UInt32 TPM_LEFTALIGN = 0x0000;
        public const UInt32 TPM_CENTERALIGN = 0x0004;
        public const UInt32 TPM_RIGHTALIGN = 0x0008;
        public const UInt32 TPM_TOPALIGN = 0x0000;
        public const UInt32 TPM_VCENTERALIGN = 0x0010;
        public const UInt32 TPM_BOTTOMALIGN = 0x0020;
        public const UInt32 TPM_HORIZONTAL = 0x0000;
        public const UInt32 TPM_VERTICAL = 0x0040;
        public const UInt32 TPM_RETURNCMD = 0x0100;

        public const UInt32 TPM_RECURSE = 0x0001;
        public const UInt32 TPM_HORPOSANIMATION = 0x0400;
        public const UInt32 TPM_HORNEGANIMATION = 0x0800;
        public const UInt32 TPM_VERPOSANIMATION = 0x1000;
        public const UInt32 TPM_VERNEGANIMATION = 0x2000;
        public const UInt32 TPM_NOANIMATION = 0x4000;
        public const UInt32 TPM_LAYOUTRTL = 0x8000;
    }
}