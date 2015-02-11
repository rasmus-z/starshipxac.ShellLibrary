using System;

namespace starshipxac.Shell.PropertySystem.Interop
{
	/// <summary>
	/// 
	/// </summary>
	/// <remarks>
	/// http://msdn.microsoft.com/en-us/library/windows/desktop/bb761542(v=vs.85).aspx
	/// </remarks>
	internal enum PROPDESC_GROUPING_RANGE
	{
		PDGR_DISCRETE = 0,
		PDGR_ALPHANUMERIC = 1,
		PDGR_SIZE = 2,
		PDGR_DYNAMIC = 3,
		PDGR_DATE = 4,
		PDGR_PERCENT = 5,
		PDGR_ENUMERATED = 6
	}
}