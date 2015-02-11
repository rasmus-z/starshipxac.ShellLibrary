using System;
using System.Runtime.InteropServices;
using starshipxac.Shell.Interop;

namespace starshipxac.Windows.Shell.Controls.Interop
{
	[ComImport]
	[Guid(ShellIID.IContextMenu2)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IContextMenu2 : IContextMenu
	{
		[PreserveSig]
		HRESULT HandleMenuMsg(
			UInt32 uMsg,
			IntPtr wParam,
			IntPtr lParam);
	}
}