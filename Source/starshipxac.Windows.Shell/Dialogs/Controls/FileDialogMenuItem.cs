using System;
using System.Diagnostics.Contracts;
using System.Threading;

namespace starshipxac.Windows.Shell.Dialogs.Controls
{
	public class FileDialogMenuItem : FileDialogControl
	{
		private string text;

		public FileDialogMenuItem(string name)
			: this(name, String.Empty)
		{
			Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(name));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="name">コントロール名。</param>
		/// <param name="text">コントロールテキスト。</param>
		public FileDialogMenuItem(string name, string text)
			: base(name)
		{
			Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(name));

			this.text = text;
		}

		public FileDialogMenu Menu { get; internal set; }

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

		#region Click Event

		public event EventHandler Click;

		protected virtual void OnClick(EventArgs args)
		{
			var handle = Interlocked.CompareExchange(ref this.Click, null, null);
			if (handle != null)
			{
				handle(this, args);
			}
		}

		#endregion

		internal void RaiseClickEvent()
		{
			if (this.Enabled)
			{
				this.Click(this, EventArgs.Empty);
			}
		}
	}
}