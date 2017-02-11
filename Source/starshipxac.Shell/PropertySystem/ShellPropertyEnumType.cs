using System;
using System.Diagnostics.Contracts;
using starshipxac.Shell.PropertySystem.Interop;

namespace starshipxac.Shell.PropertySystem
{
    /// <summary>
    ///     Define property enumeration type.
    /// </summary>
    public class ShellPropertyEnumType
    {
        private string displayText;
        private PROPENUMTYPE? enumType;
        private object minValue;
        private object setValue;
        private object enumerationValue;

        internal ShellPropertyEnumType(IPropertyEnumType propertyEnumTypeNative)
        {
            Contract.Requires<ArgumentNullException>(propertyEnumTypeNative != null);

            this.PropertyEnumTypeNative = propertyEnumTypeNative;
        }

        private IPropertyEnumType PropertyEnumTypeNative { get; }

        /// <summary>
        ///     Get the display text.
        /// </summary>
        public string DisplayText
        {
            get
            {
                if (this.displayText == null)
                {
                    this.PropertyEnumTypeNative.GetDisplayText(out this.displayText);
                }
                return this.displayText;
            }
        }

        /// <summary>
        ///     Get the numeration type.
        /// </summary>
        internal PROPENUMTYPE EnumType
        {
            get
            {
                if (!this.enumType.HasValue)
                {
                    PROPENUMTYPE tempEnumType;
                    this.PropertyEnumTypeNative.GetEnumType(out tempEnumType);
                    this.enumType = tempEnumType;
                }
                return this.enumType.Value;
            }
        }

        /// <summary>
        ///     Get the range of minimal value.
        /// </summary>
        public object RangeMinValue
        {
            get
            {
                if (this.minValue == null)
                {
                    using (var propVar = new PropVariant())
                    {
                        this.PropertyEnumTypeNative.GetRangeMinValue(propVar);
                        this.minValue = propVar.Value;
                    }
                }
                return this.minValue;
            }
        }

        /// <summary>
        ///     Get the ranget set value.
        /// </summary>
        public object RangeSetValue
        {
            get
            {
                if (this.setValue == null)
                {
                    using (var propVar = new PropVariant())
                    {
                        this.PropertyEnumTypeNative.GetRangeSetValue(propVar);
                        this.setValue = propVar.Value;
                    }
                }
                return this.setValue;
            }
        }

        /// <summary>
        ///     Get the range value.
        /// </summary>
        public object RangeValue
        {
            get
            {
                if (this.enumerationValue == null)
                {
                    using (var propVar = new PropVariant())
                    {
                        this.PropertyEnumTypeNative.GetValue(propVar);
                        this.enumerationValue = propVar.Value;
                    }
                }
                return this.enumerationValue;
            }
        }
    }
}