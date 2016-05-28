using System;
using System.Diagnostics.Contracts;
using System.IO;
using starshipxac.Shell;
using starshipxac.Shell.Interop;
using starshipxac.Windows.Shell.Interop;

namespace starshipxac.Windows.Shell.FileSystem.Internal
{
    internal class FileOperationProgressSink : IFileOperationProgressSink
    {
        public FileOperationProgressSink(FileOperationProgress progress)
        {
            Contract.Requires<ArgumentNullException>(progress != null);

            this.Progress = progress;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.Progress != null);
        }

        public FileOperationProgress Progress { get; }

        public HRESULT StartOperations()
        {
            this.Progress.StartOperations();
            return COMErrorCodes.S_OK;
        }

        public HRESULT FinishOperations(HRESULT hrResult)
        {
            this.Progress.FinishOperations(hrResult);
            return COMErrorCodes.S_OK;
        }

        public HRESULT UpdateProgress(uint iWorkTotal, uint iWorkSoFar)
        {
            this.Progress.UpdateProgress(iWorkTotal, iWorkSoFar);
            return COMErrorCodes.S_OK;
        }

        public HRESULT ResetTimer()
        {
            return COMErrorCodes.S_OK;
        }

        public HRESULT PauseTimer()
        {
            return COMErrorCodes.S_OK;
        }

        public HRESULT ResumeTimer()
        {
            return COMErrorCodes.S_OK;
        }

        public HRESULT PreCopyItem(uint dwFlags, IShellItem psiItem, IShellItem psiDestinationFolder, string pszNewName)
        {
            this.Progress.PreCopy((TransferSourceFlags)dwFlags,
                CreateShellObject(psiItem), CreateShellObject(psiDestinationFolder), pszNewName);
            return COMErrorCodes.S_OK;
        }

        public HRESULT PostCopyItem(uint dwFlags, IShellItem psiItem, IShellItem psiDestinationFolder, string pszNewName,
            HRESULT hrCopy,
            IShellItem psiNewlyCreated)
        {
            this.Progress.PostCopy((TransferSourceFlags)dwFlags,
                CreateShellObject(psiItem), CreateShellObject(psiDestinationFolder), pszNewName,
                hrCopy, CreateShellObject(psiNewlyCreated));
            return COMErrorCodes.S_OK;
        }

        public HRESULT PreMoveItem(uint dwFlags, IShellItem psiItem, IShellItem psiDestinationFolder, string pszNewName)
        {
            this.Progress.PreMove((TransferSourceFlags)dwFlags,
                CreateShellObject(psiItem), CreateShellObject(psiDestinationFolder), pszNewName);
            return COMErrorCodes.S_OK;
        }

        public HRESULT PostMoveItem(uint dwFlags, IShellItem psiItem, IShellItem psiDestinationFolder, string pszNewName,
            HRESULT hrMove,
            IShellItem psiNewlyCreated)
        {
            this.Progress.PostMove((TransferSourceFlags)dwFlags,
                CreateShellObject(psiItem), CreateShellObject(psiDestinationFolder), pszNewName,
                hrMove, CreateShellObject(psiNewlyCreated));
            return COMErrorCodes.S_OK;
        }

        public HRESULT PreDeleteItem(uint dwFlags, IShellItem psiItem)
        {
            this.Progress.PreDelete((TransferSourceFlags)dwFlags, CreateShellObject(psiItem));
            return COMErrorCodes.S_OK;
        }

        public HRESULT PostDeleteItem(uint dwFlags, IShellItem psiItem, HRESULT hrDelete, IShellItem psiNewlyCreated)
        {
            this.Progress.PostDelete((TransferSourceFlags)dwFlags,
                CreateShellObject(psiItem),
                hrDelete, CreateShellObject(psiNewlyCreated));
            return COMErrorCodes.S_OK;
        }

        public HRESULT PreRenameItem(uint dwFlags, IShellItem psiItem, string pszNewName)
        {
            this.Progress.PreRename((TransferSourceFlags)dwFlags,
                CreateShellObject(psiItem), pszNewName);
            return COMErrorCodes.S_OK;
        }

        public HRESULT PostRenameItem(uint dwFlags, IShellItem psiItem, string pszNewName, HRESULT hrRename,
            IShellItem psiNewlyCreated)
        {
            this.Progress.PostRename((TransferSourceFlags)dwFlags,
                CreateShellObject(psiItem), pszNewName,
                hrRename, CreateShellObject(psiNewlyCreated));
            return COMErrorCodes.S_OK;
        }

        public HRESULT PreNewItem(uint dwFlags, IShellItem psiDestinationFolder, string pszNewName)
        {
            this.Progress.PreNew((TransferSourceFlags)dwFlags,
                CreateShellObject(psiDestinationFolder), pszNewName);
            return COMErrorCodes.S_OK;
        }

        public HRESULT PostNewItem(uint dwFlags, IShellItem psiDestinationFolder, string pszNewName, string pszTemplateName,
            FileAttributes dwFileAttributes,
            HRESULT hrNew, IShellItem psiNewItem)
        {
            this.Progress.PostNew((TransferSourceFlags)dwFlags,
                CreateShellObject(psiDestinationFolder), pszNewName, pszTemplateName,
                dwFileAttributes,
                hrNew, CreateShellObject(psiNewItem));
            return COMErrorCodes.S_OK;
        }

        #region Private Methods

        private ShellObject CreateShellObject(IShellItem shellItem)
        {
            if (shellItem == null)
            {
                return null;
            }

            return ShellFactory.FromShellItem(new ShellItem((IShellItem2)shellItem));
        }

        #endregion
    }
}