using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Windows;
using starshipxac.Shell;

namespace starshipxac.Windows.Shell.Dialogs
{
	/// <summary>
	/// �t�H���_�[�I������_�C�A���O��\�����܂��B
	/// </summary>
	public sealed class FolderSelectDialog : FolderSelectDialogBase
	{
		private IEnumerable<ShellFolder> shellFolders;

		/// <summary>
		/// <see cref="FolderSelectDialog"/>�N���X�����������܂��B
		/// </summary>
		static FolderSelectDialog()
		{
			EmptyShellFolders = new List<ShellFolder>();
		}

		/// <summary>
		/// <see cref="FolderSelectDialog"/>�N���X�̐V�����C���X�^���X�����������܂��B
		/// </summary>
		public FolderSelectDialog()
		{
			this.Multiselect = false;
		}

		/// <summary>
		/// �_�C�A���O�̃^�C�g�����w�肵�āA
		/// <see cref="FolderSelectDialog"/>�N���X�̐V�����C���X�^���X�����������܂��B
		/// </summary>
		/// <param name="title">�_�C�A���O�^�C�g���B</param>
		public FolderSelectDialog(string title)
			: base(title)
		{
		}

		/// <summary>
		/// �t�H���_�[�𕡐��I���\���ǂ����������l���擾�܂��͐ݒ肵�܂��B
		/// </summary>
		public bool Multiselect { get; set; }

		/// <summary>
		/// �I�������t�H���_�[�̃R���N�V�������擾���܂��B
		/// </summary>
		public IEnumerable<ShellFolder> ShellFolders
		{
			get
			{
				if (this.shellFolders == null)
				{
					this.shellFolders = GetShellFolders();
				}
				return this.shellFolders;
			}
		}

		/// <summary>
		/// ��̃t�H���_�[�R���N�V�������擾�܂��͐ݒ肵�܂��B
		/// </summary>
		private static IEnumerable<ShellFolder> EmptyShellFolders { get; set; }

		/// <summary>
		/// �t�H���_�[�I���_�C�A���O��\�����܂��B
		/// </summary>
		/// <returns>�_�C�A���O���s���ʁB</returns>
		public FileDialogResult Show()
		{
			return ShowDialog();
		}

		/// <summary>
		/// �e�E�B���h�E���w�肵�āA�t�H���_�[�I���_�C�A���O��\�����܂��B
		/// </summary>
		/// <param name="parentWindow">�e�E�B���h�E�B</param>
		/// <returns>�_�C�A���O���s���ʁB</returns>
		public FileDialogResult Show(Window parentWindow)
		{
			Contract.Requires<ArgumentNullException>(parentWindow != null);

			return ShowDialog(parentWindow);
		}

		protected override FileDialogOptions GetDialogOptions()
		{
			var result = base.GetDialogOptions();

			if (this.Multiselect)
			{
				result |= FileDialogOptions.MultiSelect;
			}

			return result;
		}
	}
}