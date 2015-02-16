using System;
using System.IO;
using System.Runtime.InteropServices;
using starshipxac.Shell.Interop;

namespace starshipxac.Windows.Shell.Interop
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// http://msdn.microsoft.com/en-us/library/windows/desktop/bb775722(v=vs.85).aspx
    /// </remarks>
    [ComImport]
    [Guid(ShellIID.IFileOperationProgressSink)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IFileOperationProgressSink
    {
        [PreserveSig]
        HRESULT StartOperations();

        [PreserveSig]
        HRESULT FinishOperations(HRESULT hrResult);

        [PreserveSig]
        HRESULT PreRenameItem(UInt32 dwFlags,
            IShellItem psiItem,
            [MarshalAs(UnmanagedType.LPWStr)] string pszNewName);

        [PreserveSig]
        HRESULT PostRenameItem(UInt32 dwFlags,
            IShellItem psiItem,
            [MarshalAs(UnmanagedType.LPWStr)] string pszNewName,
            HRESULT hrRename,
            IShellItem psiNewlyCreated);

        [PreserveSig]
        HRESULT PreMoveItem(UInt32 dwFlags,
            IShellItem psiItem,
            IShellItem psiDestinationFolder,
            [MarshalAs(UnmanagedType.LPWStr)] string pszNewName);

        [PreserveSig]
        HRESULT PostMoveItem(UInt32 dwFlags,
            IShellItem psiItem,
            IShellItem psiDestinationFolder,
            [MarshalAs(UnmanagedType.LPWStr)] string pszNewName,
            HRESULT hrMove,
            IShellItem psiNewlyCreated);

        [PreserveSig]
        HRESULT PreCopyItem(UInt32 dwFlags,
            IShellItem psiItem,
            IShellItem psiDestinationFolder,
            [MarshalAs(UnmanagedType.LPWStr)] string pszNewName);

        [PreserveSig]
        HRESULT PostCopyItem(UInt32 dwFlags,
            IShellItem psiItem,
            IShellItem psiDestinationFolder,
            [MarshalAs(UnmanagedType.LPWStr)] string pszNewName,
            HRESULT hrCopy,
            IShellItem psiNewlyCreated);

        [PreserveSig]
        HRESULT PreDeleteItem(UInt32 dwFlags,
            IShellItem psiItem);

        [PreserveSig]
        HRESULT PostDeleteItem(UInt32 dwFlags,
            IShellItem psiItem,
            HRESULT hrDelete,
            IShellItem psiNewlyCreated);

        [PreserveSig]
        HRESULT PreNewItem(UInt32 dwFlags,
            IShellItem psiDestinationFolder,
            [MarshalAs(UnmanagedType.LPWStr)] string pszNewName);

        [PreserveSig]
        HRESULT PostNewItem(UInt32 dwFlags,
            IShellItem psiDestinationFolder,
            [MarshalAs(UnmanagedType.LPWStr)] string pszNewName,
            [MarshalAs(UnmanagedType.LPWStr)] string pszTemplateName,
            FileAttributes dwFileAttributes,
            HRESULT hrNew,
            IShellItem psiNewItem);

        [PreserveSig]
        HRESULT UpdateProgress(UInt32 iWorkTotal, UInt32 iWorkSoFar);

        [PreserveSig]
        HRESULT ResetTimer();

        [PreserveSig]
        HRESULT PauseTimer();

        [PreserveSig]
        HRESULT ResumeTimer();
    }
}