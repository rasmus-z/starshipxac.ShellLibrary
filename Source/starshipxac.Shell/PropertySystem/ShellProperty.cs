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
    ///     シェルプロパティを保持します。
    /// </summary>
    /// <typeparam name="T">プロパティの型。</typeparam>
    /// <remarks>
    ///     Properties: http://msdn.microsoft.com/en-us/library/windows/desktop/dd561977(v=vs.85).aspx
    /// </remarks>
    public class ShellProperty<T> : IShellProperty
    {
        private ShellPropertyDescription description;
        private IconReference iconReference;

        /// <summary>
        ///     <see cref="ShellProperty{T}" />クラスの新しいインスタンスを初期化します。
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
        ///     <see cref="ShellProperty{T}" />クラスの新しいインスタンスを初期化します。
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
        ///     <see cref="ShellProperty{T}"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="propertyKey"></param>
        /// <param name="description"></param>
        /// <param name="shellObject"></param>
        /// <remarks>
        ///     このコンストラクタは、<see cref="ShellPropertyFactory" />で使用されます。パラメーターの順番は変更しないでください。
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
        ///     <see cref="ShellProperty{T}"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="propertyKey"></param>
        /// <param name="description"></param>
        /// <param name="propertyStore"></param>
        /// <remarks>
        ///     このコンストラクタは、<see cref="ShellPropertyFactory" />で使用されます。パラメーターの順番は変更しないでください。
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

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.PropertyKey != null);
        }

        /// <summary>
        ///     <see cref="ShellObject" />を取得します。
        /// </summary>
        private ShellObject ShellObject { get; }

        /// <summary>
        ///     プロパティストアを取得または設定します。
        /// </summary>
        private ShellPropertyStore PropertyStore { get; }

        /// <summary>
        ///     プロパティキーを取得します。
        /// </summary>
        public ShellPropertyKey PropertyKey { get; }

        /// <summary>
        ///     プロパティ定義を取得します。
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
        ///     プロパティの標準的な名前を取得します。
        /// </summary>
        public string CanonicalName => this.Description.CanonicalName;

        /// <summary>
        ///     プロパティの値を取得または設定します。
        /// </summary>
        public T Value
        {
            get
            {
                if (this.ShellObject != null)
                {
                    if (this.ShellObject.PropertyStore != null)
                    {
                        return this.ShellObject.PropertyStore.GetValue<T>(this.PropertyKey);
                    }
                    else
                    {
                        return this.ShellObject.ShellItem.GetPropertyValue<T>(this.PropertyKey.PropertyKeyNative);
                    }
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
        ///     <see cref="Object" />型のプロパティ値を取得します。
        /// </summary>
        public object ValueAsObject
        {
            get
            {
                if (this.ShellObject != null)
                {
                    if (this.ShellObject.PropertyStore != null)
                    {
                        return this.ShellObject.PropertyStore.GetValue(this.PropertyKey);
                    }
                    else
                    {
                        return this.ShellObject.ShellItem.GetPropertyValue(this.PropertyKey.PropertyKeyNative);
                    }
                }
                else if (this.PropertyStore != null)
                {
                    return this.PropertyStore.GetValue(this.PropertyKey);
                }

                return null;
            }
        }

        /// <summary>
        ///     プロパティ値の型を取得します。
        /// </summary>
        public Type ValueType => Description.ValueType;

        public bool AllowSetTruncatedValue { get; set; }

        /// <summary>
        ///     アイコンリソースを取得します。
        /// </summary>
        public IconReference IconReference
        {
            get
            {
                if (this.iconReference == null)
                {
                    using (var store = ShellPropertyStore.Create(this.ShellObject))
                    {
                        using (var propVar = new PropVariant())
                        {
                            store.GetPropVariant(this.PropertyKey, propVar);

                            var referencePath = this.Description.GetImageReferencePath(propVar);
                            if (referencePath != null)
                            {
                                this.iconReference = new IconReference(referencePath);
                            }
                        }
                    }
                }
                return this.iconReference;
            }
        }

        /// <summary>
        ///     プロパティ値を取得します。
        /// </summary>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public T GetValue(T defaultValue = default(T))
        {
            try
            {
                if (this.ShellObject != null)
                {
                    if (this.ShellObject.PropertyStore != null)
                    {
                        return this.ShellObject.PropertyStore.GetValue<T>(this.PropertyKey);
                    }
                    else
                    {
                        return this.ShellObject.ShellItem.GetPropertyValue<T>(this.PropertyKey.PropertyKeyNative);
                    }
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
        ///     プロパティ値の取得を試みます。
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool TryGetValue(out T result)
        {
            try
            {
                if (this.ShellObject != null)
                {
                    if (this.ShellObject.PropertyStore != null)
                    {
                        result = this.ShellObject.PropertyStore.GetValue<T>(this.PropertyKey);
                        return true;
                    }
                    else
                    {
                        result = this.ShellObject.ShellItem.GetPropertyValue<T>(this.PropertyKey.PropertyKeyNative);
                        return true;
                    }
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
        ///     プロパティ値をクリアします。
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
        /// プロパティ値の表示用テキストを取得します。
        /// </summary>
        /// <param name="formatFlags">取得するテキストの種類。</param>
        /// <returns></returns>
        public string GetDisplayText(PropertyDescriptionFormatFlags formatFlags)
        {
            using (var store = ShellPropertyStore.Create(this.ShellObject))
            {
                using (var propVar = new PropVariant())
                {
                    store.GetPropVariant(this.PropertyKey, propVar);

                    return this.Description.GetDisplayText(propVar, formatFlags);
                }
            }
        }

        public bool TryGetDisplayText(PropertyDescriptionFormatFlags formatFlags, out string text)
        {
            using (var store = ShellPropertyStore.Create(this.ShellObject))
            {
                using (var propVar = new PropVariant())
                {
                    store.GetPropVariant(this.PropertyKey, propVar);

                    if (!this.Description.TryGetDisplayText(propVar, formatFlags, out text))
                    {
                        text = String.Empty;
                        return false;
                    }
                    return true;
                }
            }
        }

        /// <summary>
        ///     <see cref="ShellProperty{T}"/>の文字列表現を取得します。
        /// </summary>
        /// <returns><see cref="ShellProperty{T}"/>の文字列表現。</returns>
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