using System;

namespace starshipxac.Shell.Interop.KnownFolder
{
	/// <summary>
	/// �W���t�H���_�[�J�e�S���[���`���܂��B
	/// </summary>
	/// <remarks>
	/// http://msdn.microsoft.com/en-us/library/windows/desktop/bb762512(v=vs.85).aspx
	/// </remarks>
	public enum KF_CATEGORY
	{
		/// <summary>
		/// ���z�t�H���_�[�B
		/// </summary>
		/// <remarks>
		/// ��: <c>Control Panel</c>, <c>Printers</c>
		/// </remarks>
		KF_CATEGORY_VIRTUAL = 1,

		/// <summary>
		/// �t�@�C���V�X�e����̌Œ�t�H���_�[�B
		/// </summary>
		/// <remarks>
		/// ��: <c>Windows</c>, <c>Program Files</c>
		/// </remarks>
		KF_CATEGORY_FIXED = 2,

		/// <summary>
		/// �t�@�C���V�X�e����̋��ʃt�H���_�[�B
		/// </summary>
		/// <remarks>
		/// ��: <c>Documents</c>
		/// </remarks>
		KF_CATEGORY_COMMON = 3,

		/// <summary>
		/// ���[�U�[���Ƃ̃t�H���_�[�B
		/// </summary>
		/// <remarks>
		/// ��: <c>%USERPROFILE%\Prictures</c>
		/// </remarks>
		KF_CATEGORY_PERUSER = 4,
	}
}