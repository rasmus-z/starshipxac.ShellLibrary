using System;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using starshipxac.Shell.Properties;
using starshipxac.Shell.PropertySystem.Interop;

namespace starshipxac.Shell.PropertySystem
{
    public class ShellPropertyWriter : IDisposable
    {
        internal ShellPropertyWriter(ShellObject shellObject)
        {
            Contract.Requires<ArgumentNullException>(shellObject != null);

            this.ShellObject = shellObject;

            try
            {
                this.WritablePropertyStore = ShellPropertyStore.CreateWritable(this.ShellObject);

                if (this.ShellObject.PropertyStore == null)
                {
                    this.ShellObject.PropertyStore = this.WritablePropertyStore;
                }
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
        /// ファイナライザー。
        /// </summary>
        ~ShellPropertyWriter()
        {
            Dispose(false);
        }

        /// <summary>
        /// <see cref="ShellPropertyWriter"/>によって使用されているすべてのリソースを解放します。
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// <see cref="ShellPropertyWriter"/>によって使用されているすべてのリソースを解放し、
        /// オプションでマネージリソースも解放します。
        /// </summary>
        /// <param name="disposing">
        /// マネージリソースとアンマネージリソースの両方を解放する場合は<c>true</c>。
        /// アンマネージリソースだけを解放する場合は<c>false</c>。
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            Close();
        }

        /// <summary>
        /// <see cref="ShellPropertyWriter"/>をコミットしてから閉じます。
        /// </summary>
        public void Close()
        {
            if (this.WritablePropertyStore != null)
            {
                this.WritablePropertyStore.Commit();
                this.WritablePropertyStore.Dispose();
                this.WritablePropertyStore = null;
            }

            this.ShellObject.PropertyStore = null;
        }

        /// <summary>
        /// <see cref="Shell.ShellObject"/>を取得します。
        /// </summary>
        protected ShellObject ShellObject { get; }

        private ShellPropertyStore WritablePropertyStore { get; set; }

        public void WriteProperty(ShellPropertyKey propertyKey, object value)
        {
            Contract.Requires<ArgumentNullException>(propertyKey != null);

            WriteProperty(propertyKey, value, true);
        }

        public void WriteProperty(ShellPropertyKey propertyKey, object value, bool allowTruncatedValue)
        {
            Contract.Requires<ArgumentNullException>(propertyKey != null);
            Contract.Requires<InvalidOperationException>(this.WritablePropertyStore != null);

            using (var propVar = PropVariant.FromObject(value))
            {
                this.WritablePropertyStore.SetValue(propertyKey, propVar, allowTruncatedValue);
            }
        }

        public void WriteProperty(string canonicalName, object value)
        {
            WriteProperty(canonicalName, value, true);
        }

        public void WriteProperty(string canonicalName, object value, bool allowTruncatedValue)
        {
            var propertyKey = ShellPropertyKey.FromCanonicalName(canonicalName);
            WriteProperty(propertyKey, value, allowTruncatedValue);
        }

        public void WriteProperty(IShellProperty shellProperty, object value)
        {
            Contract.Requires<ArgumentNullException>(shellProperty != null);

            WriteProperty(shellProperty, value, true);
        }

        public void WriteProperty(IShellProperty shellProperty, object value, bool allowTruncatedValue)
        {
            Contract.Requires<ArgumentNullException>(shellProperty != null);

            WriteProperty(shellProperty.PropertyKey, value, allowTruncatedValue);
        }

        public void WriteProperty<T>(ShellProperty<T> shellProperty, T value)
        {
            Contract.Requires<ArgumentNullException>(shellProperty != null);

            WriteProperty(shellProperty, value, true);
        }

        public void WriteProperty<T>(ShellProperty<T> shellProperty, T value, bool allowTruncatedValue)
        {
            Contract.Requires<ArgumentNullException>(shellProperty != null);

            WriteProperty(shellProperty.PropertyKey, value, allowTruncatedValue);
        }
    }
}