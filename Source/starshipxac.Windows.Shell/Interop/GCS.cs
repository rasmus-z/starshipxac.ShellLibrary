using System;

namespace starshipxac.Windows.Shell.Interop
{
    /// <summary>
    /// コマンド文字列取得フラグを定義します。
    /// </summary>
    /// <remarks>
    /// http://msdn.microsoft.com/en-us/library/windows/desktop/bb776094(v=vs.85).aspx
    /// </remarks>
    internal static class GCS
    {
        public const UInt32 GCS_VERBA = 0;
        public const UInt32 GCS_HELPTEXTA = 1;
        public const UInt32 GCS_VALIDATEA = 2;

        public const UInt32 GCS_VERBW = 4;
        public const UInt32 GCS_HELPTEXTW = 5;
        public const UInt32 GCS_VALIDATEW = 6;
    }
}