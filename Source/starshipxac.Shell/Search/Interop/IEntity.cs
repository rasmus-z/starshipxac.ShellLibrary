using System;
using System.Runtime.InteropServices;

namespace starshipxac.Shell.Search.Interop
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// http://msdn.microsoft.com/en-us/library/windows/desktop/bb231373(v=vs.85).aspx
    /// </remarks>
    [ComImport]
    [Guid(SearchIID.IEntity)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IEntity
    {
    }
}