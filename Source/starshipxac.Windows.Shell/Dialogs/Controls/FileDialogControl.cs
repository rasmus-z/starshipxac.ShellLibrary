using System;
using System.Diagnostics.Contracts;

namespace starshipxac.Windows.Shell.Dialogs.Controls
{
    /// <summary>
    ///     Define the control base class of the file dialog.
    /// </summary>
    public abstract class FileDialogControl : IEquatable<FileDialogControl>
    {
        public static readonly int MinDialogControlId = 16;

        private static int nextId = MinDialogControlId;

        /// <summary>
        ///     Initialize a new instance of the <see cref="FileDialogControl" /> class
        ///     to the specified control name.
        /// </summary>
        /// <param name="name">コントロール名。</param>
        protected FileDialogControl(string name)
        {
            Contract.Requires<ArgumentNullException>(name != null);

            this.Id = GetNextId();
            this.Name = name;
        }

        public FileDialogBase Dialog { get; private set; }

        /// <summary>
        ///     Get the control name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     Get the control ID.
        /// </summary>
        public int Id { get; }

        /// <summary>
        ///     Get or set a value that determines whether the control is valid.
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
                this.Dialog?.SetControlEnabled(this, value);
            }
        }

        /// <summary>
        ///     Get or set a value that determines whether to display the control.
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
                this.Dialog?.SetControlVisible(this, value);
            }
        }

        /// <summary>
        ///     Get or set the control text.
        /// </summary>
        public abstract string Text { get; set; }

        /// <summary>
        ///     Get the next control ID.
        /// </summary>
        /// <returns>Control ID.</returns>
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
}