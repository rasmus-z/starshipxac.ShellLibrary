using System;

namespace starshipxac.Shell.Interop.Library
{
	/// <summary>
	/// ライブラリ保存フラグを定義します。
	/// </summary>
	/// <remarks>
	/// http://msdn.microsoft.com/en-us/library/windows/desktop/dd378451(v=vs.85).aspx
	/// </remarks>
	internal enum LIBRARYSAVEFLAGS
	{
		LSF_FAILIFTHERE = 0,
		LSF_OVERRIDEEXISTING = 0x1,
		LSF_MAKEUNIQUENAME = 0x2
	};
}