using System;

namespace starshipxac.Shell.PropertySystem.Interop
{
	/// <summary>
	/// 
	/// </summary>
	/// <remarks>
	/// http://msdn.microsoft.com/en-us/library/windows/desktop/bb761535(v=vs.85).aspx
	/// </remarks>
	internal enum PROPDESC_DISPLAYTYPE
	{
		PDDT_STRING = 0,
		PDDT_NUMBER = 1,
		PDDT_BOOLEAN = 2,
		PDDT_DATETIME = 3,
		PDDT_ENUMERATED = 4
	}
}