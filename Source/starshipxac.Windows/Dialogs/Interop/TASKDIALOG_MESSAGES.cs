using System;
using starshipxac.Windows.Interop;

namespace starshipxac.Windows.Dialogs.Interop
{
    internal enum TASKDIALOG_MESSAGES : uint
    {
        TDM_NAVIGATE_PAGE = WindowMessages.WM_USER + 101,

        /// <summary>
        /// �{�^���N���b�N
        /// </summary>
        /// <remarks>
        /// <c>wParam</c> = �{�^��ID
        /// </remarks>
        TDM_CLICK_BUTTON = WindowMessages.WM_USER + 102,

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// <c>wParam</c> = 0(nonMarque), != 0(Marquee)
        /// </remarks>
        TDM_SET_MARQUEE_PROGRESS_BAR = WindowMessages.WM_USER + 103,

        /// <summary>
        /// �v���O���X�o�[���
        /// </summary>
        /// <remarks>
        /// <c>wParam</c> = �v���O���X�o�[���
        /// </remarks>
        TDM_SET_PROGRESS_BAR_STATE = WindowMessages.WM_USER + 104,

        /// <summary>
        /// �v���O���X�o�[�͈�
        /// </summary>
        /// <remarks>
        /// <c>lParam</c> = MAKELPARAM(nMinRange, nMaxRange)
        /// </remarks>
        TDM_SET_PROGRESS_BAR_RANGE = WindowMessages.WM_USER + 105,

        /// <summary>
        /// �v���O���X�o�[�ʒu
        /// </summary>
        /// <remarks>
        /// <c>wParam</c> = �V�����ʒu
        /// </remarks>
        TDM_SET_PROGRESS_BAR_POS = WindowMessages.WM_USER + 106,

        /// <summary>
        /// �}�[�L�[�ݒ�
        /// </summary>
        /// <remarks>
        /// <para>
        /// <c>wParam</c> = 0(��~), != 0(�J�n)
        /// </para>
        /// <para>
        /// <c>lParam</c> = �~���s�P�ʂ̃X�s�[�h
        /// </para>
        /// </remarks>
        TDM_SET_PROGRESS_BAR_MARQUEE = WindowMessages.WM_USER + 107,

        /// <summary>
        /// �G�������g�e�L�X�g
        /// </summary>
        /// <remarks>
        /// <para>
        /// <c>wParam</c> = TASKDIALOG_ELEMENTS
        /// </para>
        /// <para>
        /// <c>lParam</c> = �V�����e�L�X�g
        /// </para>
        /// </remarks>
        TDM_SET_ELEMENT_TEXT = WindowMessages.WM_USER + 108,

        /// <summary>
        /// ���W�I�{�^���N���b�N
        /// </summary>
        /// <remarks>
        /// <c>wParam</c> = ���W�I�{�^��ID
        /// </remarks>
        TDM_CLICK_RADIO_BUTTON = WindowMessages.WM_USER + 110,

        /// <summary>
        /// �{�^���L��/�����ݒ�
        /// </summary>
        /// <remarks>
        /// <para>
        /// <c>wParam</c> = �{�^��ID
        /// </para>
        /// <para>
        /// <c>lParam</c> = 0(����), != 0(�L��)
        /// </para>
        /// </remarks>
        TDM_ENABLE_BUTTON = WindowMessages.WM_USER + 111,

        /// <summary>
        /// ���W�I�{�^���L��/�����ݒ�
        /// </summary>
        /// <remarks>
        /// <para>
        /// <c>wParam</c> = ���W�I�{�^��ID
        /// </para>
        /// <para>
        /// <c>lParam</c> = 0(����), != 0(�L��)
        /// </para>
        /// </remarks>
        TDM_ENABLE_RADIO_BUTTON = WindowMessages.WM_USER + 112,

        /// <summary>
        /// �m�F�`�F�b�N�{�b�N�X�N���b�N
        /// </summary>
        /// <remarks>
        /// <para>
        /// <c>wParam</c> = 0(�`�F�b�N���Ȃ�), 1(�`�F�b�N)
        /// </para>
        /// <para>
        /// <c>lParam</c> = 1(�t�H�[�J�X�ݒ�)
        /// </para>
        /// </remarks>
        TDM_CLICK_VERIFICATION = WindowMessages.WM_USER + 113,

        /// <summary>
        /// �G�������g�e�L�X�g�X�V
        /// </summary>
        /// <remarks>
        /// <para>
        /// <c>wParam</c> = TASKDIALOG_ELEMENTS
        /// </para>
        /// <para>
        /// <c>lParam</c> = �V�����G�������g�e�L�X�g
        /// </para>
        /// </remarks>
        TDM_UPDATE_ELEMENT_TEXT = WindowMessages.WM_USER + 114,

        /// <summary>
        /// ���i�{�^���v����Ԑݒ�
        /// </summary>
        /// <remarks>
        /// <para>
        /// <c>wParam</c> = �{�^��ID
        /// </para>
        /// <para>
        /// <c>lParam</c> = 0(�v�����Ȃ�), 1(�v��)
        /// </para>
        /// </remarks>
        TDM_SET_BUTTON_ELEVATION_REQUIRED_STATE = WindowMessages.WM_USER + 115,

        /// <summary>
        /// �A�C�R���X�V
        /// </summary>
        /// <remarks>
        /// <para>
        /// <c>wParam</c> = TASKDIALOG_ICON_ELEMENTS
        /// </para>
        /// <para>
        /// <c>lParam</c> = �V�����A�C�R��(hIcon if TDF_USE_HICON_* was set, PCWSTR otherwise)
        /// </para>
        /// </remarks>
        TDM_UPDATE_ICON = WindowMessages.WM_USER + 116
    }
}