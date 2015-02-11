using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Windows;
using starshipxac.Shell;

namespace starshipxac.Windows.Shell.Dialogs
{
	/// <summary>
	/// ファイルを選択するダイアログを表示します。
	/// </summary>
	public sealed class FileOpenDialog : FileOpenDialogBase
	{
		private IEnumerable<ShellFile> shellFiles;

		/// <summary>
		/// <see cref="FileOpenDialog"/>クラスの新しいインスタンスを初期化します。
		/// </summary>
		public FileOpenDialog()
		{
			this.MultiSelect = false;
		}

		/// <summary>
		/// ダイアログのタイトルを指定して、
		/// <see cref="FileOpenDialog"/>クラスの新しいインスタンスを初期化します。
		/// </summary>
		/// <param name="title">ダイアログタイトル。</param>
		public FileOpenDialog(string title)
			: base(title)
		{
		}

		/// <summary>
		/// ファイルを複数選択可能かどうか示す値を取得または設定します。
		/// </summary>
		public bool MultiSelect { get; set; }

		/// <summary>
		/// 読み込み専用ファイルのみ選択可能にするかどうかを示す値を取得または設定します。
		/// </summary>
		public bool EnsureReadOnly { get; set; }

		/// <summary>
		/// ファイルシステム以外のアイテムを選択可能にするかどうかを示す値を取得または設定します。
		/// </summary>
		public bool AllowNonFileSystemItem { get; set; }

		/// <summary>
		/// 選択したファイルのコレクションを取得します。
		/// </summary>
		public IEnumerable<ShellFile> ShellFiles
		{
			get
			{
				if (this.shellFiles == null)
				{
					this.shellFiles = GetShellFiles();
				}
				return this.shellFiles;
			}
		}

		/// <summary>
		/// ファイル選択ダイアログを表示します。
		/// </summary>
		/// <returns>ダイアログ実行結果。</returns>
		public FileDialogResult Show()
		{
			return ShowDialog();
		}

		/// <summary>
		/// ファイル選択ダイアログを表示します。
		/// </summary>
		/// <param name="parentWindow">親ウィンドウ。</param>
		/// <returns>ダイアログ実行結果。</returns>
		public FileDialogResult Show(Window parentWindow)
		{
			Contract.Requires<ArgumentNullException>(parentWindow != null);

			return ShowDialog(parentWindow);
		}

		protected override FileDialogOptions GetDialogOptions()
		{
			var result = base.GetDialogOptions();

			if (this.MultiSelect)
			{
				result |= FileDialogOptions.MultiSelect;
			}
			if (this.EnsureReadOnly)
			{
				result |= FileDialogOptions.EnsureReadOnly;
			}
			if (!this.AllowNonFileSystemItem)
			{
				result |= FileDialogOptions.ForceFileSystem;
			}
			else
			{
				result |= FileDialogOptions.AllNonStotageItems;
			}

			return result;
		}
	}
}