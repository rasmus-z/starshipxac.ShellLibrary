using System;
using System.Diagnostics.CodeAnalysis;

namespace starshipxac.Shell.Interop.Library
{
    /// <summary>
    ///     Specifies the library options.
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/dd378450(v=vs.85).aspx
    /// </remarks>
    [Flags]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal enum LIBRARYOPTIONFLAGS
    {
        /// <summary>
        ///     No library options are set.
        /// </summary>
        LOF_DEFAULT = 0,

        /// <summary>
        ///     Pin the library to the navigation pane.
        /// </summary>
        LOF_PINNEDTONAVPANE = 0x1,

        /// <summary>
        ///     All valid library options flags.
        /// </summary>
        LOF_MASK_ALL = 0x1
    };
}