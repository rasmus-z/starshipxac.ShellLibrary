using System;
using System.ComponentModel;
using starshipxac.Shell;

namespace starshipxac.Windows.Shell.Dialogs
{
	/// <summary>
	/// �t�H���_�ύX�C�x���g�f�[�^���`���܂��B
	/// </summary>
	public class FileDialogFolderChangeEventArgs : CancelEventArgs
	{
		/// <summary>
		/// <see cref="FileDialogFolderChangeEventArgs"/>�N���X�̐V�����C���X�^���X�����������܂��B
		/// </summary>
		/// <param name="folder">�t�H���_�[���B</param>
		public FileDialogFolderChangeEventArgs(ShellFolder folder)
		{
			this.ShellFolder = folder;
		}

		/// <summary>
		/// �t�H���_�[�����擾���܂��B
		/// </summary>
		public ShellFolder ShellFolder { get; private set; }
	}
}