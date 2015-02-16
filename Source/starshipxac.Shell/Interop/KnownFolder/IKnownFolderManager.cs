using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace starshipxac.Shell.Interop.KnownFolder
{
#pragma warning disable 0108

    /// <summary>
    /// 規定フォルダー管理インターフェイスを定義します。
    /// </summary>
    /// <remarks>
    /// http://msdn.microsoft.com/en-us/library/windows/desktop/bb761744(v=vs.85).aspx
    /// </remarks>
    [ComImport]
    [Guid(ShellIID.IKnownFolderManager)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IKnownFolderManager
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void FolderIdFromCsidl(int csidl, [Out] out Guid knownFolderID);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void FolderIdToCsidl([In] [MarshalAs(UnmanagedType.LPStruct)] Guid id, [Out] out int csidl);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetFolderIds([Out] out IntPtr folders, [Out] out UInt32 count);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT GetFolder([In] [MarshalAs(UnmanagedType.LPStruct)] Guid id,
            [Out] [MarshalAs(UnmanagedType.Interface)] out IKnownFolder knownFolder);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetFolderByName(string canonicalName,
            [Out] [MarshalAs(UnmanagedType.Interface)] out IKnownFolder knownFolder);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void RegisterFolder(
            [In] [MarshalAs(UnmanagedType.LPStruct)] Guid knownFolderGuid,
            [In] ref KNOWNFOLDER_DEFINITION pKFD);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void UnregisterFolder(
            [In] [MarshalAs(UnmanagedType.LPStruct)] Guid knownFolderGuid);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void FindFolderFromPath(
            [In] [MarshalAs(UnmanagedType.LPWStr)] string path,
            [In] int mode,
            [Out] [MarshalAs(UnmanagedType.Interface)] out IKnownFolder knownFolder);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT FindFolderFromIDList(IntPtr pidl, [Out] [MarshalAs(UnmanagedType.Interface)] out IKnownFolder knownFolder);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Redirect();
    }
}