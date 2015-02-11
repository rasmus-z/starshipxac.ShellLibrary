using System;
using System.Runtime.InteropServices;
using starshipxac.Shell.Interop;

namespace starshipxac.Windows.Shell.Controls.Interop
{
	[ComImport]
	[Guid(ShellIID.IContextMenu3)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IContextMenu3 : IContextMenu2
	{
		[PreserveSig]
		HRESULT HandleMenuMsg2(
			UInt32 uMsg,
			IntPtr wParam,
			IntPtr lParam,
			[Out] IntPtr plResult);
	}
}