using System;
using starshipxac.Shell.Interop;

namespace starshipxac.Shell.Media.Imaging
{
	/// <summary>
	/// �V�F���C���[�W�擾�I�v�V�������`���܂��B
	/// </summary>
	public enum ShellItemImageFormatOptions
	{
		Default,

		/// <summary>
		/// �T���l�C���̂݁B
		/// </summary>
		ThumbnailOnly = SIIGBF.SIIGBF_THUMBNAILONLY,

		/// <summary>
		/// �A�C�R���̂݁B
		/// </summary>
		IconOnly = SIIGBF.SIIGBF_ICONONLY,
	}
}