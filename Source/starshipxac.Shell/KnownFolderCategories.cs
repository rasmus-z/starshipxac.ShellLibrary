using System;
using starshipxac.Shell.Interop.KnownFolder;

namespace starshipxac.Shell
{
    /// <summary>
    /// 標準フォルダーカテゴリーを定義します。
    /// </summary>
    public enum KnownFolderCategories
    {
        /// <summary>
        /// 未定義のフォルダーカテゴリー。
        /// </summary>
        None = 0,

        /// <summary>
        /// 仮想フォルダー。
        /// </summary>
        Virtual = KF_CATEGORY.KF_CATEGORY_VIRTUAL,

        /// <summary>
        /// 固定フォルダー。
        /// </summary>
        Fixed = KF_CATEGORY.KF_CATEGORY_FIXED,

        /// <summary>
        /// 共通フォルダー。
        /// </summary>
        Common = KF_CATEGORY.KF_CATEGORY_COMMON,

        /// <summary>
        /// ユーザーごとのフォルダー。
        /// </summary>
        PerUser = KF_CATEGORY.KF_CATEGORY_PERUSER
    }
}