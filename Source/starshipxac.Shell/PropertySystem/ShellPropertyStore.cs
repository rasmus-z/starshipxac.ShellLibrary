using System;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using starshipxac.Shell.Interop;
using starshipxac.Shell.Properties;
using starshipxac.Shell.PropertySystem.Interop;

namespace starshipxac.Shell.PropertySystem
{
    /// <summary>
    ///     Define property store class.
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
        ///     Finalizer.
        /// </summary>
        ~ShellPropertyStore()
        {
            Dispose(false);
        }

        /// <summary>
        ///     Release all resources used by <see cref="ShellPropertyStore" /> class.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Release all resources used by <see cref="ShellPropertyStore" /> class,
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
                // Release unmanaged resource.
                Marshal.ReleaseComObject(this.propertyStoreNative);

                this.disposed = true;
            }
        }

        /// <summary>
        ///     Create a new instance of the <see cref="ShellPropertyStore" /> class
        ///     to the specified <see cref="ShellObject" />.
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
        ///     Create a new instance of the writable <see cref="ShellPropertyStore" /> class
        ///     to the specified <see cref="ShellObject" />.
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

        /// <summary>
        ///     Get the count.
        /// </summary>
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
        ///     Get the property value.
        /// </summary>
        /// <param name="propertyKey">Property key.</param>
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

        /// <summary>
        ///     Get the property value.
        /// </summary>
        /// <typeparam name="T">Property type.</typeparam>
        /// <param name="propertyKey">Property key.</param>
        /// <returns></returns>
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

        /// <summary>
        ///     Get the <see cref="PropVariant" />.
        /// </summary>
        /// <param name="propertyKey">Property key.</param>
        /// <param name="propVariant"></param>
        internal void GetPropVariant(ShellPropertyKey propertyKey, PropVariant propVariant)
        {
            Contract.Requires<ArgumentNullException>(propertyKey != null);

            var key = propertyKey.PropertyKeyNative;
            this.propertyStoreNative.GetValue(ref key, propVariant);
        }

        /// <summary>
        ///     Set the property value.
        /// </summary>
        /// <param name="propertyKey">Property key.</param>
        /// <param name="propVar">Property value.</param>
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