using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using starshipxac.Shell;
using starshipxac.Shell.Internal;
using starshipxac.Shell.Interop;
using starshipxac.Windows.Shell.Dialogs.Interop;

namespace starshipxac.Windows.Shell.Dialogs
{
    /// <summary>
    /// �t�@�C���I���_�C�A���O�̊��N���X���`���܂��B
    /// </summary>
    public abstract class FileOpenDialogBase : FileDialogBase
    {
        private bool restoreDirectory = true;
        private bool addToMruList = true;
        private bool setFilter = false;

        /// <summary>
        /// <see cref="FileOpenDialogBase"/>�N���X�̐V�����C���X�^���X�����������܂��B
        /// </summary>
        protected FileOpenDialogBase()
        {
            this.FileTypeFilters = new FileTypeFilterCollection();
        }

        /// <summary>
        /// �_�C�A���O�̃^�C�g�����w�肵�āA
        /// <see cref="FileOpenDialogBase"/>�N���X�̐V�����C���X�^���X�����������܂��B
        /// </summary>
        /// <param name="title"></param>
        protected FileOpenDialogBase(string title)
            : base(title)
        {
            this.FileTypeFilters = new FileTypeFilterCollection();
        }

        [ContractInvariantMethod]
        private void CommonFileDialogBaseInvaliant()
        {
            Contract.Invariant(this.FileTypeFilters != null);
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
                var fileDialogNative = (IFileOpenDialog)this.FileDialogInternal.FileDialogNative;
                if (fileDialogNative == null)
                {
                    return null;
                }

                uint fileType;
                fileDialogNative.GetFileTypeIndex(out fileType);
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
                var fileDialogNative = (IFileOpenDialog)this.FileDialogInternal.FileDialogNative;
                if (fileDialogNative == null)
                {
                    return -1;
                }

                uint fileType;
                fileDialogNative.GetFileTypeIndex(out fileType);
                return (int)fileType;
            }
        }

        /// <summary>
        /// ���[�U�[���I�������t�@�C�����̃R���N�V�������擾���܂��B
        /// </summary>
        /// <returns>�t�@�C�����̃R���N�V�����B</returns>
        public IEnumerable<ShellFile> GetShellFiles()
        {
            var fileDialogNative = (IFileOpenDialog)this.FileDialogInternal.FileDialogNative;

            IShellItemArray shellItemArray;
            fileDialogNative.GetResults(out shellItemArray);

            var count = ShellItemArray.GetShellItemCount(shellItemArray);
            for (var index = 0; index < count; ++index)
            {
                var shellItem = ShellItemArray.GetShellItemAt(shellItemArray, index);
                var shellFile = ShellFactory.Create(shellItem) as ShellFile;
                if (shellFile != null)
                {
                    yield return shellFile;
                }
            }
        }

        /// <summary>
        /// <see cref="FileOpenDialogNative"/>���쐬���܂��B
        /// </summary>
        internal override IFileDialog2 CreateNativeFileDialog()
        {
            return new FileOpenDialogNative();
        }

        protected override FileDialogOptions GetDialogOptions()
        {
            var result = base.GetDialogOptions();

            if (this.RestoreDirectory)
            {
                result |= FileDialogOptions.RestoreDirectory;
            }

            result |= FileDialogOptions.PathMustExist;
            result |= FileDialogOptions.FileMustExist;

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
                this.FileDialogInternal.SetFilters(this.FileTypeFilters);
                this.setFilter = true;
            }
        }
    }
}