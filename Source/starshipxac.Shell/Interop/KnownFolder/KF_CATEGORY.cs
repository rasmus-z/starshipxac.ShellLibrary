using System;
using System.Diagnostics.CodeAnalysis;

namespace starshipxac.Shell.Interop.KnownFolder
{
    /// <summary>
    ///     Value that represent a category by which a folder registered
    ///     with the Known Folder system can be classified.
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/bb762512(v=vs.85).aspx
    /// </remarks>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum KF_CATEGORY
    {
        /// <summary>
        ///     Virtual folders are not part of the file system, which is to say that they have no path.
        /// </summary>
        /// <remarks>
        ///     Example: <c>Control Panel</c>, <c>Printers</c>
        /// </remarks>
        KF_CATEGORY_VIRTUAL = 1,

        /// <summary>
        ///     Fixed file system folders are not managed by the Shell
        ///     and are usually given a permanent path when the system is installed.
        /// </summary>
        /// <remarks>
        ///     Example: <c>Windows</c>, <c>Program Files</c>
        /// </remarks>
        KF_CATEGORY_FIXED = 2,

        /// <summary>
        ///     Common folders are those file system folders used for sharing data and settings, accessible
        ///     by all users of a system.
        /// </summary>
        /// <remarks>
        ///     Example: <c>Documents</c>
        /// </remarks>
        KF_CATEGORY_COMMON = 3,

        /// <summary>
        ///     Per-user folders are those stored under each user's profile and accessible only by that user.
        /// </summary>
        /// <remarks>
        ///     Example: <c>%USERPROFILE%\Prictures</c>
        /// </remarks>
        KF_CATEGORY_PERUSER = 4,
    }
}