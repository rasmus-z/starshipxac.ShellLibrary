using System;
using System.Runtime.InteropServices;

namespace starshipxac.Shell.Interop
{
	/// <summary>
	/// アイテムIDリスト構造体を定義します。
	/// </summary>
	/// <remarks>
	/// http://msdn.microsoft.com/en-us/library/windows/desktop/bb773321(v=vs.85).aspx
	/// </remarks>
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct ITEMIDLIST
	{
		[MarshalAs(UnmanagedType.Struct)]
		public SHITEMID mkid;
	}
}