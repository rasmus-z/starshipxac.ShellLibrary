using System;
using System.Threading.Tasks;
using System.Windows;
using starshipxac.Shell;

namespace starshipxac.Windows.Shell.Dialogs
{
    /// <summary>
    /// �ۑ�����t�@�C����I������_�C�A���O��\�����܂��B
    /// </summary>
    public sealed class SaveFileSelector : FileSaveDialogBase
    {
        private bool overwritePrompt = true;
        private bool createPrompt = false;
        private bool isExpandedMode = false;

        /// <summary>
        /// <see cref="SaveFileSelector"/>�N���X�̐V�����C���X�^���X�����������܂��B
        /// </summary>
        public SaveFileSelector()
        {
        }

        /// <summary>
        /// �_�C�A���O�̃^�C�g�����w�肵�āA
        /// <see cref="SaveFileSelector"/>�N���X�̐V�����C���X�^���X�����������܂��B
        /// </summary>
        /// <param name="title">�_�C�A���O�̃^�C�g���B</param>
        public SaveFileSelector(string title)
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
        /// �ۑ�����t�@�C����I������_�C�A���O��\�����܂��B
        /// </summary>
        /// <returns>�ۑ�����t�@�C���B</returns>
        public async Task<ShellFile> SelectSaveFileAsync()
        {
            ShellFile result = null;

            await InvokeAsync(() =>
            {
                var dialogResult = ShowDialog();
                if (dialogResult == FileDialogResult.Ok)
                {
                    result = GetShellFile();
                }
            });

            return result;
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