using System;
using System.Diagnostics.CodeAnalysis;

namespace starshipxac.Shell.Interop.Library
{
    /// <summary>
    ///     Used by <see cref="ShellLibraryNativeMethods.SHShowManageLibraryUI(IShellItem, IntPtr, string, string, LIBRARYMANAGEDIALOGOPTIONS)"/> to define options for handling a name collision when saving a library.
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/dd378449(v=vs.85).aspx
    /// </remarks>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal enum LIBRARYMANAGEDIALOGOPTIONS
    {
        /// <summary>
        ///     Show default warning UI to the user.
        /// </summary>
        LMD_DEFAULT = 0,

        /// <summary>
        ///     Do not display a warning dialog to the user in collisions that concern network locations that cannot be indexed.
        /// </summary>
        LMD_ALLOWUNINDEXABLENETWORKLOCATIONS = 0x1
    }
}