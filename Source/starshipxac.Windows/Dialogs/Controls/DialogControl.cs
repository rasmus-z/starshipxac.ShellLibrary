using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;

namespace starshipxac.Windows.Dialogs.Controls
{
    /// <summary>
    /// ダイアログコントロールの基底クラスを定義します。
    /// </summary>
    public abstract class DialogControl : IEquatable<DialogControl>
    {
        public static readonly int MinDialogControlId = 16;

        private static int nextId = MinDialogControlId;

        /// <summary>
        /// コントロール名を指定して、
        /// <see cref="DialogControl"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="name">コントロール名。</param>
        protected DialogControl(string name)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(name));

            this.Id = GetNextId();
            this.Name = name;
        }

        protected DialogControl(int id, string name)
        {
            Contract.Requires<ArgumentOutOfRangeException>(id > 0);
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(name));

            this.Id = id;
            this.Name = name;
        }

        /// <summary>
        /// コントロール名を取得します。
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// コントロールIDを取得します。
        /// </summary>
        public int Id { get; private set; }

        ///// <summary>
        ///// この<see cref="DialogControl"/>を保持している<see cref="IDialogControlHost"/>を取得または設定します。
        ///// </summary>
        //public IDialogControlHost Dialog { get; set; }

        ///// <summary>
        ///// プロパティの変更を開始します。
        ///// </summary>
        ///// <param name="propertyName"></param>
        //protected ChangePropertyTransaction BeginChangeProperty([CallerMemberName] string propertyName = "")
        //{
        //    return new ChangePropertyTransaction(this, propertyName);
        //}

        ///// <summary>
        ///// プロパティの変更を通知します。
        ///// </summary>
        ///// <param name="propertyName">通知するプロパティ名。</param>
        //protected void ApplyPropertyChange([CallerMemberName] string propertyName = "")
        //{
        //    Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(propertyName));

        //    this.Dialog.ApplyControlPropertyChange(propertyName, this);
        //}

        /// <summary>
        /// 次のコントロールIDを取得します。
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

        public static bool operator ==(DialogControl left, DialogControl right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(DialogControl left, DialogControl right)
        {
            return !Equals(left, right);
        }

        public bool Equals(DialogControl other)
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
            return Equals((DialogControl)obj);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public override string ToString()
        {
            return String.Format("DialogControl{{id: {0}, name: {1}}}", this.Id, this.Name);
        }

        //protected class ChangePropertyTransaction : IDisposable
        //{
        //    public ChangePropertyTransaction(DialogControl control, [CallerMemberName] string propertyName = "")
        //    {
        //        Contract.Requires<ArgumentNullException>(control != null);

        //        this.Control = control;
        //        this.PropertyName = propertyName;

        //        if (this.Control.Dialog != null)
        //        {
        //            this.Control.Dialog.IsControlPropertyChangeAllowed(this.PropertyName, this.Control);
        //        }
        //    }

        //    ~ChangePropertyTransaction()
        //    {
        //        Dispose();
        //    }

        //    public void Dispose()
        //    {
        //        if (this.Control.Dialog != null)
        //        {
        //            this.Control.Dialog.ApplyControlPropertyChange(this.PropertyName, this.Control);
        //        }
        //    }

        //    public DialogControl Control { get; private set; }
        //    public string PropertyName { get; private set; }
        //}
    }
}