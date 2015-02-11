using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using starshipxac.Shell.Interop;

namespace starshipxac.Windows.Shell.Controls.Explorers.Interop
{
	/// <summary>
	/// 
	/// </summary>
	/// <remarks>
	/// http://msdn.microsoft.com/en-us/library/windows/desktop/bb761804(v=vs.85).aspx
	/// </remarks>
	[ComImport]
	[Guid(ExplorerBrowserIIDGuid.IInputObject)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IInputObject
	{
		[PreserveSig]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		HRESULT UIActivateIO(bool fActivate, ref MSG pMsg);

		[PreserveSig]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		HRESULT HasFocusIO();

		[PreserveSig]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		HRESULT TranslateAcceleratorIO(ref MSG pMsg);
	};
}