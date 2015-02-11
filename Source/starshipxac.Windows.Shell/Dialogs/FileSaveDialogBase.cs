using System;
using System.Diagnostics.Contracts;
using System.Linq;
using starshipxac.Shell;
using starshipxac.Shell.Interop;
using starshipxac.Windows.Shell.Dialogs.Interop;

namespace starshipxac.Windows.Shell.Dialogs
{
	public abstract class FileSaveDialogBase : FileDialogBase
	{
		private bool validateNames = true;
		private bool appendExtension = true;
		private bool restoreDirectory = true;
		private bool addToMruList = true;
		private bool setFilter = false;
		private string defaultFileExtension;

		protected FileSaveDialogBase()
		{
			this.FileTypeFilters = new FileTypeFilterCollection();
		}

		protected FileSaveDialogBase(string title)
			: base(title)
		{
			this.FileTypeFilters = new FileTypeFilterCollection();
		}

		[ContractInvariantMethod]
		private void ObjectInvarinat()
		{
			Contract.Invariant(this.FileTypeFilters != null);
		}

		/// <summary>
		/// �t�@�C���������؂��邩�ǂ����������l���擾�܂��͐ݒ肵�܂��B
		/// </summary>
		/// <exception cref="InvalidOperationException">�_�C�A���O�\�����͕ύX�ł��܂���B</exception>
		public bool ValidateNames
		{
			get
			{
				return this.validateNames;
			}
			set
			{
				ThrowIfDialogShowingPropertyCannotBeChanged();
				this.validateNames = value;
			}
		}

		/// <summary>
		/// ���[�U�[���g���q���w�肵�Ȃ��ꍇ�A�t�@�C�����Ɏ����I�Ɋg���q��t�����邩�ǂ����������l���擾�܂��͐ݒ肵�܂��B
		/// </summary>
		/// <exception cref="InvalidOperationException">�_�C�A���O�\�����͕ύX�ł��܂���B</exception>
		public bool AppendExtension
		{
			get
			{
				return this.appendExtension;
			}
			set
			{
				ThrowIfDialogShowingPropertyCannotBeChanged();
				this.appendExtension = value;
			}
		}

		/// <summary>
		/// �I����Ƀf�B���N�g�������̈ʒu�ɖ߂����ǂ����������l���擾�܂��͐ݒ肵�܂��B
		/// </summary>
		/// <exception cref="InvalidOperationException">�_�C�A���O�\�����͕ύX�ł��܂���B</exception>
		public bool RestoreDirectory
		{
			get
			{
				return this.restoreDirectory;
			}
			set
			{
				ThrowIfDialogShowingPropertyCannotBeChanged();
				this.restoreDirectory = value;
			}
		}

		/// <summary>
		/// �w�肵���t�@�C�����ŋߎg�p�����t�@�C���ꗗ�ɒǉ����邩�ǂ����𔻒肷��l���擾�܂��͐ݒ肵�܂��B
		/// </summary>
		/// <exception cref="InvalidOperationException">�_�C�A���O�\�����͕ύX�ł��܂���B</exception>
		public bool AddToMostRecentlyUsedList
		{
			get
			{
				return this.addToMruList;
			}
			set
			{
				ThrowIfDialogShowingPropertyCannotBeChanged();
				this.addToMruList = value;
			}
		}

		/// <summary>
		/// �v���p�e�B��ҏW�ł��邩�ǂ����𔻒肷��l���擾�܂��͐ݒ肵�܂��B
		/// </summary>
		public bool AllowPropertyEditing { get; set; }

		/// <summary>
		/// �K��̃t�@�C�������擾�܂��͐ݒ肵�܂��B
		/// </summary>
		public string DefaultFileName { get; set; }

		/// <summary>
		/// �t�@�C�����ɒǉ�����g���q���擾�܂��͐ݒ肵�܂��B
		/// <c>null</c>�܂��͋󕶎���̏ꍇ�́A�t�@�C�����Ɋg���q��ǉ����܂���B
		/// </summary>
		public string DefaultFileExtension
		{
			get
			{
				return this.defaultFileExtension;
			}
			set
			{
				this.defaultFileExtension = value;
				if (String.IsNullOrWhiteSpace(this.defaultFileExtension))
				{
					this.AppendExtension = false;
				}
				else
				{
					this.AppendExtension = true;
				}
			}
		}

		/// <summary>
		/// �t�@�C���_�C�A���O�{�b�N�X�̃t�@�C���̎�ނɕ\�������t�B���^�[�̃R���N�V�������擾���܂��B
		/// </summary>
		public FileTypeFilterCollection FileTypeFilters { get; private set; }

		/// <summary>
		/// �t�@�C���_�C�A���O�{�b�N�X�Ō��ݑI������Ă���t�B���^�[���擾���܂��B
		/// </summary>
		public FileTypeFilter SelectedFileTypeFilter
		{
			get
			{
				var nativeDialog = (IFileSaveDialog)this.FileDialogInternal.FileDialogNative;
				if (nativeDialog == null)
				{
					return null;
				}

				uint fileType;
				nativeDialog.GetFileTypeIndex(out fileType);
				return this.FileTypeFilters[(int)fileType];
			}
		}

		/// <summary>
		/// �t�@�C���_�C�A���O�{�b�N�X�Ō��ݑI������Ă���t�B���^�[�̃C���f�b�N�X���擾���܂��B
		/// </summary>
		public int SelectedFileTypeFilterIndex
		{
			get
			{
				var nativeDialog = (IFileSaveDialog)this.FileDialogInternal.FileDialogNative;
				if (nativeDialog == null)
				{
					return -1;
				}

				uint fileType;
				nativeDialog.GetFileTypeIndex(out fileType);
				return (int)fileType;
			}
		}

		/// <summary>
		/// ���[�U�[���w�肵���t�@�C�������擾���܂��B
		/// </summary>
		/// <returns></returns>
		public ShellFile GetShellFile()
		{
			var fileDialogNative = (IFileSaveDialog)this.FileDialogInternal.FileDialogNative;
			IShellItem shellItem;
			fileDialogNative.GetResult(out shellItem);
			if (shellItem == null)
			{
				return null;
			}
			return ShellFactory.Create(shellItem) as ShellFile;
		}

		/// <summary>
		/// �㏑���ۑ��_�C�A���O�̏����t�@�C������ݒ肵�܂��B
		/// </summary>
		/// <param name="shellFile">�����t�@�C�����B</param>
		public void SetSaveAsFile(ShellFile shellFile)
		{
			Contract.Requires<ArgumentNullException>(shellFile != null);

			var fileDialogNative = (IFileSaveDialog)this.FileDialogInternal.FileDialogNative;
			fileDialogNative.SetSaveAsItem(shellFile.ShellItem.ShellItemInterface);
		}

		#region FileDialogBase Members

		internal override IFileDialog2 CreateNativeFileDialog()
		{
			return new FileSaveDialogNative();
		}

		protected override FileDialogOptions GetDialogOptions()
		{
			var result = base.GetDialogOptions();

			if (this.appendExtension)
			{
				result |= FileDialogOptions.AppendDefaultExtension;
			}
			if (this.restoreDirectory)
			{
				result |= FileDialogOptions.RestoreDirectory;
			}

			return result;
		}

		/// <summary>
		/// �l�C�e�B�u�_�C�A���O�ɐݒ��K�p���܂��B
		/// </summary>
		protected override void SetNativeSettings()
		{
			base.SetNativeSettings();

			// �t�B���^�[
			if (this.FileTypeFilters.Any() && !this.setFilter)
			{
				SetFilter();
			}

			// �f�t�H���g�t�@�C����
			if (!String.IsNullOrWhiteSpace(this.DefaultFileName))
			{
				this.FileDialogInternal.SetDefaultFileName(this.DefaultFileName);
			}

			// �f�t�H���g�g���q
			if (!String.IsNullOrEmpty(this.DefaultFileExtension))
			{
				this.FileDialogInternal.SetDefaultExtension(this.DefaultFileExtension);
			}
		}

		/// <summary>
		/// �t�@�C���_�C�A���O�Ƀt�B���^�[��ݒ肵�܂��B
		/// </summary>
		private void SetFilter()
		{
			this.FileDialogInternal.SetFilters(this.FileTypeFilters);
			this.setFilter = true;

			if (!String.IsNullOrWhiteSpace(this.DefaultFileExtension))
			{
				for (var index = 0; index < this.FileTypeFilters.Count; ++index)
				{
					var filter = this.FileTypeFilters[index];
					if (filter.Extensions.Contains(this.DefaultFileExtension))
					{
						this.FileDialogInternal.SetFilterIndex(index + 1);
						break;
					}
				}
			}
		}

		#endregion
	}
}