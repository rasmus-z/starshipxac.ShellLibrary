using System;
using System.Diagnostics.Contracts;

namespace starshipxac.Windows.Shell.Dialogs.Controls
{
    /// <summary>
    ///     ファイルダイアログのコントロール基底クラスを定義します。
    /// </summary>
    [ContractClass(typeof(FileDialogControlContract))]
    public abstract class FileDialogControl : IEquatable<FileDialogControl>
    {
        public static readonly int MinDialogControlId = 16;

        private static int nextId = MinDialogControlId;

        /// <summary>
        ///     コントロール名を指定して、
        ///     <see cref="FileDialogControl" />クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="name">コントロール名。</param>
        protected FileDialogControl(string name)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(name));

            this.Id = GetNextId();
            this.Name = name;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.Name != null);
        }

        public FileDialogBase Dialog { get; private set; }

        /// <summary>
        ///     コントロール名を取得します。
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     コントロールIDを取得します。
        /// </summary>
        public int Id { get; }

        /// <summary>
        ///     コントロールが有効かどうかを判定する値を取得または設定します。
        /// </summary>
        public bool Enabled
        {
            get
            {
                ThrowIfNotInitialized();
                return this.Dialog.GetControlEnabled(this);
            }
            set
            {
                ThrowIfNotInitialized();
                this.Dialog.SetControlEnabled(this, value);
            }
        }

        /// <summary>
        ///     コントロールを表示するかどうかを判定する値を取得または設定します。
        /// </summary>
        public bool Visible
        {
            get
            {
                ThrowIfNotInitialized();
                return this.Dialog.GetControlVisible(this);
            }
            set
            {
                ThrowIfNotInitialized();
                this.Dialog.SetControlVisible(this, value);
            }
        }

        /// <summary>
        ///     コントロールテキストを取得または設定します。
        /// </summary>
        public abstract string Text { get; set; }

        /// <summary>
        ///     次のコントロールIDを取得します。
        /// </summary>
        /// <returns>コントロールID。</returns>
        private static int GetNextId()
        {
            var result = nextId;
            if (nextId == Int32.MaxValue)
            {
                nextId = Int32.MinValue;
            }
            else
            {
                nextId += 1;
            }
            return result;
        }

        internal virtual void Attach(FileDialogBase dialog)
        {
            Contract.Requires<ArgumentNullException>(dialog != null);

            this.Dialog = dialog;
        }

        internal virtual void Detach()
        {
            Contract.Requires<InvalidOperationException>(this.Dialog != null);

            this.Dialog = null;
        }

        protected void ThrowIfNotInitialized()
        {
            if (this.Dialog == null)
            {
                throw new InvalidOperationException();
            }
        }

        public static bool operator ==(FileDialogControl left, FileDialogControl right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(FileDialogControl left, FileDialogControl right)
        {
            return !Equals(left, right);
        }

        public bool Equals(FileDialogControl other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return this.Id == other.Id;
        }

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
            return Equals((FileDialogControl)obj);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"FileDialogControl[Name={this.Name}, Id={this.Id}]";
        }
    }

    [ContractClassFor(typeof(FileDialogControl))]
    abstract class FileDialogControlContract : FileDialogControl
    {
        protected FileDialogControlContract(string name)
            : base(name)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(name));
        }

        public override string Text
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}