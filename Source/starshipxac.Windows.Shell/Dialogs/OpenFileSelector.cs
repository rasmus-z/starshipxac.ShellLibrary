using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using starshipxac.Shell;

namespace starshipxac.Windows.Shell.Dialogs
{
	/// <summary>
	/// �t�@�C���I���_�C�A���O��\�����܂��B
	/// </summary>
	public sealed class OpenFileSelector : FileOpenDialogBase
	{
		private bool multiSelect = false;

		/// <summary>
		/// <see cref="OpenFileSelector"/>�N���X�����������܂��B
		/// </summary>
		static OpenFileSelector()
		{
			EmptyShellFiles = new List<ShellFile>();
		}

		/// <summary>
		/// <see cref="OpenFileSelector"/>�N���X�̐V�����C���X�^���X�����������܂��B
		/// </summary>
		public OpenFileSelector()
		{
		}

		/// <summary>
		/// �_�C�A���O�̃^�C�g�����w�肵�āA
		/// <see cref="OpenFileSelector"/>�N���X�̐V�����C���X�^���X�����������܂��B
		/// </summary>
		/// <param name="title">�_�C�A���O�̃^�C�g���B</param>
		public OpenFileSelector(string title)
			: base(title)
		{
		}

		/// <summary>
		/// �ǂݍ��ݐ�p�t�@�C���̂ݑI���\�ɂ��邩�ǂ����������l���擾�܂��͐ݒ肵�܂��B
		/// </summary>
		public bool EnsureReadOnly { get; set; }

		/// <summary>
		/// �t�@�C���V�X�e���ȊO�̃A�C�e����I���\�ɂ��邩�ǂ����������l���擾�܂��͐ݒ肵�܂��B
		/// </summary>
		public bool AllowNonFileSystemItem { get; set; }

		/// <summary>
		/// ��̃t�@�C���R���N�V�������擾�܂��͐ݒ肵�܂��B
		/// </summary>
		private static IEnumerable<ShellFile> EmptyShellFiles { get; set; }

		private ShellFile SelectSingleFile()
		{
			this.multiSelect = false;
			var dialogResult = ShowDialog();
			if (dialogResult != FileDialogResult.Ok)
			{
				return null;
			}
			return GetShellFiles().FirstOrDefault();
		}

		/// <summary>
		/// 1�̃t�@�C����I���ł���_�C�A���O��\�����܂��B
		/// </summary>
		/// <returns>�I�������t�@�C�����B���[�U�[���L�����Z�������ꍇ��<c>null</c>�B</returns>
		public async Task<ShellFile> SelectSingleFileAsync()
		{
			ShellFile result = null;

			this.multiSelect = false;

			await InvokeAsync(() =>
			{
				var dialogResult = ShowDialog();
				if (dialogResult == FileDialogResult.Ok)
				{
					result = GetShellFiles().FirstOrDefault();
				}
			});

			return result;
		}

		/// <summary>
		/// �����̃t�@�C����I���ł���_�C�A���O��\�����܂��B
		/// </summary>
		/// <returns>�I�������t�@�C�����̃R���N�V�����B</returns>
		public async Task<IEnumerable<ShellFile>> SelectMultipleFilesAsync()
		{
			var result = EmptyShellFiles;

			this.multiSelect = true;

			await InvokeAsync(() =>
			{
				var dialogResult = ShowDialog();
				if (dialogResult == FileDialogResult.Ok)
				{
					result = GetShellFiles();
				}
			});

			return result;
		}

		protected override FileDialogOptions GetDialogOptions()
		{
			var result = base.GetDialogOptions();

			if (this.multiSelect)
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

		private async Task InvokeAsync(Action action)
		{
			if (Application.Current.Dispatcher.CheckAccess())
			{
				action();
			}
			else
			{
				await Application.Current.Dispatcher.InvokeAsync(action);
			}
		}
	}
}