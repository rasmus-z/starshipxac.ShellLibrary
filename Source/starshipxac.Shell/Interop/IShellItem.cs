using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace starshipxac.Shell.Interop
{
    /// <summary>
    ///     シェルアイテムインターフェイスを定義します。
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/bb761144(v=vs.85).aspx
    /// </remarks>
    [ComImport]
    [Guid(ShellIID.IShellItem)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IShellItem
    {
        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT BindToHandler(
            [In] IntPtr pbc,
            [In] ref Guid bhid,
            [In] ref Guid riid,
            [Out] [MarshalAs(UnmanagedType.Interface)] out object ppv);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT GetParent(
            [MarshalAs(UnmanagedType.Interface)] out IShellItem ppsi);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT GetDisplayName(
            [In] SIGDN sigdnName,
            [MarshalAs(UnmanagedType.LPWStr)] out string ppszName);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT GetAttributes(
            [In] UInt32 sfgaoMask,
            out UInt32 psfgaoAttribs);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT Compare(
            [In] [MarshalAs(UnmanagedType.Interface)] IShellItem psi,
            [In] SICHINTF hint,
            out int piOrder);
    }
}