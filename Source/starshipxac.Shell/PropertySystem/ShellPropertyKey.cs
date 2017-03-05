using System;
using System.Diagnostics.Contracts;
using starshipxac.Shell.Interop;
using starshipxac.Shell.Properties;
using starshipxac.Shell.PropertySystem.Interop;

namespace starshipxac.Shell.PropertySystem
{
    /// <summary>
    ///     Define property key.
    /// </summary>
    public class ShellPropertyKey : IEquatable<ShellPropertyKey>
    {
        private string canonicalName;

        public ShellPropertyKey(string formatId, UInt32 propId)
            : this(new Guid(formatId), propId)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(formatId));
        }

        public ShellPropertyKey(Guid formatId, UInt32 propId)
        {
            this.PropertyKeyNative = new PROPERTYKEY(formatId, propId);
            this.canonicalName = null;
        }

        internal ShellPropertyKey(PROPERTYKEY propertyKey)
        {
            this.PropertyKeyNative = propertyKey;
            this.canonicalName = null;
        }

        internal ShellPropertyKey(PROPERTYKEY propertyKeyNative, string canonicalName)
        {
            this.PropertyKeyNative = propertyKeyNative;
            this.canonicalName = canonicalName;
        }

        public static ShellPropertyKey FromCanonicalName(string canonicalName)
        {
            PROPERTYKEY propertyKey;
            var hr = PropertySystemNativeMethods.PSGetPropertyKeyFromName(canonicalName, out propertyKey);
            if (HRESULT.Failed(hr))
            {
                throw new ArgumentException(
                    ErrorMessages.ShellInvalidCanonicalName,
                    ShellException.FromHRESULT(hr));
            }

            return new ShellPropertyKey(propertyKey, canonicalName);
        }

        internal PROPERTYKEY PropertyKeyNative;

        /// <summary>
        ///     Get the property name.
        /// </summary>
        public string Name
        {
            get
            {
                if (this.canonicalName == null)
                {
                    PropertySystemNativeMethods.PSGetNameFromPropertyKey(ref this.PropertyKeyNative, out this.canonicalName);
                    if (this.canonicalName == null)
                    {
                        this.canonicalName = String.Empty;
                    }
                }
                return this.canonicalName;
            }
        }

        /// <summary>
        ///     Get the property's <c>FormatID</c>.
        /// </summary>
        public Guid FormatId => this.PropertyKeyNative.fmtid;

        /// <summary>
        ///     Get the property's <c>PropID</c>.
        /// </summary>
        public UInt32 PropertyId => this.PropertyKeyNative.pid;

        public static bool operator ==(ShellPropertyKey left, ShellPropertyKey right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ShellPropertyKey left, ShellPropertyKey right)
        {
            return !Equals(left, right);
        }

        public bool Equals(ShellPropertyKey other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return this.PropertyKeyNative == other.PropertyKeyNative;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (this.GetType() != obj.GetType())
            {
                return false;
            }
            return Equals((ShellPropertyKey)obj);
        }

        public override int GetHashCode()
        {
            return this.FormatId.GetHashCode() ^ this.PropertyId.GetHashCode();
        }

        public override string ToString()
        {
            return $"{{Name: {Name}, FormatId: {FormatId}, PropertyId: {PropertyId}}}";
        }
    }
}