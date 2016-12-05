using System;
using System.Runtime.InteropServices;

namespace starshipxac.Shell.Interop.Library
{
    internal static class ShellLibraryNativeMethods
    {
        /// <summary>
        ///     Creates an <see cref="IShellLibrary"/> object.
        /// </summary>
        /// <param name="riid"></param>
        /// <param name="ppvLibrary"></param>
        /// <returns></returns>
        /// <remarks>
        ///     https://msdn.microsoft.com/en-us/library/windows/desktop/dd378433(v=vs.85).aspx
        /// </remarks>
        [DllImport("Shell32", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi, SetLastError = true)]
        internal static extern HRESULT SHCreateLibrary(
            [In] [MarshalAs(UnmanagedType.LPStruct)] Guid riid,
            [Out] [MarshalAs(UnmanagedType.Interface)] out IShellLibrary ppvLibrary);

        /// <summary>
        ///     Shows the library management dialog box,
        ///     which enables users to manage the library folders and default save location.
        /// </summary>
        /// <param name="psiLibrary"></param>
        /// <param name="hwndOwner"></param>
        /// <param name="pszTitle"></param>
        /// <param name="pszInstruction"></param>
        /// <param name="lmdOptions"></param>
        /// <returns></returns>
        /// <remarks>
        ///     https://msdn.microsoft.com/en-us/library/windows/desktop/dd378441(v=vs.85).aspx
        /// </remarks>
        [DllImport("Shell32", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi, SetLastError = true)]
        internal static extern HRESULT SHShowManageLibraryUI(
            [In] [MarshalAs(UnmanagedType.Interface)] IShellItem psiLibrary,
            [In] IntPtr hwndOwner,
            [In] string pszTitle,
            [In] string pszInstruction,
            [In] LIBRARYMANAGEDIALOGOPTIONS lmdOptions);
    }
}