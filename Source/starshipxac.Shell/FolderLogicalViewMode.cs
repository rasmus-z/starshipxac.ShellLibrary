using System;
using starshipxac.Shell.Interop;

namespace starshipxac.Shell
{
	/// <summary>
	/// �r���[���[�h���`���܂��B
	/// </summary>
	public enum FolderLogicalViewMode
	{
		/// <summary>
		/// �r���[�͎w�肳��Ă��܂���B
		/// </summary>
		Unspecified = FOLDERLOGICALVIEWMODE.FLVM_UNSPECIFIED,

		/// <summary>
		/// This should have the same affect as Unspecified.
		/// </summary>
		None = 0,

		/// <summary>
		/// �ŏ��l�B���؂̖ړI�ł̂ݎg�p����܂��B
		/// </summary>
		First = FOLDERLOGICALVIEWMODE.FLVM_FIRST,

		/// <summary>
		/// �K��̃r���[�B
		/// </summary>
		Details = FOLDERLOGICALVIEWMODE.FLVM_DETAILS,

		/// <summary>
		/// �^�C���\���B
		/// </summary>
		Tiles = FOLDERLOGICALVIEWMODE.FLVM_TILES,

		/// <summary>
		/// �A�C�R���\���B
		/// </summary>
		Icons = FOLDERLOGICALVIEWMODE.FLVM_ICONS,

		/// <summary>
		/// ���X�g�\���B
		/// </summary>
		List = FOLDERLOGICALVIEWMODE.FLVM_LIST,

		/// <summary>
		/// �R���e���c�\���B
		/// </summary>
		Content = FOLDERLOGICALVIEWMODE.FLVM_CONTENT,

		/// <summary>
		/// �ő�l�B���؂̖ړI�ł̂ݎg�p����܂��B
		/// </summary>
		Last = FOLDERLOGICALVIEWMODE.FLVM_LAST,
	}
}