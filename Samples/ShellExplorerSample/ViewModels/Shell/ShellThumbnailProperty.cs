using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using Livet;
using starshipxac.Shell;
using starshipxac.Shell.Media.Imaging;
using starshipxac.Windows.Shell.Media.Imaging;

namespace ShellExplorerSample.ViewModels.Shell
{
    public class ShellThumbnailProperty : INotifyPropertyChanged, IDisposable
    {
        private bool disposed = false;

        private readonly ThumbnailMode thumbnailMode;
        private ImageSource imageSource = null;

        private static readonly ConcurrentDictionary<string, PropertyChangedEventArgs> PropertyChangedEventArgsDictionary =
            new ConcurrentDictionary<string, PropertyChangedEventArgs>();

        public ShellThumbnailProperty(ShellObject shellObject, ThumbnailMode thumbnailMode)
        {
            Contract.Requires<ArgumentNullException>(shellObject != null);

            this.thumbnailMode = thumbnailMode;
            this.ShellObject = shellObject;
        }

        ~ShellThumbnailProperty()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.ShellThumbnail?.Dispose();
                }

                this.disposed = true;
            }
        }

        private ShellObject ShellObject { get; }

        private ShellThumbnail ShellThumbnail { get; set; }

        public double Width => this.ShellThumbnail?.OriginalWidth ?? 0;

        public double Height => this.ShellThumbnail?.OriginalHeight ?? 0;

        public ImageSource ImageSource
        {
            get
            {
                if (this.imageSource == null)
                {
                    DispatcherHelper.UIDispatcher.InvokeAsync(async () =>
                    {
                        this.ShellThumbnail = this.ShellObject.GetThumbnail(this.thumbnailMode);
                        await ShellImageSourceFactory.LoadAsync(this.ShellThumbnail, UpdateImageSource);
                    });
                }
                return this.imageSource;
            }
            set
            {
                this.imageSource = value;
                RaisePropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     プロパティの値が変更されたことを通知します。
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            Contract.Requires<ArgumentNullException>(propertyName != null);

            this.PropertyChanged?.Invoke(
                this,
                PropertyChangedEventArgsDictionary.GetOrAdd(propertyName, name => new PropertyChangedEventArgs(name)));
        }

        private void UpdateImageSource(ImageSource thumbnailImage)
        {
            this.ImageSource = thumbnailImage;
        }
    }
}