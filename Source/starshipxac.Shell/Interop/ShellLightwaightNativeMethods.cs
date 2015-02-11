using System;
using System.Runtime.InteropServices;
using System.Text;

namespace starshipxac.Shell.Interop
{
	internal static class ShellLightwaightNativeMethods
	{
		[DllImport("shlwapi.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		internal static extern int StrCmpLogicalW(String x, String y);

		[DllImport("shlwapi.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		internal static extern HRESULT StrFormatByteSizeEx(
			UInt64 ull,
			SFBS_FLAGS flags,
			[MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszBuf,
			int cchBuf);

		[DllImport("shlwapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool PathCompactPathEx(
			[MarshalAs(UnmanagedType.LPTStr)] StringBuilder pszOut,
			[MarshalAs(UnmanagedType.LPTStr)] string pszSrc,
			int cchMax,
			UInt32 dwFlags);
	}
}