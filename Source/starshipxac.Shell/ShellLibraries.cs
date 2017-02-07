using System;
using System.Collections.Generic;
using System.Linq;
using static starshipxac.Shell.ShellKnownFolderFactory;
using static starshipxac.Shell.ShellLibraryFactory;

namespace starshipxac.Shell
{
    /// <summary>
    ///     Get the library folder.
    /// </summary>
    public static class ShellLibraries
    {
        /// <summary>
        ///     Get the documents library.
        /// </summary>
        public static ShellLibrary DocumentsLibrary => Load(FromCanonicalName("DocumentsLibrary"));

        /// <summary>
        ///     Get the music library.
        /// </summary>
        public static ShellLibrary MusicLibrary => Load(FromCanonicalName("MusicLibrary"));

        /// <summary>
        ///     Get the pictures library.
        /// </summary>
        public static ShellLibrary PicturesLibrary => Load(FromCanonicalName("PicturesLibrary"));

        /// <summary>
        ///     Get the video library.
        /// </summary>
        public static ShellLibrary VideosLibrary => Load(FromCanonicalName("VideosLibrary"));

        /// <summary>
        ///     Get the recorded TV library.
        /// </summary>
        public static ShellLibrary RecordedTVLibrary => Load(FromCanonicalName("RecordedTVLibrary"));

        /// <summary>
        ///     Enumerate all libraries.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ShellLibrary> EnumerateLibraries()
        {
            return ShellKnownFolders.Libraries.EnumerateObjects().OfType<ShellLibrary>();
        }

        /// <summary>
        ///     Enumerate all public libraries.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ShellLibrary> EnumeratePublicLibraries()
        {
            return ShellKnownFolders.PublicLibraries.EnumerateObjects().OfType<ShellLibrary>();
        }
    }
}