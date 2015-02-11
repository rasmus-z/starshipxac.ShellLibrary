using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using starshipxac.Shell.Interop;

namespace starshipxac.Shell.PropertySystem.Interop
{
	/// <summary>
	/// <c>IPropertyStore</c>インターフェイスを定義します。
	/// </summary>
	/// <remarks>
	/// <c>http://msdn.microsoft.com/en-us/library/windows/desktop/bb761474(v=vs.85).aspx</c>
	/// </remarks>
	[ComImport]
	[Guid(PropertySystemIID.IPropertyStore)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IPropertyStore
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		HRESULT GetCount([Out] out uint propertyCount);

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		HRESULT GetAt([In] uint propertyIndex, out PROPERTYKEY key);

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		HRESULT GetValue([In] ref PROPERTYKEY key, [Out] PropVariant pv);

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[PreserveSig]
		HRESULT SetValue([In] ref PROPERTYKEY key, [In] PropVariant pv);

		[PreserveSig]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		HRESULT Commit();
	}
}