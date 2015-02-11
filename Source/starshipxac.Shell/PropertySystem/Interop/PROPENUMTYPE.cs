using System;

namespace starshipxac.Shell.PropertySystem.Interop
{
	/// <summary>
	/// プロパティ列挙種別を定義します。
	/// </summary>
	/// <remarks>
	/// http://msdn.microsoft.com/en-us/library/windows/desktop/bb761487(v=vs.85).aspx
	/// </remarks>
	internal enum PROPENUMTYPE
	{
		PET_DISCRETEVALUE = 0,
		PET_RANGEDVALUE = 1,
		PET_DEFAULTVALUE = 2,
		PET_ENDRANGE = 3
	};
}