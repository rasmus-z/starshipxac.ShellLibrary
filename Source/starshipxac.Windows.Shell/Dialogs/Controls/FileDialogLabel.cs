using System;

namespace starshipxac.Windows.Shell.Dialogs.Controls
{
	public class FileDialogLabel : FileDialogControl
	{
		private string text;

		public FileDialogLabel(string name)
			: this(name, String.Empty)
		{
		}

		public FileDialogLabel(string name, string text)
			: base(name)
		{
			this.text = text;
		}

		public override string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				ThrowIfNotInitialized();
				if (this.text == value)
				{
					return;
				}

				this.text = value;
				if (this.Dialog != null)
				{
					this.Dialog.SetControlLabel(this, this.text);
				}
			}
		}

		internal override void Attach(FileDialogBase dialog)
		{
			base.Attach(dialog);

			this.Dialog.AddLabel(this, this.text);
		}
	}
}