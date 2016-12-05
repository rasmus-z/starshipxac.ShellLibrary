using System;
using System.Diagnostics.CodeAnalysis;

namespace starshipxac.Shell.Interop.Library
{
    /// <summary>
    ///     Specifies the options for handling a name collision when saving a library. 
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/dd378451(v=vs.85).aspx
    /// </remarks>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal enum LIBRARYSAVEFLAGS
    {
        /// <summary>
        ///     If a library with the same name already exists, the save operation fails. 
        /// </summary>
        LSF_FAILIFTHERE = 0,

        /// <summary>
        ///     If a library with the same name already exists, the save operation overwrites the existing library. 
        /// </summary>
        LSF_OVERRIDEEXISTING = 0x1,

        /// <summary>
        ///     If a library with the same name already exists, the save operation generates a new, unique name for the library. 
        /// </summary>
        LSF_MAKEUNIQUENAME = 0x2
    };
}