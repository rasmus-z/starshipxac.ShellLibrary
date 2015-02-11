using System;

namespace starshipxac.Shell.Interop
{
	/// <summary>
	/// <c>StrFormatByteSizeEx</c>関数で使用するフラグを定義します。
	/// </summary>
	/// <remarks>
	/// http://msdn.microsoft.com/en-us/library/windows/desktop/bb892889(v=vs.85).aspx
	/// </remarks>
	internal enum SFBS_FLAGS
	{
		SFBS_FLAGS_ROUND_TO_NEAREST_DISPLAYED_DIGIT = 0x00000001,
		SFBS_FLAGS_TRUNCATE_UNDISPLAYED_DECIMAL_DIGITS = 0x00000002
	}
}