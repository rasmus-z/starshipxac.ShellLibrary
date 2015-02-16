using System;
using System.Collections.Generic;
using starshipxac.Shell;
using starshipxac.Shell.Internal;
using starshipxac.Shell.Interop;
using starshipxac.Windows.Shell.Dialogs.Interop;

namespace starshipxac.Windows.Shell.Dialogs
{
    public abstract class FolderSelectDialogBase : FileDialogBase
    {
        private bool forceFileSystem = false;

        protected FolderSelectDialogBase()
        {
        }

        protected FolderSelectDialogBase(string title)
            : base(title)
        {
        }

        /// <summary>
        /// �t�@�C���V�X�e����̃t�H���_�[�̂ݑI���\�ɂ��邩�ǂ����������l���擾�܂��͐ݒ肵�܂��B
        /// </summary>
        public bool ForceFileSystem
        {
            get
            {
                return this.forceFileSystem;
            }
            set
            {
                ThrowIfDialogShowingPropertyCannotBeChanged();
                this.forceFileSystem = value;
            }
        }

        /// <summary>
        /// ���[�U�[���I�������t�H���_�[���̃R���N�V�������擾���܂��B
        /// </summary>
        /// <returns>�t�H���_�[���̃R���N�V�����B</returns>
        public IEnumerable<ShellFolder> GetShellFolders()
        {
            var fileDialogNative = (IFileOpenDialog)this.FileDialogInternal.FileDialogNative;

            IShellItemArray shellItemArray;
            fileDialogNative.GetResults(out shellItemArray);

            var count = ShellItemArray.GetShellItemCount(shellItemArray);
            for (var index = 0; index < count; ++index)
            {
                var shellItem = ShellItemArray.GetShellItemAt(shellItemArray, index);
                var shellFolder = ShellFactory.Create(shellItem) as ShellFolder;
                if (shellFolder != null)
                {
                    yield return shellFolder;
                }
            }
        }

        internal override IFileDialog2 CreateNativeFileDialog()
        {
            return new FileOpenDialogNative();
        }

        protected override FileDialogOptions GetDialogOptions()
        {
            var result = base.GetDialogOptions();

            result |= FileDialogOptions.SelectFolers;

            if (this.ForceFileSystem)
            {
                result |= FileDialogOptions.ForceFileSystem;
            }

            return result;
        }
    }
}