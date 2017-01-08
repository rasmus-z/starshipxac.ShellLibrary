using System;
using starshipxac.Shell.Interop;

namespace starshipxac.Shell
{
    /// <summary>
    ///     Define display name type of <see cref="ShellObject"/>.
    /// </summary>
    public enum DisplayNameTypes
    {
        /// <summary>
        ///     Relative display name from the desktop.
        /// </summary>
        Default = SIGDN.SIGDN_NORMALDISPLAY,

        /// <summary>
        ///     Relative display name from the parent folder.
        /// </summary>
        RelativeToParent = SIGDN.SIGDN_PARENTRELATIVEPARSING,

        /// <summary>
        ///     The relative display name from the parent folder displayed in the address bar.
        /// </summary>
        RelativeToParentAddressBar = SIGDN.SIGDN_PARENTRELATIVEFORADDRESSBAR,

        /// <summary>
        ///     Relative path name from the desktop.
        /// </summary>
        RelativeToDesktop = SIGDN.SIGDN_DESKTOPABSOLUTEPARSING,

        /// <summary>
        ///     A relative editable name from the parent folder.
        /// </summary>
        RelativeToParentEditing = SIGDN.SIGDN_PARENTRELATIVEEDITING,

        /// <summary>
        ///     A relative editable name from the desktop.
        /// </summary>
        RelativeToDesktopEditing = SIGDN.SIGDN_DESKTOPABSOLUTEEDITING,

        /// <summary>
        /// </summary>
        RelativeForUI = SIGDN.SIGDN_PARENTRELATIVEFORUI,

        /// <summary>
        ///     Path name on file system.
        /// </summary>
        FileSystemPath = SIGDN.SIGDN_FILESYSPATH,

        /// <summary>
        ///     Relative URL.
        /// </summary>
        Url = SIGDN.SIGDN_URL
    }
}