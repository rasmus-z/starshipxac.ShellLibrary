using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using Reactive.Bindings;
using starshipxac.Shell;
using starshipxac.Shell.Media.Imaging;
using starshipxac.Shell.PropertySystem;

namespace ShellExplorerSample.ViewModels.Shell
{
    public class ShellNonFileSystemItemViewModel : ShellObjectViewModel
    {
        private ShellNonFileSystemItemViewModel(ShellObject shellObject)
            : base(shellObject)
        {
            #region Reactive Property

            this.DisplayName = new ReactiveProperty<string>(this.ShellObject.DisplayName);
            this.ItemTypeText = new ReactiveProperty<string>(
                new ShellProperty<string>(this.ShellObject, "System.ItemTypeText").Value);
            this.DateCreated = new ReactiveProperty<DateTime>(this.ShellObject.DateCreated);
            this.DateModified = new ReactiveProperty<DateTime>(this.ShellObject.DateModified);
            this.Thumbnail = new ReactiveProperty<ShellThumbnail>();

            #endregion
        }

        public static async Task<ShellNonFileSystemItemViewModel> CreateAsync(ShellObject shellObject)
        {
            Contract.Requires<ArgumentNullException>(shellObject != null);

            var result = new ShellNonFileSystemItemViewModel(shellObject);
            result.Thumbnail.Value = await shellObject.GetThumbnailAsync(ThumbnailMode.ListView);

            return result;
        }

        public override ReactiveProperty<string> DisplayName { get; }

        public override ReactiveProperty<string> ItemTypeText { get; }

        public override ReactiveProperty<DateTime> DateCreated { get; }

        public override ReactiveProperty<DateTime> DateModified { get; }

        public override ReactiveProperty<ShellThumbnail> Thumbnail { get; }
    }
}