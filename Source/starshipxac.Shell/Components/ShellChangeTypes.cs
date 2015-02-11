using System;

namespace starshipxac.Shell.Components
{
	/// <summary>
	/// シェル変更通知イベントを定義します。
	/// </summary>
	[Flags]
	public enum ShellChangeTypes : uint
	{
		/// <summary>
		/// 通知なし。
		/// </summary>
		None = 0,

		/// <summary>
		/// 名前変更通知。
		/// </summary>
		ItemRename = 0x00000001,

		/// <summary>
		/// 新規作成通知。
		/// </summary>
		ItemCreate = 0x00000002,

		/// <summary>
		/// 削除通知。
		/// </summary>
		ItemDelete = 0x00000004,

		/// <summary>
		/// ディレクトリ作成通知。
		/// </summary>
		DirectoryCreate = 0x00000008,

		/// <summary>
		/// ディレクトリ削除通知。
		/// </summary>
		DirectoryDelete = 0x00000010,

		/// <summary>
		/// メディア挿入通知。
		/// </summary>
		MediaInsert = 0x00000020,

		/// <summary>
		/// メディア除去通知。
		/// </summary>
		MediaRemove = 0x00000040,

		/// <summary>
		/// ドライブ除去通知。
		/// </summary>
		DriveRemove = 0x00000080,

		/// <summary>
		/// ドライブ追加通知。
		/// </summary>
		DriveAdd = 0x00000100,

		/// <summary>
		/// ネットワーク共有通知。
		/// </summary>
		NetShare = 0x00000200,

		/// <summary>
		/// ネットワーク共有解除通知。
		/// </summary>
		NetUnshare = 0x00000400,

		/// <summary>
		/// 属性変更通知。
		/// </summary>
		AttributesChange = 0x00000800,

		/// <summary>
		/// ディレクトリ更新通知。
		/// </summary>
		DirectoryContentsUpdate = 0x00001000,

		/// <summary>
		/// 更新通知。
		/// </summary>
		Update = 0x00002000,

		/// <summary>
		/// サーバー切断通知。
		/// </summary>
		ServerDisconnect = 0x00004000,

		/// <summary>
		/// システムイメージ更新通知。
		/// </summary>
		SystemImageUpdate = 0x00008000,

		/// <summary>
		/// ディレクトリ名変更通知。
		/// </summary>
		DirectoryRename = 0x00020000,

		/// <summary>
		/// 空き領域通知。
		/// </summary>
		FreeSpace = 0x00040000,

		/// <summary>
		/// 拡張イベント。
		/// </summary>
		ExtendedEvent = 0x04000000,

		/// <summary>
		/// 関連づけ変更通知。
		/// </summary>
		AssociationChange = 0x08000000,

		/// <summary>
		/// ディスクイベント通知マスク。
		/// </summary>
		DiskEventsMask = 0x0002381F,

		/// <summary>
		/// グローバルイベント通知マスク。
		/// </summary>
		GlobalEventsMask = 0x0C0581E0,

		/// <summary>
		/// 全イベント通知マスク。
		/// </summary>
		AllEventsMask = 0x7FFFFFFF,

		/// <summary>
		/// システムイベント通知マスク。
		/// </summary>
		FromInterrupt = 0x80000000,
	}
}