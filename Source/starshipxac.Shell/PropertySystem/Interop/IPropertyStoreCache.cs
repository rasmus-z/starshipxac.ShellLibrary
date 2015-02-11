using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using starshipxac.Shell.Interop;

namespace starshipxac.Shell.PropertySystem.Interop
{
	/// <summary>
	/// <c>IPropertyStoreCache</c>インターフェイスを定義します。
	/// </summary>
	/// <remarks>
	/// http://msdn.microsoft.com/en-us/library/windows/desktop/bb761466(v=vs.85).aspx
	/// </remarks>
	[ComImport]
	[Guid(PropertySystemIID.IPropertyStoreCache)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IPropertyStoreCache
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		HRESULT GetState(ref PROPERTYKEY key, [Out] out PSC_STATE state);

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		HRESULT GetValueAndState(ref PROPERTYKEY propKey, [Out] PropVariant pv, [Out] out PSC_STATE state);

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		HRESULT SetState(ref PROPERTYKEY propKey, PSC_STATE state);

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		HRESULT SetValueAndState(ref PROPERTYKEY propKey, [In] PropVariant pv, PSC_STATE state);
	}
}