using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Threading;
using starshipxac.Shell.Interop;
using starshipxac.Shell.Media.Imaging;
using starshipxac.Shell.PropertySystem;

namespace starshipxac.Shell
{
    /// <summary>
    /// シェルアイテム基底クラスを定義します。
    /// </summary>
    public abstract class ShellObject : INotifyPropertyChanged, IDisposable, IEquatable<ShellObject>
    {
        private bool disposed = false;

        private ShellProperties properties;
        private ShellObject parentShellObject;
        private bool getParentShellObject = false;

        private ShellProperty<string> contentTypeProperty;
        private ShellProperty<DateTime?> dateCreatedProperty;
        private ShellProperty<DateTime?> dateModifiedProperty;
        private ShellProperty<DateTime?> dateAccessedProperty;
        private ShellItemImageFactory imageFactory;

        private static readonly ConcurrentDictionary<string, PropertyChangedEventArgs> propertyChangedEventArgsDictionary =
            new ConcurrentDictionary<string, PropertyChangedEventArgs>();

        /// <summary>
        /// <see cref="ShellItem"/>を指定して、<see cref="ShellObject"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="shellItem"><see cref="ShellItem"/>。</param>
        internal ShellObject(ShellItem shellItem)
        {
            Contract.Requires<ArgumentNullException>(shellItem != null);

            this.ShellItem = shellItem;
        }

        /// <summary>
        /// ファイナライザー。
        /// </summary>
        ~ShellObject()
        {
            Dispose(false);
        }

        /// <summary>
        /// <see cref="ShellObject"/>によって使用されているすべてのリソースを解放します。
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// <see cref="ShellObject"/>によって使用されているすべてのリソースを解放し、
        /// オプションでマネージリソースも解放します。
        /// </summary>
        /// <param name="disposing">
        /// マネージリソースとアンマネージリソースの両方を解放する場合は<c>true</c>。
        /// アンマネージリソースだけを解放する場合は<c>false</c>。
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    // マネージリソース解放
                    this.properties.Dispose();
                    this.properties = null;
                    this.parentShellObject = null;
                }

                this.disposed = true;
            }
        }

        // ReSharper disable once UnusedMember.Locala
        [ContractInvariantMethod]
        private void ObjectInvaliant()
        {
            Contract.Invariant(this.ShellItem != null);
        }

        /// <summary>
        /// <see cref="ShellItem"/>を取得します。
        /// </summary>
        internal ShellItem ShellItem { get; set; }

        /// <summary>
        /// <c>PIDL</c>を取得または設定します。
        /// </summary>
        internal virtual PIDL PIDL
        {
            get
            {
                return this.ShellItem.PIDL;
            }
        }

        /// <summary>
        /// <see cref="ShellPropertyStore"/>を取得または設定します。
        /// </summary>
        internal ShellPropertyStore PropertyStore { get; set; }

        /// <summary>
        /// 解析名を取得します。
        /// </summary>
        public virtual string ParsingName
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                return this.ShellItem.ParsingName;
            }
        }

        /// <summary>
        /// アイテム名を取得します。
        /// </summary>
        public virtual string Name
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                return this.ShellItem.GetName();
            }
        }

        /// <summary>
        /// 表示名を取得します。
        /// </summary>
        public virtual string DisplayName
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                return this.ShellItem.GetDisplayName();
            }
        }

        /// <summary>
        /// 親の<see cref="ShellObject"/>を取得します。
        /// </summary>
        public ShellObject Parent
        {
            get
            {
                if (!this.getParentShellObject)
                {
                    this.parentShellObject = GetParent();
                    this.getParentShellObject = true;
                }
                return this.parentShellObject;
            }
        }

        /// <summary>
        /// <see cref="ShellObject"/>がファイルシステム上のアイテムかどうかを判定する値を取得します。
        /// </summary>
        public bool IsFileSystem
        {
            get
            {
                return this.ShellItem.IsFileSystem;
            }
        }

        /// <summary>
        /// ファイル種別を示す文字列を取得します。
        /// </summary>
        public string ContentType
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                if (this.contentTypeProperty == null)
                {
                    // System.ContentType
                    this.contentTypeProperty = this.Properties.Create<string>("System.ContentType");
                }
                return this.contentTypeProperty.Value ?? String.Empty;
            }
        }

        /// <summary>
        /// アイテム作成日時(UTC)を取得します。
        /// </summary>
        public virtual DateTime DateCreated
        {
            get
            {
                if (this.dateCreatedProperty == null)
                {
                    // System.DateCreated
                    this.dateCreatedProperty = this.Properties.Create<DateTime?>("System.DateCreated");
                }
                return this.dateCreatedProperty.Value.GetValueOrDefault(DateTime.MinValue);
            }
        }

        /// <summary>
        /// アイテム更新日時(UTC)を取得します。
        /// </summary>
        public virtual DateTime DateModified
        {
            get
            {
                if (this.dateModifiedProperty == null)
                {
                    // System.DateModified
                    this.dateModifiedProperty = this.Properties.Create<DateTime?>("System.DateModified");
                }
                return this.dateModifiedProperty.Value.GetValueOrDefault(DateTime.MinValue);
            }
        }

        /// <summary>
        /// アイテムアクセス日時(UTC)を取得します。
        /// </summary>
        public virtual DateTime DateAccessed
        {
            get
            {
                if (this.dateAccessedProperty == null)
                {
                    // System.DateAccessed
                    this.dateAccessedProperty = this.Properties.Create<DateTime?>("System.DateAccessed");
                }
                return this.dateAccessedProperty.Value.GetValueOrDefault(DateTime.MinValue);
            }
        }

        /// <summary>
        /// <see cref="ShellItemImageFactory"/>を取得します。
        /// </summary>
        public ShellItemImageFactory ImageFactory
        {
            get
            {
                Contract.Ensures(Contract.Result<ShellItemImageFactory>() != null);
                if (this.imageFactory == null)
                {
                    this.imageFactory = new ShellItemImageFactory((IShellItemImageFactory)this.ShellItem.ShellItemInterface);
                }
                return this.imageFactory;
            }
        }

        /// <summary>
        /// プロパティのコレクションを取得します。
        /// </summary>
        public ShellProperties Properties
        {
            get
            {
                Contract.Ensures(Contract.Result<ShellProperties>() != null);
                if (this.properties == null)
                {
                    this.properties = new ShellProperties(this);
                }
                return this.properties;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// <paramref name="displayNameType"/>で指定した表示名を取得します。
        /// </summary>
        /// <param name="displayNameType">表示名の種別。</param>
        /// <returns>取得した表示名。</returns>
        public virtual string GetDisplayName(DisplayNameTypes displayNameType)
        {
            Contract.Ensures(Contract.Result<string>() != null);
            return this.ShellItem.GetDisplayName(displayNameType);
        }

        /// <summary>
        /// 親の<see cref="ShellObject"/>を取得します。
        /// </summary>
        /// <returns>取得した親の<see cref="ShellObject"/>。</returns>
        /// <exception cref="ShellException">親の<see cref="ShellObject"/>の取得に失敗しました。</exception>
        private ShellObject GetParent()
        {
            var parentShellItem = this.ShellItem.GetParent();
            if (parentShellItem == null)
            {
                return null;
            }
            return ShellFactory.FromShellItem(parentShellItem);
        }

        /// <summary>
        /// プロパティの値が変更されたことを通知します。
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            Contract.Requires<ArgumentNullException>(propertyName != null);

            var handler = Interlocked.CompareExchange(ref this.PropertyChanged, null, null);
            if (handler != null)
            {
                handler(this, GetPropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// 指定したプロパティ名の変更を通知するイベント情報を作成します。
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        private static PropertyChangedEventArgs GetPropertyChangedEventArgs(string propertyName)
        {
            Contract.Requires(propertyName != null);
            Contract.Ensures(Contract.Result<PropertyChangedEventArgs>() != null);

            return propertyChangedEventArgsDictionary.GetOrAdd(propertyName, name => new PropertyChangedEventArgs(name));
        }

        /// <summary>
        /// 2つの<see cref="ShellObject"/>を比較して、等しいかどうかを判定します。
        /// </summary>
        /// <param name="left">1つめの<see cref="ShellObject"/>。</param>
        /// <param name="right">2つめの<see cref="ShellObject"/>。</param>
        /// <returns>
        /// 2つの<see cref="ShellObject"/>が等しい場合は<c>true</c>。
        /// それ以外の場合は<c>false</c>。
        /// </returns>
        public static bool operator ==(ShellObject left, ShellObject right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// 2つの<see cref="ShellObject"/>を比較して、等しくないかどうかを判定します。
        /// </summary>
        /// <param name="left">1つめの<see cref="ShellObject"/>。</param>
        /// <param name="right">2つめの<see cref="ShellObject"/>。</param>
        /// <returns>
        /// 2つの<see cref="ShellObject"/>が等しくない場合は<c>true</c>。
        /// それ以外の場合は<c>false</c>。
        /// </returns>
        public static bool operator !=(ShellObject left, ShellObject right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        /// 指定した<see cref="ShellObject"/>の値が、現在の<see cref="ShellObject"/>と等しいかどうかを判定します。
        /// </summary>
        /// <param name="other">現在の<see cref="ShellObject"/>と比較する<see cref="ShellObject"/>。</param>
        /// <returns>
        /// <paramref name="other"/>と現在の<see cref="ShellObject"/>が等しい場合は<c>true</c>。
        /// それ以外の場合は<c>false</c>。
        /// </returns>
        public bool Equals(ShellObject other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return this.ShellItem.Equals(other.ShellItem);
        }

        /// <summary>
        /// 指定したオブジェクトの値が、現在の<see cref="ShellObject"/>と等しいかどうかを判定します。
        /// </summary>
        /// <param name="obj">現在の<see cref="ShellObject"/>と比較するオブジェクト。</param>
        /// <returns>
        /// <paramref name="obj"/>と現在の<see cref="ShellObject"/>が等しい場合は<c>true</c>。
        /// それ以外の場合は<c>false</c>。
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
            if (this.GetType() != obj.GetType())
            {
                return false;
            }
            return Equals((ShellObject)obj);
        }

        /// <summary>
        /// このインスタンスのハッシュコードを取得します。
        /// </summary>
        /// <returns>32ビット符号付き整数ハッシュコード。</returns>
        public override int GetHashCode()
        {
            return this.ShellItem.GetHashCode();
        }

        /// <summary>
        /// このインスタンスの文字列表現を取得します。
        /// </summary>
        /// <returns>このインスタンスの文字列表現。</returns>
        public override string ToString()
        {
            return String.Format("{0}: {{ParsingName={1}}}", this.GetType().Name, this.ParsingName);
        }
    }
}