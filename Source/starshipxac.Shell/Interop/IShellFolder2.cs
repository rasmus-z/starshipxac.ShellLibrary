using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using starshipxac.Shell.PropertySystem.Interop;

namespace starshipxac.Shell.Interop
{
#pragma warning disable 108,114
    /// <summary>
    ///     シェルフォルダーインターフェイス2を定義します。
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/bb775055(v=vs.85).aspx
    /// </remarks>
    [ComImport]
    [Guid(ShellIID.IShellFolder2)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComConversionLoss]
    internal interface IShellFolder2 : IShellFolder
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

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetDefaultSearchGUID(out Guid pguid);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void EnumSearches([Out] out IntPtr ppenum);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetDefaultColumn([In] uint dwRes, out uint pSort, out uint pDisplay);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetDefaultColumnState([In] uint iColumn, out uint pcsFlags);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetDetailsEx([In] ref IntPtr pidl, [In] ref PROPERTYKEY pscid, [MarshalAs(UnmanagedType.Struct)] out object pv);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetDetailsOf([In] ref IntPtr pidl, [In] uint iColumn, out IntPtr psd);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void MapColumnToSCID([In] uint iColumn, out PROPERTYKEY pscid);
    }
}