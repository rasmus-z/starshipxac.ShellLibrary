using System;
using starshipxac.Shell.Interop;

namespace starshipxac.Shell
{
    /// <summary>
    /// ライブラリフォルダー種別を定義します。
    /// </summary>
    public static class LibraryFolderType
    {
        /// <summary>
        /// 全般
        /// </summary>
        public static Guid Generic
        {
            get
            {
                return KnownFolderTypes.FOLDERTYPEID_Generic;
            }
        }

        /// <summary>
        /// ドキュメントフォルダー
        /// </summary>
        public static Guid Documents
        {
            get
            {
                return KnownFolderTypes.FOLDERTYPEID_Documents;
            }
        }

        /// <summary>
        /// ミュージックフォルダー
        /// </summary>
        public static Guid Music
        {
            get
            {
                return KnownFolderTypes.FOLDERTYPEID_Music;
            }
        }

        /// <summary>
        /// ピクチャーフォルダー
        /// </summary>
        public static Guid Pictures
        {
            get
            {
                return KnownFolderTypes.FOLDERTYPEID_Pictures;
            }
        }

        /// <summary>
        /// ビデオフォルダー
        /// </summary>
        public static Guid Videos
        {
            get
            {
                return KnownFolderTypes.FOLDERTYPEID_Videos;
            }
        }
    }
}