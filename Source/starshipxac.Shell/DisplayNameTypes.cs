using System;
using starshipxac.Shell.Interop;

namespace starshipxac.Shell
{
	/// <summary>
	/// <see cref="ShellObject"/>�̕\������ʂ��`���܂��B
	/// </summary>
	public enum DisplayNameTypes
	{
		/// <summary>
		/// �f�X�N�g�b�v����̑��ΓI�ȕ\�����B
		/// </summary>
		Default = SIGDN.SIGDN_NORMALDISPLAY,

		/// <summary>
		/// �e�t�H���_����̑��ΓI�ȕ\�����B
		/// </summary>
		RelativeToParent = SIGDN.SIGDN_PARENTRELATIVEPARSING,

		/// <summary>
		/// �A�h���X�o�[�ɕ\������e�t�H���_�[����̑��ΓI�ȕ\�����B
		/// </summary>
		RelativeToParentAddressBar = SIGDN.SIGDN_PARENTRELATIVEFORADDRESSBAR,

		/// <summary>
		/// �f�X�N�g�b�v����̑��ΓI�ȃp�X���B
		/// </summary>
		RelativeToDesktop = SIGDN.SIGDN_DESKTOPABSOLUTEPARSING,

		/// <summary>
		/// �e�t�H���_����̑��ΓI�ȕҏW�\�Ȗ��O�B
		/// </summary>
		RelativeToParentEditing = SIGDN.SIGDN_PARENTRELATIVEEDITING,

		/// <summary>
		/// �f�X�N�g�b�v����̑��ΓI�ȕҏW�\�Ȗ��O�B
		/// </summary>
		RelativeToDesktopEditing = SIGDN.SIGDN_DESKTOPABSOLUTEEDITING,

		/// <summary>
		/// 
		/// </summary>
		RelativeForUI = SIGDN.SIGDN_PARENTRELATIVEFORUI,

		/// <summary>
		/// �t�@�C���V�X�e����̃p�X���B
		/// </summary>
		FileSystemPath = SIGDN.SIGDN_FILESYSPATH,

		/// <summary>
		/// ���ΓI��URL�B
		/// </summary>
		Url = SIGDN.SIGDN_URL,
	}
}