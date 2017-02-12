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
    ///     Define shell image source.
    /// </summary>
    public class ShellImageSource : INotifyPropertyChanged, IDisposable
    {
        private bool disposed = false;

        private ImageSource imageSource = null;

        private static readonly ConcurrentDictionary<string, PropertyChangedEventArgs> PropertyChangedEventArgsDictionary =
            new ConcurrentDictionary<string, PropertyChangedEventArgs>();

        /// <summary>
        ///     Initialize a new instance of the <see cref="ShellImageSource" /> class.
        /// </summary>
        public ShellImageSource()
        {
        }

        /// <summary>
        ///     Initialize a new instance of the <see cref="ShellImageSource" /> class,
        ///     to the specified <see cref="ShellThumbnail" />.
        /// </summary>
        /// <param name="shellThumbnail"><see cref="ShellThumbnail" />.</param>
        public ShellImageSource(ShellThumbnail shellThumbnail)
        {
            Contract.Requires<ArgumentNullException>(shellThumbnail != null);

            this.ShellThumbnail = shellThumbnail;
        }

        /// <summary>
        ///     Finalizer.
        /// </summary>
        ~ShellImageSource()
        {
            Dispose(false);
        }

        /// <summary>
        ///     Release all resources used by <see cref="ShellImageSource" />.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Release all resources used by <see cref="ShellImageSource" />.
        ///     and optionally releases managed resources.
        /// </summary>
        /// <param name="disposing">
        ///     <c>true</c> to release both managed and unmanaged resources.
        ///     <c>false</c> to release only unmanaged resources.
        /// </param>
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

        /// <summary>
        ///     Get or set the <see cref="ShellThumbnail" />.
        /// </summary>
        private ShellThumbnail ShellThumbnail { get; set; }

        /// <summary>
        ///     Get the image width.
        /// </summary>
        public double Width => this.ShellThumbnail?.OriginalWidth ?? 0;

        /// <summary>
        ///     Get the image height.
        /// </summary>
        public double Height => this.ShellThumbnail?.OriginalHeight ?? 0;

        /// <summary>
        ///     Get the <see cref="ImageSource" /> step by step.
        /// </summary>
        public ImageSource ImageSource
        {
            get
            {
                if (this.imageSource == null)
                {
                    Application.Current.Dispatcher.InvokeAsync(async () =>
                    {
                        await ShellImageSourceFactory.LoadAsync(
                            this.ShellThumbnail,
                            x => this.ImageSource = x);
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

        /// <summary>
        ///     Notify of property change.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Set the <see cref="ShellThumbnail" />.
        /// </summary>
        /// <param name="thumbnail"><see cref="ShellThumbnail" />.</param>
        public void SetSource(ShellThumbnail thumbnail)
        {
            this.ShellThumbnail = thumbnail;
        }

        /// <summary>
        ///     Notify that the property value has changed.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            Contract.Requires<ArgumentNullException>(propertyName != null);

            this.PropertyChanged?.Invoke(
                this,
                PropertyChangedEventArgsDictionary.GetOrAdd(propertyName, name => new PropertyChangedEventArgs(name)));
        }
    }
}