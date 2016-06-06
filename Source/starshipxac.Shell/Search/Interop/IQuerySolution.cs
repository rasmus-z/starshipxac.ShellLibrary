using System;
using System.Runtime.InteropServices;
using starshipxac.Shell.Interop;
using starshipxac.Shell.PropertySystem.Interop;
using starshipxac.Shell.Runtime.Interop;

namespace starshipxac.Shell.Search.Interop
{
    /// <summary>
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/bb231346(v=vs.85).aspx
    /// </remarks>
    [ComImport]
    [Guid(SearchIID.IQuerySolution)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IQuerySolution : IConditionFactory
    {
#pragma warning disable 108
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
            /*[In] ICondition pc, [In] int sqro, [In] ref SYSTEMTIME pstReferenceTime, [Out] out ICondition ppcResolved*/);

        [PreserveSig]
        HRESULT GetQuery([Out] [MarshalAs(UnmanagedType.Interface)] out ICondition ppQueryNode,
            [Out] [MarshalAs(UnmanagedType.Interface)] out IEntity ppMainType);

        [PreserveSig]
        HRESULT GetErrors([In] ref Guid riid, [Out] out /* void** */ IntPtr ppParseErrors);

        [PreserveSig]
        HRESULT GetLexicalData([MarshalAs(UnmanagedType.LPWStr)] out string ppszInputString,
            [Out] /* ITokenCollection** */ out IntPtr ppTokens, [Out] out uint plcid,
            [Out] /* IUnknown** */ out IntPtr ppWordBreaker);
#pragma warning restore 108
    }
}