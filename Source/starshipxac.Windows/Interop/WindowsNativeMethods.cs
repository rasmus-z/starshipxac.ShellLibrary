using System;
using System.Runtime.InteropServices;
using System.Text;

namespace starshipxac.Windows.Interop
{
    internal static class WindowsNativeMethods
    {
        // Window
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.U2)]
        internal static extern UInt16 RegisterClassEx([In] ref WNDCLASSEX lpwndClass);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern IntPtr CreateWindowEx(
            UInt32 dwExStyle,
            string lpClassName,
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

        internal static readonly UInt32 CW_USEDEFAULT = 0x80000000;

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        internal delegate IntPtr WndProc(IntPtr hwnd, UInt32 msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool IsWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool IsWindowEnabled(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll")]
        internal static extern IntPtr GetParent(IntPtr hWnd);

        [DllImport("user32.dll")]
        internal static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll")]
        internal static extern IntPtr GetShellWindow();

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, UInt32 uFlags);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(HandleRef hwnd, out RECT lpRect);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool EnumWindows(ENUMWINDOWSPROC lpEnumFunc, IntPtr lParam);

        // Hook

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr SetWindowsHookEx(int idHook, HOOKPROC lpfn, IntPtr hmod, UInt32 dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        // Window Message

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern UInt32 RegisterWindowMessage(string lpString);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern IntPtr SendMessage(IntPtr hWnd, UInt32 msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool PostMessage(IntPtr hWnd, UInt32 msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SendNotifyMessage(IntPtr hWnd, UInt32 msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool ReplyMessage(IntPtr lResult);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate bool ENUMWINDOWSPROC(IntPtr hWnd, IntPtr lParam);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate IntPtr HOOKPROC(int code, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        internal static extern IntPtr DefWindowProc(IntPtr hWnd, UInt32 msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DestroyWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        // Resource

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern int LoadString(IntPtr instanceHandle, int id, StringBuilder buffer, int bufferSize);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern IntPtr LoadImage(IntPtr hInstance, string lpszName, UInt32 uType,
            int cxDesired, int cyDesired, UInt32 fuLoad);

        internal static IntPtr LoadIconImage(IntPtr hInstance, string lpszName, int cxDesired, int cyDesired, uint fuLoad)
        {
            return LoadImage(hInstance, lpszName, ImageTypes.IMAGE_ICON, cxDesired, cyDesired, fuLoad);
        }

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DestroyIcon(IntPtr hIcon);

        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DeleteObject(IntPtr hObject);
    }
}