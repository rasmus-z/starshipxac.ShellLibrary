using System;
using System.Runtime.InteropServices;
using starshipxac.Shell.Interop;

namespace starshipxac.Shell.Runtime.Interop
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// http://msdn.microsoft.com/en-us/library/windows/desktop/ms683764(v=vs.85).aspx
    /// </remarks>
    [ComImport]
    [Guid(ComIID.IEnumUnknown)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IEnumUnknown
    {
        [PreserveSig]
        HRESULT Next(UInt32 requestedNumber, ref IntPtr buffer, ref UInt32 fetchedNumber);

        [PreserveSig]
        HRESULT Skip(UInt32 number);

        [PreserveSig]
        HRESULT Reset();

        [PreserveSig]
        HRESULT Clone(out IEnumUnknown result);
    }
}