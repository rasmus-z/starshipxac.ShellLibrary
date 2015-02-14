using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Livet;
using ShellExplorerSample.ViewModels.Shell;
using starshipxac.Shell;

namespace ShellExplorerSample.ViewModels
{
	/// <summary>
	/// Shell Explorer Sample View Model.
	/// </summary>
	/// <remarks>
	/// Gitライブラリフォルダーの<see cref="ShellLibrary.EnumerateItems"/>メソッドで <c>FileNotFoundException</c>が発生する場合は、
	/// プロジェクトのプロパティ -> ビルドの「32ビットの優先」のチェックを外す。
	/// </remarks>
	public class MainWindowViewModel : ViewModel
	{
		public void Initialize()
		{
			ShellViewModelFactory.Initialize();

			this.RootFolders = new List<ShellFolderViewModel>()
			{
				ShellViewModelFactory.CreateRoot(ShellKnownFolders.OneDrive),
				ShellViewModelFactory.CreateRoot(ShellKnownFolders.HomeGroup),
				ShellViewModelFactory.CreateRoot(ShellKnownFolders.Computer),
				ShellViewModelFactory.CreateRoot(ShellKnownFolders.Libraries)
			};
		}

		#region RootFolders変更通知プロパティ

		private IReadOnlyList<ShellFolderViewModel> rootFolders;

		public IReadOnlyList<ShellFolderViewModel> RootFolders
		{
			get
			{
				return this.rootFolders;
			}
			private set
			{
				this.rootFolders = value;
				RaisePropertyChanged();
			}
		}

		#endregion

		#region SelectedFolder変更通知プロパティ

		private ShellFolderViewModel selectedFolder;

		public ShellFolderViewModel SelectedFolder
		{
			get
			{
				return this.selectedFolder;
			}
			set
			{
				if (this.selectedFolder == value)
				{
					return;
				}
				this.selectedFolder = value;
				RaisePropertyChanged();

				Debug.WriteLine("selectedFolder={0}", this.selectedFolder);

				this.ShellItems = this.SelectedFolder.ShellItems;
			}
		}

		#endregion

		#region ShellItems変更通知プロパティ

		private ObservableCollection<ShellObjectViewModel> shellItems;

		public ObservableCollection<ShellObjectViewModel> ShellItems
		{
			get
			{
				return this.shellItems;
			}
			private set
			{
				if (this.shellItems == value)
				{
					return;
				}
				this.shellItems = value;
				RaisePropertyChanged();
			}
		}

		#endregion
	}
}