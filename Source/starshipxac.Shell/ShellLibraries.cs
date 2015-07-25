using System;
using System.Collections.Generic;
using System.Linq;
using static starshipxac.Shell.ShellKnownFolderFactory;
using static starshipxac.Shell.ShellLibraryFactory;

namespace starshipxac.Shell
{
    /// <summary>
    /// シェル標準ライブラリを取得します。
    /// </summary>
    public static class ShellLibraries
    {
        /// <summary>
        /// ドキュメントライブラリを取得します。
        /// </summary>
        public static ShellLibrary DocumentsLibrary => Load(FromCanonicalName("DocumentsLibrary"));

        /// <summary>
        /// ミュージックライブラリを取得します。
        /// </summary>
        public static ShellLibrary MusicLibrary => Load(FromCanonicalName("MusicLibrary"));

        /// <summary>
        /// ピクチャライブラリを取得します。
        /// </summary>
        public static ShellLibrary PicturesLibrary => Load(FromCanonicalName("PicturesLibrary"));

        /// <summary>
        /// ビデオライブラリを取得します。
        /// </summary>
        public static ShellLibrary VideosLibrary => Load(FromCanonicalName("VideosLibrary"));

        /// <summary>
        /// 録画されたTVライブラリを取得します。
        /// </summary>
        public static ShellLibrary RecordedTVLibrary => Load(FromCanonicalName("RecordedTVLibrary"));

        /// <summary>
        /// 全てのライブラリを列挙します。
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ShellLibrary> EnumerateLibraries()
        {
            return ShellKnownFolders.Libraries.EnumerateObjects().OfType<ShellLibrary>();
        }

        /// <summary>
        /// 全てのパブリックライブラリを列挙します。
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ShellLibrary> EnumeratePublicLibraries()
        {
            return ShellKnownFolders.PublicLibraries.EnumerateObjects().OfType<ShellLibrary>();
        }
    }
}