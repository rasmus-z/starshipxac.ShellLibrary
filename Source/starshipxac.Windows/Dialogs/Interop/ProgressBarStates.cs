using System;

namespace starshipxac.Windows.Dialogs.Interop
{
    /// <summary>
    /// プログレスバー状態を定義します。
    /// </summary>
    /// <remarks>
    /// http://msdn.microsoft.com/en-us/library/windows/desktop/bb760850(v=vs.85).aspx
    /// </remarks>
    internal static class ProgressBarStates
    {
        public const UInt32 PBST_NORMAL = 0x0001;
        public const UInt32 PBST_ERROR = 0x0002;
        public const UInt32 PBST_PAUSED = 0x0003;
    }
}