using System;
using System.Runtime.InteropServices;

namespace starshipxac.Shell.Search.Interop
{
    [ComImport]
    [Guid(SearchIID.IConditionFactory)]
    [CoClass(typeof(ConditionFactoryCoClass))]
    internal interface IConditionFactoryNative : IConditionFactory
    {
    }
}