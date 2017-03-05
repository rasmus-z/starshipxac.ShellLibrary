using System;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using starshipxac.Shell.Properties;
using starshipxac.Shell.PropertySystem.Internal;
using starshipxac.Shell.PropertySystem.Interop;
using starshipxac.Shell.Resources;

namespace starshipxac.Shell.PropertySystem
{
    /// <summary>
    ///     Define shell property class.
    /// </summary>
    /// <typeparam name="T">Property type.</typeparam>
    /// <remarks>
    ///     Properties: http://msdn.microsoft.com/en-us/library/windows/desktop/dd561977(v=vs.85).aspx
    /// </remarks>
    public class ShellProperty<T> : IShellProperty
    {
        private ShellPropertyDescription description;
        private IconReference iconReference;

        /// <summary>
        ///     Initialize a instance of the <see cref="ShellProperty{T}" /> class.
        /// </summary>
        /// <param name="shellObject"></param>
        /// <param name="formatId"></param>
        /// <param name="propId"></param>
        public ShellProperty(ShellObject shellObject, Guid formatId, UInt32 propId)
        {
            Contract.Requires<ArgumentNullException>(shellObject != null);

            this.PropertyKey = ShellPropertyKeyFactory.Create(formatId, propId);

            this.ShellObject = shellObject;
            this.AllowSetTruncatedValue = false;

            if (this.Description.ValueType != typeof(T))
            {
                throw new InvalidOperationException(
                    String.Format(ErrorMessages.ShellPropertyUnmatchValueType, typeof(T), this.Description.ValueType));
            }
        }

        /// <summary>
        ///     Initialize a instance of the <see cref="ShellProperty{T}" /> class.
        /// </summary>
        /// <param name="shellObject"></param>
        /// <param name="canonicalName"></param>
        public ShellProperty(ShellObject shellObject, string canonicalName)
        {
            Contract.Requires<ArgumentNullException>(shellObject != null);
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(canonicalName));

            this.PropertyKey = ShellPropertyKeyFactory.Create(canonicalName);

            this.ShellObject = shellObject;
            this.AllowSetTruncatedValue = false;

            if (this.Description.ValueType != typeof(T))
            {
                throw new InvalidOperationException(
                    String.Format(ErrorMessages.ShellPropertyUnmatchValueType, typeof(T), this.Description.ValueType));
            }
        }

        /// <summary>
        ///     Initialize a instance of the <see cref="ShellProperty{T}" /> class.
        /// </summary>
        /// <param name="propertyKey"></param>
        /// <param name="description"></param>
        /// <param name="shellObject"></param>
        /// <remarks>
        ///     This constructor is used in <see cref="ShellPropertyFactory" />.
        ///     Do not change the order of the parameters.
        /// </remarks>
        internal ShellProperty(ShellPropertyKey propertyKey, ShellPropertyDescription description, ShellObject shellObject)
        {
            Contract.Requires<ArgumentNullException>(propertyKey != null);
            Contract.Requires<ArgumentNullException>(shellObject != null);

            this.ShellObject = shellObject;

            this.PropertyKey = propertyKey;
            this.description = description;
            this.AllowSetTruncatedValue = false;

            if (this.Description.ValueType != typeof(T))
            {
                throw new InvalidOperationException(
                    String.Format(ErrorMessages.ShellPropertyUnmatchValueType, typeof(T), this.Description.ValueType));
            }
        }

        /// <summary>
        ///     Initialize a instance of the <see cref="ShellProperty{T}" /> class.
        /// </summary>
        /// <param name="propertyKey"></param>
        /// <param name="description"></param>
        /// <param name="propertyStore"></param>
        /// <remarks>
        ///     This constructor is used in <see cref="ShellPropertyFactory" />.
        ///     Do not change the order of the parameters.
        /// </remarks>
        internal ShellProperty(ShellPropertyKey propertyKey, ShellPropertyDescription description, ShellPropertyStore propertyStore)
        {
            Contract.Requires<ArgumentNullException>(propertyKey != null);
            Contract.Requires<ArgumentNullException>(propertyStore != null);

            this.PropertyStore = propertyStore;

            this.PropertyKey = propertyKey;
            this.description = description;
            this.AllowSetTruncatedValue = false;

            if (this.Description.ValueType != typeof(T))
            {
                throw new InvalidOperationException(
                    String.Format(ErrorMessages.ShellPropertyUnmatchValueType, typeof(T), this.Description.ValueType));
            }
        }

        /// <summary>
        ///     Get the <see cref="ShellObject" />.
        /// </summary>
        private ShellObject ShellObject { get; }

        /// <summary>
        ///     Get the <see cref="ShellPropertyStore" />.
        /// </summary>
        private ShellPropertyStore PropertyStore { get; }

        /// <summary>
        ///     Get the property key.
        /// </summary>
        public ShellPropertyKey PropertyKey { get; }

        /// <summary>
        ///     Get the property description.
        /// </summary>
        public ShellPropertyDescription Description
        {
            get
            {
                if (this.description == null)
                {
                    this.description = ShellPropertyDescriptionFactory.Create(this.PropertyKey);
                }
                return this.description;
            }
        }

        /// <summary>
        ///     Get the canonical name.
        /// </summary>
        public string CanonicalName => this.Description.CanonicalName;

        /// <summary>
        ///     Get or set property value.
        /// </summary>
        public T Value
        {
            get
            {
                if (this.ShellObject != null)
                {
                    return this.ShellObject.ShellItem.GetPropertyValue<T>(this.PropertyKey);
                }
                else if (this.PropertyStore != null)
                {
                    return this.PropertyStore.GetValue<T>(this.PropertyKey);
                }

                return default(T);
            }
            set
            {
                if (this.ShellObject != null)
                {
                    using (var propertyWriter = new ShellPropertyWriter(this.ShellObject))
                    {
                        propertyWriter.WriteProperty(this, value, this.AllowSetTruncatedValue);
                    }
                }
                else if (this.PropertyStore != null)
                {
                    throw new InvalidOperationException(ErrorMessages.ShellPropertyCannotSetProperty);
                }
            }
        }

        /// <summary>
        ///     Get the property value type of <see cref="Object" />.
        /// </summary>
        public object ValueAsObject
        {
            get
            {
                if (this.ShellObject != null)
                {
                    return this.ShellObject.ShellItem.GetPropertyValue(this.PropertyKey);
                }
                else if (this.PropertyStore != null)
                {
                    return this.PropertyStore.GetValue(this.PropertyKey);
                }

                return null;
            }
        }

        /// <summary>
        ///     Get the property value type.
        /// </summary>
        public Type ValueType => Description.ValueType;

        public bool AllowSetTruncatedValue { get; set; }

        /// <summary>
        ///     Get the icon resource.
        /// </summary>
        public IconReference IconReference
        {
            get
            {
                if (this.iconReference == null)
                {
                    using (var store = ShellPropertyStore.Create(this.ShellObject))
                    {
                        var propVar = default(PropVariant);
                        try
                        {
                            store.GetPropVariant(this.PropertyKey, out propVar);

                            var referencePath = this.Description.GetImageReferencePath(ref propVar);
                            if (referencePath != null)
                            {
                                this.iconReference = new IconReference(referencePath);
                            }
                        }
                        finally
                        {
                            propVar.Clear();
                        }
                    }
                }
                return this.iconReference;
            }
        }

        /// <summary>
        ///     Get the property value.
        /// </summary>
        /// <param name="defaultValue">Default value.</param>
        /// <returns></returns>
        public T GetValue(T defaultValue = default(T))
        {
            try
            {
                if (this.ShellObject != null)
                {
                    return this.ShellObject.ShellItem.GetPropertyValue<T>(this.PropertyKey);
                }
                else if (this.PropertyStore != null)
                {
                    return this.PropertyStore.GetValue<T>(this.PropertyKey);
                }

                return defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        ///     Try get the property value.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool TryGetValue(out T result)
        {
            try
            {
                if (this.ShellObject != null)
                {
                    result = this.ShellObject.ShellItem.GetPropertyValue<T>(this.PropertyKey);
                    return true;
                }
                else if (this.PropertyStore != null)
                {
                    result = this.PropertyStore.GetValue<T>(this.PropertyKey);
                    return true;
                }

                result = default(T);
                return false;
            }
            catch (Exception)
            {
                result = default(T);
                return false;
            }
        }

        /// <summary>
        ///     Gets the text for displaying the property value.
        /// </summary>
        /// <param name="formatFlags">Format flags.</param>
        /// <returns></returns>
        public string GetDisplayText(PropertyDescriptionFormatFlags formatFlags)
        {
            using (var store = ShellPropertyStore.Create(this.ShellObject))
            {
                var propVar = default(PropVariant);
                try
                {
                    store.GetPropVariant(this.PropertyKey, out propVar);

                    return this.Description.GetDisplayText(ref propVar, formatFlags);
                }
                finally
                {
                    propVar.Clear();
                }
            }
        }

        public bool TryGetDisplayText(PropertyDescriptionFormatFlags formatFlags, out string text)
        {
            using (var store = ShellPropertyStore.Create(this.ShellObject))
            {
                var propVar = default(PropVariant);
                try
                {
                    store.GetPropVariant(this.PropertyKey, out propVar);

                    if (!this.Description.TryGetDisplayText(ref propVar, formatFlags, out text))
                    {
                        text = String.Empty;
                        return false;
                    }
                    return true;
                }
                finally
                {
                    propVar.Clear();
                }
            }
        }

        /// <summary>
        ///     Clear property value.
        /// </summary>
        public void ClearValue()
        {
            try
            {
                using (var writableStore = ShellPropertyStore.CreateWritable(this.ShellObject))
                {
                    writableStore.ClearValue(this.PropertyKey);
                    writableStore.Commit();
                }
            }
            catch (InvalidComObjectException e)
            {
                throw new ShellException(ErrorMessages.ShellPropertyUnableToGetWritableProperty, e);
            }
            catch (InvalidCastException e)
            {
                throw new ShellException(ErrorMessages.ShellPropertyUnableToGetWritableProperty, e);
            }
        }

        /// <summary>
        ///     Get the string of <see cref="ShellProperty{T}" />.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (this.Value == null)
            {
                return String.Empty;
            }
            return $"{this.Value}";
        }
    }
}