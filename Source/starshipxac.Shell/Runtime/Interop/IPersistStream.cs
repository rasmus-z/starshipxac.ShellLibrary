using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using starshipxac.Shell.Interop;

namespace starshipxac.Shell.Runtime.Interop
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// http://msdn.microsoft.com/en-us/library/windows/desktop/ms690091(v=vs.85).aspx
    /// </remarks>
    [ComImport]
    [Guid(ComIID.IPersistStream)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IPersistStream
    {
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
    }
}