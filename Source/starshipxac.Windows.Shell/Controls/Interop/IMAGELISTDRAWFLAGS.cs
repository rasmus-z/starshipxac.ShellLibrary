using System;

namespace starshipxac.Windows.Shell.Controls.Interop
{
	/// <summary>
	/// イメージリスト描画フラグを表します。
	/// </summary>
	internal static class IMAGELISTDRAWFLAGS
	{
		public const UInt32 IDL_NORMAL = 0x00000000;
		public const UInt32 IDL_TRANSPARENT = 0x0000001;
		public const UInt32 IDL_BLEND25 = 0x00000002;
		public const UInt32 IDL_BLEND50 = 0x00000004;
		public const UInt32 IDL_MASK = 0x00000010;
		public const UInt32 IDL_IMAGE = 0x00000020;
		public const UInt32 IDL_ROP = 0x00000040;
		public const UInt32 IDL_OVERLAYMASK = 0x00000F00;
		public const UInt32 IDL_PRESERVEALPHA = 0x00001000;
		public const UInt32 IDL_SCALE = 0x00004000;
		public const UInt32 IDL_ASYNC = 0x00008000;

		public const UInt32 IDL_FOCUS = IDL_BLEND25;
		public const UInt32 IDL_SELECTED = IDL_BLEND50;
		public const UInt32 IDL_BLEND = IDL_BLEND50;
	}
}