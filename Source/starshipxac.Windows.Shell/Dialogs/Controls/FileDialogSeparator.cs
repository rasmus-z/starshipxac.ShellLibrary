using System;

namespace starshipxac.Windows.Shell.Dialogs.Controls
{
	public class FileDialogSeparator : FileDialogControl
	{
		public FileDialogSeparator()
			: base(String.Empty)
		{
		}

		public override string Text
		{
			get
			{
				return String.Empty;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		internal override void Attach(FileDialogBase dialog)
		{
			base.Attach(dialog);

			this.Dialog.AddSeparator(this);
		}
	}
}