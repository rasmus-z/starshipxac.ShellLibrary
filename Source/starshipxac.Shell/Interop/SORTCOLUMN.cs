using System;
using System.Runtime.InteropServices;
using starshipxac.Shell.PropertySystem.Interop;

namespace starshipxac.Shell.Interop
{
	/// <summary>
	/// ソートカラム情報を定義します。
	/// </summary>
	/// <remarks>
	/// http://msdn.microsoft.com/en-us/library/windows/desktop/bb759817(v=vs.85).aspx
	/// </remarks>
	[StructLayout(LayoutKind.Sequential)]
	internal struct SORTCOLUMN
	{
		public SORTCOLUMN(PROPERTYKEY propertyKey, SORTDIRECTION direction)
			: this()
		{
			this.propkey = propertyKey;
			this.direction = direction;
		}

		public PROPERTYKEY propkey;
		public SORTDIRECTION direction;
	}
}