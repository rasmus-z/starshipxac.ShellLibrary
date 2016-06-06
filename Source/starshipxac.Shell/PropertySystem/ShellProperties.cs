using System;
using System.Diagnostics.Contracts;
using starshipxac.Shell.PropertySystem.Internal;

namespace starshipxac.Shell.PropertySystem
{
    public class ShellProperties : IDisposable
    {
        private bool disposed = false;

        private ShellPropertyCollection propertyCollection;

        internal ShellProperties(ShellObject shellObject)
        {
            Contract.Requires<ArgumentNullException>(shellObject != null);

            this.ShellObject = shellObject;
        }

        /// <summary>
        ///     ファイナライザー。
        /// </summary>
        ~ShellProperties()
        {
            Dispose(false);
        }

        /// <summary>
        ///     <see cref="ShellProperties" />によって使用されているすべてのリソースを解放します。
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     <see cref="ShellProperties" />によって使用されているすべてのリソースを解放し、
        ///     オプションで、マネージリソースも解放します。
        /// </summary>
        /// <param name="disposing">
        ///     マネージリソースとアンマネージリソースの両方を解放する場合は<c>true</c>。
        ///     アンマネージリソースだけを解放する場合は<c>false</c>。
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (this.propertyCollection != null)
                    {
                        this.propertyCollection.Dispose();
                        this.propertyCollection = null;
                    }
                }

                this.disposed = true;
            }
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.ShellObject != null);
        }

        private ShellObject ShellObject { get; }

        /// <summary>
        ///     プロパティコレクションを取得します。
        /// </summary>
        public ShellPropertyCollection DefaultPropertyCollection
        {
            get
            {
                Contract.Ensures(Contract.Result<ShellPropertyCollection>() != null);
                return this.propertyCollection ?? (this.propertyCollection = new ShellPropertyCollection(this.ShellObject));
            }
        }

        public ShellProperty<T> Create<T>(string formatId, UInt32 propertyId)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(formatId));
            Contract.Ensures(Contract.Result<ShellProperty<T>>() != null);

            return new ShellProperty<T>(this.ShellObject, new Guid(formatId), propertyId);
        }

        public ShellProperty<T> Create<T>(string canonicalName)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(canonicalName));

            return new ShellProperty<T>(this.ShellObject, canonicalName);
        }

        public IShellProperty Create(string canonicalName)
        {
            Contract.Requires<ArgumentNullException>(canonicalName != null);
            Contract.Ensures(Contract.Result<IShellProperty>() != null);

            var propertyKey = ShellPropertyKey.FromCanonicalName(canonicalName);
            return ShellPropertyFactory.CreateShellProperty(propertyKey, this.ShellObject);
        }
    }
}