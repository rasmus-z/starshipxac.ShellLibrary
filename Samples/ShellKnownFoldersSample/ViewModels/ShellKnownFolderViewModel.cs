using System;
using System.Reactive.Linq;
using System.Windows;
using Codeplex.Reactive;
using Livet;
using starshipxac.Shell;
using starshipxac.Windows.Shell.Media.Imaging;

namespace ShellKnownFoldersSample.ViewModels
{
    public class ShellKnownFolderViewModel : ViewModel
    {
        public ShellKnownFolderViewModel(ShellKnownFolder knownFolder)
        {
            this.ThumbnailFactory = new ShellThumbnailFactory(new Size(64, 64));

            #region Reactive Property

            this.KnownFolder = new ReactiveProperty<ShellKnownFolder>(knownFolder);

            this.Thumbnail = new ReactiveProperty<ShellThumbnail>(new ShellThumbnail(
                this.KnownFolder.Value, this.ThumbnailFactory));

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

        public void Initialize()
        {
        }

        private ShellThumbnailFactory ThumbnailFactory { get; set; }

        public ReactiveProperty<ShellKnownFolder> KnownFolder { get; private set; }

        public ReactiveProperty<ShellThumbnail> Thumbnail { get; private set; }

        public ReactiveProperty<string> DisplayName { get; private set; }

        public ReactiveProperty<string> CanonicalName { get; private set; }

        public ReactiveProperty<string> Category { get; private set; }

        public ReactiveProperty<string> Description { get; private set; }

        public ReactiveProperty<string> Path { get; private set; }
    }
}