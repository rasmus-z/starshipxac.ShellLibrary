using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using starshipxac.Shell.Interop;
using starshipxac.Shell.Properties;
using starshipxac.Shell.PropertySystem.Internal;
using starshipxac.Shell.PropertySystem.Interop;
using starshipxac.Shell.Search.Interop;

namespace starshipxac.Shell.PropertySystem
{
    /// <summary>
    ///     プロパティの定義内容を取得します。
    /// </summary>
    public class ShellPropertyDescription : IDisposable
    {
        private bool disposed = false;

        private IPropertyDescription propertyDescriptionNative;
        private string canonicalName;
        private PROPERTYKEY propertyKey;
        private string displayName;
        private string editInvitation;
        private VarEnum? varEnumType = null;
        private PropertyDisplayTypes? displayType;
        private PropertyAggregationTypes? aggregationTypes;
        private uint? defaultColumWidth;
        private PropertyTypeFlags? propertyTypeFlags;
        private PropertyViewFlags? propertyViewFlags;
        private Type valueType;
        private ReadOnlyCollection<ShellPropertyEnumType> propertyEnumTypes;
        private PropertyColumnStates? columnState;
        private PropertyConditionTypes? conditionType;
        private PropertyConditionOperations? conditionOperation;
        private PropertyGroupingRange? groupingRange;
        private PropertySortDescription? sortDescription;

        internal ShellPropertyDescription(PROPERTYKEY propertyKey)
        {
            Contract.Requires<ArgumentNullException>(propertyKey != null);

            this.propertyKey = propertyKey;

            var guid = new Guid(PropertySystemIID.IPropertyDescription);
            var hr = PropertySystemNativeMethods.PSGetPropertyDescription(ref this.propertyKey, ref guid,
                out this.propertyDescriptionNative);
            if (HRESULT.Failed(hr))
            {
                throw new ShellException(
                    String.Format(ErrorMessages.ShellPropertyDescriptionError, propertyKey),
                    hr);
            }
        }

        /// <summary>
        ///     ファイナライザー。
        /// </summary>
        ~ShellPropertyDescription()
        {
            Dispose(false);
        }

        /// <summary>
        ///     <see cref="ShellPropertyDescription" />によって使用されているすべてのリソースを解放します。
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     <see cref="ShellPropertyDescription" />によって使用されているすべてのリソースを解放し、
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
                // アンマネージリソース解放
                if (this.propertyDescriptionNative != null)
                {
                    Marshal.ReleaseComObject(this.propertyDescriptionNative);
                    this.propertyDescriptionNative = null;
                }

                this.disposed = true;
            }
        }

        /// <summary>
        ///     プロパティ定義を取得します。
        /// </summary>
        internal IPropertyDescription PropertyDescriptionNative => this.propertyDescriptionNative;

        /// <summary>
        ///     プロパティの標準的な名前を取得します。
        /// </summary>
        public string CanonicalName
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                if (this.canonicalName == null)
                {
                    PropertySystemNativeMethods.PSGetNameFromPropertyKey(ref this.propertyKey, out this.canonicalName);
                    if (this.canonicalName == null)
                    {
                        this.canonicalName = String.Empty;
                    }
                }
                return this.canonicalName;
            }
        }

        /// <summary>
        ///     プロパティ表示名を取得します。
        /// </summary>
        public string DisplayName
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                if (this.PropertyDescriptionNative != null && this.displayName == null)
                {
                    IntPtr ppszName;
                    var hr = this.PropertyDescriptionNative.GetDisplayName(out ppszName);
                    if (HRESULT.Succeeded(hr) && ppszName != IntPtr.Zero)
                    {
                        this.displayName = Marshal.PtrToStringUni(ppszName);
                        Marshal.FreeCoTaskMem(ppszName);
                    }
                }
                return this.displayName;
            }
        }

        /// <summary>
        ///     エディットコントロールで使用される文字列を取得します。
        /// </summary>
        public string EditInvitation
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                if (this.PropertyDescriptionNative != null && this.editInvitation == null)
                {
                    IntPtr ppszInvite;
                    var hr = this.PropertyDescriptionNative.GetEditInvitation(out ppszInvite);
                    if (HRESULT.Succeeded(hr) && ppszInvite != IntPtr.Zero)
                    {
                        this.editInvitation = Marshal.PtrToStringUni(ppszInvite);
                        Marshal.FreeCoTaskMem(ppszInvite);
                    }
                }
                return this.editInvitation;
            }
        }

        /// <summary>
        ///     <see cref="VarEnum" />を取得します。
        /// </summary>
        public VarEnum VarEnumType
        {
            get
            {
                if (this.PropertyDescriptionNative != null && this.varEnumType == null)
                {
                    VarEnum tempType;
                    var hr = this.PropertyDescriptionNative.GetPropertyType(out tempType);
                    if (HRESULT.Succeeded(hr))
                    {
                        this.varEnumType = tempType;
                    }
                }
                return this.varEnumType ?? default(VarEnum);
            }
        }

        /// <summary>
        ///     プロパティ値の型を取得します。
        /// </summary>
        public Type ValueType
        {
            get
            {
                if (this.valueType == null)
                {
                    this.valueType = ShellPropertyFactory.VarEnumToSystemType(this.VarEnumType);
                }
                return this.valueType;
            }
        }

        /// <summary>
        ///     プロパティ表示種別を取得します。
        /// </summary>
        public PropertyDisplayTypes DisplayType
        {
            get
            {
                if (this.PropertyDescriptionNative != null && this.displayType == null)
                {
                    PROPDESC_DISPLAYTYPE tempDisplayType;
                    var hr = this.PropertyDescriptionNative.GetDisplayType(out tempDisplayType);
                    if (HRESULT.Succeeded(hr))
                    {
                        this.displayType = (PropertyDisplayTypes)tempDisplayType;
                    }
                }
                return this.displayType ?? default(PropertyDisplayTypes);
            }
        }

        /// <summary>
        ///     カラムの幅を取得します。
        /// </summary>
        public uint DefaultColumWidth
        {
            get
            {
                if (this.PropertyDescriptionNative != null && !this.defaultColumWidth.HasValue)
                {
                    uint tempDefaultColumWidth;
                    var hr = this.PropertyDescriptionNative.GetDefaultColumnWidth(out tempDefaultColumWidth);
                    if (HRESULT.Succeeded(hr))
                    {
                        this.defaultColumWidth = tempDefaultColumWidth;
                    }
                }
                return this.defaultColumWidth ?? default(uint);
            }
        }

        /// <summary>
        ///     複数アイテムの表示種別を取得します。
        /// </summary>
        public PropertyAggregationTypes AggregationTypes
        {
            get
            {
                if (this.PropertyDescriptionNative != null && this.aggregationTypes == null)
                {
                    PROPDESC_AGGREGATION_TYPE tempAggregationTypes;
                    var hr = this.PropertyDescriptionNative.GetAggregationType(out tempAggregationTypes);
                    if (HRESULT.Succeeded(hr))
                    {
                        this.aggregationTypes = (PropertyAggregationTypes)tempAggregationTypes;
                    }
                }
                return this.aggregationTypes ?? default(PropertyAggregationTypes);
            }
        }

        /// <summary>
        ///     プロパティ種別のコレクションを取得します。
        /// </summary>
        public ReadOnlyCollection<ShellPropertyEnumType> PropertyEnumTypes
        {
            get
            {
                Contract.Ensures(Contract.Result<ReadOnlyCollection<ShellPropertyEnumType>>() != null);
                if (this.PropertyDescriptionNative != null && this.propertyEnumTypes == null)
                {
                    var propEnumTypeList = new List<ShellPropertyEnumType>();

                    var guid = new Guid(PropertySystemIID.IPropertyEnumTypeList);
                    IPropertyEnumTypeList propertyEnumTypeList;
                    var hr = this.PropertyDescriptionNative.GetEnumTypeList(ref guid, out propertyEnumTypeList);
                    if (propertyEnumTypeList != null && HRESULT.Succeeded(hr))
                    {
                        uint count;
                        propertyEnumTypeList.GetCount(out count);
                        guid = new Guid(PropertySystemIID.IPropertyEnumType);

                        for (uint i = 0; i < count; i++)
                        {
                            IPropertyEnumType nativeEnumType;
                            propertyEnumTypeList.GetAt(i, ref guid, out nativeEnumType);
                            propEnumTypeList.Add(new ShellPropertyEnumType(nativeEnumType));
                        }
                    }

                    this.propertyEnumTypes = new ReadOnlyCollection<ShellPropertyEnumType>(propEnumTypeList);
                }
                return this.propertyEnumTypes;
            }
        }

        /// <summary>
        ///     カラム状態を取得します。
        /// </summary>
        public PropertyColumnStates ColumnState
        {
            get
            {
                if (this.PropertyDescriptionNative != null && this.columnState == null)
                {
                    SHCOLSTATE state;
                    var hr = this.PropertyDescriptionNative.GetColumnState(out state);
                    if (HRESULT.Succeeded(hr))
                    {
                        this.columnState = (PropertyColumnStates)state;
                    }
                }
                return this.columnState ?? default(PropertyColumnStates);
            }
        }

        public PropertyConditionTypes ConditionType
        {
            get
            {
                if (this.PropertyDescriptionNative != null && this.conditionType == null)
                {
                    PROPDESC_CONDITION_TYPE tempConditionType;
                    CONDITION_OPERATION tempConditionOperation;
                    var hr = this.PropertyDescriptionNative.GetConditionType(out tempConditionType, out tempConditionOperation);
                    if (HRESULT.Succeeded(hr))
                    {
                        conditionOperation = (PropertyConditionOperations)tempConditionOperation;
                        this.conditionType = (PropertyConditionTypes)tempConditionType;
                    }
                }
                return this.conditionType ?? default(PropertyConditionTypes);
            }
        }

        public PropertyConditionOperations ConditionOperation
        {
            get
            {
                if (this.PropertyDescriptionNative != null && this.conditionOperation == null)
                {
                    PROPDESC_CONDITION_TYPE tempConditionType;
                    CONDITION_OPERATION tempConditionOperation;
                    var hr = this.PropertyDescriptionNative.GetConditionType(out tempConditionType, out tempConditionOperation);
                    if (HRESULT.Succeeded(hr))
                    {
                        this.conditionOperation = (PropertyConditionOperations)tempConditionOperation;
                        this.conditionType = (PropertyConditionTypes)tempConditionType;
                    }
                }

                return this.conditionOperation ?? default(PropertyConditionOperations);
            }
        }

        public PropertyGroupingRange GroupingRange
        {
            get
            {
                if (this.PropertyDescriptionNative != null && this.groupingRange == null)
                {
                    PROPDESC_GROUPING_RANGE tempGroupingRange;
                    var hr = this.PropertyDescriptionNative.GetGroupingRange(out tempGroupingRange);
                    if (HRESULT.Succeeded(hr))
                    {
                        this.groupingRange = (PropertyGroupingRange)tempGroupingRange;
                    }
                }
                return this.groupingRange ?? default(PropertyGroupingRange);
            }
        }

        public PropertySortDescription SortDescription
        {
            get
            {
                if (this.PropertyDescriptionNative != null && this.sortDescription == null)
                {
                    PROPDESC_SORTDESCRIPTION tempSortDescription;
                    var hr = this.PropertyDescriptionNative.GetSortDescription(out tempSortDescription);
                    if (HRESULT.Succeeded(hr))
                    {
                        this.sortDescription = (PropertySortDescription)tempSortDescription;
                    }
                }

                return this.sortDescription ?? default(PropertySortDescription);
            }
        }

        public PropertyTypeFlags TypeFlags
        {
            get
            {
                if (this.PropertyDescriptionNative != null && this.propertyTypeFlags == null)
                {
                    PROPDESC_TYPE_FLAGS tempFlags;
                    var hr = this.PropertyDescriptionNative.GetTypeFlags(PROPDESC_TYPE_FLAGS.PDTF_MASK_ALL, out tempFlags);
                    this.propertyTypeFlags = HRESULT.Succeeded(hr) ? (PropertyTypeFlags)tempFlags : default(PropertyTypeFlags);
                }
                return this.propertyTypeFlags ?? default(PropertyTypeFlags);
            }
        }

        public PropertyViewFlags ViewFlags
        {
            get
            {
                if (this.PropertyDescriptionNative != null && this.propertyViewFlags == null)
                {
                    PROPDESC_VIEW_FLAGS tempFlags;
                    var hr = this.PropertyDescriptionNative.GetViewFlags(out tempFlags);
                    this.propertyViewFlags = HRESULT.Succeeded(hr) ? (PropertyViewFlags)tempFlags : default(PropertyViewFlags);
                }
                return this.propertyViewFlags ?? default(PropertyViewFlags);
            }
        }

        public bool HasSystemDescription => this.PropertyDescriptionNative != null;

        public string GetSortDescriptionLabel(bool descending)
        {
            Contract.Ensures(Contract.Result<string>() != null);

            var result = string.Empty;
            if (this.PropertyDescriptionNative != null)
            {
                IntPtr ptr;
                var hr = this.PropertyDescriptionNative.GetSortDescriptionLabel(descending, out ptr);
                if (HRESULT.Succeeded(hr) && ptr != IntPtr.Zero)
                {
                    result = Marshal.PtrToStringUni(ptr);
                    Marshal.FreeCoTaskMem(ptr);
                }
            }
            return result;
        }

        internal string FormatForDisplay(PropVariant propVariant, PropertyDescriptionFormatFlags formatFlags)
        {
            var flags = (PROPDESC_FORMAT_FLAGS)formatFlags;
            string result;
            var hr = this.PropertyDescriptionNative.FormatForDisplay(propVariant, ref flags, out result);
            if (HRESULT.Failed(hr))
            {
                throw ShellException.FromHRESULT(hr);
            }
            return result;
        }

        internal bool TryFormatForDisplay(PropVariant propVariant, PropertyDescriptionFormatFlags formatFlags, out string result)
        {
            var flags = (PROPDESC_FORMAT_FLAGS)formatFlags;
            var hr = this.PropertyDescriptionNative.FormatForDisplay(propVariant, ref flags, out result);
            if (HRESULT.Failed(hr))
            {
                result = null;
                return false;
            }
            return true;
        }

        internal string GetImageReferencePath(PropVariant propVariant)
        {
            string result;
            ((IPropertyDescription2)this.PropertyDescriptionNative).GetImageReferenceForValue(propVariant, out result);
            return result;
        }
    }
}