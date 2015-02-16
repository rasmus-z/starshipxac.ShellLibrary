using System;
using System.IO;
using System.Runtime.InteropServices;
using starshipxac.Shell.Interop;

namespace starshipxac.Windows.Shell.Interop
{
    /// <summary>
    /// <c>IFileOperation</c>を定義します。
    /// <list type="table">
    ///   <item>
    ///     <term>Header</term>
    ///     <description>Shobjidl.h</description>
    ///   </item>
    ///   <item>
    ///     <term>DLL</term>
    ///     <description>Shell32.dll (version 6.0.6000 or later)</description>
    ///   </item>
    /// </list>
    /// </summary>
    /// <remarks>
    /// http://msdn.microsoft.com/en-us/library/windows/desktop/bb775771(v=vs.85).aspx
    /// <para>
    /// メソッドは、<c>Shobjidl.h</c>で定義されているとおりの順番で記述する必要があります。
    /// </para>
    /// </remarks>
    [ComImport]
    [Guid(ShellIID.IFileOperation)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IFileOperation
    {
        UInt32 Advise(IFileOperationProgressSink pfops);

        void Unadvise(UInt32 dwCookie);

        void SetOperationFlags(UInt32 dwOperationFlags);

        void SetProgressMessage([MarshalAs(UnmanagedType.LPWStr)] string pszMessage);

        void SetProgressDialog([MarshalAs(UnmanagedType.Interface)] object popd);

        void SetProperties([MarshalAs(UnmanagedType.Interface)] object pproparray);

        void SetOwnerWindow(IntPtr hwndOwner);

        void ApplyPropertiesToItem(IShellItem psiItem);

        void ApplyPropertiesToItems([MarshalAs(UnmanagedType.Interface)] object punkItems);

        void RenameItem(IShellItem psiItem,
            [MarshalAs(UnmanagedType.LPWStr)] string pszNewName,
            IFileOperationProgressSink pfopsItem);

        void RenameItems([MarshalAs(UnmanagedType.Interface)] object punkItems,
            [MarshalAs(UnmanagedType.LPWStr)] string pszNewName);

        void MoveItem(IShellItem psiItem,
            IShellItem psiDestinationFolder,
            [MarshalAs(UnmanagedType.LPWStr)] string pszNewName,
            IFileOperationProgressSink pfopsItem);

        void MoveItems([MarshalAs(UnmanagedType.Interface)] object punkItems,
            IShellItem psiDestinationFolder);

        void CopyItem(IShellItem psiItem,
            IShellItem psiDestinationFolder,
            [MarshalAs(UnmanagedType.LPWStr)] string pszCopyName,
            IFileOperationProgressSink pfopsItem);

        void CopyItems([MarshalAs(UnmanagedType.Interface)] object punkItems,
            IShellItem psiDestinationFolder);

        void DeleteItem(IShellItem psiItem,
            IFileOperationProgressSink pfopsItem);

        void DeleteItems([MarshalAs(UnmanagedType.Interface)] object punkItems);

        void NewItem(IShellItem psiDestinationFolder,
            FileAttributes dwFileAttributes,
            [MarshalAs(UnmanagedType.LPWStr)] string pszNewName,
            [MarshalAs(UnmanagedType.LPWStr)] string pszTemplateName,
            IFileOperationProgressSink pfopsItem);

        void PerformOperations();

        [return: MarshalAs(UnmanagedType.Bool)]
        bool GetAnyOperationsAborted();
    }
}