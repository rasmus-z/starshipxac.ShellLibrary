using System;

namespace starshipxac.Shell.Interop.KnownFolder
{
	/// <summary>
	/// 標準フォルダー定義フラグを定義します。
	/// </summary>
	/// <remarks>
	/// http://msdn.microsoft.com/en-us/library/windows/desktop/bb762513(v=vs.85).aspx
	/// </remarks>
	[Flags]
	public enum KF_DEFINITION_FLAGS : uint
	{
		KFDF_LOCAL_REDIRECT_ONLY = 0x2,
		KFDF_ROAMABLE = 0x4,
		KFDF_PRECREATE = 0x8,
		KFDF_STREAM = 0x10,
		KFDF_PUBLISHEXPANDEDPATH = 0x20,
		KFDF_NO_REDIRECT_UI = 0x40
	}
}