using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using starshipxac.Shell.Interop;
using starshipxac.Shell.Properties;
using starshipxac.Shell.PropertySystem.Interop;

namespace starshipxac.Shell
{
    /// <summary>
    ///     Define <c>ShellItem</c>.
    /// </summary>
    public sealed class ShellItem : IDisposable
    {
        private bool disposed = false;

        /// <summary>
        ///     Initialize a new instance of the <see cref="ShellItem" /> class
        ///     to the specified <see cref="IShellItem" />.
        /// </summary>
        /// <param name="shellItem"><see cref="IShellItem" />.</param>
        internal ShellItem(IShellItem shellItem)
            : this((IShellItem2)shellItem)
        {
            Contract.Requires<ArgumentNullException>(shellItem != null);
        }

        /// <summary>
        ///     Initialize a new instance of the <see cref="ShellItem" /> class
        ///     to the specified <see cref="IShellItem2" />.
        /// </summary>
        /// <param name="shellItem2"><see cref="IShellItem2" />.</param>
        internal ShellItem(IShellItem2 shellItem2)
        {
            Contract.Requires<ArgumentNullException>(shellItem2 != null);

            this.ShellItemInterface = shellItem2;
            this.PIDL = PIDL.FromShellItem(this.ShellItemInterface);

            try
            {
                var attributes = GetAttributes(SFGAO.SFGAO_LINK | SFGAO.SFGAO_FILESYSTEM | SFGAO.SFGAO_FOLDER);

                this.IsLink = (attributes & SFGAO.SFGAO_LINK) != 0;
                this.IsFileSystem = (attributes & SFGAO.SFGAO_FILESYSTEM) != 0;
                this.IsFolder = (attributes & SFGAO.SFGAO_FOLDER) != 0;
            }
            catch
            {
                this.IsLink = false;
                this.IsFileSystem = false;
                this.IsFolder = false;
            }
        }

        /// <summary>
        ///     Finalizer.
        /// </summary>
        ~ShellItem()
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

                Marshal.FinalReleaseComObject(this.ShellItemInterface);

                this.disposed = true;
            }
        }

        /// <summary>
        ///     Create new instance of the <see cref="ShellItem" /> class
        ///     to the specified <c>ParsingName</c>.
        /// </summary>
        /// <param name="parsingName">Parsing Name.</param>
        /// <returns><see cref="ShellItem" />.</returns>
        /// <exception cref="ArgumentException"><paramref name="parsingName" /> is <c>null</c> or empty string.</exception>
        /// <exception cref="ShellException">Failed to create <see cref="ShellItem" />.</exception>
        public static ShellItem FromParsingName(string parsingName)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(parsingName));
            Contract.Ensures(Contract.Result<ShellItem>() != null);

            IShellItem2 shellItem2;
            var code = ShellNativeMethods.SHCreateItemFromParsingName(
                parsingName,
                IntPtr.Zero,
                ref ShellIIDGuid.IShellItem2,
                out shellItem2);
            if (shellItem2 == null || HRESULT.Failed(code))
            {
                throw new ShellException(ErrorMessages.ShellFactoryUnableToCreateItem, Marshal.GetExceptionForHR(code));
            }
            return new ShellItem(shellItem2);
        }

        /// <summary>
        ///     Create new instance of the <see cref="ShellItem" /> class
        ///     to the specified <c>PIDL</c>.
        /// </summary>
        /// <param name="pidl"><c>PIDL</c>.</param>
        /// <returns><see cref="ShellItem" />.</returns>
        /// <exception cref="ArgumentException"><paramref name="pidl" /> is <c>null</c>.</exception>
        /// <exception cref="ShellException">Failed to create <see cref="ShellItem" />.</exception>
        internal static ShellItem FromPIDL(PIDL pidl)
        {
            Contract.Requires<ArgumentException>(pidl != PIDL.Null);
            Contract.Ensures(Contract.Result<ShellItem>() != null);

            IShellItem2 shellItem;
            var code = ShellNativeMethods.SHCreateItemFromIDList(
                pidl,
                ref ShellIIDGuid.IShellItem2,
                out shellItem);
            if (HRESULT.Failed(code))
            {
                throw new ShellException(ErrorMessages.ShellFactoryUnableToCreateItem, Marshal.GetExceptionForHR(code));
            }
            return new ShellItem(shellItem);
        }

        /// <summary>
        ///     Create new instance of the <see cref="ShellItem" /> class
        ///     to the specified <c>IDListPtr</c> and parent <see cref="IShellFolder" />.
        /// </summary>
        /// <param name="idListPtr"><c>IDListPtr</c>.</param>
        /// <param name="parentFolderInterface">Parent <see cref="IShellFolder" />.</param>
        /// <returns><see cref="ShellItem" />.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="idListPtr" /> is <see cref="IntPtr.Zero" />.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="parentFolderInterface" /> is <c>null</c>.</exception>
        /// <exception cref="ShellException">Failed to create <see cref="ShellItem" />.</exception>
        internal static ShellItem FromIdList(IntPtr idListPtr, IShellFolder parentFolderInterface)
        {
            Contract.Requires<ArgumentNullException>(idListPtr != IntPtr.Zero);
            Contract.Requires<ArgumentNullException>(parentFolderInterface != null);
            Contract.Ensures(Contract.Result<ShellItem>() != null);

            IShellItem shellItem;
            var code = ShellNativeMethods.SHCreateItemWithParent(
                IntPtr.Zero,
                parentFolderInterface,
                idListPtr,
                ShellIIDGuid.IShellItem2,
                out shellItem);
            if (HRESULT.Failed(code))
            {
                throw new ShellException(ErrorMessages.ShellFactoryUnableToCreateItem, Marshal.GetExceptionForHR(code));
            }

            return new ShellItem((IShellItem2)shellItem);
        }

        /// <summary>
        ///     Define class invariant method.
        /// </summary>
        [ContractInvariantMethod]
        private void ObjectInvaliant()
        {
            Contract.Invariant(this.ShellItemInterface != null);
        }

        /// <summary>
        ///     Get <see cref="IShellItem2" />.
        /// </summary>
        internal IShellItem2 ShellItemInterface { get; }

        /// <summary>
        ///     Get <c>PIDL</c>.
        /// </summary>
        internal PIDL PIDL { get; }

        /// <summary>
        ///     Get a value that determines whether <see cref="ShellItem" /> is link.
        /// </summary>
        public bool IsLink { get; }

        /// <summary>
        ///     Get a value that determines whether <see cref="ShellItem" /> is an item on the file system.
        /// </summary>
        public bool IsFileSystem { get; }

        /// <summary>
        ///     Get a value that determines whether <see cref="ShellItem" /> is folder.
        /// </summary>
        public bool IsFolder { get; }

        /// <summary>
        ///     Get a value that determines whether <see cref="ShellItem" /> is stream.
        /// </summary>
        public bool IsStream => (GetAttributes(SFGAO.SFGAO_STREAM) & SFGAO.SFGAO_STREAM) != 0;

        /// <summary>
        ///     Get the parsing name.
        /// </summary>
        /// <returns></returns>
        public string GetParsingName()
        {
            return GetParsingName(this.ShellItemInterface);
        }

        /// <summary>
        ///     Get the item type string.
        /// </summary>
        /// <returns></returns>
        public string GetItemType()
        {
            return GetItemType(this.ShellItemInterface);
        }

        /// <summary>
        ///     Get <see cref="IShellFolder" />.
        /// </summary>
        /// <returns><see cref="IShellFolder" />.</returns>
        /// <exception cref="ShellException">Failed to acquire <see cref="IShellFolder" />.</exception>
        internal IShellFolder GetShellFolder()
        {
            Contract.Ensures(Contract.Result<IShellFolder>() != null);

            object result;

            var handler = ShellBHID.BHID_SFObject;
            var hr = this.ShellItemInterface.BindToHandler(
                IntPtr.Zero,
                ref handler,
                ref ShellIIDGuid.IShellFolder,
                out result);
            if (HRESULT.Failed(hr))
            {
                //if (!String.Equals(this.ParsingName, Environment.GetFolderPath(Environment.SpecialFolder.Desktop), StringComparison.InvariantCultureIgnoreCase))
                //{
                //    throw ShellException.FromHRESULT(hr);
                //}
                throw ShellException.FromHRESULT(hr);
            }

            return result as IShellFolder;
        }

        /// <summary>
        ///     Get <see cref="IStream" />.
        /// </summary>
        /// <returns><see cref="IStream" />.</returns>
        /// <exception cref="IStream">Failed to acquire <see cref="IStream" />.</exception>
        internal IStream GetStream()
        {
            Contract.Ensures(Contract.Result<IStream>() != null);

            object result;

            var handler = ShellBHID.BHID_Stream;
            var hr = this.ShellItemInterface.BindToHandler(
                IntPtr.Zero,
                ref handler,
                ref ShellIIDGuid.IStream,
                out result);
            if (HRESULT.Failed(hr))
            {
                throw ShellException.FromHRESULT(hr);
            }

            return (IStream)result;
        }

        /// <summary>
        ///     Get default display name.
        /// </summary>
        /// <returns></returns>
        public string GetDisplayName()
        {
            Contract.Ensures(Contract.Result<string>() != null);
            return GetDisplayName(DisplayNameTypes.Default);
        }

        /// <summary>
        ///     Get the display name of the specified display name type.
        /// </summary>
        /// <param name="displayNameType">Display name type.</param>
        /// <returns>Display name.</returns>
        /// <exception cref="ShellException">Failed to acquire display name.</exception>
        public string GetDisplayName(DisplayNameTypes displayNameType)
        {
            Contract.Ensures(Contract.Result<string>() != null);

            string result;
            var hr = this.ShellItemInterface.GetDisplayName((SIGDN)displayNameType, out result);
            if (HRESULT.Failed(hr))
            {
                throw new ShellException(ErrorMessages.ShellItemCannotGetDisplayName, hr);
            }

            return result;
        }

        /// <summary>
        ///     Get the parent <see cref="ShellItem" />.
        /// </summary>
        /// <returns>parent <see cref="ShellItem" />. If the parent does not exist, it returns <c>null</c>.</returns>
        /// <exception cref="ShellException">Failed to acquire parent <see cref="ShellItem" />.</exception>
        public ShellItem GetParent()
        {
            IShellItem parentShellItem;
            var hr = this.ShellItemInterface.GetParent(out parentShellItem);
            if (hr == COMErrorCodes.MK_E_NOOBJECT)
            {
                return null;
            }
            if (HRESULT.Failed(hr))
            {
                throw ShellException.FromHRESULT(hr);
            }

            return new ShellItem((IShellItem2)parentShellItem);
        }

        /// <summary>
        ///     Get the parsing name from the specified <see cref="IShellItem2" />.
        /// </summary>
        /// <param name="shellItem"><see cref="IShellItem2" />.</param>
        /// <returns>Parsing name.</returns>
        /// <exception cref="ShellException">Failed to acquire parsing name.</exception>
        private static string GetParsingName(IShellItem2 shellItem)
        {
            Contract.Requires(shellItem != null);
            Contract.Ensures(Contract.Result<string>() != null);

            string result;
            var hr = shellItem.GetDisplayName(SIGDN.SIGDN_DESKTOPABSOLUTEPARSING, out result);
            if (HRESULT.Failed(hr) && hr != COMErrorCodes.E_INVALIDARG)
            {
                throw new ShellException(ErrorMessages.ShellHelperGetParsingNameFailed, hr);
            }
            return result;
        }

        /// <summary>
        ///     Get the item type from the specified <see cref="IShellItem2" />.
        /// </summary>
        /// <param name="shellItem"><see cref="IShellItem2" />.</param>
        /// <returns>Item type string.</returns>
        private static string GetItemType(IShellItem2 shellItem)
        {
            Contract.Requires(shellItem != null);
            Contract.Ensures(Contract.Result<string>() != null);

            var itemTypeKey = new PROPERTYKEY(new Guid("28636AA6-953D-11D2-B5D6-00C04FD918D0"), 11);
            string result;
            var hr = shellItem.GetString(ref itemTypeKey, out result);
            if (HRESULT.Failed(hr))
            {
                try
                {
                    result = Path.GetExtension(GetParsingName(shellItem));
                }
                catch (Exception)
                {
                    result = String.Empty;
                }
            }

            return result;
        }

        /// <summary>
        ///     Get the attribute.
        /// </summary>
        /// <param name="mask">Attribute mask.</param>
        /// <returns>Attribute.</returns>
        /// <remarks>
        ///     <pre>
        ///         Acquiring unnecessary flags slows processing speed.
        ///         Please specify only the flag you want to acquire as <paramref name="mask" />.
        ///     </pre>
        /// </remarks>
        private UInt32 GetAttributes(UInt32 mask = 0xFFFFFFFF)
        {
            UInt32 result;
            this.ShellItemInterface.GetAttributes(mask, out result);
            return result;
        }

        internal object GetPropertyValue(PROPERTYKEY propertyKey)
        {
            using (var propVar = new PropVariant())
            {
                this.ShellItemInterface.GetProperty(ref propertyKey, propVar);
                return propVar.Value;
            }
        }

        internal T GetPropertyValue<T>(PROPERTYKEY propertyKey)
        {
            using (var propVar = new PropVariant())
            {
                this.ShellItemInterface.GetProperty(ref propertyKey, propVar);
                return (T)propVar.Value;
            }
        }
    }
}