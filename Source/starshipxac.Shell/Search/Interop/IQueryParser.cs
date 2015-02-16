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
    /// http://msdn.microsoft.com/en-us/library/windows/desktop/bb231353(v=vs.85).aspx
    /// </remarks>
    [ComImport]
    [Guid(SearchIID.IQueryParser)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IQueryParser
    {
        [PreserveSig]
        HRESULT Parse([In] [MarshalAs(UnmanagedType.LPWStr)] string pszInputString, [In] IEnumUnknown pCustomProperties,
            [Out] out IQuerySolution ppSolution);

        [PreserveSig]
        HRESULT SetOption([In] STRUCTURED_QUERY_SINGLE_OPTION option, [In] PropVariant pOptionValue);

        [PreserveSig]
        HRESULT GetOption([In] STRUCTURED_QUERY_SINGLE_OPTION option, [Out] PropVariant pOptionValue);

        [PreserveSig]
        HRESULT SetMultiOption([In] STRUCTURED_QUERY_MULTIOPTION option, [In] [MarshalAs(UnmanagedType.LPWStr)] string pszOptionKey,
            [In] PropVariant pOptionValue);

        [PreserveSig]
        HRESULT GetSchemaProvider([Out] out IntPtr ppSchemaProvider);

        [PreserveSig]
        HRESULT RestateToString([In] ICondition pCondition, [In] bool fUseEnglish,
            [Out] [MarshalAs(UnmanagedType.LPWStr)] out string ppszQueryString);

        [PreserveSig]
        HRESULT ParsePropertyValue([In] [MarshalAs(UnmanagedType.LPWStr)] string pszPropertyName,
            [In] [MarshalAs(UnmanagedType.LPWStr)] string pszInputString, [Out] out IQuerySolution ppSolution);

        [PreserveSig]
        HRESULT RestatePropertyValueToString([In] ICondition pCondition, [In] bool fUseEnglish,
            [Out] [MarshalAs(UnmanagedType.LPWStr)] out string ppszPropertyName,
            [Out] [MarshalAs(UnmanagedType.LPWStr)] out string ppszQueryString);
    }
}