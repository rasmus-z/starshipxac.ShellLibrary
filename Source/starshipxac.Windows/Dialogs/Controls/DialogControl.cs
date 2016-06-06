using System;
using System.Diagnostics.Contracts;

namespace starshipxac.Windows.Dialogs.Controls
{
    /// <summary>
    ///     ダイアログコントロールの基底クラスを定義します。
    /// </summary>
    public abstract class DialogControl : IEquatable<DialogControl>
    {
        public static readonly int MinDialogControlId = 16;
        private static int nextId = MinDialogControlId;
        private static readonly object syncObj = new object();

        /// <summary>
        ///     コントロール名を指定して、
        ///     <see cref="DialogControl" />クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="name">コントロール名。</param>
        protected DialogControl(string name)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(name));

            this.Id = GetNextId();
            this.Name = name;
        }

        /// <summary>
        ///     コントロール名を取得します。
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     コントロールIDを取得します。
        /// </summary>
        public int Id { get; }

        /// <summary>
        ///     次のコントロールIDを取得します。
        /// </summary>
        /// <returns>コントロールID。</returns>
        private static int GetNextId()
        {
            lock (syncObj)
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
    }
}