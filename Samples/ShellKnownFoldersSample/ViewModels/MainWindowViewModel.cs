using System;
using System.Linq;
using System.Reactive.Linq;
using Codeplex.Reactive;
using Livet;
using starshipxac.Shell;

namespace ShellKnownFoldersSample.ViewModels
{
	public class MainWindowViewModel : ViewModel
	{
		public MainWindowViewModel()
		{
			#region Reactive Property

			this.KnownFolders = new ReactiveCollection<ShellKnownFolderViewModel>(
				ShellKnownFolders.AllFolders.Select(x => new ShellKnownFolderViewModel(x)).ToObservable());

			#endregion
		}

		public void Initialize()
		{
		}

		public ReactiveCollection<ShellKnownFolderViewModel> KnownFolders { get; private set; }
	}
}