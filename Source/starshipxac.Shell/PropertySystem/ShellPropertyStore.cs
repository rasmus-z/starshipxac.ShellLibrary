using System;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using starshipxac.Shell.Interop;
using starshipxac.Shell.Properties;
using starshipxac.Shell.PropertySystem.Interop;

namespace starshipxac.Shell.PropertySystem
{
    /// <summary>
    ///     プロパティストアを操作します。
    /// </summary>
    public class ShellPropertyStore : IDisposable
    {
        private bool disposed = false;

        private readonly IPropertyStore propertyStoreNative;

        private ShellPropertyStore(IPropertyStore propertyStoreNative)
        {
            Contract.Requires<ArgumentNullException>(propertyStoreNative != null);

            this.propertyStoreNative = propertyStoreNative;
        }

        /// <summary>
        ///     ファイナライザー。
        /// </summary>
        ~ShellPropertyStore()
        {
            Dispose(false);
        }

        /// <summary>
        ///     <see cref="ShellPropertyStore" />によって使用されているすべてのリソースを解放します。
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     <see cref="ShellPropertyStore" />によって使用されているすべてのリソースを解放し、
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
                this.disposed = true;

                // アンマネージリソース解放
                Marshal.ReleaseComObject(this.propertyStoreNative);
            }
        }

        /// <summary>
        ///     <see cref="ShellPropertyStore" />を作成します。
        /// </summary>
        /// <param name="shellObject"></param>
        /// <returns></returns>
        public static ShellPropertyStore Create(ShellObject shellObject)
        {
            Contract.Requires<ArgumentNullException>(shellObject != null);

            IPropertyStore propertyStore;
            var guid = new Guid(PropertySystemIID.IPropertyStore);
            var hr = shellObject.ShellItem.ShellItemInterface.GetPropertyStore(
                GETPROPERTYSTOREFLAGS.GPS_BESTEFFORT,
                ref guid,
                out propertyStore);
            if (HRESULT.Failed(hr) || propertyStore == null)
            {
                throw ShellException.FromHRESULT(hr);
            }

            return new ShellPropertyStore(propertyStore);
        }

        /// <summary>
        ///     書き込み可能な<see cref="ShellPropertyStore" />を作成します。
        /// </summary>
        /// <param name="shellObject"></param>
        /// <returns></returns>
        public static ShellPropertyStore CreateWritable(ShellObject shellObject)
        {
            Contract.Requires<ArgumentNullException>(shellObject != null);

            IPropertyStore propertyStore;
            var guid = new Guid(PropertySystemIID.IPropertyStore);
            var hr = shellObject.ShellItem.ShellItemInterface.GetPropertyStore(
                GETPROPERTYSTOREFLAGS.GPS_READWRITE,
                ref guid,
                out propertyStore);
            if (HRESULT.Failed(hr))
            {
                throw new ShellException(
                    ErrorMessages.ShellPropertyUnableToGetWritableProperty,
                    hr);
            }

            return new ShellPropertyStore(propertyStore);
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.propertyStoreNative != null);
        }

        public int Count
        {
            get
            {
                uint result;
                this.propertyStoreNative.GetCount(out result);
                return (int)result;
            }
        }

        public ShellPropertyKey this[int index]
        {
            get
            {
                Contract.Requires<ArgumentOutOfRangeException>(0 <= index);
                return GetAt((uint)index);
            }
        }

        /// <summary>
        ///     プロパティの値を取得します。
        /// </summary>
        /// <param name="propertyKey"></param>
        /// <returns></returns>
        public object GetValue(ShellPropertyKey propertyKey)
        {
            Contract.Requires<ArgumentNullException>(propertyKey != null);

            using (var propVar = new PropVariant())
            {
                var key = propertyKey.PropertyKeyNative;
                this.propertyStoreNative.GetValue(ref key, propVar);
                return propVar.Value;
            }
        }

        public T GetValue<T>(ShellPropertyKey propertyKey)
        {
            Contract.Requires<ArgumentNullException>(propertyKey != null);

            using (var propVar = new PropVariant())
            {
                var key = propertyKey.PropertyKeyNative;
                this.propertyStoreNative.GetValue(ref key, propVar);
                return (T)propVar.Value;
            }
        }

        internal void GetPropVariant(ShellPropertyKey propertyKey, PropVariant propVariant)
        {
            Contract.Requires<ArgumentNullException>(propertyKey != null);

            var key = propertyKey.PropertyKeyNative;
            this.propertyStoreNative.GetValue(ref key, propVariant);
        }

        /// <summary>
        ///     プロパティに値を設定します。
        /// </summary>
        /// <param name="propertyKey"></param>
        /// <param name="propVar"></param>
        internal void SetValue(ShellPropertyKey propertyKey, PropVariant propVar)
        {
            Contract.Requires<ArgumentNullException>(propertyKey != null);
            Contract.Requires<ArgumentNullException>(propVar != null);

            var key = propertyKey.PropertyKeyNative;
            var hr = this.propertyStoreNative.SetValue(ref key, propVar);
            if (HRESULT.Failed(hr))
            {
                throw new ShellException(ErrorMessages.ShellPropertySetValue, hr);
            }
        }

        internal void SetValue(ShellPropertyKey propertyKey, PropVariant propVar, bool allowTruncatedValue)
        {
            Contract.Requires<ArgumentNullException>(propertyKey != null);

            var key = propertyKey.PropertyKeyNative;
            var hr = this.propertyStoreNative.SetValue(ref key, propVar);
            if (HRESULT.Failed(hr))
            {
                throw new ShellException(ErrorMessages.ShellPropertySetValue, hr);
            }
            else if (!allowTruncatedValue && hr == ShellNativeMethods.InPlaceStringTruncated)
            {
                throw new ArgumentOutOfRangeException(nameof(propVar), ErrorMessages.ShellPropertyValueTruncated);
            }
        }

        public ShellPropertyKey GetAt(uint propertyIndex)
        {
            PROPERTYKEY result;
            this.propertyStoreNative.GetAt(propertyIndex, out result);
            return new ShellPropertyKey(result);
        }

        internal PROPERTYKEY GetPropertyKeyAt(uint propertyIndex)
        {
            PROPERTYKEY result;
            this.propertyStoreNative.GetAt(propertyIndex, out result);
            return result;
        }

        public void ClearValue(ShellPropertyKey propertyKey)
        {
            Contract.Requires<ArgumentNullException>(propertyKey != null);

            using (var propVar = new PropVariant())
            {
                SetValue(propertyKey, propVar);
            }
        }

        public void Commit()
        {
            this.propertyStoreNative.Commit();
        }
    }
}