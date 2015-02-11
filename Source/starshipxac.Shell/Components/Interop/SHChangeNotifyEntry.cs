using System;
using System.Runtime.InteropServices;

namespace starshipxac.Shell.Components.Interop
{
	/// <summary>
	/// シェル変更通知構造体を定義します。
	/// </summary>
	/// <remarks>
	/// http://msdn.microsoft.com/en-us/library/windows/desktop/bb773405(v=vs.85).aspx
	/// </remarks>
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	internal struct SHChangeNotifyEntry
	{
		public IntPtr pidl;

		[MarshalAs(UnmanagedType.Bool)]
		public bool fRecursive;
	}
}