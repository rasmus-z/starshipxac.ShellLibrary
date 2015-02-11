using System;
using System.Runtime.InteropServices;
using System.Text;

namespace starshipxac.Windows.Shell.Interop
{
	internal static class MenuNativeMethods
	{
		// Menu
		[DllImport("user32", SetLastError = true, CharSet = CharSet.Auto)]
		internal static extern IntPtr CreatePopupMenu();

		[DllImport("user32", SetLastError = true, CharSet = CharSet.Auto)]
		internal static extern bool DestroyMenu(IntPtr hMenu);

		[DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
		internal static extern uint TrackPopupMenuEx(
			IntPtr hmenu,
			uint flags,
			int x,
			int y,
			IntPtr hwnd,
			IntPtr lptpm);

		[DllImport("user32.dll")]
		internal static extern bool GetMenuItemInfo(IntPtr hMenu, UInt32 uItem, bool fByPosition, ref MENUITEMINFO lpmii);

		[DllImport("user32.dll")]
		internal static extern int GetMenuString(
			IntPtr hMenu,
			UInt32 uIDItem,
			[Out] [MarshalAs(UnmanagedType.LPStr)] StringBuilder lpString,
			Int32 nMaxCount,
			UInt32 uFlag);

		internal static UInt32 MF_BYCOMMAND = 0x00000000;
		internal static UInt32 MF_BYPOSITION = 0x00000400;
	}
}