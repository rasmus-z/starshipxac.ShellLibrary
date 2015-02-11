using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using starshipxac.Shell;

namespace starshipxac.Windows.Shell.Dialogs
{
	/// <summary>
	/// �t�H���_�[��I������_�C�A���O��\�����܂��B
	/// </summary>
	public sealed class FolderSelector : FolderSelectDialogBase
	{
		private bool multiSelect = false;

		/// <summary>
		/// <see cref="FolderSelector"/>�N���X�����������܂��B
		/// </summary>
		static FolderSelector()
		{
			EmptyShellFolders = new List<ShellFolder>();
		}

		/// <summary>
		/// <see cref="FolderSelector"/>�N���X�̐V�����C���X�^���X�����������܂��B
		/// </summary>
		public FolderSelector()
		{
		}

		/// <summary>
		/// �_�C�A���O�̃^�C�g�����w�肵�āA
		/// <see cref="FolderSelector"/>�N���X�̐V�����C���X�^���X�����������܂��B
		/// </summary>
		/// <param name="title">�_�C�A���O�̃^�C�g���B</param>
		public FolderSelector(string title)
			: base(title)
		{
		}

		/// <summary>
		/// ��̃t�H���_�R���N�V�������擾�܂��͐ݒ肵�܂��B
		/// </summary>
		private static IEnumerable<ShellFolder> EmptyShellFolders { get; set; }

		/// <summary>
		/// 1�̃t�H���_��I������_�C�A���O��\�����܂��B
		/// </summary>
		/// <returns>�I�������t�H���_�B���[�U�[���L�����Z�������ꍇ��<c>null</c>�B</returns>
		public async Task<ShellFolder> SelectSingleFolderAsync()
		{
			ShellFolder result = null;

			this.multiSelect = false;

			await InvokeAsync(() =>
			{
				var dialogResult = ShowDialog();
				if (dialogResult == FileDialogResult.Ok)
				{
					result = GetShellFolders().FirstOrDefault();
				}
			});

			return result;
		}

		/// <summary>
		/// �����̃t�H���_��I������_�C�A���O��\�����܂��B
		/// </summary>
		/// <returns>�I�������t�H���_�̃R���N�V�����B</returns>
		public async Task<IEnumerable<ShellFolder>> SelectMultipleFoldersAsync()
		{
			Contract.Ensures(Contract.Result<IEnumerable<ShellFolder>>() != null);

			var result = EmptyShellFolders;

			this.multiSelect = true;

			await InvokeAsync(() =>
			{
				var dialogResult = ShowDialog();
				if (dialogResult == FileDialogResult.Ok)
				{
					result = GetShellFolders();
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