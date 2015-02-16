using System;
using System.Runtime.InteropServices;

namespace starshipxac.Shell.Interop.KnownFolder
{
    /// <summary>
    /// 標準フォルダー定義構造体を定義します。
    /// </summary>
    /// <remarks>
    /// http://msdn.microsoft.com/en-us/library/windows/desktop/bb773325(v=vs.85).aspx
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct KNOWNFOLDER_DEFINITION
    {
        public KF_CATEGORY category;

        public IntPtr pszName;

        public IntPtr pszDescription;

        public Guid fidParent;

        public IntPtr pszRelativePath;

        public IntPtr pszParsingName;

        public IntPtr pszTooltip;

        public IntPtr pszLocalizedName;

        public IntPtr pszIcon;

        public IntPtr pszSecurity;

        public UInt32 dwAttributes;

        public KF_DEFINITION_FLAGS kfdFlags;

        public Guid ftidType;
    }
}