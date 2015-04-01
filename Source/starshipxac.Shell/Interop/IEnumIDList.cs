using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace starshipxac.Shell.Interop
{
    /// <summary>
    /// シェルアイテムID列挙インターフェイスを定義します。
    /// </summary>
    /// <remarks>
    /// http://msdn.microsoft.com/en-us/library/windows/desktop/bb761982(v=vs.85).aspx
    /// </remarks>
    [ComImport]
    [Guid(ShellIID.IEnumIDList)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IEnumIDList
    {
        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT Next(uint celt, out IntPtr rgelt, out uint pceltFetched);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT Skip([In] uint celt);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT Reset();

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT Clone([MarshalAs(UnmanagedType.Interface)] out IEnumIDList ppenum);
    }
}