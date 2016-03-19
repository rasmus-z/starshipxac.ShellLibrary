using System;
using starshipxac.Shell.Interop;

namespace starshipxac.Shell
{
    /// <summary>
    ///     ライブラリフォルダー種別を定義します。
    /// </summary>
    public static class LibraryFolderTypes
    {
        /// <summary>
        ///     全般
        /// </summary>
        public static Guid Generic => KnownFolderTypes.FOLDERTYPEID_Generic;

        /// <summary>
        ///     ドキュメントフォルダー
        /// </summary>
        public static Guid Documents => KnownFolderTypes.FOLDERTYPEID_Documents;

        /// <summary>
        ///     ミュージックフォルダー
        /// </summary>
        public static Guid Music => KnownFolderTypes.FOLDERTYPEID_Music;

        /// <summary>
        ///     ピクチャーフォルダー
        /// </summary>
        public static Guid Pictures => KnownFolderTypes.FOLDERTYPEID_Pictures;

        /// <summary>
        ///     ビデオフォルダー
        /// </summary>
        public static Guid Videos => KnownFolderTypes.FOLDERTYPEID_Videos;
    }
}