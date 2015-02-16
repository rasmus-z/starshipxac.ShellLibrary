using System;
using System.Diagnostics.Contracts;
using Codeplex.Reactive;
using Livet;
using starshipxac.Shell;
using starshipxac.Windows.Shell.Media.Imaging;

namespace ShellExplorerSample.ViewModels.Shell
{
    public class ShellObjectViewModel : ViewModel
    {
        public ShellObjectViewModel(ShellObject shellObject, ShellFolderViewModel parentFolder)
        {
            Contract.Requires<ArgumentNullException>(shellObject != null);
            Contract.Requires<ArgumentNullException>(parentFolder != null);

            this.ShellObject = shellObject;
            this.ParentFolder = parentFolder;
            this.ThumbnailFactory = this.ParentFolder.ThumbnailFactory;

            InitializeReactiveProperties();
        }

        protected ShellObjectViewModel(ShellObject shellObject, ShellThumbnailFactory thumbnailFactory)
        {
            Contract.Requires<ArgumentNullException>(shellObject != null);

            this.ShellObject = shellObject;
            this.ParentFolder = null;
            this.ThumbnailFactory = thumbnailFactory;

            InitializeReactiveProperties();
        }

        internal ShellObjectViewModel(ShellFolderViewModel parentFolder)
        {
            this.ShellObject = null;
            this.ParentFolder = parentFolder;
            this.ThumbnailFactory = parentFolder.ThumbnailFactory;

            InitializeReactiveProperties();
        }

        #region ReactiveProperty

        private void InitializeReactiveProperties()
        {
            if (this.ShellObject == null)
            {
                this.ParsingName = new ReactiveProperty<string>(String.Empty);
                this.DisplayName = new ReactiveProperty<string>(String.Empty);
                this.DateCreated = new ReactiveProperty<DateTime>(DateTime.MinValue);
                this.DateModified = new ReactiveProperty<DateTime>(DateTime.MinValue);
            }
            else
            {
                this.ParsingName = new ReactiveProperty<string>(":::");
                this.DisplayName = new ReactiveProperty<string>(this.ShellObject.DisplayName);
                var itemTypeTextProperty = this.ShellObject.Properties.Create<string>("System.ItemTypeText");
                this.ItemTypeText = new ReactiveProperty<string>(itemTypeTextProperty.Value);
                
                if (this.ThumbnailFactory != null)
                {
                    this.Thumbnail = new ReactiveProperty<ShellThumbnail>(
                        new ShellThumbnail(this.ShellObject, this.ThumbnailFactory));
                }
            }
        }

        #endregion

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.ParsingName != null);
        }

        public ShellObject ShellObject { get; private set; }

        public ShellFolderViewModel ParentFolder { get; private set; }

        public ShellThumbnailFactory ThumbnailFactory { get; private set; }

        public ReactiveProperty<string> ParsingName { get; protected set; }

        public ReactiveProperty<string> DisplayName { get; private set; }

        public ReactiveProperty<string> ItemTypeText { get; private set; }

        public ReactiveProperty<DateTime> DateCreated { get; private set; }

        public ReactiveProperty<DateTime> DateModified { get; private set; }

        public ReactiveProperty<ShellThumbnail> Thumbnail { get; private set; }

        public override string ToString()
        {
            return String.Format("{0}: {{ ParsingName={1} }}",
                this.GetType().Name,
                this.ParsingName.Value);
        }
    }
}