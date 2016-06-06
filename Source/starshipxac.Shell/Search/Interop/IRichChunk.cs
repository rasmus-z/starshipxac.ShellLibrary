using System;
using System.Runtime.InteropServices;
using starshipxac.Shell.Interop;

namespace starshipxac.Shell.Search.Interop
{
    /// <summary>
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/bb231336(v=vs.85).aspx
    /// </remarks>
    [ComImport]
    [Guid(SearchIID.IRichChunk)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IRichChunk
    {
        [PreserveSig]
        HRESULT GetData(
            /*
            [out, unique]  ULONG *pFirstPos,
            [out, unique]  ULONG *pLength,
            [out, unique]  LPWSTR *ppsz,
            [out, unique]  PROPVARIANT *pValue
            */
            );
    }
}