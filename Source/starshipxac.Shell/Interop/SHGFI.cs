using System;
using System.Diagnostics.CodeAnalysis;

namespace starshipxac.Shell.Interop
{
    /// <summary>
    ///     ファイル情報取得フラグを定義します。
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/bb762179(v=vs.85).aspx
    /// </remarks>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal static class SHGFI
    {
        /// <summary>
        ///     アイコン取得。
        /// </summary>
        public const UInt32 SHGFI_ICON = 0x000000100;

        /// <summary>
        ///     表示名取得。
        /// </summary>
        public const UInt32 SHGFI_DISPLAYNAME = 0x000000200;

        /// <summary>
        ///     ファイルタイプ名取得。
        /// </summary>
        public const UInt32 SHGFI_TYPENAME = 0x000000400;

        /// <summary>
        ///     ファイル属性取得。
        /// </summary>
        public const UInt32 SHGFI_ATTRIBUTES = 0x000000800;

        /// <summary>
        ///     アイコン位置取得。
        /// </summary>
        public const UInt32 SHGFI_ICONLOCATION = 0x000001000;

        /// <summary>
        ///     実行ファイル種別取得。
        /// </summary>
        public const UInt32 SHGFI_EXETYPE = 0x000002000;

        /// <summary>
        ///     システムアイコンインデックス取得。
        /// </summary>
        public const UInt32 SHGFI_SYSICONINDEX = 0x000004000;

        /// <summary>
        ///     リンクオーバーレイ取得。
        /// </summary>
        public const UInt32 SHGFI_LINKOVERLAY = 0x000008000;

        /// <summary>
        ///     選択状態アイコン取得。
        /// </summary>
        public const UInt32 SHGFI_SELECTED = 0x000010000;

        /// <summary>
        /// </summary>
        public const UInt32 SHGFI_ATTR_SPECIFIED = 0x000020000;

        /// <summary>
        ///     大きいアイコン取得。
        /// </summary>
        public const UInt32 SHGFI_LARGEICON = 0x000000000;

        /// <summary>
        ///     小さいアイコン取得。
        /// </summary>
        public const UInt32 SHGFI_SMALLICON = 0x000000001;

        /// <summary>
        ///     開いた状態のアイコン取得。
        /// </summary>
        public const UInt32 SHGFI_OPENICON = 0x000000002;

        /// <summary>
        ///     シェルアイコンサイズ取得。
        /// </summary>
        public const UInt32 SHGFI_SHELLICONSIZE = 0x000000004;

        /// <summary>
        ///     PIDL指定。
        /// </summary>
        public const UInt32 SHGFI_PIDL = 0x000000008;

        /// <summary>
        ///     ファイル属性使用。
        /// </summary>
        public const UInt32 SHGFI_USEFILEATTRIBUTES = 0x000000010;

        /// <summary>
        ///     オーバーレイアイコン追加。
        /// </summary>
        public const UInt32 SHGFI_ADDOVERLAYS = 0x000000020;

        /// <summary>
        ///     オーバーレイインデックス。
        /// </summary>
        public const UInt32 SHGFI_OVERLAYINDEX = 0x000000040;
    }
}