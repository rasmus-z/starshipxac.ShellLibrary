using System;

namespace starshipxac.Windows.Dialogs.Interop
{
    internal enum TASKDIALOG_NOTIFICATIONS : uint
    {
        /// <summary>
        /// ダイアログ作成イベント
        /// </summary>
        TDN_CREATED = 0,

        /// <summary>
        /// ナビゲーションイベント
        /// </summary>
        TDN_NAVIGATED = 1,

        /// <summary>
        /// ボタンクリックイベント
        /// </summary>
        /// <remarks>
        /// <c>wParam</c> = ButtonId
        /// </remarks>
        TDN_BUTTON_CLICKED = 2,

        /// <summary>
        /// ハイパーリンククリックイベント
        /// </summary>
        /// <remarks>
        /// <c>lParam</c> = (LPCWSTR)pszHREF
        /// </remarks>
        TDN_HYPERLINK_CLICKED = 3,

        /// <summary>
        /// タイマーイベント
        /// </summary>
        /// <remarks>
        /// <c>wParam</c> = ミリ秒
        /// </remarks>
        TDN_TIMER = 4,

        /// <summary>
        /// タスクダイアログ終了イベント
        /// </summary>
        TDN_DESTROYED = 5,

        /// <summary>
        /// ラジオボタンクリックイベント
        /// </summary>
        /// <remarks>
        /// <c>wParam</c> = RadioButtoId
        /// </remarks>
        TDN_RADIO_BUTTON_CLICKED = 6,

        /// <summary>
        /// タスクダイアログ作成イベント(表示前)
        /// </summary>
        TDN_DIALOG_CONSTRUCTED = 7,

        /// <summary>
        /// 確認チェックボックスクリックイベント
        /// </summary>
        /// <remarks>
        /// <c>wParam</c>: チェックされている場合: = 1  されていない場合: = 0
        /// </remarks>
        TDN_VERIFICATION_CLICKED = 8,

        /// <summary>
        /// <c>F1</c>キー押下イベント
        /// </summary>
        TDN_HELP = 9,

        /// <summary>
        /// 拡張ボタンクリックイベント
        /// </summary>
        /// <remarks>
        /// <c>wParam</c>: 折りたたまれている状態: = 0, 拡張されている場合: != 0
        /// </remarks>
        TDN_EXPANDO_BUTTON_CLICKED = 10
    }
}