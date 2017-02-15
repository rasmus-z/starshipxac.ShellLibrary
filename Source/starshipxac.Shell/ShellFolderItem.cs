using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.InteropServices;
using starshipxac.Shell.Interop;

namespace starshipxac.Shell
{
    public sealed class ShellFolderItem : IDisposable
    {
        private bool disposed = false;

        private ShellFolderItem(IShellFolder shellFolderInterface)
        {
            this.ShellFolderInterface = shellFolderInterface;
        }

        ~ShellFolderItem()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                }

                // Release unmanaged resources.
                Marshal.ReleaseComObject(this.ShellFolderInterface);
                this.ShellFolderInterface = null;

                this.disposed = true;
            }
        }

        public static ShellFolderItem FromShellItem(ShellItem shellItem)
        {
            object result;

            var handler = ShellBHID.BHID_SFObject;
            var hr = shellItem.ShellItemInterface.BindToHandler(
                IntPtr.Zero,
                ref handler,
                ref ShellIIDGuid.IShellFolder,
                out result);
            if (HRESULT.Failed(hr))
            {
                throw ShellException.FromHRESULT(hr);
            }

            return new ShellFolderItem(result as IShellFolder);
        }

        internal IShellFolder ShellFolderInterface { get; private set; }

        internal IEnumIDList EnumObjects(SHCONTF options)
        {
            IEnumIDList result;
            var hr = this.ShellFolderInterface.EnumObjects(
                IntPtr.Zero,
                options,
                out result);
            if (hr == COMErrorCodes.Cancelled)
            {
                var inner = Marshal.GetExceptionForHR(hr);
                throw new DirectoryNotFoundException(inner.Message, inner);
            }
            else if (HRESULT.Failed(hr))
            {
                throw ShellException.FromHRESULT(hr);
            }
            // hr == S_FALSEの場合は、子が存在しない。(result = null)
            // In the case of hr == S_FALSE, there is no child.(result = null)
            return result;
        }

        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public IntPtr GetUIObjectOf(IntPtr pidl, string shellIID)
        {
            var childPidls = new[] { pidl };
            var riid = new Guid(shellIID);
            IntPtr result;
            this.ShellFolderInterface.GetUIObjectOf(
                IntPtr.Zero,
                (uint)childPidls.Length,
                childPidls,
                ref riid,
                IntPtr.Zero,
                out result);

            return result;
        }

        public IntPtr ParseDisplayName(string displayName)
        {
            UInt32 pchEaten = 0;
            IntPtr result;
            UInt32 attributes = 0;
            this.ShellFolderInterface.ParseDisplayName(
                IntPtr.Zero,
                IntPtr.Zero,
                displayName,
                ref pchEaten,
                out result,
                ref attributes);

            return result;
        }
    }
}