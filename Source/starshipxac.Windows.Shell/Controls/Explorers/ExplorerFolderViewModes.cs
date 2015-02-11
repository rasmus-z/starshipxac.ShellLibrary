using System;

namespace starshipxac.Windows.Shell.Controls.Explorers
{
	/// <summary>
	/// アイテム表示モードを定義します。
	/// </summary>
	public enum ExplorerFolderViewModes
	{
		/// <summary>
		/// フォルダーに最適な表示モードを選択します。
		/// </summary>
		Auto = -1,

		/// <summary>
		/// コンテンツ表示。
		/// </summary>
		Content = 8,

		/// <summary>
		/// 詳細表示。
		/// </summary>
		Details = 4,

		/// <summary>
		/// アイコン表示。
		/// </summary>
		Icon = 1,

		/// <summary>
		/// リスト表示。
		/// </summary>
		List = 3,

		/// <summary>
		/// 小アイコン表示。
		/// </summary>
		SmallIcon = 2,

		/// <summary>
		/// 縮小画像表示。
		/// </summary>
		Thumbnail = 5,

		/// <summary>
		/// filmstrip format.
		/// </summary>
		ThumbStrip = 7,

		/// <summary>
		/// タイル表示。
		/// </summary>
		Tile = 6
	}
}