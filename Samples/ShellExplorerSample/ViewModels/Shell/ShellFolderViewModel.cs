using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Data;
using Codeplex.Reactive;
using Livet;
using starshipxac.Shell;
using starshipxac.Windows.Shell.Media.Imaging;

namespace ShellExplorerSample.ViewModels.Shell
{
    public class ShellFolderViewModel : ShellObjectViewModel
    {
        public ShellFolderViewModel(ShellFolder folder, ShellFolderViewModel parentFolder)
            : base(folder, parentFolder)
        {
            Contract.Requires<ArgumentNullException>(folder != null);
            Contract.Requires<ArgumentNullException>(parentFolder != null);

            this.ShellFolder = folder;
            InitializeReactiveProperties();
        }

        public ShellFolderViewModel(ShellFolder folder, ShellThumbnailFactory thumbnailFactory)
            : base(folder, thumbnailFactory)
        {
            Contract.Requires<ArgumentNullException>(folder != null);

            this.ShellFolder = folder;
            InitializeReactiveProperties();
        }

        internal ShellFolderViewModel(ShellFolderViewModel parentFolder)
            : base(parentFolder)
        {
            Contract.Requires<ArgumentNullException>(parentFolder != null);

            this.ShellFolder = null;
            InitializeReactiveProperties();
        }

        #region ReactiveProperty

        private void InitializeReactiveProperties()
        {
            this.IsExpanded = new ReactiveProperty<bool>(false);

            this.IsSelected = new ReactiveProperty<bool>(false);

            if (this.ShellFolder == null)
            {
                this.ShellFolders = new ReactiveProperty<ObservableSynchronizedCollection<ShellFolderViewModel>>(
                    new ObservableSynchronizedCollection<ShellFolderViewModel>());
            }
            else
            {
                this.ShellFolders = this.IsExpanded
                    .Select(CreateShellFolders)
                    .ToReactiveProperty();
            }
        }

        #endregion

        public ShellFolder ShellFolder { get; private set; }

        public ReactiveProperty<bool> IsExpanded { get; private set; }

        public ReactiveProperty<bool> IsSelected { get; private set; }

        public ReactiveProperty<ObservableSynchronizedCollection<ShellFolderViewModel>> ShellFolders { get; private set; }

        private ICollectionView ShellItemCollectionView { get; set; }

        private ICollectionView ShellFolderCollectionView { get; set; }

        public IEnumerable<ShellObjectViewModel> EnumerateItems()
        {
            return this.ShellFolder.EnumerateItems()
                .Select(x => ShellViewModelFactory.Create(x, this));
        }

        public IEnumerable<ShellFolderViewModel> EnumerateFolders()
        {
            return this.ShellFolder.EnumerateFolders()
                .Select(x => ShellViewModelFactory.CreateFolder(x, this));
        }

        private ObservableSynchronizedCollection<ShellFolderViewModel> CreateShellFolders(bool expanded)
        {
            if (this.ShellFolderCollectionView != null)
            {
            }

            var result = new ObservableSynchronizedCollection<ShellFolderViewModel>();
            if (expanded)
            {
                try
                {
                    foreach (var folder in EnumerateFolders())
                    {
                        Debug.WriteLine(String.Format("  -> {0}", folder.DisplayName.Value));
                        result.Add(folder);
                    }
                }
                catch (DirectoryNotFoundException ex)
                {
                    Debug.WriteLine(String.Format("{0}: {1}", ex.GetType().Name, ex.Message));
                    result.Clear();
                }
            }
            else
            {
                result.Add(new PlaceholderViewModel(this));
            }

            this.ShellFolderCollectionView = CollectionViewSource.GetDefaultView(result);

            return result;
        }

        public override string ToString()
        {
            return this.DisplayName.Value;
        }
    }
}