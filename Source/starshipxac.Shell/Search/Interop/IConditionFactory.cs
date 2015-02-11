using System;
using System.Runtime.InteropServices;
using starshipxac.Shell.Interop;
using starshipxac.Shell.PropertySystem.Interop;
using starshipxac.Shell.Runtime.Interop;

namespace starshipxac.Shell.Search.Interop
{
	/// <summary>
	/// 
	/// </summary>
	/// <remarks>
	/// http://msdn.microsoft.com/en-us/library/windows/desktop/bb231383(v=vs.85).aspx
	/// </remarks>
	[ComImport]
	[Guid(SearchIID.IConditionFactory)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IConditionFactory
	{
		[PreserveSig]
		HRESULT MakeNot([In] ICondition pcSub, [In] bool fSimplify, [Out] out ICondition ppcResult);

		[PreserveSig]
		HRESULT MakeAndOr([In] CONDITION_TYPE ct, [In] IEnumUnknown peuSubs, [In] bool fSimplify, [Out] out ICondition ppcResult);

		[PreserveSig]
		HRESULT MakeLeaf(
			[In] [MarshalAs(UnmanagedType.LPWStr)] string pszPropertyName,
			[In] CONDITION_OPERATION cop,
			[In] [MarshalAs(UnmanagedType.LPWStr)] string pszValueType,
			[In] PropVariant ppropvar,
			IRichChunk richChunk1,
			IRichChunk richChunk2,
			IRichChunk richChunk3,
			[In] bool fExpand,
			[Out] out ICondition ppcResult);

		[PreserveSig]
		HRESULT Resolve(
			/* 
			[In] ICondition pc,
			[In] STRUCTURED_QUERY_RESOLVE_OPTION sqro,
			[In] ref SYSTEMTIME pstReferenceTime,
			[Out] out ICondition ppcResolved
			*/
			);
	}
}