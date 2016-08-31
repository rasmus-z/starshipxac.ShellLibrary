using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using starshipxac.Shell.Media.Imaging;

namespace starshipxac.Windows.Shell.Media.Imaging
{
    /// <summary>
    ///     <c>Shellobject</c>イメージを保持します。
    /// </summary>
    public class ShellImageSource : INotifyPropertyChanged, IDisposable
    {
        private bool disposed = false;

        private ImageSource imageSource = null;

        private static readonly ConcurrentDictionary<string, PropertyChangedEventArgs> PropertyChangedEventArgsDictionary =
            new ConcurrentDictionary<string, PropertyChangedEventArgs>();

        public ShellImageSource()
        {
        }

        /// <summary>
        ///     <see cref="ShellImageSource" />クラス新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="shellThumbnail"></param>
        public ShellImageSource(ShellThumbnail shellThumbnail)
        {
            Contract.Requires<ArgumentNullException>(shellThumbnail != null);

            this.ShellThumbnail = shellThumbnail;
        }

        ~ShellImageSource()
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

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
        }

        private ShellThumbnail ShellThumbnail { get; set; }

        public double Width => this.ShellThumbnail?.OriginalWidth ?? 0;

        public double Height => this.ShellThumbnail?.OriginalHeight ?? 0;

        /// <summary>
        ///     イメージを段階的に取得します。
        /// </summary>
        public ImageSource ImageSource
        {
            get
            {
                if (this.imageSource == null)
                {
                    Application.Current.Dispatcher.InvokeAsync(async () => { await ShellImageSourceFactory.LoadAsync(this.ShellThumbnail, UpdateImageSource); });
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

        public void SetSource(ShellThumbnail thumbnail)
        {
            this.ShellThumbnail = thumbnail;
        }

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

        private void UpdateImageSource(ImageSource imageSouce)
        {
            this.ImageSource = imageSouce;
        }
    }
}