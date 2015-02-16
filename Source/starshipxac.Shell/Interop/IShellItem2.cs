using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using starshipxac.Shell.PropertySystem.Interop;

namespace starshipxac.Shell.Interop
{
#pragma warning disable 108,114
    /// <summary>
    /// シェルアイテムインターフェイス2を定義します。
    /// </summary>
    /// <remarks>
    /// http://msdn.microsoft.com/en-us/library/windows/desktop/bb761130(v=vs.85).aspx
    /// <para>
    /// <see cref="IShellItem"/>で定義済みのメソッドを削除しないこと。
    /// </para>
    /// </remarks>
    [ComImport]
    [Guid(ShellIID.IShellItem2)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IShellItem2 : IShellItem
    {
        // IShellItem Methods

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

        // IShellItem2 Methods

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT GetPropertyStore(
            [In] GETPROPERTYSTOREFLAGS Flags,
            [In] ref Guid riid,
            [Out] [MarshalAs(UnmanagedType.Interface)] out IPropertyStore ppv);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetPropertyStoreWithCreateObject(
            [In] GETPROPERTYSTOREFLAGS Flags,
            [In] [MarshalAs(UnmanagedType.IUnknown)] object punkCreateObject,
            [In] ref Guid riid,
            out IntPtr ppv);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetPropertyStoreForKeys(
            [In] ref PROPERTYKEY rgKeys,
            [In] uint cKeys,
            [In] GETPROPERTYSTOREFLAGS Flags,
            [In] ref Guid riid,
            [Out] [MarshalAs(UnmanagedType.IUnknown)] out IPropertyStore ppv);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetPropertyDescriptionList(
            [In] ref PROPERTYKEY keyType,
            [In] ref Guid riid,
            out IntPtr ppv);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT Update(
            [In] [MarshalAs(UnmanagedType.Interface)] IBindCtx pbc);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetProperty(
            [In] ref PROPERTYKEY key,
            [Out] PropVariant ppropvar);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetCLSID(
            [In] ref PROPERTYKEY key,
            out Guid pclsid);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetFileTime(
            [In] ref PROPERTYKEY key,
            out System.Runtime.InteropServices.ComTypes.FILETIME pft);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetInt32(
            [In] ref PROPERTYKEY key,
            out int pi);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT GetString(
            [In] ref PROPERTYKEY key,
            [MarshalAs(UnmanagedType.LPWStr)] out string ppsz);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetUInt32(
            [In] ref PROPERTYKEY key,
            out uint pui);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetUInt64(
            [In] ref PROPERTYKEY key,
            out ulong pull);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetBool(
            [In] ref PROPERTYKEY key,
            out int pf);
    }
}