using System;
using System.Threading.Tasks;
using starshipxac.Shell;

namespace ShellFileDialogSample.Views
{
    public interface IMainView
    {
        Task<ShellFile> ShowSelectOpenFileDialogAsync();

        Task<ShellFile> ShowSelectSaveFileDialogAsync();

        Task<ShellFolder> ShowSelectFolderDialogAsync();

        ShellFile ShowCustomFileOpenDialog();
    }
}