using System;
using System.Runtime.InteropServices;

// ReSharper disable InconsistentNaming

namespace starshipxac.Windows.Shell.Interop
{
    /// <summary>
    ///     メニューアイテム情報を保持します。
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/ms647578(v=vs.85).aspx
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal struct MENUITEMINFO
    {
        public UInt32 cbSize;
        public UInt32 fMask;
        public UInt32 fType;
        public UInt32 fState;
        public UInt32 wID;
        public IntPtr hSubMenu;
        public IntPtr hbmpChecked;
        public IntPtr hbmpUnchecked;
        public IntPtr dwItemData;

        [MarshalAs(UnmanagedType.LPTStr)]
        public string dwTypeData;

        public UInt32 cch;
        public IntPtr hbmpItem;

        public static MENUITEMINFO Create()
        {
            var result = new MENUITEMINFO();
            result.cbSize = (UInt32)Marshal.SizeOf(result);
            return result;
        }
    }
}