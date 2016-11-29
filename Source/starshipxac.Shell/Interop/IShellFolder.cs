using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace starshipxac.Shell.Interop
{
    /// <summary>
    ///     <c>ShellFolder</c>インターフェイスを定義します。
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/bb775075(v=vs.85).aspx
    /// </remarks>
    [ComImport]
    [Guid(ShellIID.IShellFolder)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComConversionLoss]
    internal interface IShellFolder
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void ParseDisplayName(IntPtr hwnd,
            IntPtr pbc,
            [In] [MarshalAs(UnmanagedType.LPWStr)] string pszDisplayName,
            [In] [Out] ref UInt32 pchEaten,
            [Out] out IntPtr ppidl,
            [In] [Out] ref UInt32 pdwAttributes);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT EnumObjects([In] IntPtr hwnd,
            [In] SHCONTF grfFlags,
            [MarshalAs(UnmanagedType.Interface)] out IEnumIDList ppenumIDList);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT BindToObject([In] IntPtr pidl,
            IntPtr pbc,
            [In] ref Guid riid,
            [Out] [MarshalAs(UnmanagedType.Interface)] out IShellFolder ppv);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void BindToStorage([In] ref IntPtr pidl,
            IntPtr pbc,
            [In] ref Guid riid,
            out IntPtr ppv);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void CompareIDs([In] IntPtr lParam, [In] ref IntPtr pidl1, [In] ref IntPtr pidl2);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void CreateViewObject([In] IntPtr hwndOwner, [In] ref Guid riid, out IntPtr ppv);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetAttributesOf([In] uint cidl, [In] IntPtr apidl, [In] [Out] ref uint rgfInOut);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetUIObjectOf([In] IntPtr hwndOwner,
            [In] UInt32 cidl,
            [In] [MarshalAs(UnmanagedType.LPArray)] IntPtr[] apidl,
            [In] ref Guid riid,
            [In] IntPtr rgfReserved,
            [Out] out IntPtr ppv);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetDisplayNameOf([In] ref IntPtr pidl, [In] uint uFlags, out IntPtr pName);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void SetNameOf([In] IntPtr hwndOwner,
            [In] ref IntPtr pidl,
            [In] [MarshalAs(UnmanagedType.LPWStr)] string pszName,
            [In] UInt32 uFlags,
            [Out] IntPtr ppidlOut);
    }
}