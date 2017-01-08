using System;
using starshipxac.Shell.Interop;

namespace starshipxac.Shell
{
    /// <summary>
    ///     Define Library Folder types.
    /// </summary>
    public static class LibraryFolderTypes
    {
        /// <summary>
        ///     The folder does not fall under one of the other FOLDERTYPEID categories.
        /// </summary>
        public static Guid Generic => KnownFolderTypes.FOLDERTYPEID_Generic;

        /// <summary>
        ///     Documents folder.
        /// </summary>
        public static Guid Documents => KnownFolderTypes.FOLDERTYPEID_Documents;

        /// <summary>
        ///     Music folder.
        /// </summary>
        public static Guid Music => KnownFolderTypes.FOLDERTYPEID_Music;

        /// <summary>
        ///     Pictures folder.
        /// </summary>
        public static Guid Pictures => KnownFolderTypes.FOLDERTYPEID_Pictures;

        /// <summary>
        ///     Videos folder.
        /// </summary>
        public static Guid Videos => KnownFolderTypes.FOLDERTYPEID_Videos;
    }
}