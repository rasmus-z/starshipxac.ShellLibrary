using System;
using starshipxac.Shell.Interop;

namespace starshipxac.Shell
{
    /// <summary>
    /// ビューモードを定義します。
    /// </summary>
    public enum FolderLogicalViewMode
    {
        /// <summary>
        /// ビューは指定されていません。
        /// </summary>
        Unspecified = FOLDERLOGICALVIEWMODE.FLVM_UNSPECIFIED,

        /// <summary>
        /// This should have the same affect as Unspecified.
        /// </summary>
        None = 0,

        /// <summary>
        /// 最小値。検証の目的でのみ使用されます。
        /// </summary>
        First = FOLDERLOGICALVIEWMODE.FLVM_FIRST,

        /// <summary>
        /// 規定のビュー。
        /// </summary>
        Details = FOLDERLOGICALVIEWMODE.FLVM_DETAILS,

        /// <summary>
        /// タイル表示。
        /// </summary>
        Tiles = FOLDERLOGICALVIEWMODE.FLVM_TILES,

        /// <summary>
        /// アイコン表示。
        /// </summary>
        Icons = FOLDERLOGICALVIEWMODE.FLVM_ICONS,

        /// <summary>
        /// リスト表示。
        /// </summary>
        List = FOLDERLOGICALVIEWMODE.FLVM_LIST,

        /// <summary>
        /// コンテンツ表示。
        /// </summary>
        Content = FOLDERLOGICALVIEWMODE.FLVM_CONTENT,

        /// <summary>
        /// 最大値。検証の目的でのみ使用されます。
        /// </summary>
        Last = FOLDERLOGICALVIEWMODE.FLVM_LAST,
    }
}