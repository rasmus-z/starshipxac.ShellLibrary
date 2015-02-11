using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using starshipxac.Shell.Interop;

namespace starshipxac.Windows.Shell.Controls.Explorers.Interop
{
	/// <summary>
	/// 
	/// </summary>
	/// <remarks>
	/// http://msdn.microsoft.com/en-us/library/windows/desktop/bb761883(v=vs.85).aspx
	/// </remarks>
	[ComImport]
	[Guid(ExplorerBrowserIIDGuid.IExplorerBrowserEvents)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IExplorerBrowserEvents
	{
		[PreserveSig]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		HRESULT OnNavigationPending(IntPtr pidlFolder);

		[PreserveSig]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		HRESULT OnViewCreated([MarshalAs(UnmanagedType.IUnknown)] object psv);

		[PreserveSig]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		HRESULT OnNavigationComplete(IntPtr pidlFolder);

		[PreserveSig]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		HRESULT OnNavigationFailed(IntPtr pidlFolder);
	}
}