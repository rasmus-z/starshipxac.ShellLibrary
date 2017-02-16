using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.InteropServices;
using starshipxac.Shell.Interop;

namespace starshipxac.Shell
{
    /// <summary>
    ///     Define ShellFolderItem class.
    /// </summary>
    public sealed class ShellFolderItem : IDisposable
    {
        private bool disposed = false;

        /// <summary>
        ///     Initialize a new instance of the <see cref="ShellFolderItem" /> class
        ///     to the specified <see cref="IShellFolder" />.
        /// </summary>
        /// <param name="shellFolderInterface"><see cref="IShellFolder" />.</param>
        private ShellFolderItem(IShellFolder shellFolderInterface)
        {
            this.ShellFolderInterface = shellFolderInterface;
        }

        /// <summary>
        ///     Finalizer.
        /// </summary>
        ~ShellFolderItem()
        {
            Dispose(false);
        }

        /// <summary>
        ///     Release all resources used by <see cref="ShellFolderItem" /> class.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Release all resources used by <see cref="ShellFolderItem" /> class,
        ///     and optionally releases managed resources.
        /// </summary>
        /// <param name="disposing">
        ///     <c>true</c> to release both managed and unmanaged resources.
        ///     <c>false</c> to release only unmanaged resources.
        /// </param>
        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    // Release managed resources.
                }

                // Release unmanaged resources.
                Marshal.ReleaseComObject(this.ShellFolderInterface);
                this.ShellFolderInterface = null;

                this.disposed = true;
            }
        }

        /// <summary>
        ///     Create a new instance of the <see cref="ShellFolderItem" /> class
        ///     to the specified <see cref="ShellItem" />.
        /// </summary>
        /// <param name="shellItem"><see cref="ShellItem" />.</param>
        /// <returns></returns>
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

        /// <summary>
        ///     Get the <see cref="IShellFolder" />.
        /// </summary>
        internal IShellFolder ShellFolderInterface { get; private set; }

        /// <summary>
        ///     Get the <see cref="IEnumIDList" />.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
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