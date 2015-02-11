using System;
using System.Diagnostics.Contracts;
using System.Windows;
using starshipxac.Shell;

namespace starshipxac.Windows.Shell.Dialogs
{
	/// <summary>
	/// �ۑ�����t�@�C����I������_�C�A���O��\�����܂��B
	/// </summary>
	public class FileSaveDialog : FileSaveDialogBase
	{
		private bool overwritePrompt = true;
		private bool createPrompt = false;
		private bool isExpandedMode = false;

		private ShellFile shellFile;

		/// <summary>
		/// <see cref="FileSaveDialog"/>�N���X�̐V�����C���X�^���X�����������܂��B
		/// </summary>
		public FileSaveDialog()
		{
		}

		/// <summary>
		/// �_�C�A���O�̃^�C�g�����w�肵�āA
		/// <see cref="FileSaveDialog"/>�N���X�̐V�����C���X�^���X�����������܂��B
		/// </summary>
		/// <param name="title">�_�C�A���O�^�C�g���B</param>
		public FileSaveDialog(string title)
			: base(title)
		{
		}

		/// <summary>
		/// ���[�U�[���A���łɑ��݂���t�@�C�����w�肵���ꍇ�ɁA
		/// �x�����b�Z�[�W��\�����邩�ǂ����������l���擾�܂��͐ݒ肵�܂��B
		/// </summary>
		public bool OverwritePrompt
		{
			get
			{
				return this.overwritePrompt;
			}
			set
			{
				ThrowIfDialogShowingPropertyCannotBeChanged();
				this.overwritePrompt = value;
			}
		}

		/// <summary>
		/// ���[�U�[���A���݂��Ȃ��t�@�C�����w�肵���ꍇ�ɁA
		/// �t�@�C�����쐬���邱�Ƃ��m�F���郁�b�Z�[�W��\�����邩�ǂ����������l���擾�܂��͐ݒ肵�܂��B
		/// </summary>
		public bool CreatePrompt
		{
			get
			{
				return this.createPrompt;
			}
			set
			{
				ThrowIfDialogShowingPropertyCannotBeChanged();
				this.createPrompt = value;
			}
		}

		/// <summary>
		/// �_�C�A���O���g�����[�h�ŕ\�����邩�ǂ����������l���擾�܂��͐ݒ肵�܂��B
		/// </summary>
		public bool IsExpandedMode
		{
			get
			{
				return this.isExpandedMode;
			}
			set
			{
				ThrowIfDialogShowingPropertyCannotBeChanged();
				this.isExpandedMode = value;
			}
		}

		/// <summary>
		/// �I�������t�@�C�����擾���܂��B
		/// </summary>
		public ShellFile ShellFile
		{
			get
			{
				if (this.shellFile == null)
				{
					this.shellFile = GetShellFile();
				}
				return this.shellFile;
			}
		}

		/// <summary>
		/// �t�@�C���ۑ��_�C�A���O��\�����܂��B
		/// </summary>
		/// <returns>�_�C�A���O���s���ʁB</returns>
		public FileDialogResult Show()
		{
			return ShowDialog();
		}

		/// <summary>
		/// �t�@�C���ۑ��_�C�A���O��\�����܂��B
		/// </summary>
		/// <param name="parentWindow">�e�E�B���h�E�̃n���h���B</param>
		/// <returns>�_�C�A���O���s���ʁB</returns>
		public FileDialogResult Show(Window parentWindow)
		{
			Contract.Requires<ArgumentNullException>(parentWindow != null);

			return ShowDialog(parentWindow);
		}

		protected override FileDialogOptions GetDialogOptions()
		{
			var result = base.GetDialogOptions();

			if (this.OverwritePrompt)
			{
				result |= FileDialogOptions.OverwritePrompt;
			}
			if (this.CreatePrompt)
			{
				result |= FileDialogOptions.CreatePrompt;
			}
			if (!this.IsExpandedMode)
			{
				result |= FileDialogOptions.ExpandMode;
			}

			return result;
		}
	}
}