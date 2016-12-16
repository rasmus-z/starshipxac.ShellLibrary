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
    ///     <c>ShellItem</c>を定義します。
    /// </summary>
    public sealed class ShellItem : IDisposable
    {
        private bool disposed = false;

        /// <summary>
        ///     <see cref="IShellItem" />を指定して、
        ///     <see cref="ShellItem" />クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="shellItem"><see cref="IShellItem" />。</param>
        internal ShellItem(IShellItem shellItem)
            : this((IShellItem2)shellItem)
        {
            Contract.Requires<ArgumentNullException>(shellItem != null);
        }

        /// <summary>
        ///     <see cref="IShellItem2" />を指定して、
        ///     <see cref="ShellItem" />クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="shellItem2"><see cref="IShellItem2" />。</param>
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
        ///     解析名(<c>ParsingName</c>)を指定して、<see cref="ShellItem" />を作成します。
        /// </summary>
        /// <param name="parsingName">解析名。</param>
        /// <returns>作成した<see cref="ShellItem" />。</returns>
        /// <exception cref="ArgumentException"><paramref name="parsingName" />が<c>null</c>または空文字列です。</exception>
        /// <exception cref="ShellException"><see cref="ShellItem" />の作成に失敗しました。</exception>
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
        ///     <c>PIDL</c>から、<see cref="ShellItem" />を作成します。
        /// </summary>
        /// <param name="pidl"><c>PIDL</c>。</param>
        /// <returns>作成した<see cref="ShellItem" />。</returns>
        /// <exception cref="ArgumentException"><paramref name="pidl" />が<c>null</c>です。</exception>
        /// <exception cref="ShellException"><see cref="ShellItem" />の作成に失敗しました。</exception>
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
        ///     <c>IDListPtr</c>および親の<see cref="IShellFolder" />から、<see cref="ShellItem" />を作成します。
        /// </summary>
        /// <param name="idListPtr"><c>IDListPtr</c>。</param>
        /// <param name="parentFolderInterface">親の<see cref="IShellFolder" />。</param>
        /// <returns>作成した<see cref="ShellItem" />。</returns>
        /// <exception cref="ArgumentNullException"><paramref name="idListPtr" />が<see cref="IntPtr.Zero" />です。</exception>
        /// <exception cref="ArgumentNullException"><paramref name="parentFolderInterface" />が<c>null</c>です。</exception>
        /// <exception cref="ShellException"><see cref="ShellItem" />の作成に失敗しました。</exception>
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
        ///     クラスインバリアントメソッドを定義します。
        /// </summary>
        [ContractInvariantMethod]
        private void ObjectInvaliant()
        {
            Contract.Invariant(this.ShellItemInterface != null);
        }

        /// <summary>
        ///     <see cref="IShellItem2" />を取得します。
        /// </summary>
        internal IShellItem2 ShellItemInterface { get; }

        /// <summary>
        ///     <c>PIDL</c>を取得します。
        /// </summary>
        internal PIDL PIDL { get; }

        /// <summary>
        ///     <see cref="ShellItem" />がリンクかどうかを判定する値を取得します。
        /// </summary>
        public bool IsLink { get; }

        /// <summary>
        ///     <see cref="ShellItem" />がファイルシステム上のアイテムかどうかを判定する値を取得します。
        /// </summary>
        public bool IsFileSystem { get; }

        /// <summary>
        ///     <see cref="ShellItem" />がフォルダーかどうかを判定する値を取得します。
        /// </summary>
        public bool IsFolder { get; }

        /// <summary>
        ///     <see cref="ShellItem" />がストリームかどうかを判定する値を取得します。
        /// </summary>
        public bool IsStream => (GetAttributes(SFGAO.SFGAO_STREAM) & SFGAO.SFGAO_STREAM) != 0;

        /// <summary>
        ///     解析名を取得します。
        /// </summary>
        /// <returns></returns>
        public string GetParsingName()
        {
            return GetParsingName(this.ShellItemInterface);
        }

        /// <summary>
        /// アイテム種別を取得します。
        /// </summary>
        /// <returns></returns>
        public string GetItemType()
        {
            return GetItemType(this.ShellItemInterface);
        }

        /// <summary>
        ///     <see cref="IShellFolder" />を取得します。
        /// </summary>
        /// <returns>取得した<see cref="IShellFolder" />。</returns>
        /// <exception cref="ShellException"><see cref="IShellFolder" />の取得に失敗しました。</exception>
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
        ///     <see cref="IStream" />を取得します。
        /// </summary>
        /// <returns>取得した<see cref="IStream" />。</returns>
        /// <exception cref="IStream">の取得に失敗しました。</exception>
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
        ///     表示名を取得します。
        /// </summary>
        /// <returns></returns>
        public string GetDisplayName()
        {
            Contract.Ensures(Contract.Result<string>() != null);
            return GetDisplayName(DisplayNameTypes.Default);
        }

        /// <summary>
        ///     指定した表示名種別の表示名を取得します。
        /// </summary>
        /// <param name="displayNameType">表示名種別。</param>
        /// <returns>取得した表示名。</returns>
        /// <exception cref="ShellException">表示名の取得に失敗しました。</exception>
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
        ///     親の<see cref="ShellItem" />を取得します。
        /// </summary>
        /// <returns>取得した親の<see cref="ShellItem" />。親が存在しない場合は、<c>null</c>を返します。</returns>
        /// <exception cref="ShellException">親の<see cref="ShellItem" />の取得に失敗しました。</exception>
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
        ///     指定した<see cref="ShellItem" />から、解析名を取得します。
        /// </summary>
        /// <param name="shellItem"><see cref="ShellItem" />。</param>
        /// <returns>Shell解析名。</returns>
        /// <exception cref="ShellException">解析名の取得に失敗しました。</exception>
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
        ///     指定した<see cref="IShellItem2" />のアイテム種別を取得します。
        /// </summary>
        /// <param name="shellItem">アイテム種別を取得する<see cref="IShellItem2" />。</param>
        /// <returns>アイテム種別。</returns>
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
        ///     属性を取得します。
        /// </summary>
        /// <param name="mask">取得する属性。</param>
        /// <returns>取得した属性。</returns>
        /// <remarks>
        ///     <pre>
        ///         不要なフラグを取得すると、処理速度が遅くなります。
        ///         取得したいフラグのみを
        ///         <param name="mask" />
        ///         に指定してください。
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