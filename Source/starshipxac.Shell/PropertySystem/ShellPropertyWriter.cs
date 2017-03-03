using System;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using starshipxac.Shell.Properties;
using starshipxac.Shell.PropertySystem.Interop;

namespace starshipxac.Shell.PropertySystem
{
    public class ShellPropertyWriter : IDisposable
    {
        private bool disposed = false;

        /// <summary>
        ///     Initialize a new instance of the <see cref="ShellPropertyWriter" /> class
        ///     to the specified <see cref="ShellObject" />.
        /// </summary>
        /// <param name="shellObject"></param>
        internal ShellPropertyWriter(ShellObject shellObject)
        {
            Contract.Requires<ArgumentNullException>(shellObject != null);

            try
            {
                this.Store = ShellPropertyStore.CreateWritable(shellObject);
            }
            catch (InvalidComObjectException e)
            {
                throw new ShellException(ErrorMessages.ShellPropertyUnableToGetWritableProperty, e);
            }
            catch (InvalidCastException)
            {
                throw new ShellException(ErrorMessages.ShellPropertyUnableToGetWritableProperty);
            }
        }

        /// <summary>
        ///     Release all resources used by <see cref="ShellPropertyWriter" /> class.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Release all resources used by <see cref="ShellPropertyWriter" /> class,
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
                    this.Store.Commit();
                    this.Store.Dispose();
                }

                this.disposed = true;
            }
        }

        /// <summary>
        ///     Get a writable property store.
        /// </summary>
        private ShellPropertyStore Store { get; }

        /// <summary>
        ///     Commit and then cloase <see cref="ShellPropertyWriter" />.
        /// </summary>
        public void Close()
        {
            Dispose();
        }

        /// <summary>
        ///     Write property value.
        /// </summary>
        /// <param name="propertyKey">Property key.</param>
        /// <param name="value">Property value.</param>
        public void WriteProperty(ShellPropertyKey propertyKey, object value)
        {
            Contract.Requires<ArgumentNullException>(propertyKey != null);

            WriteProperty(propertyKey, value, true);
        }

        /// <summary>
        ///     Write property value.
        /// </summary>
        /// <param name="propertyKey">Property key.</param>
        /// <param name="value">Property value.</param>
        /// <param name="allowTruncatedValue"></param>
        public void WriteProperty(ShellPropertyKey propertyKey, object value, bool allowTruncatedValue)
        {
            Contract.Requires<ArgumentNullException>(propertyKey != null);
            Contract.Requires<InvalidOperationException>(this.Store != null);

            var propVar = PropVariant.FromObject(value);
            try
            {
                this.Store.SetValue(propertyKey, ref propVar, allowTruncatedValue);
            }
            finally
            {
                propVar.Clear();
            }
        }

        /// <summary>
        ///     Write property value.
        /// </summary>
        /// <param name="canonicalName">Property canonical name.</param>
        /// <param name="value">Property value.</param>
        public void WriteProperty(string canonicalName, object value)
        {
            WriteProperty(canonicalName, value, true);
        }

        /// <summary>
        ///     Write property value.
        /// </summary>
        /// <param name="canonicalName">Property canonical name.</param>
        /// <param name="value">Property value.</param>
        /// <param name="allowTruncatedValue"></param>
        public void WriteProperty(string canonicalName, object value, bool allowTruncatedValue)
        {
            var propertyKey = ShellPropertyKey.FromCanonicalName(canonicalName);
            WriteProperty(propertyKey, value, allowTruncatedValue);
        }

        /// <summary>
        ///     Write property value.
        /// </summary>
        /// <param name="shellProperty"><see cref="IShellProperty" />.</param>
        /// <param name="value">Property value.</param>
        public void WriteProperty(IShellProperty shellProperty, object value)
        {
            Contract.Requires<ArgumentNullException>(shellProperty != null);

            WriteProperty(shellProperty, value, true);
        }

        /// <summary>
        ///     Write property value.
        /// </summary>
        /// <param name="shellProperty"><see cref="IShellProperty" />.</param>
        /// <param name="value">Property value.</param>
        /// <param name="allowTruncatedValue"></param>
        public void WriteProperty(IShellProperty shellProperty, object value, bool allowTruncatedValue)
        {
            Contract.Requires<ArgumentNullException>(shellProperty != null);

            WriteProperty(shellProperty.PropertyKey, value, allowTruncatedValue);
        }

        /// <summary>
        ///     Write property value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="shellProperty"><see cref="ShellProperty{T}" />.</param>
        /// <param name="value">Property value.</param>
        public void WriteProperty<T>(ShellProperty<T> shellProperty, T value)
        {
            Contract.Requires<ArgumentNullException>(shellProperty != null);

            WriteProperty(shellProperty, value, true);
        }

        /// <summary>
        ///     Write property value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="shellProperty"><see cref="ShellProperty{T}" />.</param>
        /// <param name="value">Property value.</param>
        /// <param name="allowTruncatedValue"></param>
        public void WriteProperty<T>(ShellProperty<T> shellProperty, T value, bool allowTruncatedValue)
        {
            Contract.Requires<ArgumentNullException>(shellProperty != null);

            WriteProperty(shellProperty.PropertyKey, value, allowTruncatedValue);
        }
    }
}