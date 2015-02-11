using System;

namespace starshipxac.Windows.Shell.Interop
{
	/// <summary>
	/// コマンド実行マスクを定義します。
	/// </summary>
	/// <remarks>
	/// http://msdn.microsoft.com/en-us/library/windows/desktop/bb773215(v=vs.85).aspx
	/// </remarks>
	internal static class CMIC_MASK
	{
		public const UInt32 CMIC_MASK_HOTKEY = 0x00000020;
		public const UInt32 CMIC_MASK_ICON = 0x00000010;
		public const UInt32 CMIC_MASK_FLAG_NO_UI = 0x00000400;
		public const UInt32 CMIC_MASK_UNICODE = 0x00004000;
		public const UInt32 CMIC_MASK_NO_CONSOLE = 0x00008000;
		public const UInt32 CMIC_MASK_ASYNCOK = 0x00100000;
		public const UInt32 CMIC_MASK_NOZONECHECKS = 0x00800000;
		public const UInt32 CMIC_MASK_SHIFT_DOWN = 0x10000000;
		public const UInt32 CMIC_MASK_CONTROL_DOWN = 0x40000000;
		public const UInt32 CMIC_MASK_FLAG_LOG_USAGE = 0x04000000;
		public const UInt32 CMIC_MASK_PTINVOKE = 0x2000000;
	}
}