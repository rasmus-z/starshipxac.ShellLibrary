using System;
using starshipxac.Shell.Interop;

namespace starshipxac.Shell.Media.Imaging
{
    /// <summary>
    /// シェルイメージ取得オプションを定義します。
    /// </summary>
    public enum ShellItemImageFormatOptions
    {
        Default,

        /// <summary>
        /// サムネイルのみ。
        /// </summary>
        ThumbnailOnly = SIIGBF.SIIGBF_THUMBNAILONLY,

        /// <summary>
        /// アイコンのみ。
        /// </summary>
        IconOnly = SIIGBF.SIIGBF_ICONONLY,
    }
}