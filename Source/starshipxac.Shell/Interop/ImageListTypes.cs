using System;

namespace starshipxac.Shell.Interop
{
    /// <summary>
    /// イメージリストアイコンサイズを定義します。
    /// </summary>
    /// <remarks>
    /// http://msdn.microsoft.com/en-us/library/windows/desktop/bb762185(v=vs.85).aspx
    /// </remarks>
    internal static class ImageListTypes
    {
        public const int SHIL_LARGE = 0x0;
        public const int SHIL_SMALL = 0x1;
        public const int SHIL_EXTRALARGE = 0x2;
        public const int SHIL_SYSSMALL = 0x3;
        public const int SHIL_JUMBO = 0x4;
        public const int SHIL_LAST = SHIL_JUMBO;
    }
}