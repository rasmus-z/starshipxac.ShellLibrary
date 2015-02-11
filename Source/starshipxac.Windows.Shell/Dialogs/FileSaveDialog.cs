using System;
using System.Diagnostics.Contracts;
using System.Windows;
using starshipxac.Shell;

namespace starshipxac.Windows.Shell.Dialogs
{
	/// <summary>
	/// 保存するファイルを選択するダイアログを表示します。
	/// </summary>
	public class FileSaveDialog : FileSaveDialogBase
	{
		private bool overwritePrompt = true;
		private bool createPrompt = false;
		private bool isExpandedMode = false;

		private ShellFile shellFile;

		/// <summary>
		/// <see cref="FileSaveDialog"/>クラスの新しいインスタンスを初期化します。
		/// </summary>
		public FileSaveDialog()
		{
		}

		/// <summary>
		/// ダイアログのタイトルを指定して、
		/// <see cref="FileSaveDialog"/>クラスの新しいインスタンスを初期化します。
		/// </summary>
		/// <param name="title">ダイアログタイトル。</param>
		public FileSaveDialog(string title)
			: base(title)
		{
		}

		/// <summary>
		/// ユーザーが、すでに存在するファイルを指定した場合に、
		/// 警告メッセージを表示するかどうかを示す値を取得または設定します。
		/// </summary>
		public bool OverwritePrompt
		{
			get
			{
				return this.overwritePrompt;
			}
			set
			{
				ThrowIfDialogShowingPropertyCannotBeChanged();
				this.overwritePrompt = value;
			}
		}

		/// <summary>
		/// ユーザーが、存在しないファイルを指定した場合に、
		/// ファイルを作成することを確認するメッセージを表示するかどうかを示す値を取得または設定します。
		/// </summary>
		public bool CreatePrompt
		{
			get
			{
				return this.createPrompt;
			}
			set
			{
				ThrowIfDialogShowingPropertyCannotBeChanged();
				this.createPrompt = value;
			}
		}

		/// <summary>
		/// ダイアログを拡張モードで表示するかどうかを示す値を取得または設定します。
		/// </summary>
		public bool IsExpandedMode
		{
			get
			{
				return this.isExpandedMode;
			}
			set
			{
				ThrowIfDialogShowingPropertyCannotBeChanged();
				this.isExpandedMode = value;
			}
		}

		/// <summary>
		/// 選択したファイルを取得します。
		/// </summary>
		public ShellFile ShellFile
		{
			get
			{
				if (this.shellFile == null)
				{
					this.shellFile = GetShellFile();
				}
				return this.shellFile;
			}
		}

		/// <summary>
		/// ファイル保存ダイアログを表示します。
		/// </summary>
		/// <returns>ダイアログ実行結果。</returns>
		public FileDialogResult Show()
		{
			return ShowDialog();
		}

		/// <summary>
		/// ファイル保存ダイアログを表示します。
		/// </summary>
		/// <param name="parentWindow">親ウィンドウのハンドル。</param>
		/// <returns>ダイアログ実行結果。</returns>
		public FileDialogResult Show(Window parentWindow)
		{
			Contract.Requires<ArgumentNullException>(parentWindow != null);

			return ShowDialog(parentWindow);
		}

		protected override FileDialogOptions GetDialogOptions()
		{
			var result = base.GetDialogOptions();

			if (this.OverwritePrompt)
			{
				result |= FileDialogOptions.OverwritePrompt;
			}
			if (this.CreatePrompt)
			{
				result |= FileDialogOptions.CreatePrompt;
			}
			if (!this.IsExpandedMode)
			{
				result |= FileDialogOptions.ExpandMode;
			}

			return result;
		}
	}
}