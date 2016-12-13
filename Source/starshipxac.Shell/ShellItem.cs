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
    public sealed class ShellItem : IEquatable<ShellItem>, IDisposable
    {
        private bool disposed = false;
        private PIDL pidl;
        private string parsingName;
        private string itemType;

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

            var code = ShellNativeMethods.SHCreateItemFromParsingName(parsingName, IntPtr.Zero,
                ref ShellIIDGuid.IShellItem2, out shellItem2);
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
            var code = ShellNativeMethods.SHCreateItemFromIDList(pidl, ref ShellIIDGuid.IShellItem2, out shellItem);
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
        internal PIDL PIDL
        {
            get
            {
                Contract.Ensures(Contract.Result<PIDL>() != PIDL.Null);
                if (this.pidl == PIDL.Null)
                {
                    this.pidl = PIDL.FromShellItem(this.ShellItemInterface);
                }
                return this.pidl;
            }
        }

        /// <summary>
        ///     解析名を取得します。
        /// </summary>
        public string ParsingName
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                if (this.parsingName == null)
                {
                    this.parsingName = GetParsingName(this.ShellItemInterface);
                }
                return this.parsingName;
            }
        }

        /// <summary>
        ///     アイテム種別を取得します。
        /// </summary>
        /// <remarks>
        ///     アイテムの拡張子を取得します。
        /// </remarks>
        public string ItemType
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                if (this.itemType == null)
                {
                    this.itemType = GetItemType(this.ShellItemInterface);
                }
                return this.itemType;
            }
        }

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
        ///     <see cref="IShellFolder" />を取得します。
        /// </summary>
        /// <returns>取得した<see cref="IShellFolder" />。</returns>
        /// <exception cref="ShellException"><see cref="IShellFolder" />の取得に失敗しました。</exception>
        internal IShellFolder GetShellFolder()
        {
            Contract.Ensures(Contract.Result<IShellFolder>() != null);

            object result;

            var handler = ShellBHID.BHID_SFObject;
            var hr = this.ShellItemInterface.BindToHandler(IntPtr.Zero, ref handler, ref ShellIIDGuid.IShellFolder, out result);
            if (HRESULT.Failed(hr))
            {
                if (!String.Equals(this.ParsingName, Environment.GetFolderPath(Environment.SpecialFolder.Desktop), StringComparison.InvariantCultureIgnoreCase))
                {
                    throw ShellException.FromHRESULT(hr);
                }
            }

            return result as IShellFolder;
        }

        /// <summary>
        ///     <see cref="IShellFolder" />の取得を試みます。
        /// </summary>
        /// <param name="shellFolderInterface">取得した<see cref="IShellFolder" />。</param>
        /// <returns>取得に成功した場合は<c>true</c>。それ以外の場合は<c>false</c>。</returns>
        internal bool TryGetShellFolder(out IShellFolder shellFolderInterface)
        {
            object shellFolder;

            var handler = ShellBHID.BHID_SFObject;
            var hr = this.ShellItemInterface.BindToHandler(IntPtr.Zero, ref handler, ref ShellIIDGuid.IShellFolder, out shellFolder);
            if (HRESULT.Failed(hr))
            {
                if (!String.Equals(this.ParsingName, Environment.GetFolderPath(Environment.SpecialFolder.Desktop), StringComparison.InvariantCultureIgnoreCase))
                {
                    shellFolderInterface = null;
                    return false;
                }
            }

            shellFolderInterface = shellFolder as IShellFolder;
            return true;
        }

        internal HRESULT GetShellFolderInterface(out IShellFolder shellFolderInterface)
        {
            object shellFolder;

            var handler = ShellBHID.BHID_SFObject;
            var result = this.ShellItemInterface.BindToHandler(
                IntPtr.Zero,
                ref handler,
                ref ShellIIDGuid.IShellFolder,
                out shellFolder);
            if (HRESULT.Failed(result))
            {
                if (!String.Equals(this.ParsingName, Environment.GetFolderPath(Environment.SpecialFolder.Desktop), StringComparison.InvariantCultureIgnoreCase))
                {
                    shellFolderInterface = null;
                    return result;
                }
            }

            shellFolderInterface = shellFolder as IShellFolder;
            return new HRESULT(0);
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
            var hr = this.ShellItemInterface.BindToHandler(IntPtr.Zero, ref handler, ref ShellIIDGuid.IStream, out result);
            if (HRESULT.Failed(hr))
            {
                throw ShellException.FromHRESULT(hr);
            }

            return (IStream)result;
        }

        /// <summary>
        ///     アイテム名を取得します。
        /// </summary>
        /// <returns>取得したアイテム名。</returns>
        public string GetName()
        {
            Contract.Ensures(Contract.Result<string>() != null);
            return Path.GetFileName(this.ParsingName);
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
            Contract.Requires<ArgumentNullException>(shellItem != null);
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
        internal static string GetItemType(IShellItem2 shellItem)
        {
            Contract.Requires<ArgumentNullException>(shellItem != null);
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
        ///     指定したパスの絶対パスを取得します。
        /// </summary>
        /// <param name="path">絶対パスを取得するパス。</param>
        /// <returns>絶対パス。</returns>
        /// <exception cref="ArgumentException"><paramref name="path" />が<c>null</c>または空文字列です。</exception>
        internal static string GetAbsolutePath(string path)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(path));

            if (Uri.IsWellFormedUriString(path, UriKind.Absolute))
            {
                return path;
            }
            return Path.GetFullPath(path);
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
        internal UInt32 GetAttributes(UInt32 mask = 0xFFFFFFFF)
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

        /// <summary>
        ///     指定したオブジェクトの値が、現在の<see cref="ShellItem" />と等しいかどうかを判定します。
        /// </summary>
        /// <param name="obj">現在の<see cref="ShellItem" />と比較するオブジェクト。</param>
        /// <returns>
        ///     <paramref name="obj" />と現在の<see cref="ShellItem" />が等しい場合は<c>true</c>。
        ///     それ以外の場合は<c>false</c>。
        /// </returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            return Equals((ShellItem)obj);
        }

        /// <summary>
        ///     指定した<see cref="ShellItem" />の値が、現在の<see cref="ShellItem" />と等しいかどうかを判定します。
        /// </summary>
        /// <param name="other">現在の<see cref="ShellItem" />と比較する<see cref="ShellItem" />。</param>
        /// <returns>
        ///     <paramref name="other" />と現在の<see cref="ShellItem" />が等しい場合は<c>true</c>。
        ///     それ以外の場合は<c>false</c>。
        /// </returns>
        public bool Equals(ShellItem other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            int order;
            var hr = this.ShellItemInterface.Compare(other.ShellItemInterface, SICHINTF.SICHINT_TEST_FILESYSPATH_IF_NOT_EQUAL,
                out order);
            if (hr == COMErrorCodes.S_FALSE)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        ///     2つの<see cref="ShellItem" />を比較して、等しいかどうかを判定します。
        /// </summary>
        /// <param name="left">1つめの<see cref="ShellItem" />。</param>
        /// <param name="right">2つめの<see cref="ShellItem" />。</param>
        /// <returns>
        ///     2つの<see cref="ShellItem" />が等しい場合は<c>true</c>。
        ///     それ以外の場合は<c>false</c>。
        /// </returns>
        public static bool operator ==(ShellItem left, ShellItem right)
        {
            return Equals(left, right);
        }

        /// <summary>
        ///     2つの<see cref="ShellItem" />を比較して、等しくないかどうかを判定します。
        /// </summary>
        /// <param name="left">1つめの<see cref="ShellItem" />。</param>
        /// <param name="right">2つめの<see cref="ShellItem" />。</param>
        /// <returns>
        ///     2つの<see cref="ShellItem" />が等しくない場合は<c>true</c>。
        ///     それ以外の場合は<c>false</c>。
        /// </returns>
        public static bool operator !=(ShellItem left, ShellItem right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        ///     このインスタンスをハッシュコードを取得します。
        /// </summary>
        /// <returns>32ビット符号付き整数ハッシュコード。</returns>
        public override int GetHashCode()
        {
            return this.ShellItemInterface.GetHashCode();
        }

        /// <summary>
        ///     このインスタンスの文字列表現を取得します。
        /// </summary>
        /// <returns>このインスタンスの文字列表現。</returns>
        public override string ToString()
        {
            return "ShellItem";
        }
    }
}