using System;

namespace starshipxac.Shell.Interop
{
	/// <summary>
	/// ビットマップイメージのアルファ種別を定義します。
	/// </summary>
	/// <remarks>
	/// http://msdn.microsoft.com/en-us/library/windows/desktop/bb774612(v=vs.85).aspx
	/// </remarks>
	internal enum WTS_ALPHATYPE
	{
		WTSAT_UNKNOWN = 0,
		WTSAT_RGB = 1,
		WTSAT_ARGB = 2
	}
}