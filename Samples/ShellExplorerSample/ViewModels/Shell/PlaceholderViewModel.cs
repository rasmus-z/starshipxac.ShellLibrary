using System;
using System.Diagnostics.Contracts;

namespace ShellExplorerSample.ViewModels.Shell
{
    internal class PlaceholderViewModel : ShellFolderViewModel
    {
        public PlaceholderViewModel(ShellFolderViewModel parentFolder)
            : base(parentFolder)
        {
            Contract.Requires<ArgumentNullException>(parentFolder != null);
        }

        public override string ParsingName
        {
            get
            {
                return ":::";
            }
        }

        public override string DisplayName
        {
            get
            {
                return String.Empty;
            }
        }
    }
}