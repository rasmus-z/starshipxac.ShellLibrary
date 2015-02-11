using System;

namespace starshipxac.Shell.Interop
{
	/// <summary>
	/// 
	/// </summary>
	/// <remarks>
	/// http://msdn.microsoft.com/en-us/library/windows/desktop/bb774624(v=vs.85).aspx
	/// </remarks>
	[Flags]
	internal enum WTS_CACHEFLAGS
	{
		WTS_DEFAULT = 0,
		WTS_LOWQUALITY = 0x1,
		WTS_CACHED = 0x2
	}
}