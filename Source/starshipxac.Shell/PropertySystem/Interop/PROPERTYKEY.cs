using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace starshipxac.Shell.PropertySystem.Interop
{
    /// <summary>
    ///     Specifies the FMTID/PID identifier that programmatically identifies a property. Replaces <c>SHCOLUMNID</c>.
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/bb773381(v=vs.85).aspx
    /// </remarks>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public struct PROPERTYKEY : IEquatable<PROPERTYKEY>
    {
        public PROPERTYKEY(Guid formatId, UInt32 propId)
            : this()
        {
            this.fmtid = formatId;
            this.pid = propId;
        }

        /// <summary>
        ///     A unique GUID for the property.
        /// </summary>
        public Guid fmtid { get; }

        /// <summary>
        ///     A property identifier (PID).
        ///     This parameter is not used as in <c>SHCOLUMNID</c>.
        ///     It is recommended that you set this value to <c>PID_FIRST_USABLE</c>.
        ///     Any value greater than or equal to 2 is acceptable.
        /// </summary>
        public UInt32 pid { get; }

        public bool Equals(PROPERTYKEY other)
        {
            return this.fmtid.Equals(other.fmtid) && (this.pid == other.pid);
        }

        public override bool Equals(object obj)
        {
            var result = false;
            if (obj is PROPERTYKEY)
            {
                var other = (PROPERTYKEY)obj;
                result = this.Equals(other);
            }
            return result;
        }

        public static bool operator ==(PROPERTYKEY left, PROPERTYKEY right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(PROPERTYKEY left, PROPERTYKEY right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            return this.fmtid.GetHashCode() ^ this.pid.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "PROPERTYKEY: {{fmtid={0}, pid={1}}}",
                this.fmtid.ToString("B"), this.pid);
        }
    }
}