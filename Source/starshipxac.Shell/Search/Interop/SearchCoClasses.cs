using System;
using System.Runtime.InteropServices;

namespace starshipxac.Shell.Search.Interop
{
    [ComImport]
    [ClassInterface(ClassInterfaceType.None)]
    [TypeLibType(TypeLibTypeFlags.FCanCreate)]
    [Guid(SearchCLSID.ConditionFactory)]
    internal class ConditionFactoryCoClass
    {
    }

    [ComImport]
    [ClassInterface(ClassInterfaceType.None)]
    [TypeLibType(TypeLibTypeFlags.FCanCreate)]
    [Guid(SearchCLSID.QueryParserManager)]
    internal class QueryParserManagerCoClass
    {
    }
}