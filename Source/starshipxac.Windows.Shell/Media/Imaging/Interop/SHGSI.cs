using System;
using starshipxac.Shell.Interop;

// ReSharper disable InconsistentNaming

namespace starshipxac.Windows.Shell.Media.Imaging.Interop
{
    /// <summary>
    ///     標準アイコン取得フラグを定義します。
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         http://msdn.microsoft.com/en-us/library/windows/desktop/bb762542(v=vs.85).aspx
    ///     </para>
    /// </remarks>
    internal static class SHGSI
    {
        /// <summary>
        ///     アイコンのパスとインデックス取得(常に有効)。
        /// </summary>
        public const UInt32 SHGSI_ICONLOCATION = 0;

        /// <summary>
        ///     アイコン取得。
        /// </summary>
        public const UInt32 SHGSI_ICON = SHGFI.SHGFI_ICON;

        /// <summary>
        ///     システムアイコンインデックス取得。
        /// </summary>
        public const UInt32 SHGSI_SYSICONINDEX = SHGFI.SHGFI_SYSICONINDEX;

        /// <summary>
        ///     リンクオーバーレイ取得。
        /// </summary>
        public const UInt32 SHGSI_LINKOVERLAY = SHGFI.SHGFI_LINKOVERLAY;

        /// <summary>
        ///     選択状態アイコン取得。
        /// </summary>
        public const UInt32 SHGSI_SELECTED = SHGFI.SHGFI_SELECTED;

        /// <summary>
        ///     大きいアイコン取得。
        /// </summary>
        public const UInt32 SHGSI_LARGEICON = SHGFI.SHGFI_LARGEICON;

        /// <summary>
        ///     小さいアイコン取得。
        /// </summary>
        public const UInt32 SHGSI_SMALLICON = SHGFI.SHGFI_SMALLICON;

        /// <summary>
        ///     シェルアイコンサイズ取得。
        /// </summary>
        public const UInt32 SHGSI_SHELLICONSIZE = SHGFI.SHGFI_SHELLICONSIZE;
    }
}