using System;
using System.Diagnostics.Contracts;
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
		}

		public ShellFile ShellFile
		{
			get
			{
				Contract.Ensures(Contract.Result<ShellFile>() != null);
				return (ShellFile)this.ShellObject;
			}
		}

		public string Path
		{
			get
			{
				Contract.Ensures(Contract.Result<string>() != null);
				return this.ShellFile.Path;
			}
		}

		public override string ToString()
		{
			return String.Format("{0}: {{ Path={1} }}",
				this.GetType().Name,
				this.Path);
		}
	}
}