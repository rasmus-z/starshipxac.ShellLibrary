using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Livet;
using Reactive.Bindings;
using starshipxac.Shell;
using starshipxac.Shell.Media.Imaging;
using starshipxac.Windows.Shell.Media.Imaging;

namespace ShellKnownFoldersSample.ViewModels
{
    public class ShellKnownFolderViewModel : ViewModel
    {
        public ShellKnownFolderViewModel(ShellKnownFolder knownFolder)
        {
            #region Reactive Property

            this.KnownFolder = new ReactiveProperty<ShellKnownFolder>(knownFolder);

            this.Thumbnail = new ReactiveProperty<ShellImageSource>();

            this.DisplayName = this.KnownFolder
                .Select(x => x.DisplayName)
                .ToReactiveProperty();

            this.CanonicalName = this.KnownFolder
                .Select(x => x.CanonicalName)
                .ToReactiveProperty();

            this.Category = this.KnownFolder
                .Select(x => x.Category.ToString())
                .ToReactiveProperty();

            this.Description = this.KnownFolder
                .Select(x => x.Description)
                .ToReactiveProperty();

            this.Path = this.KnownFolder
                .Select(x => x.Path)
                .ToReactiveProperty();

            #endregion
        }

        public static async Task<ShellKnownFolderViewModel> CreateAsync(ShellKnownFolder shellKnownFolder)
        {
            var result = new ShellKnownFolderViewModel(shellKnownFolder);
            result.Thumbnail.Value = new ShellImageSource(await shellKnownFolder.GetThumbnailAsync(ThumbnailMode.ListView));

            return result;
        }

        public ReactiveProperty<ShellKnownFolder> KnownFolder { get; }

        public ReactiveProperty<ShellImageSource> Thumbnail { get; }

        public ReactiveProperty<string> DisplayName { get; private set; }

        public ReactiveProperty<string> CanonicalName { get; private set; }

        public ReactiveProperty<string> Category { get; private set; }

        public ReactiveProperty<string> Description { get; private set; }

        public ReactiveProperty<string> Path { get; private set; }
    }
}