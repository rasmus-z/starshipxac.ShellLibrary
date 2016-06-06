using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace starshipxac.Shell.Interop
{
    /// <summary>
    ///     Win32エラーコードを定義します。
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Header File: WinError.h
    ///     </para>
    /// </remarks>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public static class ErrorCodes
    {
        public const int ERROR_SUCCESS = 0;
        public const int ERROR_OUTOFMEMORY = 14;
        public const int ERROR_HANDLE_EOF = 38;
        public const int ERROR_INVALID_PARAMETER = 87;
        public const int ERROR_BUSY = 170;
        public const int ERROR_CANCELLED = 1223;

        public const int MaxErrorCode = 0x0000FFFF;

        /// <summary>
        ///     最後に発生した Win32エラーを取得します。
        /// </summary>
        public static int LastError => Marshal.GetLastWin32Error();

        /// <summary>
        ///     最後に発生した Win32エラーをエラーメッセージに変換します。
        /// </summary>
        /// <returns>エラーメッセージ。</returns>
        public static string FormatLastErrorMessage()
        {
            return FormatErrorMessage(LastError);
        }

        /// <summary>
        ///     指定した Win32エラーコードをエラーメッセージに変換します。
        /// </summary>
        /// <param name="errorCode">Win32エラーコード。</param>
        /// <returns>エラーメッセージ。</returns>
        public static string FormatErrorMessage(int errorCode)
        {
            var buffer = IntPtr.Zero;

            var chars = FormatMessage(
                FORMAT_MESSAGE_ALLOCATE_BUFFER | FORMAT_MESSAGE_IGNORE_INSERTS | FORMAT_MESSAGE_FROM_SYSTEM,
                IntPtr.Zero,
                (UInt32)errorCode,
                0,
                ref buffer, 0, IntPtr.Zero);
            if (chars <= 0)
            {
                return String.Empty;
            }

            try
            {
                return Marshal.PtrToStringAnsi(buffer);
            }
            finally
            {
                LocalFree(buffer);
            }
        }

        #region Native Methods

        private const UInt32 FORMAT_MESSAGE_ALLOCATE_BUFFER = 0x00000100;
        private const UInt32 FORMAT_MESSAGE_IGNORE_INSERTS = 0x00000200;
        private const UInt32 FORMAT_MESSAGE_FROM_SYSTEM = 0x00001000;
        private const UInt32 FORMAT_MESSAGE_ARGUMENT_ARRAY = 0x00002000;
        private const UInt32 FORMAT_MESSAGE_FROM_HMODULE = 0x00000800;
        private const UInt32 FORMAT_MESSAGE_FROM_STRING = 0x00000400;

        [DllImport("Kernel32.dll", SetLastError = true)]
        internal static extern UInt32 FormatMessage(UInt32 dwFlags, IntPtr lpSource,
            UInt32 dwMessageId, UInt32 dwLanguageId,
            ref IntPtr lpBuffer, UInt32 nSize, IntPtr pArguments);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern IntPtr LocalFree(IntPtr hMem);

        #endregion
    }
}