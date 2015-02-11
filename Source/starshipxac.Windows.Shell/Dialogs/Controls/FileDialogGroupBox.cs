using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace starshipxac.Windows.Shell.Dialogs.Controls
{
	public class FileDialogGroupBox : FileDialogControl
	{
		private string text;
		private readonly Collection<FileDialogControl> items;

		public FileDialogGroupBox(string name, params FileDialogControl[] controls)
			: this(name, String.Empty, controls)
		{
		}

		public FileDialogGroupBox(string name, string text, params FileDialogControl[] controls)
			: base(name)
		{
			this.text = text;

			if (controls == null)
			{
				this.items = new Collection<FileDialogControl>();
			}
			else
			{
				this.items = new Collection<FileDialogControl>(this.items);
			}
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

				this.text = value;
				if (this.Dialog != null)
				{
					this.Dialog.SetControlLabel(this, this.text);
				}
			}
		}

		public IReadOnlyList<FileDialogControl> Items
		{
			get
			{
				return this.items;
			}
		}

		internal override void Attach(FileDialogBase dialog)
		{
			base.Attach(dialog);

			this.Dialog.StartVisualGroup(this, this.text);

			foreach (var item in this.Items)
			{
				item.Attach(this.Dialog);
			}
			this.Dialog.EndVisualGroup();
		}

		internal override void Detach()
		{
			foreach (var control in this.Items)
			{
				control.Detach();
			}

			base.Detach();
		}
	}
}