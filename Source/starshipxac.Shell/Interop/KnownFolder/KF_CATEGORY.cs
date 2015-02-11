using System;

namespace starshipxac.Shell.Interop.KnownFolder
{
	/// <summary>
	/// 標準フォルダーカテゴリーを定義します。
	/// </summary>
	/// <remarks>
	/// http://msdn.microsoft.com/en-us/library/windows/desktop/bb762512(v=vs.85).aspx
	/// </remarks>
	public enum KF_CATEGORY
	{
		/// <summary>
		/// 仮想フォルダー。
		/// </summary>
		/// <remarks>
		/// 例: <c>Control Panel</c>, <c>Printers</c>
		/// </remarks>
		KF_CATEGORY_VIRTUAL = 1,

		/// <summary>
		/// ファイルシステム上の固定フォルダー。
		/// </summary>
		/// <remarks>
		/// 例: <c>Windows</c>, <c>Program Files</c>
		/// </remarks>
		KF_CATEGORY_FIXED = 2,

		/// <summary>
		/// ファイルシステム上の共通フォルダー。
		/// </summary>
		/// <remarks>
		/// 例: <c>Documents</c>
		/// </remarks>
		KF_CATEGORY_COMMON = 3,

		/// <summary>
		/// ユーザーごとのフォルダー。
		/// </summary>
		/// <remarks>
		/// 例: <c>%USERPROFILE%\Prictures</c>
		/// </remarks>
		KF_CATEGORY_PERUSER = 4,
	}
}