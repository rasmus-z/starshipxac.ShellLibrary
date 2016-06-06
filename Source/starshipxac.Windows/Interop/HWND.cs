using System;
using System.Diagnostics.CodeAnalysis;

namespace starshipxac.Windows.Interop
{
    /// <summary>
    ///     ウィンドウハンドルを定義します。
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public static class HWND
    {
        public static readonly IntPtr Null = IntPtr.Zero;

        public static readonly IntPtr HWND_TOP = (IntPtr)0;
        public static readonly IntPtr HWND_BOTTOM = (IntPtr)1;
        public static readonly IntPtr HWND_TOPMOST = (IntPtr)(-1);
        public static readonly IntPtr HWND_NOTOPMOST = (IntPtr)(-2);

        public static readonly IntPtr HWND_BROADCAST = (IntPtr)0xffff;

        /// <summary>
        ///     メッセージ受信用ウィンドウ
        /// </summary>
        public static readonly IntPtr HWND_MESSAGE = (IntPtr)(-3);

        /// <summary>
        ///     デスクトップウィンドウ
        /// </summary>
        public static readonly IntPtr HWND_DESKTOP = (IntPtr)0;
    }
}