using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.IO;
using System.Runtime.CompilerServices;
using starshipxac.Shell.Media.Imaging;
using starshipxac.Shell.PropertySystem;

namespace starshipxac.Shell
{
    /// <summary>
    ///     Define shell object abstract class.
    /// </summary>
    public abstract class ShellObject : INotifyPropertyChanged, IDisposable, IEquatable<ShellObject>
    {
        private bool disposed = false;

        private string parsingName;

        private ShellProperties properties;
        private ShellProperty<string> contentTypeProperty;
        private ShellProperty<DateTime?> dateCreatedProperty;
        private ShellProperty<DateTime?> dateModifiedProperty;
        private ShellProperty<DateTime?> dateAccessedProperty;
        private ShellThumbnailFactory thumbnailFactory;

        private static readonly ConcurrentDictionary<string, PropertyChangedEventArgs> PropertyChangedEventArgsDictionary =
            new ConcurrentDictionary<string, PropertyChangedEventArgs>();

        /// <summary>
        ///     Initialize a new instance of the <see cref="ShellObject" /> class
        ///     to the specified <see cref="ShellItem" />.
        /// </summary>
        /// <param name="shellItem"><see cref="ShellItem" />.</param>
        internal ShellObject(ShellItem shellItem)
        {
            Contract.Requires<ArgumentNullException>(shellItem != null);

            this.ShellItem = shellItem;
        }

        /// <summary>
        ///     Finalizer.
        /// </summary>
        ~ShellObject()
        {
            Dispose(false);
        }

        /// <summary>
        ///     Release all resources used by <see cref="ShellObject" />.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Release all resources used by <see cref="ShellObject" />,
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
                    // Release managed resources.
                    this.ShellItem.Dispose();
                }

                this.disposed = true;
            }
        }

        [ContractInvariantMethod]
        private void ObjectInvaliant()
        {
            Contract.Invariant(this.ShellItem != null);
        }

        /// <summary>
        ///     Get <see cref="Shell.ShellItem" />.
        /// </summary>
        internal ShellItem ShellItem { get; }

        /// <summary>
        ///     Get the parsing name.
        /// </summary>
        public virtual string ParsingName
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                if (this.parsingName == null)
                {
                    this.parsingName = this.ShellItem.GetParsingName();
                }
                return this.parsingName;
            }
        }

        /// <summary>
        ///     Get the name.
        /// </summary>
        public virtual string Name
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                return Path.GetFileName(this.ParsingName);
            }
        }

        /// <summary>
        ///     Get default display name.
        /// </summary>
        public virtual string DisplayName
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                return this.ShellItem.GetDisplayName();
            }
        }

        /// <summary>
        ///     Get a value that determines whether <see cref="ShellObject" /> is an item on the file system.
        /// </summary>
        public bool IsFileSystem => this.ShellItem.IsFileSystem;

        /// <summary>
        ///     Get a content type string.
        /// </summary>
        public string ContentType
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                if (this.contentTypeProperty == null)
                {
                    // System.ContentType
                    this.contentTypeProperty = new ShellProperty<string>(this, "System.ContentType");
                }
                return this.contentTypeProperty.Value ?? String.Empty;
            }
        }

        /// <summary>
        ///     Get the item creation date and time(UTC).
        /// </summary>
        public virtual DateTime DateCreated
        {
            get
            {
                if (this.dateCreatedProperty == null)
                {
                    // System.DateCreated
                    this.dateCreatedProperty = new ShellProperty<DateTime?>(this, "System.DateCreated");
                }
                return this.dateCreatedProperty.Value.GetValueOrDefault(DateTime.MinValue);
            }
        }

        /// <summary>
        ///     Get the item modification date and time(UTC).
        /// </summary>
        public virtual DateTime DateModified
        {
            get
            {
                if (this.dateModifiedProperty == null)
                {
                    // System.DateModified
                    this.dateModifiedProperty = new ShellProperty<DateTime?>(this, "System.DateModified");
                }
                return this.dateModifiedProperty.Value.GetValueOrDefault(DateTime.MinValue);
            }
        }

        /// <summary>
        ///     Get the item access date and time(UTC).
        /// </summary>
        public virtual DateTime DateAccessed
        {
            get
            {
                if (this.dateAccessedProperty == null)
                {
                    // System.DateAccessed
                    this.dateAccessedProperty = new ShellProperty<DateTime?>(this, "System.DateAccessed");
                }
                return this.dateAccessedProperty.Value.GetValueOrDefault(DateTime.MinValue);
            }
        }

        /// <summary>
        ///     Get the <see cref="ShellThumbnailFactory" />.
        /// </summary>
        private ShellThumbnailFactory ThumbnailFactory
        {
            get
            {
                Contract.Ensures(Contract.Result<ShellThumbnailFactory>() != null);
                if (this.thumbnailFactory == null)
                {
                    this.thumbnailFactory = new ShellThumbnailFactory(this.ShellItem);
                }
                return this.thumbnailFactory;
            }
        }

        /// <summary>
        ///     Get the property collection.
        /// </summary>
        public ShellProperties Properties
        {
            get
            {
                Contract.Ensures(Contract.Result<ShellProperties>() != null);
                if (this.properties == null)
                {
                    this.properties = new ShellProperties(this);
                }
                return this.properties;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Get the display name of the specified <paramref name="displayNameType" />.
        /// </summary>
        /// <param name="displayNameType">Display name type.</param>
        /// <returns>Display name.</returns>
        public virtual string GetDisplayName(DisplayNameTypes displayNameType)
        {
            Contract.Ensures(Contract.Result<string>() != null);
            return this.ShellItem.GetDisplayName(displayNameType);
        }

        /// <summary>
        ///     Get the thumbnail.
        /// </summary>
        /// <param name="thumbnailMode">Thumbnail mode.</param>
        /// <returns><see cref="ShellThumbnail" />.</returns>
        public ShellThumbnail GetThumbnail(ThumbnailMode thumbnailMode)
        {
            Contract.Ensures(Contract.Result<ShellThumbnail>() != null);

            return this.ThumbnailFactory.Create(thumbnailMode);
        }

        /// <summary>
        ///     Get the thumbnail of the specified width and height.
        /// </summary>
        /// <param name="width">取得するサムネイルの幅。</param>
        /// <param name="height">取得するサムネイルの高さ。</param>
        /// <returns></returns>
        public ShellThumbnail GetThumbnail(double width, double height)
        {
            Contract.Requires<ArgumentOutOfRangeException>(0.0 <= width);
            Contract.Requires<ArgumentOutOfRangeException>(0.0 <= height);
            Contract.Ensures(Contract.Result<ShellThumbnail>() != null);

            return this.ThumbnailFactory.Create(width, height);
        }

        /// <summary>
        ///     Get parent folder instance.
        /// </summary>
        /// <returns>Parent <see cref="ShellFolder" />. If the parent folder does not exist, it returns <c>null</c>.</returns>
        /// <exception cref="ShellException">Failed to acquire parent <see cref="ShellFolder" />.</exception>
        public ShellFolder GetFolder()
        {
            var parentShellItem = this.ShellItem.GetParent();
            if (parentShellItem == null)
            {
                return null;
            }
            return ShellFactory.FromShellFolderItem(parentShellItem);
        }

        /// <summary>
        ///     Notify that the property value has changed.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            Contract.Requires<ArgumentNullException>(propertyName != null);

            this.PropertyChanged?.Invoke(this, GetPropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        ///     Creates event information to notify change of the specified property name.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <returns><see cref="PropertyChangedEventArgs" />.</returns>
        private static PropertyChangedEventArgs GetPropertyChangedEventArgs(string propertyName)
        {
            Contract.Requires(propertyName != null);
            Contract.Ensures(Contract.Result<PropertyChangedEventArgs>() != null);

            return PropertyChangedEventArgsDictionary.GetOrAdd(propertyName, name => new PropertyChangedEventArgs(name));
        }

        /// <summary>
        ///     Compare the two <see cref="ShellObject" /> to determine if they are equal.
        /// </summary>
        /// <param name="left">The first <see cref="ShellObject" />.</param>
        /// <param name="right">The second <see cref="ShellObject" />.</param>
        /// <returns>
        ///     If the two <see cref="ShellObject" /> are equal, it returns <c>true</c>.
        ///     Otherwise returns <c>false</c>.
        /// </returns>
        public static bool operator ==(ShellObject left, ShellObject right)
        {
            return Equals(left, right);
        }

        /// <summary>
        ///     Compare the two <see cref="ShellObject" /> to determine if they are not equal.
        /// </summary>
        /// <param name="left">The first <see cref="ShellObject" />.</param>
        /// <param name="right">The second <see cref="ShellObject" />.</param>
        /// <returns>
        ///     If the two <see cref="ShellObject" /> are not equal, it returns <c>true</c>.
        ///     Otherwise returns <c>false</c>.
        /// </returns>
        public static bool operator !=(ShellObject left, ShellObject right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        ///     Determines whether the value of the specified <see cref="ShellObject" /> is equal to the current
        ///     <see cref="ShellObject" />.
        /// </summary>
        /// <param name="other">Compare with the current <see cref="ShellObject" /> <see cref="ShellObject" />.</param>
        /// <returns>
        ///     If the current <see cref="ShellObject" /> is equal to <paramref name="other" />, it returns <c>true</c>.
        ///     Otherwise returns <c>false</c>.
        /// </returns>
        public bool Equals(ShellObject other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            //return this.ShellItem.Equals(other.ShellItem);
            return String.Compare(this.ParsingName, other.ParsingName, StringComparison.OrdinalIgnoreCase) == 0;
        }

        /// <summary>
        ///     Determines whether the value of the specified object is equal to the current <see cref="ShellObject" />.
        /// </summary>
        /// <param name="obj">The object to be compared with the current <see cref="ShellObject" />.</param>
        /// <returns>
        ///     If the current <see cref="ShellObject" /> is equal to <paramref name="obj" />, it returns <c>true</c>.
        ///     Otherwise returns <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (this.GetType() != obj.GetType())
            {
                return false;
            }
            //return Equals((ShellObject)obj);
            return String.Compare(this.ParsingName, ((ShellObject)obj).ParsingName, StringComparison.OrdinalIgnoreCase) == 0;
        }

        /// <summary>
        ///     Get a hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            //return this.ShellItem.GetHashCode();
            return this.ParsingName.GetHashCode();
        }

        /// <summary>
        ///     Get a string representation of this instance.
        /// </summary>
        /// <returns>A string representation of this instance.</returns>
        public override string ToString()
        {
            return $"{{{nameof(ParsingName)}: {ParsingName}}}";
        }
    }
}