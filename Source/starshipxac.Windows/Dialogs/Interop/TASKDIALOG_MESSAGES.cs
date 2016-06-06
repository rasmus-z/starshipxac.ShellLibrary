using System;
using System.Diagnostics.CodeAnalysis;
using starshipxac.Windows.Interop;

namespace starshipxac.Windows.Dialogs.Interop
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal enum TASKDIALOG_MESSAGES : uint
    {
        TDM_NAVIGATE_PAGE = WindowMessages.WM_USER + 101,

        /// <summary>
        ///     ボタンクリック
        /// </summary>
        /// <remarks>
        ///     <c>wParam</c> = ボタンID
        /// </remarks>
        TDM_CLICK_BUTTON = WindowMessages.WM_USER + 102,

        /// <summary>
        /// </summary>
        /// <remarks>
        ///     <c>wParam</c> = 0(nonMarque), != 0(Marquee)
        /// </remarks>
        TDM_SET_MARQUEE_PROGRESS_BAR = WindowMessages.WM_USER + 103,

        /// <summary>
        ///     プログレスバー状態
        /// </summary>
        /// <remarks>
        ///     <c>wParam</c> = プログレスバー状態
        /// </remarks>
        TDM_SET_PROGRESS_BAR_STATE = WindowMessages.WM_USER + 104,

        /// <summary>
        ///     プログレスバー範囲
        /// </summary>
        /// <remarks>
        ///     <c>lParam</c> = MAKELPARAM(nMinRange, nMaxRange)
        /// </remarks>
        TDM_SET_PROGRESS_BAR_RANGE = WindowMessages.WM_USER + 105,

        /// <summary>
        ///     プログレスバー位置
        /// </summary>
        /// <remarks>
        ///     <c>wParam</c> = 新しい位置
        /// </remarks>
        TDM_SET_PROGRESS_BAR_POS = WindowMessages.WM_USER + 106,

        /// <summary>
        ///     マーキー設定
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         <c>wParam</c> = 0(停止), != 0(開始)
        ///     </para>
        ///     <para>
        ///         <c>lParam</c> = ミリ行単位のスピード
        ///     </para>
        /// </remarks>
        TDM_SET_PROGRESS_BAR_MARQUEE = WindowMessages.WM_USER + 107,

        /// <summary>
        ///     エレメントテキスト
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         <c>wParam</c> = TASKDIALOG_ELEMENTS
        ///     </para>
        ///     <para>
        ///         <c>lParam</c> = 新しいテキスト
        ///     </para>
        /// </remarks>
        TDM_SET_ELEMENT_TEXT = WindowMessages.WM_USER + 108,

        /// <summary>
        ///     ラジオボタンクリック
        /// </summary>
        /// <remarks>
        ///     <c>wParam</c> = ラジオボタンID
        /// </remarks>
        TDM_CLICK_RADIO_BUTTON = WindowMessages.WM_USER + 110,

        /// <summary>
        ///     ボタン有効/無効設定
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         <c>wParam</c> = ボタンID
        ///     </para>
        ///     <para>
        ///         <c>lParam</c> = 0(無効), != 0(有効)
        ///     </para>
        /// </remarks>
        TDM_ENABLE_BUTTON = WindowMessages.WM_USER + 111,

        /// <summary>
        ///     ラジオボタン有効/無効設定
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         <c>wParam</c> = ラジオボタンID
        ///     </para>
        ///     <para>
        ///         <c>lParam</c> = 0(無効), != 0(有効)
        ///     </para>
        /// </remarks>
        TDM_ENABLE_RADIO_BUTTON = WindowMessages.WM_USER + 112,

        /// <summary>
        ///     確認チェックボックスクリック
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         <c>wParam</c> = 0(チェックしない), 1(チェック)
        ///     </para>
        ///     <para>
        ///         <c>lParam</c> = 1(フォーカス設定)
        ///     </para>
        /// </remarks>
        TDM_CLICK_VERIFICATION = WindowMessages.WM_USER + 113,

        /// <summary>
        ///     エレメントテキスト更新
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         <c>wParam</c> = TASKDIALOG_ELEMENTS
        ///     </para>
        ///     <para>
        ///         <c>lParam</c> = 新しいエレメントテキスト
        ///     </para>
        /// </remarks>
        TDM_UPDATE_ELEMENT_TEXT = WindowMessages.WM_USER + 114,

        /// <summary>
        ///     昇格ボタン要求状態設定
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         <c>wParam</c> = ボタンID
        ///     </para>
        ///     <para>
        ///         <c>lParam</c> = 0(要求しない), 1(要求)
        ///     </para>
        /// </remarks>
        TDM_SET_BUTTON_ELEVATION_REQUIRED_STATE = WindowMessages.WM_USER + 115,

        /// <summary>
        ///     アイコン更新
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         <c>wParam</c> = TASKDIALOG_ICON_ELEMENTS
        ///     </para>
        ///     <para>
        ///         <c>lParam</c> = 新しいアイコン(hIcon if TDF_USE_HICON_* was set, PCWSTR otherwise)
        ///     </para>
        /// </remarks>
        TDM_UPDATE_ICON = WindowMessages.WM_USER + 116
    }
}