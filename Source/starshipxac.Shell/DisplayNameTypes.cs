using System;
using starshipxac.Shell.Interop;

namespace starshipxac.Shell
{
	/// <summary>
	/// <see cref="ShellObject"/>の表示名種別を定義します。
	/// </summary>
	public enum DisplayNameTypes
	{
		/// <summary>
		/// デスクトップからの相対的な表示名。
		/// </summary>
		Default = SIGDN.SIGDN_NORMALDISPLAY,

		/// <summary>
		/// 親フォルダからの相対的な表示名。
		/// </summary>
		RelativeToParent = SIGDN.SIGDN_PARENTRELATIVEPARSING,

		/// <summary>
		/// アドレスバーに表示する親フォルダーからの相対的な表示名。
		/// </summary>
		RelativeToParentAddressBar = SIGDN.SIGDN_PARENTRELATIVEFORADDRESSBAR,

		/// <summary>
		/// デスクトップからの相対的なパス名。
		/// </summary>
		RelativeToDesktop = SIGDN.SIGDN_DESKTOPABSOLUTEPARSING,

		/// <summary>
		/// 親フォルダからの相対的な編集可能な名前。
		/// </summary>
		RelativeToParentEditing = SIGDN.SIGDN_PARENTRELATIVEEDITING,

		/// <summary>
		/// デスクトップからの相対的な編集可能な名前。
		/// </summary>
		RelativeToDesktopEditing = SIGDN.SIGDN_DESKTOPABSOLUTEEDITING,

		/// <summary>
		/// 
		/// </summary>
		RelativeForUI = SIGDN.SIGDN_PARENTRELATIVEFORUI,

		/// <summary>
		/// ファイルシステム上のパス名。
		/// </summary>
		FileSystemPath = SIGDN.SIGDN_FILESYSPATH,

		/// <summary>
		/// 相対的なURL。
		/// </summary>
		Url = SIGDN.SIGDN_URL,
	}
}