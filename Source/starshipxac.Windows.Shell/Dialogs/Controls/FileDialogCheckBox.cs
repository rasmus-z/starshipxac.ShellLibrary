using System;

namespace starshipxac.Windows.Shell.Dialogs.Controls
{
    public class FileDialogCheckBox : FileDialogControl
    {
        private string text;
        private bool isChecked;

        public FileDialogCheckBox(string name)
            : this(name, String.Empty, false)
        {
        }

        public FileDialogCheckBox(string name, string text)
            : this(name, text, false)
        {
        }

        public FileDialogCheckBox(string name, string text, bool isChecked)
            : base(name)
        {
            this.text = text;
            this.isChecked = isChecked;
        }

        public override string Text
        {
            get
            {
                return this.text;
            }
            set
            {
                if (this.text == value)
                {
                    return;
                }

                this.text = value ?? String.Empty;
                if (this.Dialog != null)
                {
                    this.Dialog.SetControlLabel(this, this.text);
                }
            }
        }

        public bool IsChecked
        {
            get
            {
                if (this.Dialog != null)
                {
                    this.isChecked = this.Dialog.GetCheckBoxChecked(this);
                }
                return this.isChecked;
            }
            set
            {
                if (this.isChecked == value)
                {
                    return;
                }

                this.isChecked = value;
                if (this.Dialog != null)
                {
                    this.Dialog.SetCheckBoxChecked(this, this.isChecked);
                }
            }
        }

        #region Checked Event

        public event EventHandler Checked;

        protected virtual void OnChecked(EventArgs args)
        {
            var handler = this.Checked;
            if (handler != null)
            {
                handler(this, args);
            }
        }

        #endregion

        #region Unchecked Event

        public event EventHandler Unchecked;

        protected virtual void OnUnchecked(EventArgs args)
        {
            var handler = this.Unchecked;
            if (handler != null)
            {
                handler(this, args);
            }
        }

        #endregion

        internal void RaiseCheckedChangedEvent(bool isChecked)
        {
            if (this.Enabled)
            {
                if (isChecked)
                {
                    OnChecked(EventArgs.Empty);
                }
                else
                {
                    OnUnchecked(EventArgs.Empty);
                }
            }
        }

        internal override void Attach(FileDialogBase dialog)
        {
            base.Attach(dialog);

            this.Dialog.AddCheckBox(this);

            this.Dialog.SetControlLabel(this, this.text);
            this.Dialog.SetCheckBoxChecked(this, this.isChecked);
        }

        internal override void Detach()
        {
            this.isChecked = this.Dialog.GetCheckBoxChecked(this);

            base.Detach();
        }
    }
}