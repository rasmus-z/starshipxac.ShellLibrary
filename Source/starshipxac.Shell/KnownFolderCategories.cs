using System;
using starshipxac.Shell.Interop.KnownFolder;

namespace starshipxac.Shell
{
    /// <summary>
    ///     Define known folder categories
    /// </summary>
    public enum KnownFolderCategories
    {
        /// <summary>
        ///     None.
        /// </summary>
        None = 0,

        /// <summary>
        ///     Virtual folders are not part of the file system, which is to say that they have no path.
        /// </summary>
        Virtual = KF_CATEGORY.KF_CATEGORY_VIRTUAL,

        /// <summary>
        ///     Fixed file system folders are not managed by the Shell
        ///     and are usually given a permanent path when the system is installed.
        /// </summary>
        Fixed = KF_CATEGORY.KF_CATEGORY_FIXED,

        /// <summary>
        ///     Common folders are those file system folders used for sharing data and settings, accessible
        ///     by all users of a system.
        /// </summary>
        Common = KF_CATEGORY.KF_CATEGORY_COMMON,

        /// <summary>
        ///     Per-user folders are those stored under each user's profile and accessible only by that user.
        /// </summary>
        PerUser = KF_CATEGORY.KF_CATEGORY_PERUSER
    }
}