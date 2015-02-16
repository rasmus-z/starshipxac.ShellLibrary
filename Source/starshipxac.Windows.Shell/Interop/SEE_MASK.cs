using System;

namespace starshipxac.Windows.Shell.Interop
{
    /// <summary>
    /// <c>SHELLEXECUTEINFO</c>マスクを定義します。
    /// </summary>
    /// <remarks>
    /// http://msdn.microsoft.com/en-us/library/windows/desktop/bb759784(v=vs.85).aspx
    /// </remarks>
    internal static class SEE_MASK
    {
        public const UInt32 SEE_MASK_DEFAULT = 0x00000000;
        public const UInt32 SEE_MASK_CLASSNAME = 0x00000001;
        public const UInt32 SEE_MASK_CLASSKEY = 0x00000003;
        public const UInt32 SEE_MASK_IDLIST = 0x00000004;
        public const UInt32 SEE_MASK_INVOKEIDLIST = 0x0000000C;
        public const UInt32 SEE_MASK_ICON = 0x00000010;
        public const UInt32 SEE_MASK_HOTKEY = 0x00000020;
        public const UInt32 SEE_MASK_NOCLOSEPROCESS = 0x00000040;
        public const UInt32 SEE_MASK_CONNECTNETDRV = 0x00000080;
        public const UInt32 SEE_MASK_NOASYNC = 0x00000100;
        public const UInt32 SEE_MASK_FLAG_DDEWAIT = 0x00000100;
        public const UInt32 SEE_MASK_DOENVSUBST = 0x00000200;
        public const UInt32 SEE_MASK_FLAG_NO_UI = 0x00000400;
        public const UInt32 SEE_MASK_UNICODE = 0x00004000;
        public const UInt32 SEE_MASK_NO_CONSOLE = 0x00008000;
        public const UInt32 SEE_MASK_ASYNCOK = 0x00100000;
        public const UInt32 SEE_MASK_NOQUERYCLASSSTORE = 0x01000000;
        public const UInt32 SEE_MASK_HMONITOR = 0x00200000;
        public const UInt32 SEE_MASK_NOZONECHECKS = 0x00800000;
        public const UInt32 SEE_MASK_WAITFORINPUTIDLE = 0x02000000;
        public const UInt32 SEE_MASK_FLAG_LOG_USAGE = 0x04000000;
        public const UInt32 SEE_MASK_FLAG_HINST_IS_SITE = 0x08000000;
    }
}