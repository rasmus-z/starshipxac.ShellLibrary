using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

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
        public static ShellLibrary DocumentsLibrary
        {
            get
            {
                Contract.Ensures(Contract.Result<ShellLibrary>() != null);
                return ShellLibraryFactory.Load(ShellKnownFolderFactory.FromCanonicalName("DocumentsLibrary"));
            }
        }

        /// <summary>
        /// ミュージックライブラリを取得します。
        /// </summary>
        public static ShellLibrary MusicLibrary
        {
            get
            {
                Contract.Ensures(Contract.Result<ShellLibrary>() != null);
                return ShellLibraryFactory.Load(ShellKnownFolderFactory.FromCanonicalName("MusicLibrary"));
            }
        }

        /// <summary>
        /// ピクチャライブラリを取得します。
        /// </summary>
        public static ShellLibrary PicturesLibrary
        {
            get
            {
                Contract.Ensures(Contract.Result<ShellLibrary>() != null);
                return ShellLibraryFactory.Load(ShellKnownFolderFactory.FromCanonicalName("PicturesLibrary"));
            }
        }

        /// <summary>
        /// ビデオライブラリを取得します。
        /// </summary>
        public static ShellLibrary VideosLibrary
        {
            get
            {
                Contract.Ensures(Contract.Result<ShellLibrary>() != null);
                return ShellLibraryFactory.Load(ShellKnownFolderFactory.FromCanonicalName("VideosLibrary"));
            }
        }

        /// <summary>
        /// 録画されたTVライブラリを取得します。
        /// </summary>
        public static ShellLibrary RecordedTVLibrary
        {
            get
            {
                Contract.Ensures(Contract.Result<ShellLibrary>() != null);
                return ShellLibraryFactory.Load(ShellKnownFolderFactory.FromCanonicalName("RecordedTVLibrary"));
            }
        }

        /// <summary>
        /// 全てのライブラリを列挙します。
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ShellLibrary> EnumerateLibraries()
        {
            return ShellKnownFolders.Libraries.EnumerateItems().OfType<ShellLibrary>();
        }

        /// <summary>
        /// 全てのパブリックライブラリを列挙します。
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ShellLibrary> EnumeratePublicLibraries()
        {
            return ShellKnownFolders.PublicLibraries.EnumerateItems().OfType<ShellLibrary>();
        }
    }
}