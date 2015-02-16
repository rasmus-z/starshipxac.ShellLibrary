using System;
using System.Diagnostics.Contracts;
using Codeplex.Reactive;
using starshipxac.Shell;

namespace ShellExplorerSample.ViewModels.Shell
{
    public class ShellFileViewModel : ShellObjectViewModel
    {
        public ShellFileViewModel(ShellFile shellFile, ShellFolderViewModel parentFolder)
            : base(shellFile, parentFolder)
        {
            Contract.Requires<ArgumentNullException>(shellFile != null);
            Contract.Requires<ArgumentNullException>(parentFolder != null);

            this.Path = new ReactiveProperty<string>(this.ShellFile.Path);
        }

        public ShellFile ShellFile
        {
            get
            {
                Contract.Ensures(Contract.Result<ShellFile>() != null);
                return (ShellFile)this.ShellObject;
            }
        }

        public ReactiveProperty<string> Path { get; private set; }

        public override string ToString()
        {
            return String.Format("{0}: {{ Path={1} }}",
                this.GetType().Name,
                this.Path.Value);
        }
    }
}