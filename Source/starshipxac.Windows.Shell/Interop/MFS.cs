using System;

namespace starshipxac.Windows.Shell.Interop
{
	/// <summary>
	/// メニューアイテム状態を定義します。
	/// </summary>
	/// <remarks>
	/// http://msdn.microsoft.com/en-us/library/windows/desktop/ms647578(v=vs.85).aspx
	/// </remarks>
	internal static class MFS
	{
		public const UInt32 MFS_CHECKED = 0x00000008;
		public const UInt32 MFS_DEFAULT = 0x00001000;
		public const UInt32 MFS_DISABLED = 0x00000003;
		public const UInt32 MFS_ENABLED = 0x00000000;
		public const UInt32 MFS_GRAYED = 0x00000003;
		public const UInt32 MFS_HILITE = 0x00000080;
		public const UInt32 MFS_UNCHECKED = 0x00000000;
		public const UInt32 MFS_UNHILITE = 0x00000000;
	}
}