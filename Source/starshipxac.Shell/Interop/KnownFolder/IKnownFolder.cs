using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace starshipxac.Shell.Interop.KnownFolder
{
    /// <summary>
    ///     標準フォルダーインターフェイスを定義します。
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/bb761768(v=vs.85).aspx
    /// </remarks>
    [ComImport]
    [Guid(ShellIID.IKnownFolder)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IKnownFolder
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        Guid GetId();

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        KF_CATEGORY GetCategory();

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [PreserveSig]
        HRESULT GetShellItem([In] int i, ref Guid interfaceGuid,
            [Out] [MarshalAs(UnmanagedType.Interface)] out IShellItem2 shellItem);

        [return: MarshalAs(UnmanagedType.LPWStr)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        string GetPath([In] int option);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void SetPath([In] int i, [In] string path);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetIDList([In] int i,
            [Out] out IntPtr itemIdentifierListPointer);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        Guid GetFolderType();

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        KF_REDIRECTION_CAPABILITIES GetRedirectionCapabilities();

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetFolderDefinition([Out] [MarshalAs(UnmanagedType.Struct)] out KNOWNFOLDER_DEFINITION definition);
    }
}