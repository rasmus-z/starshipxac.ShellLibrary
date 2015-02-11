using System;
using System.Runtime.InteropServices;

namespace starshipxac.Shell.Interop.Library
{
	internal static class ShellLibraryNativeMethods
	{
		[DllImport("Shell32", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi, SetLastError = true)]
		internal static extern HRESULT SHShowManageLibraryUI(
			[In] [MarshalAs(UnmanagedType.Interface)] IShellItem library,
			[In] IntPtr hwndOwner,
			[In] string title,
			[In] string instruction,
			[In] LIBRARYMANAGEDIALOGOPTIONS lmdOptions);
	}
}