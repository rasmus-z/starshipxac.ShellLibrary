using System;
using System.Diagnostics.CodeAnalysis;
using starshipxac.Shell.PropertySystem.Interop;

namespace starshipxac.Shell.PropertySystem
{
    /// <summary>
    ///     プロパティ文字列の書式を定義します。
    /// </summary>
    [Flags]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum PropertyDescriptionFormatFlags
    {
        /// <summary>
        ///     プロパティの<c>.propdesc</c>ファイルで指定された書式設定を使用します。
        /// </summary>
        Default = PROPDESC_FORMAT_FLAGS.PDFF_DEFAULT,

        /// <summary>
        ///     プロパティ表示名を値の前に付けます。
        /// </summary>
        /// <remarks>
        ///     プロパティの<c>.propinfo</c>ファイル内の<c>labelInfo</c>要素の<c>hideLabelPrefix</c>属性が
        ///     <c>true</c>に設定されている場合、このフラグは無視されます。
        /// </remarks>
        PrefixName = PROPDESC_FORMAT_FLAGS.PDFF_PREFIXNAME,

        /// <summary>
        ///     ファイル名として文字列を作成します。
        /// </summary>
        FileName = PROPDESC_FORMAT_FLAGS.PDFF_FILENAME,

        /// <summary>
        ///     バイトサイズを常にキロバイトとします。
        /// </summary>
        AlwaysKB = PROPDESC_FORMAT_FLAGS.PDFF_ALWAYSKB,

        /// <summary>
        ///     予約されています。
        /// </summary>
        RightToLeft = PROPDESC_FORMAT_FLAGS.PDFF_RESERVED_RIGHTTOLEFT,

        /// <summary>
        ///     短い形式の時刻文字列を作成します。
        ///     <para>
        ///         例: <c>'hh:mm am/pm'</c>
        ///     </para>
        /// </summary>
        ShortTime = PROPDESC_FORMAT_FLAGS.PDFF_SHORTTIME,

        /// <summary>
        ///     長い形式の時刻文字列を作成します。
        ///     <para>
        ///         例: <c>'hh:mm:ss am/pm'</c>
        ///     </para>
        /// </summary>
        LongTime = PROPDESC_FORMAT_FLAGS.PDFF_LONGTIME,

        /// <summary>
        ///     日時の時刻部分を非表示にします。
        /// </summary>
        HideTime = PROPDESC_FORMAT_FLAGS.PDFF_HIDETIME,

        /// <summary>
        ///     短い形式の日付文字列を作成します。
        ///     <para>
        ///         例: <c>'3/21/04'</c>
        ///     </para>
        /// </summary>
        ShortDate = PROPDESC_FORMAT_FLAGS.PDFF_SHORTDATE,

        /// <summary>
        ///     長い形式の日付文字列を作成します。
        ///     <para>
        ///         例: <c>'Monday, March 21, 2004'</c>
        ///     </para>
        /// </summary>
        LongDate = PROPDESC_FORMAT_FLAGS.PDFF_LONGDATE,

        /// <summary>
        ///     日時の日付部分を非表示にします。
        /// </summary>
        HideDate = PROPDESC_FORMAT_FLAGS.PDFF_HIDEDATE,

        /// <summary>
        ///     "Yesterday"のような日付を作成します。
        /// </summary>
        RelativeDate = PROPDESC_FORMAT_FLAGS.PDFF_RELATIVEDATE,

        /// <summary>
        ///     「あなたの名前を入力してください」などのユーザーのための手がかりとして、テキストボックスに表示される文字列。
        /// </summary>
        UseEditInvitation = PROPDESC_FORMAT_FLAGS.PDFF_USEEDITINVITATION,

        /// <summary>
        ///     このフラグを使用する場合は、<see cref="UseEditInvitation" />フラグも指定する必要があります。
        ///     書式設定フラグが<see cref="ReadOnly" />の場合、<see cref="UseEditInvitation" />は、「不明」である文字列を返します。
        /// </summary>
        ReadOnly = PROPDESC_FORMAT_FLAGS.PDFF_READONLY,

        /// <summary>
        ///     自動的に読み順を検出しません。
        /// </summary>
        NoAutoReadingOrder = PROPDESC_FORMAT_FLAGS.PDFF_NOAUTOREADINGORDER,
    }
}