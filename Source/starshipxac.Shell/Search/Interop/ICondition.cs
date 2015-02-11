using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using starshipxac.Shell.Interop;
using starshipxac.Shell.PropertySystem.Interop;
using starshipxac.Shell.Runtime.Interop;

namespace starshipxac.Shell.Search.Interop
{

	/// <summary>
	/// 
	/// </summary>
	/// <remarks>
	/// http://msdn.microsoft.com/en-us/library/windows/desktop/bb231395(v=vs.85).aspx
	/// </remarks>
	[ComImport]
	[Guid(SearchIID.ICondition)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface ICondition : IPersistStream
	{
#pragma warning disable 108
		[PreserveSig]
		void GetClassID(out Guid pClassID);

		[PreserveSig]
		HRESULT IsDirty();

		[PreserveSig]
		HRESULT Load([In] [MarshalAs(UnmanagedType.Interface)] IStream stm);

		[PreserveSig]
		HRESULT Save([In] [MarshalAs(UnmanagedType.Interface)] IStream stm, bool fRemember);

		[PreserveSig]
		HRESULT GetSizeMax(out ulong cbSize);

		[PreserveSig]
		HRESULT GetConditionType([Out] out CONDITION_TYPE pNodeType);

		[PreserveSig]
		HRESULT GetSubConditions([In] ref Guid riid, [Out] [MarshalAs(UnmanagedType.Interface)] out object ppv);

		[PreserveSig]
		HRESULT GetComparisonInfo(
			[Out] [MarshalAs(UnmanagedType.LPWStr)] out string ppszPropertyName,
			[Out] out CONDITION_OPERATION pcop,
			[Out] PropVariant ppropvar);

		[PreserveSig]
		HRESULT GetValueType([Out] [MarshalAs(UnmanagedType.LPWStr)] out string ppszValueTypeName);

		[PreserveSig]
		HRESULT GetValueNormalization([Out] [MarshalAs(UnmanagedType.LPWStr)] out string ppszNormalization);

		[PreserveSig]
		HRESULT GetInputTerms([Out] out IRichChunk ppPropertyTerm, [Out] out IRichChunk ppOperationTerm, [Out] out IRichChunk ppValueTerm);

		[PreserveSig]
		HRESULT Clone([Out] out ICondition ppc);
#pragma warning restore 108
	}
}