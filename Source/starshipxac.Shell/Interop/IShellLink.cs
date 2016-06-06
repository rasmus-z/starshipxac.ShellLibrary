using System;
using System.Runtime.InteropServices;
using System.Text;

namespace starshipxac.Shell.Interop
{
    /// <summary>
    ///     シェルリンクインターフェイスを定義します。
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/bb774950(v=vs.85).aspx
    /// </remarks>
    [ComImport]
    [Guid(ShellIID.IShellLink)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IShellLink
    {
        void GetPath(
            [Out] [MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszFile,
            int cchMaxPath,
            //ref _WIN32_FIND_DATAW pfd,
            IntPtr pfd,
            uint fFlags);

        void GetIDList(out IntPtr ppidl);

        void SetIDList(IntPtr pidl);

        void GetDescription(
            [Out] [MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszFile,
            int cchMaxName);

        void SetDescription([MarshalAs(UnmanagedType.LPWStr)] string pszName);

        void GetWorkingDirectory(
            [Out] [MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszDir,
            int cchMaxPath);

        void SetWorkingDirectory([MarshalAs(UnmanagedType.LPWStr)] string pszDir);

        void GetArguments(
            [Out] [MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszArgs,
            int cchMaxPath);

        void SetArguments([MarshalAs(UnmanagedType.LPWStr)] string pszArgs);

        void GetHotKey(out short wHotKey);

        void SetHotKey(short wHotKey);

        void GetShowCmd(out uint iShowCmd);

        void SetShowCmd(uint iShowCmd);

        void GetIconLocation(
            [Out] [MarshalAs(UnmanagedType.LPWStr)] out StringBuilder pszIconPath,
            int cchIconPath,
            out int iIcon);

        void SetIconLocation(
            [MarshalAs(UnmanagedType.LPWStr)] string pszIconPath,
            int iIcon);

        void SetRelativePath(
            [MarshalAs(UnmanagedType.LPWStr)] string pszPathRel,
            uint dwReserved);

        void Resolve(IntPtr hwnd, uint fFlags);

        void SetPath([MarshalAs(UnmanagedType.LPWStr)] string pszFile);
    }
}