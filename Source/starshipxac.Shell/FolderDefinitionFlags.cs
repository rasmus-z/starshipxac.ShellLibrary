using System;
using starshipxac.Shell.Interop.KnownFolder;

namespace starshipxac.Shell
{
    /// <summary>
    ///     Define the behavior of the folder.
    /// </summary>
    [Flags]
    public enum FolderDefinitionFlags : uint
    {
        /// <summary>
        ///     None.
        /// </summary>
        None = 0,

        /// <summary>
        ///     Prevent a per-user known folder from being redirected to a network location.
        /// </summary>
        LocalRedirectOnly = KF_DEFINITION_FLAGS.KFDF_LOCAL_REDIRECT_ONLY,

        /// <summary>
        ///     Can be roamed through a PC-to-PC synchronization.
        /// </summary>
        Roamable = KF_DEFINITION_FLAGS.KFDF_ROAMABLE,

        /// <summary>
        ///     Create the folder when the user first logs on.
        /// </summary>
        Precreate = KF_DEFINITION_FLAGS.KFDF_PRECREATE,

        /// <summary>
        ///     The known folder is a file rather than a folder.
        /// </summary>
        Stream = KF_DEFINITION_FLAGS.KFDF_STREAM,

        /// <summary>
        ///     The full path of the known folder, with any environment variables fully expanded,
        ///     is stored in the registry under <c>HKEY_CURRENT_USER</c>.
        /// </summary>
        PublishExpandedPath = KF_DEFINITION_FLAGS.KFDF_PUBLISHEXPANDEDPATH,

        /// <summary>
        ///     Prevent showing the Locations tab in the property dialog of the known folder.
        /// </summary>
        NoRedirectUI = KF_DEFINITION_FLAGS.KFDF_NO_REDIRECT_UI,
    }
}