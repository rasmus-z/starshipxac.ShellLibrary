using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using starshipxac.Shell.Properties;
using starshipxac.Shell.PropertySystem.Internal;

namespace starshipxac.Shell.PropertySystem
{
    /// <summary>
    /// プロパティのコレクションを定義します。
    /// </summary>
    public class ShellPropertyCollection : ReadOnlyCollection<IShellProperty>, IDisposable
    {
        private bool disposed = false;

        public ShellPropertyCollection(ShellObject shellObject)
            : base(new List<IShellProperty>())
        {
            Contract.Requires<ArgumentNullException>(shellObject != null);

            this.ShellObject = shellObject;

            using (var store = ShellPropertyStore.Create(this.ShellObject))
            {
                AddProperties(store);
            }
        }

        internal ShellPropertyCollection(ShellPropertyStore propertyStore)
            : base(new List<IShellProperty>())
        {
            Contract.Requires<ArgumentNullException>(propertyStore != null);

            this.PropertyStore = propertyStore;
            AddProperties(propertyStore);
        }

        /// <summary>
        /// ファイナライザー。
        /// </summary>
        ~ShellPropertyCollection()
        {
            Dispose(false);
        }

        /// <summary>
        /// <see cref="ShellPropertyCollection"/>によって使用されているすべてのリソースを解放します。
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// <see cref="ShellPropertyCollection"/>によって使用されているすべてのリソースを解放し、
        /// オプションでマネージリソースも解放します。
        /// </summary>
        /// <param name="disposing">
        /// マネージリソースとアンマネージリソースの両方を解放する場合は<c>true</c>。
        /// アンマネージリソースだけを解放する場合は<c>false</c>。
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                this.disposed = true;

                if (disposing)
                {
                    // マネージリソース解放
                    this.PropertyStore.Dispose();
                }
            }
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
        }

        private ShellObject ShellObject { get; }

        private ShellPropertyStore PropertyStore { get; }

        private void AddProperties(ShellPropertyStore propertyStore)
        {
            Contract.Requires<ArgumentNullException>(propertyStore != null);

            var propertyCount = propertyStore.Count;
            for (uint i = 0; i < propertyCount; i++)
            {
                var propertyKey = propertyStore.GetAt(i);
                try
                {
                    if (this.ShellObject != null)
                    {
                        Items.Add(ShellPropertyFactory.CreateShellProperty(propertyKey, this.ShellObject));
                    }
                    else
                    {
                        Items.Add(ShellPropertyFactory.CreateShellProperty(propertyKey, this.PropertyStore));
                    }
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                }
            }
        }

        public bool Contains(string canonicalName)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(canonicalName),
                ErrorMessages.PropertyCollectionNullCanonicalName);

            return Items.Any(p => p.CanonicalName == canonicalName);
        }

        public bool Contains(ShellPropertyKey key)
        {
            return Items.Any(p => p.PropertyKey == key);
        }

        public IShellProperty this[string canonicalName]
        {
            get
            {
                Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(canonicalName),
                    ErrorMessages.PropertyCollectionNullCanonicalName);

                var result = Items.FirstOrDefault(p => p.CanonicalName == canonicalName);
                if (result == null)
                {
                    throw new ArgumentOutOfRangeException(ErrorMessages.PropertyCollectionCanonicalInvalidIndex);
                }

                return result;
            }
        }

        public IShellProperty this[ShellPropertyKey key]
        {
            get
            {
                var result = Items.FirstOrDefault(p => p.PropertyKey == key);
                if (result == null)
                {
                    throw new ArgumentOutOfRangeException(ErrorMessages.PropertyCollectionInvalidIndex);
                }

                return result;
            }
        }
    }
}