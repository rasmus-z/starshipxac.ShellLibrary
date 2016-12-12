using System;
using System.Diagnostics.CodeAnalysis;

namespace starshipxac.Shell.Components.Interop
{
    /// <summary>
    ///     受信する通知を定義します。
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/bb762120(v=vs.85).aspx
    /// </remarks>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal static class SHCNRF
    {
        /// <summary>
        ///     Interrupt level notifications from the file system.
        /// </summary>
        public const int SHCNRF_InterruptLevel = 0x0001;

        /// <summary>
        ///     Shell-level notifications from the shell.
        /// </summary>
        public const int SHCNRF_ShellLevel = 0x0002;

        /// <summary>
        ///     Interrupt events on the whole subtree.
        /// </summary>
        public const int SHCNRF_RecursiveInterrupt = 0x1000;

        /// <summary>
        ///     Messages received use shared memory.
        /// </summary>
        public const int SHCNRF_NewDelivery = 0x8000;
    }
}