using System;

namespace starshipxac.Shell.PropertySystem.Interop
{
	/// <summary>
	/// 
	/// </summary>
	/// <remarks>
	/// http://msdn.microsoft.com/en-us/library/windows/desktop/bb762522(v=vs.85).aspx
	/// </remarks>
	internal enum PROPDESC_AGGREGATION_TYPE
	{
		PDAT_DEFAULT = 0,
		PDAT_FIRST = 1,
		PDAT_SUM = 2,
		PDAT_AVERAGE = 3,
		PDAT_DATERANGE = 4,
		PDAT_UNION = 5,
		PDAT_MAX = 6,
		PDAT_MIN = 7
	}
}