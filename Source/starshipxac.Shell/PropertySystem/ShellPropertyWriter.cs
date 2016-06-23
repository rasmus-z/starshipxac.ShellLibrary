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
        ///     <see cref="ShellPropertyWriter" />クラスの新しいインスタンスを初期化します。
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
        ///     ファイナライザー。
        /// </summary>
        ~ShellPropertyWriter()
        {
            Dispose(false);
        }

        /// <summary>
        ///     <see cref="ShellPropertyWriter" />によって使用されているすべてのリソースを解放します。
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     <see cref="ShellPropertyWriter" />によって使用されているすべてのリソースを解放し、
        ///     オプションでマネージリソースも解放します。
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
                    this.Store.Commit();
                    this.Store.Dispose();
                }

                this.disposed = true;
            }
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.Store != null);
        }

        /// <summary>
        ///     書き込み可能なプロパティストアを取得します。
        /// </summary>
        private ShellPropertyStore Store { get; }

        /// <summary>
        ///     <see cref="ShellPropertyWriter" />をコミットしてから閉じます。
        /// </summary>
        public void Close()
        {
            Dispose();
        }

        /// <summary>
        ///     プロパティに値を書き込みます。
        /// </summary>
        /// <param name="propertyKey"></param>
        /// <param name="value"></param>
        public void WriteProperty(ShellPropertyKey propertyKey, object value)
        {
            Contract.Requires<ArgumentNullException>(propertyKey != null);

            WriteProperty(propertyKey, value, true);
        }

        /// <summary>
        ///     プロパティに値を書き込みます。
        /// </summary>
        /// <param name="propertyKey"></param>
        /// <param name="value"></param>
        /// <param name="allowTruncatedValue"></param>
        public void WriteProperty(ShellPropertyKey propertyKey, object value, bool allowTruncatedValue)
        {
            Contract.Requires<ArgumentNullException>(propertyKey != null);
            Contract.Requires<InvalidOperationException>(this.Store != null);

            using (var propVar = PropVariant.FromObject(value))
            {
                this.Store.SetValue(propertyKey, propVar, allowTruncatedValue);
            }
        }

        /// <summary>
        ///     プロパティに値を書き込みます。
        /// </summary>
        /// <param name="canonicalName"></param>
        /// <param name="value"></param>
        public void WriteProperty(string canonicalName, object value)
        {
            WriteProperty(canonicalName, value, true);
        }

        /// <summary>
        ///     プロパティに値を書き込みます。
        /// </summary>
        /// <param name="canonicalName"></param>
        /// <param name="value"></param>
        /// <param name="allowTruncatedValue"></param>
        public void WriteProperty(string canonicalName, object value, bool allowTruncatedValue)
        {
            var propertyKey = ShellPropertyKey.FromCanonicalName(canonicalName);
            WriteProperty(propertyKey, value, allowTruncatedValue);
        }

        /// <summary>
        ///     プロパティに値を書き込みます。
        /// </summary>
        /// <param name="shellProperty"></param>
        /// <param name="value"></param>
        public void WriteProperty(IShellProperty shellProperty, object value)
        {
            Contract.Requires<ArgumentNullException>(shellProperty != null);

            WriteProperty(shellProperty, value, true);
        }

        /// <summary>
        ///     プロパティに値を書き込みます。
        /// </summary>
        /// <param name="shellProperty"></param>
        /// <param name="value"></param>
        /// <param name="allowTruncatedValue"></param>
        public void WriteProperty(IShellProperty shellProperty, object value, bool allowTruncatedValue)
        {
            Contract.Requires<ArgumentNullException>(shellProperty != null);

            WriteProperty(shellProperty.PropertyKey, value, allowTruncatedValue);
        }

        /// <summary>
        ///     プロパティに値を書き込みます。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="shellProperty"></param>
        /// <param name="value"></param>
        public void WriteProperty<T>(ShellProperty<T> shellProperty, T value)
        {
            Contract.Requires<ArgumentNullException>(shellProperty != null);

            WriteProperty(shellProperty, value, true);
        }

        /// <summary>
        ///     プロパティに値を書き込みます。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="shellProperty"></param>
        /// <param name="value"></param>
        /// <param name="allowTruncatedValue"></param>
        public void WriteProperty<T>(ShellProperty<T> shellProperty, T value, bool allowTruncatedValue)
        {
            Contract.Requires<ArgumentNullException>(shellProperty != null);

            WriteProperty(shellProperty.PropertyKey, value, allowTruncatedValue);
        }
    }
}