using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace starshipxac.Windows.Shell.Controls.Explorers.Interop
{
    /// <summary>
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/bb773308(v=vs.85).aspx
    /// </remarks>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal class FOLDERSETTINGS
    {
        public FOLDERVIEWMODE ViewMode;
        public FOLDERFLAGS fFlags;
    }
}