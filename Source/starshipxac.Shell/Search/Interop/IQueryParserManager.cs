using System;
using System.Runtime.InteropServices;
using starshipxac.Shell.Interop;
using starshipxac.Shell.PropertySystem.Interop;

namespace starshipxac.Shell.Search.Interop
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// http://msdn.microsoft.com/en-us/library/windows/desktop/bb231349(v=vs.85).aspx
    /// </remarks>
    [ComImport]
    [Guid(SearchIID.IQueryParserManager)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IQueryParserManager
    {
        [PreserveSig]
        HRESULT CreateLoadedParser([In] [MarshalAs(UnmanagedType.LPWStr)] string pszCatalog, [In] ushort langidForKeywords,
            [In] ref Guid riid, [Out] out IQueryParser ppQueryParser);

        [PreserveSig]
        HRESULT InitializeOptions([In] bool fUnderstandNQS, [In] bool fAutoWildCard, [In] IQueryParser pQueryParser);

        [PreserveSig]
        HRESULT SetOption([In] QUERY_PARSER_MANAGER_OPTION option, [In] PropVariant pOptionValue);
    };
}