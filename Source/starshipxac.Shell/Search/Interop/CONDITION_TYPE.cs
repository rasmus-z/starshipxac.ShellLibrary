using System;

namespace starshipxac.Shell.Search.Interop
{
	/// <summary>
	/// 
	/// </summary>
	/// <remarks>
	/// http://msdn.microsoft.com/en-us/library/windows/desktop/aa965692(v=vs.85).aspx
	/// </remarks>
	internal enum CONDITION_TYPE
	{
		CT_AND_CONDITION = 0,
		CT_OR_CONDITION = (CT_AND_CONDITION + 1),
		CT_NOT_CONDITION = (CT_OR_CONDITION + 1),
		CT_LEAF_CONDITION = (CT_NOT_CONDITION + 1)
	}
}