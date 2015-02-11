using System;

namespace starshipxac.Shell.Interop
{
	/// <summary>
	/// 表示名フラグを定義します。
	/// </summary>
	/// <remarks>
	/// http://msdn.microsoft.com/en-us/library/windows/desktop/bb762544(v=vs.85).aspx
	/// </remarks>
	internal enum SIGDN
	{
		SIGDN_NORMALDISPLAY = 0x00000000,

		SIGDN_PARENTRELATIVEPARSING = unchecked((int)0x80018001),

		SIGDN_DESKTOPABSOLUTEPARSING = unchecked((int)0x80028000),

		SIGDN_PARENTRELATIVEEDITING = unchecked((int)0x80031001),

		SIGDN_DESKTOPABSOLUTEEDITING = unchecked((int)0x8004c000),

		SIGDN_FILESYSPATH = unchecked((int)0x80058000),

		SIGDN_URL = unchecked((int)0x80068000),

		SIGDN_PARENTRELATIVEFORADDRESSBAR = unchecked((int)0x8007c001),

		SIGDN_PARENTRELATIVE = unchecked((int)0x80080001),

		SIGDN_PARENTRELATIVEFORUI = unchecked((int)0x80094001)
	}
}