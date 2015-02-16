using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;

namespace starshipxac.Shell.Internal
{
    internal delegate IntPtr WindowSourceHook(IntPtr hWnd, UInt32 mes, IntPtr wParam, IntPtr lParam, ref bool handled);

    /// <summary>
    /// メッセージ通知用ウィンドウを定義します。
    /// </summary>
    internal class WindowSource : IDisposable
    {
        private bool disposed = false;

        // Delegateを保持しておくために必要。
        private static readonly WndProc StaticWndProc = WindowProc;

        private static readonly ConcurrentDictionary<IntPtr, WindowSourceHook> hooks =
            new ConcurrentDictionary<IntPtr, WindowSourceHook>();

        /// <summary>
        /// ウィンドウクラス名。
        /// </summary>
        private const string WindowClassName = "starshipxac.Shell.WindowClass";

        public static readonly IntPtr HWND_MESSAGE = (IntPtr)(-3);

        /// <summary>
        /// <see cref="WindowSource"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="parameters">ウィンドウ作成パラメータ。</param>
        public WindowSource(WindowSourceParameters parameters)
        {
            Contract.Requires<ArgumentNullException>(parameters != null);

            this.Handle = CreateWindow(parameters);
        }

        ~WindowSource()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                this.disposed = true;

                if (disposing)
                {
                }

                RemoveHook(this.Handle);
                DestroyWindow(this.Handle);
            }
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.Handle != IntPtr.Zero);
        }

        /// <summary>
        /// ウィンドウハンドルを取得します。
        /// </summary>
        public IntPtr Handle { get; private set; }

        public bool IsDisposed
        {
            get
            {
                return this.disposed;
            }
        }

        private static void AddHook(IntPtr hwnd, WindowSourceHook hook)
        {
            Contract.Requires<ArgumentNullException>(hook != null);
            hooks.TryAdd(hwnd, hook);
        }

        private static void RemoveHook(IntPtr hwnd)
        {
            Contract.Requires<ArgumentException>(hwnd != IntPtr.Zero);
            WindowSourceHook hook;
            hooks.TryRemove(hwnd, out hook);
        }

        public HandleRef CreateHandleRef()
        {
            Contract.Ensures(Contract.Result<HandleRef>().Handle != IntPtr.Zero);
            return new HandleRef(this, this.Handle);
        }

        /// <summary>
        /// ウィンドウを作成します。
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns>作成したウィンドウハンドル。</returns>
        private IntPtr CreateWindow(WindowSourceParameters parameters)
        {
            Contract.Requires<ArgumentNullException>(parameters != null);
            Contract.Ensures(Contract.Result<IntPtr>() != IntPtr.Zero);

            var res = RegisterClass();

            var hwnd = CreateWindowEx(
                0,
                (IntPtr)res,
                parameters.WindowName,
                0,
                0, 0, 0, 0,
                HWND_MESSAGE,
                IntPtr.Zero,
                IntPtr.Zero,
                IntPtr.Zero);
            if (hwnd == IntPtr.Zero)
            {
                throw new Win32Exception();
            }

            if (parameters.WindowSourceHook != null)
            {
                AddHook(hwnd, parameters.WindowSourceHook);
            }

            return hwnd;
        }

        private static ushort RegisterClass()
        {
            var wndclass = WNDCLASSEX.Create();
            wndclass.lpszClassName = WindowClassName;
            wndclass.lpfnWndProc = StaticWndProc;

            var result = RegisterClassEx(ref wndclass);
            if (result == 0)
            {
                throw new Win32Exception("RegisterClass error.");
            }

            return result;
        }

        private static IntPtr WindowProc(IntPtr hwnd, UInt32 message, IntPtr wParam, IntPtr lParam)
        {
            var handled = false;

            WindowSourceHook hook;
            if (hooks.TryGetValue(hwnd, out hook))
            {
                var ret = hook(hwnd, message, wParam, lParam, ref handled);
                if (handled)
                {
                    return ret;
                }
            }
            return DefWindowProc(hwnd, message, wParam, lParam);
        }

        public override int GetHashCode()
        {
            return this.Handle.GetHashCode();
        }

        #region Interop

        internal delegate IntPtr WndProc(IntPtr hwnd, UInt32 msg, IntPtr wParam, IntPtr lParam);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        internal struct WNDCLASSEX
        {
            public static WNDCLASSEX Create()
            {
                var result = new WNDCLASSEX
                {
                    cbSize = (UInt32)Marshal.SizeOf(typeof(WNDCLASSEX))
                };
                return result;
            }

            [MarshalAs(UnmanagedType.U4)]
            public UInt32 cbSize;

            [MarshalAs(UnmanagedType.U4)]
            public UInt32 style;

            public WndProc lpfnWndProc;
            public int cbClsExtra;
            public int cbWndExtra;
            public IntPtr hInstance;
            public IntPtr hIcon;
            public IntPtr hCursor;
            public IntPtr hbrBackground;
            public string lpszMenuName;
            public string lpszClassName;
            public IntPtr hIconSm;
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.U2)]
        internal static extern UInt16 RegisterClassEx([In] ref WNDCLASSEX lpwndClass);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern IntPtr CreateWindowEx(
            UInt32 dwExStyle,
            IntPtr lpClassName,
            string lpWindowName,
            UInt32 dwStyle,
            int x,
            int y,
            int nWidth,
            int nHeight,
            IntPtr hWndParent,
            IntPtr hMenu,
            IntPtr hInstance,
            IntPtr lpParam);

        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DestroyWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        internal static extern IntPtr DefWindowProc(IntPtr hWnd, UInt32 msg, IntPtr wParam, IntPtr lParam);

        #endregion
    }
}