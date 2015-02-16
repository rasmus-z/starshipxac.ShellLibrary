using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
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
        }

        public ShellFolderViewModel(ShellFolder folder, ShellThumbnailFactory thumbnailFactory)
            : base(folder, thumbnailFactory)
        {
            Contract.Requires<ArgumentNullException>(folder != null);

            this.ShellFolder = folder;
        }

        internal ShellFolderViewModel(ShellFolderViewModel parentFolder)
            : base(parentFolder)
        {
            Contract.Requires<ArgumentNullException>(parentFolder != null);

            this.ShellFolder = null;
        }

        public ShellFolder ShellFolder { get; private set; }

        #region IsExpanded変更通知プロパティ

        private bool isExpanded;

        public bool IsExpanded
        {
            get
            {
                return this.isExpanded;
            }
            set
            {
                if (this.isExpanded == value)
                {
                    return;
                }
                this.isExpanded = value;

                if (this.isExpanded)
                {
                    this.shellFolders = GetShellFolders();
                    RaisePropertyChanged(() => this.ShellFolders);
                }

                RaisePropertyChanged();

                Debug.WriteLine("IsExpanded={0}: {1}", this.IsExpanded, this.ShellFolder);
            }
        }

        #endregion

        #region IsSelected変更通知プロパティ

        private bool isSelected;

        public bool IsSelected
        {
            get
            {
                return this.isSelected;
            }
            set
            {
                if (this.isSelected == value)
                {
                    return;
                }
                this.isSelected = value;
                RaisePropertyChanged();

                Debug.WriteLine("IsSelected={0}: {1}", this.IsSelected, this.ShellFolder);
            }
        }

        #endregion

        #region ShellItems変更通知プロパティ

        private ObservableCollection<ShellObjectViewModel> shellItems;

        public ObservableCollection<ShellObjectViewModel> ShellItems
        {
            get
            {
                Contract.Ensures(Contract.Result<ObservableCollection<ShellObjectViewModel>>() != null);
                if (this.shellItems == null)
                {
                    this.shellItems = new ObservableCollection<ShellObjectViewModel>(this.ShellFolder.EnumerateItems()
                        .Select(x => ShellViewModelFactory.Create(x, this)));
                }
                return this.shellItems;
            }
        }

        #endregion

        #region ShellFolders変更通知プロパティ

        private ObservableCollection<ShellFolderViewModel> shellFolders;

        public ObservableCollection<ShellFolderViewModel> ShellFolders
        {
            get
            {
                Contract.Ensures(Contract.Result<ObservableCollection<ShellFolderViewModel>>() != null);
                Debug.WriteLine(String.Format("Get ShellFolders: {0}", this.DisplayName));
                if (this.shellFolders == null)
                {
                    this.shellFolders = new ObservableCollection<ShellFolderViewModel>()
                    {
                        new PlaceholderViewModel(this)
                    };
                }
                Debug.WriteLine(String.Format("    Count = {0}", this.shellFolders.Count));

                return this.shellFolders;
            }
        }

        private ObservableCollection<ShellFolderViewModel> GetShellFolders()
        {
            var result = new ObservableCollection<ShellFolderViewModel>();
            try
            {
                foreach (var folder in this.ShellFolder.EnumerateFolders())
                {
                    Debug.WriteLine(String.Format("  -> {0}", folder.DisplayName));
                    result.Add(new ShellFolderViewModel(folder, this));
                }
            }
            catch (DirectoryNotFoundException ex)
            {
                Debug.WriteLine(String.Format("{0}: {1}", ex.GetType().Name, ex.Message));
                this.shellFolders = new ObservableCollection<ShellFolderViewModel>();
            }
            return result;
        }

        #endregion

        public override string ToString()
        {
            return this.DisplayName;
        }
    }
}