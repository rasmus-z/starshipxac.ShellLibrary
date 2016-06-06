using System;
using System.Diagnostics.Contracts;
using System.Windows;
using System.Windows.Interop;

namespace starshipxac.Windows.Extensions
{
    /// <summary>
    ///     <see cref="Window" />の拡張メソッドを定義します。
    /// </summary>
    public static class WindowExtension
    {
        /// <summary>
        ///     ウィンドウハンドルを取得します。
        /// </summary>
        /// <param name="window">ウィンドウ。</param>
        /// <returns>ウィンドウハンドル。</returns>
        public static IntPtr GetWindowHandle(this Window window)
        {
            Contract.Requires<ArgumentNullException>(window != null);

            var windowInteropHelper = new WindowInteropHelper(window);
            return windowInteropHelper.Handle;
        }

        /// <summary>
        ///     ウィンドウがサイズ変更可能かどうかを判定します。
        /// </summary>
        /// <param name="window">ウィンドウ。</param>
        /// <returns>サイズ変更が可能な場合は、<c>true</c>。それ以外の場合は<c>false</c>。</returns>
        public static bool CanResize(this Window window)
        {
            Contract.Requires<ArgumentNullException>(window != null);

            return window.ResizeMode == ResizeMode.CanResize || window.ResizeMode == ResizeMode.CanResizeWithGrip;
        }

        /// <summary>
        ///     ウィンドウを最小化できるかどうかを判定します。
        /// </summary>
        /// <param name="window"></param>
        /// <returns></returns>
        public static bool CanMinimize(this Window window)
        {
            Contract.Requires<ArgumentNullException>(window != null);

            return window.ResizeMode == ResizeMode.CanMinimize;
        }
    }
}