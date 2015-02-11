using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Windows;
using starshipxac.Shell;

namespace starshipxac.Windows.Shell.Dialogs
{
	/// <summary>
	/// �t�@�C����I������_�C�A���O��\�����܂��B
	/// </summary>
	public sealed class FileOpenDialog : FileOpenDialogBase
	{
		private IEnumerable<ShellFile> shellFiles;

		/// <summary>
		/// <see cref="FileOpenDialog"/>�N���X�̐V�����C���X�^���X�����������܂��B
		/// </summary>
		public FileOpenDialog()
		{
			this.MultiSelect = false;
		}

		/// <summary>
		/// �_�C�A���O�̃^�C�g�����w�肵�āA
		/// <see cref="FileOpenDialog"/>�N���X�̐V�����C���X�^���X�����������܂��B
		/// </summary>
		/// <param name="title">�_�C�A���O�^�C�g���B</param>
		public FileOpenDialog(string title)
			: base(title)
		{
		}

		/// <summary>
		/// �t�@�C���𕡐��I���\���ǂ��������l���擾�܂��͐ݒ肵�܂��B
		/// </summary>
		public bool MultiSelect { get; set; }

		/// <summary>
		/// �ǂݍ��ݐ�p�t�@�C���̂ݑI���\�ɂ��邩�ǂ����������l���擾�܂��͐ݒ肵�܂��B
		/// </summary>
		public bool EnsureReadOnly { get; set; }

		/// <summary>
		/// �t�@�C���V�X�e���ȊO�̃A�C�e����I���\�ɂ��邩�ǂ����������l���擾�܂��͐ݒ肵�܂��B
		/// </summary>
		public bool AllowNonFileSystemItem { get; set; }

		/// <summary>
		/// �I�������t�@�C���̃R���N�V�������擾���܂��B
		/// </summary>
		public IEnumerable<ShellFile> ShellFiles
		{
			get
			{
				if (this.shellFiles == null)
				{
					this.shellFiles = GetShellFiles();
				}
				return this.shellFiles;
			}
		}

		/// <summary>
		/// �t�@�C���I���_�C�A���O��\�����܂��B
		/// </summary>
		/// <returns>�_�C�A���O���s���ʁB</returns>
		public FileDialogResult Show()
		{
			return ShowDialog();
		}

		/// <summary>
		/// �t�@�C���I���_�C�A���O��\�����܂��B
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

			if (this.MultiSelect)
			{
				result |= FileDialogOptions.MultiSelect;
			}
			if (this.EnsureReadOnly)
			{
				result |= FileDialogOptions.EnsureReadOnly;
			}
			if (!this.AllowNonFileSystemItem)
			{
				result |= FileDialogOptions.ForceFileSystem;
			}
			else
			{
				result |= FileDialogOptions.AllNonStotageItems;
			}

			return result;
		}
	}
}