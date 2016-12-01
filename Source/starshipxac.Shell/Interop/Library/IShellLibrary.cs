using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace starshipxac.Shell.Interop.Library
{
    /// <summary>
    ///     Exposes methods for creating and managing libraries.
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/dd391719(v=vs.85).aspx
    /// </remarks>
    [ComImport]
    [Guid(ShellIID.IShellLibrary)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IShellLibrary
    {
        /// <summary>
        ///     Loads the library from a specified library definition file.
        /// </summary>
        /// <param name="library"></param>
        /// <param name="grfMode"></param>
        /// <returns></returns>
        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT LoadLibraryFromItem(
            [In] [MarshalAs(UnmanagedType.Interface)] IShellItem library,
            [In] UInt32 grfMode);

        /// <summary>
        ///     Loads the library that is referenced by a <c>KNOWNFOLDERID</c>.
        /// </summary>
        /// <param name="knownfidLibrary"></param>
        /// <param name="grfMode"></param>
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void LoadLibraryFromKnownFolder(
            [In] ref Guid knownfidLibrary,
            [In] UInt32 grfMode);

        /// <summary>
        ///     Adds a folder to the library.
        /// </summary>
        /// <param name="location"></param>
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void AddFolder([In] [MarshalAs(UnmanagedType.Interface)] IShellItem location);

        /// <summary>
        ///     Removes a folder from the library.
        /// </summary>
        /// <param name="location"></param>
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void RemoveFolder([In] [MarshalAs(UnmanagedType.Interface)] IShellItem location);

        /// <summary>
        ///     Gets the set of child folders that are contained in the library.
        /// </summary>
        /// <param name="lff"></param>
        /// <param name="riid"></param>
        /// <param name="ppv"></param>
        /// <returns></returns>
        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT GetFolders(
            [In] LIBRARYFOLDERFILTER lff,
            [In] ref Guid riid,
            [MarshalAs(UnmanagedType.Interface)] out IShellItemArray ppv);

        /// <summary>
        ///     Resolves the target location of a library folder, even if the folder has been moved or renamed.
        /// </summary>
        /// <param name="folderToResolve"></param>
        /// <param name="timeout"></param>
        /// <param name="riid"></param>
        /// <param name="ppv"></param>
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void ResolveFolder(
            [In] [MarshalAs(UnmanagedType.Interface)] IShellItem folderToResolve,
            [In] uint timeout,
            [In] ref Guid riid,
            [MarshalAs(UnmanagedType.Interface)] out IShellItem ppv);

        /// <summary>
        ///     Retrieves the default target folder that the library uses for save operations.
        /// </summary>
        /// <param name="dsft"></param>
        /// <param name="riid"></param>
        /// <param name="ppv"></param>
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetDefaultSaveFolder(
            [In] DEFAULTSAVEFOLDERTYPE dsft,
            [In] ref Guid riid,
            [MarshalAs(UnmanagedType.Interface)] out IShellItem ppv);

        /// <summary>
        ///     Sets the default target folder that the library will use for save operations.
        /// </summary>
        /// <param name="dsft"></param>
        /// <param name="si"></param>
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void SetDefaultSaveFolder(
            [In] DEFAULTSAVEFOLDERTYPE dsft,
            [In] [MarshalAs(UnmanagedType.Interface)] IShellItem si);

        /// <summary>
        ///     Gets the library's options.
        /// </summary>
        /// <param name="lofOptions"></param>
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetOptions(
            out LIBRARYOPTIONFLAGS lofOptions);

        /// <summary>
        ///     Sets the library options.
        /// </summary>
        /// <param name="lofMask"></param>
        /// <param name="lofOptions"></param>
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void SetOptions(
            [In] LIBRARYOPTIONFLAGS lofMask,
            [In] LIBRARYOPTIONFLAGS lofOptions);

        /// <summary>
        ///     Gets the library's folder type. 
        /// </summary>
        /// <param name="ftid"></param>
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetFolderType(out Guid ftid);

        /// <summary>
        ///     Sets the library's folder type.
        /// </summary>
        /// <param name="ftid"></param>
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void SetFolderType([In] ref Guid ftid);

        /// <summary>
        ///     Gets the default icon for the library.
        /// </summary>
        /// <param name="icon"></param>
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetIcon([MarshalAs(UnmanagedType.LPWStr)] out string icon);

        /// <summary>
        ///     Sets the default icon for the library.
        /// </summary>
        /// <param name="icon"></param>
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void SetIcon([In] [MarshalAs(UnmanagedType.LPWStr)] string icon);

        /// <summary>
        ///     Commits library updates to an existing Library Description file.
        /// </summary>
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Commit();

        /// <summary>
        ///     Saves the library to a new Library Description (*.library-ms) file.
        /// </summary>
        /// <param name="folderToSaveIn"></param>
        /// <param name="libraryName"></param>
        /// <param name="lsf"></param>
        /// <param name="savedTo"></param>
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Save(
            [In] [MarshalAs(UnmanagedType.Interface)] IShellItem folderToSaveIn,
            [In] [MarshalAs(UnmanagedType.LPWStr)] string libraryName,
            [In] LIBRARYSAVEFLAGS lsf,
            [MarshalAs(UnmanagedType.Interface)] out IShellItem2 savedTo);

        /// <summary>
        ///     Saves the library to a new file in a specified known folder.
        /// </summary>
        /// <param name="kfidToSaveIn"></param>
        /// <param name="libraryName"></param>
        /// <param name="lsf"></param>
        /// <param name="savedTo"></param>
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void SaveInKnownFolder(
            [In] ref Guid kfidToSaveIn,
            [In] [MarshalAs(UnmanagedType.LPWStr)] string libraryName,
            [In] LIBRARYSAVEFLAGS lsf,
            [MarshalAs(UnmanagedType.Interface)] out IShellItem2 savedTo);
    };
}