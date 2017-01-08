using System;
using starshipxac.Shell.Interop;

namespace starshipxac.Shell
{
    /// <summary>
    ///     Define the view mode.
    /// </summary>
    public enum FolderLogicalViewMode
    {
        /// <summary>
        ///     The view is not specified.
        /// </summary>
        Unspecified = FOLDERLOGICALVIEWMODE.FLVM_UNSPECIFIED,

        /// <summary>
        ///     This should have the same affect as Unspecified.
        /// </summary>
        None = 0,

        /// <summary>
        ///     The minimum valid enumeration value. Used for validation purposes only.
        /// </summary>
        First = FOLDERLOGICALVIEWMODE.FLVM_FIRST,

        /// <summary>
        ///     Details view.
        /// </summary>
        Details = FOLDERLOGICALVIEWMODE.FLVM_DETAILS,

        /// <summary>
        ///     Tiles view.
        /// </summary>
        Tiles = FOLDERLOGICALVIEWMODE.FLVM_TILES,

        /// <summary>
        ///     Icons view.
        /// </summary>
        Icons = FOLDERLOGICALVIEWMODE.FLVM_ICONS,

        /// <summary>
        ///     List view.
        /// </summary>
        List = FOLDERLOGICALVIEWMODE.FLVM_LIST,

        /// <summary>
        ///     Content view.
        /// </summary>
        Content = FOLDERLOGICALVIEWMODE.FLVM_CONTENT,

        /// <summary>
        ///     The maximum valid enumeration value. Used for validation purposes only.
        /// </summary>
        Last = FOLDERLOGICALVIEWMODE.FLVM_LAST
    }
}