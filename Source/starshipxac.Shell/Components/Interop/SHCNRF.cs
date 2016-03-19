using System;

namespace starshipxac.Shell.Components.Interop
{
    /// <summary>
    ///     受信する通知を定義します。
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/bb762120(v=vs.85).aspx
    /// </remarks>
    internal static class SHCNRF
    {
        public const int SHCNRF_InterruptLevel = 0x0001;
        public const int SHCNRF_ShellLevel = 0x0002;
        public const int SHCNRF_RecursiveInterrupt = 0x1000;
        public const int SHCNRF_NewDelivery = 0x8000;
    }
}