using System;
using System.Diagnostics.CodeAnalysis;

namespace starshipxac.Windows.Interop
{
    /// <summary>
    /// </summary>
    /// <remarks>
    ///     https://msdn.microsoft.com/en-us/library/windows/desktop/ms633548(v=vs.85).aspx
    /// </remarks>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal static class ShowWindowCommands
    {
        public const int SW_HIDE = 0;
        public const int SW_SHOWNORMAL = 1;
        public const int SW_SHOWMINIMIZED = 2;
        public const int SW_MAXIMIZE = 3;
        public const int SW_SHOWMAXIMIZED = 3;
        public const int SW_SHOWNOACTIVE = 4;
        public const int SW_SHOW = 5;
        public const int SW_MINIMIZE = 6;
        public const int SW_SHOWMINNOACTIVE = 7;
        public const int SW_SHOWNA = 8;
        public const int SW_RESTORE = 9;
        public const int SW_SHOWDEFAULT = 10;
    }
}