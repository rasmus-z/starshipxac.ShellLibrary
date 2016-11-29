using System;
using System.Runtime.InteropServices;
using System.Text;

namespace starshipxac.Shell.Interop
{
    /// <summary>
    ///     Shell Lightweight Utility Functions
    /// </summary>
    /// <remarks>
    ///     https://msdn.microsoft.com/en-us/library/windows/desktop/bb759844(v=vs.85).aspx
    /// </remarks>
    internal static class ShellLightwaightNativeMethods
    {
        /// <summary>
        ///     Compares two Unicode strings.
        ///     Digits in the strings are considered as numerical content rather than text.
        ///     This test is not case-sensitive.
        /// </summary>
        /// <param name="x">A pointer to the first null-terminated string to be compared.</param>
        /// <param name="y">A pointer to the second null-terminated string to be compared.</param>
        /// <returns>
        ///     <list type="bullet">
        ///         <item>Returns zero if the strings are identical.</item>
        ///         <item>Returns 1 if the string pointed to by psz1 has a greater value than that pointed to by psz2.</item>
        ///         <item>Returns -1 if the string pointed to by psz1 has a lesser value than that pointed to by psz2.</item>
        ///     </list>
        /// </returns>
        /// <remarks>
        ///     https://msdn.microsoft.com/en-us/library/windows/desktop/bb759947(v=vs.85).aspx
        /// </remarks>
        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        internal static extern int StrCmpLogicalW(String x, String y);

        /// <summary>
        ///     Converts a numeric value into a string that represents the number in bytes, kilobytes, megabytes, or gigabytes, depending on the size.
        /// </summary>
        /// <param name="ull">The numeric value to be converted.</param>
        /// <param name="flags">One of the SFBS_FLAGS enumeration values that specifies whether to round or truncate undisplayed digits. This value cannot be NULL.</param>
        /// <param name="pszBuf">A pointer to a buffer that receives the converted string.</param>
        /// <param name="cchBuf">The size of the buffer pointed to by pszBuf, in characters.</param>
        /// <returns>If this function succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.</returns>
        /// <remarks>
        ///     https://msdn.microsoft.com/en-us/library/windows/desktop/bb892884%28v=vs.85%29.aspx?f=255&MSPPError=-2147217396
        /// </remarks>
        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
        internal static extern HRESULT StrFormatByteSizeEx(
            UInt64 ull,
            SFBS_FLAGS flags,
            [MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszBuf,
            int cchBuf);

        /// <summary>
        ///     Truncates a path to fit within a certain number of characters by replacing path components with ellipses.
        /// </summary>
        /// <param name="pszOut">The address of the string that has been altered.</param>
        /// <param name="pszSrc">A pointer to a null-terminated string of length MAX_PATH that contains the path to be altered.</param>
        /// <param name="cchMax">The maximum number of characters to be contained in the new string, including the terminating null character. For example, if cchMax = 8, the resulting string can contain a maximum of 7 characters plus the terminating null character.</param>
        /// <param name="dwFlags"></param>
        /// <returns>Returns TRUE if successful, or FALSE otherwise.</returns>
        /// <remarks>
        ///     https://msdn.microsoft.com/en-us/library/windows/desktop/bb773578(v=vs.85).aspx
        /// </remarks>
        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool PathCompactPathEx(
            [MarshalAs(UnmanagedType.LPTStr)] StringBuilder pszOut,
            [MarshalAs(UnmanagedType.LPTStr)] string pszSrc,
            int cchMax,
            UInt32 dwFlags);

        /// <summary>
        ///     Parses a file location string that contains a file location and icon index, and returns separate values.
        /// </summary>
        /// <param name="pszIconFile"></param>
        /// <returns></returns>
        /// <remarks>
        ///     https://msdn.microsoft.com/en-us/library/windows/desktop/bb773737(v=vs.85).aspx
        /// </remarks>
        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern int PathParseIconLocation(
            [MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszIconFile);

        /// <summary>
        ///     Extracts a specified text resource when given that resource in the form of an indirect string (a string that begins with the '@' symbol).
        /// </summary>
        /// <param name="pszSource"></param>
        /// <param name="pszOutBuf"></param>
        /// <param name="cchOutBuf"></param>
        /// <param name="ppvReserved"></param>
        /// <returns></returns>
        /// <remarks>
        ///     https://msdn.microsoft.com/en-us/library/windows/desktop/bb759919(v=vs.85).aspx
        /// </remarks>
        [DllImport("shlwapi.dll", BestFitMapping = false, CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = false,
            ThrowOnUnmappableChar = true)]
        internal static extern int SHLoadIndirectString(string pszSource, StringBuilder pszOutBuf, int cchOutBuf,
            IntPtr ppvReserved);
    }
}