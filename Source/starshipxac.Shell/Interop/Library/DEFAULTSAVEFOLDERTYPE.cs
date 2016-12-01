using System;
using System.Diagnostics.CodeAnalysis;

namespace starshipxac.Shell.Interop.Library
{
    /// <summary>
    ///     Specifies the default save location.
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/dd378443(v=vs.85).aspx
    /// </remarks>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal enum DEFAULTSAVEFOLDERTYPE
    {
        /// <summary>
        ///     The current user determines the save folder.
        ///     If the current user is the library's owner, use the private save location (<see cref="DSFT_PRIVATE"/>).
        ///     If the current user is not the library's owner, use the public save location (<see cref="DSFT_PUBLIC"/>).
        /// </summary>
        DSFT_DETECT = 1,

        /// <summary>
        ///     The library's private save location, which can only be accessed by the library's owner.
        /// </summary>
        DSFT_PRIVATE = 2,

        /// <summary>
        ///     The library's public save location, which can be accessed by all users.
        /// </summary>
        DSFT_PUBLIC = 3
    };
}