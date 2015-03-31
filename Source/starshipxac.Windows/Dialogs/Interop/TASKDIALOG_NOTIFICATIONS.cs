using System;

namespace starshipxac.Windows.Dialogs.Interop
{
    internal enum TASKDIALOG_NOTIFICATIONS : uint
    {
        /// <summary>
        /// �_�C�A���O�쐬�C�x���g
        /// </summary>
        TDN_CREATED = 0,

        /// <summary>
        /// �i�r�Q�[�V�����C�x���g
        /// </summary>
        TDN_NAVIGATED = 1,

        /// <summary>
        /// �{�^���N���b�N�C�x���g
        /// </summary>
        /// <remarks>
        /// <c>wParam</c> = ButtonId
        /// </remarks>
        TDN_BUTTON_CLICKED = 2,

        /// <summary>
        /// �n�C�p�[�����N�N���b�N�C�x���g
        /// </summary>
        /// <remarks>
        /// <c>lParam</c> = (LPCWSTR)pszHREF
        /// </remarks>
        TDN_HYPERLINK_CLICKED = 3,

        /// <summary>
        /// �^�C�}�[�C�x���g
        /// </summary>
        /// <remarks>
        /// <c>wParam</c> = �~���b
        /// </remarks>
        TDN_TIMER = 4,

        /// <summary>
        /// �^�X�N�_�C�A���O�I���C�x���g
        /// </summary>
        TDN_DESTROYED = 5,

        /// <summary>
        /// ���W�I�{�^���N���b�N�C�x���g
        /// </summary>
        /// <remarks>
        /// <c>wParam</c> = RadioButtoId
        /// </remarks>
        TDN_RADIO_BUTTON_CLICKED = 6,

        /// <summary>
        /// �^�X�N�_�C�A���O�쐬�C�x���g(�\���O)
        /// </summary>
        TDN_DIALOG_CONSTRUCTED = 7,

        /// <summary>
        /// �m�F�`�F�b�N�{�b�N�X�N���b�N�C�x���g
        /// </summary>
        /// <remarks>
        /// <c>wParam</c>: �`�F�b�N����Ă���ꍇ: = 1  ����Ă��Ȃ��ꍇ: = 0
        /// </remarks>
        TDN_VERIFICATION_CLICKED = 8,

        /// <summary>
        /// <c>F1</c>�L�[�����C�x���g
        /// </summary>
        TDN_HELP = 9,

        /// <summary>
        /// �g���{�^���N���b�N�C�x���g
        /// </summary>
        /// <remarks>
        /// <c>wParam</c>: �܂肽���܂�Ă�����: = 0, �g������Ă���ꍇ: != 0
        /// </remarks>
        TDN_EXPANDO_BUTTON_CLICKED = 10
    }
}