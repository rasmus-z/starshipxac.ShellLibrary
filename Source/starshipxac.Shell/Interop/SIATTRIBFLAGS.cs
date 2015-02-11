using System;

namespace starshipxac.Shell.Interop
{
	/// <summary>
	/// シェル属性フラグを定義します。
	/// </summary>
	/// <remarks>
	/// http://msdn.microsoft.com/en-us/library/windows/desktop/bb761096(v=vs.85).aspx
	/// </remarks>
	internal enum SIATTRIBFLAGS
	{
		SIATTRIBFLAGS_AND = 0x00000001,

		SIATTRIBFLAGS_OR = 0x00000002,

		SIATTRIBFLAGS_APPCOMPAT = 0x00000003,

		SIATTRIBFLAGS_MASK = 0x00000003,

		SIATTRIBFLAGS_ALLITEMS = 0x00004000
	}
}