using System;
using Reactive.Bindings;
using starshipxac.Shell;
using starshipxac.Windows.Shell.Media.Imaging;

namespace ShellExplorerSample.ViewModels.Shell
{
    public class ShellNonFileSystemItemViewModel : ShellObjectViewModel
    {
        public ShellNonFileSystemItemViewModel(ShellObject shellObject, ShellThumbnailFactory thumbnailFactory)
            : base(shellObject, thumbnailFactory)
        {
            #region Reactive Property

            this.DisplayName = new ReactiveProperty<string>(this.ShellObject.DisplayName);
            this.ItemTypeText = new ReactiveProperty<string>(
                this.ShellObject.Properties.Create<string>("System.ItemTypeText").Value);
            this.DateCreated = new ReactiveProperty<DateTime>(this.ShellObject.DateCreated);
            this.DateModified = new ReactiveProperty<DateTime>(this.ShellObject.DateModified);
            this.Thumbnail = new ReactiveProperty<ShellThumbnail>(
                new ShellThumbnail(this.ShellObject, this.ThumbnailFactory));

            #endregion
        }

        public override ReactiveProperty<string> DisplayName { get; }

        public override ReactiveProperty<string> ItemTypeText { get; }

        public override ReactiveProperty<DateTime> DateCreated { get; }

        public override ReactiveProperty<DateTime> DateModified { get; }

        public override ReactiveProperty<ShellThumbnail> Thumbnail { get; }
    }
}