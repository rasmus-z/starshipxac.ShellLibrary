using System;
using System.Diagnostics.Contracts;
using starshipxac.Shell.Interop;
using starshipxac.Shell.Properties;
using starshipxac.Shell.PropertySystem.Interop;

namespace starshipxac.Shell.PropertySystem
{
    /// <summary>
    /// プロパティキー情報を定義します。
    /// </summary>
    public class ShellPropertyKey : IEquatable<ShellPropertyKey>
    {
        private PROPERTYKEY propertyKeyNative;
        private string canonicalName;

        public ShellPropertyKey(string formatId, UInt32 propId)
            : this(new Guid(formatId), propId)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(formatId));
        }

        public ShellPropertyKey(Guid formatId, UInt32 propId)
        {
            this.propertyKeyNative = new PROPERTYKEY(formatId, propId);
            this.canonicalName = null;
        }

        internal ShellPropertyKey(PROPERTYKEY propertyKey)
        {
            this.propertyKeyNative = propertyKey;
            this.canonicalName = null;
        }

        internal ShellPropertyKey(PROPERTYKEY propertyKeyNative, string canonicalName)
        {
            this.propertyKeyNative = propertyKeyNative;
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

        internal PROPERTYKEY PropertyKeyNative => this.propertyKeyNative;

        /// <summary>
        /// プロパティ名を取得します。
        /// </summary>
        public string Name
        {
            get
            {
                if (this.canonicalName == null)
                {
                    PropertySystemNativeMethods.PSGetNameFromPropertyKey(ref this.propertyKeyNative, out this.canonicalName);
                    if (this.canonicalName == null)
                    {
                        this.canonicalName = String.Empty;
                    }
                }
                return this.canonicalName;
            }
        }

        /// <summary>
        /// プロパティの<c>FormatID</c>を取得します。
        /// </summary>
        public Guid FormatId => this.propertyKeyNative.fmtid;

        /// <summary>
        /// プロパティの<c>PropID</c>を取得します。
        /// </summary>
        public UInt32 PropertyId => this.propertyKeyNative.pid;

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
            return this.propertyKeyNative == other.propertyKeyNative;
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