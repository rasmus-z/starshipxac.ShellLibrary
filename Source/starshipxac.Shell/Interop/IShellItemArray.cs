using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using starshipxac.Shell.PropertySystem.Interop;

namespace starshipxac.Shell.Interop
{
    /// <summary>
    /// シェルアイテム配列インターフェイスを定義します。
    /// </summary>
    /// <remarks>
    /// http://msdn.microsoft.com/en-us/library/windows/desktop/bb761106(v=vs.85).aspx
    /// </remarks>
    [ComImport]
    [Guid(ShellIID.IShellItemArray)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IShellItemArray
    {
        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT BindToHandler(
            [In] [MarshalAs(UnmanagedType.Interface)] IntPtr pbc,
            [In] ref Guid rbhid,
            [In] ref Guid riid,
            out IntPtr ppvOut);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT GetPropertyStore(
            [In] int Flags,
            [In] ref Guid riid,
            out IntPtr ppv);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT GetPropertyDescriptionList(
            [In] ref PROPERTYKEY keyType,
            [In] ref Guid riid,
            out IntPtr ppv);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT GetAttributes(
            [In] SIATTRIBFLAGS dwAttribFlags,
            [In] UInt32 sfgaoMask,
            out UInt32 psfgaoAttribs);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT GetCount(out uint pdwNumItems);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT GetItemAt(
            [In] uint dwIndex,
            [MarshalAs(UnmanagedType.Interface)] out IShellItem ppsi);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT EnumItems([MarshalAs(UnmanagedType.Interface)] out IntPtr ppenumShellItems);
    }
}