using System;
using System.Diagnostics.Contracts;
using starshipxac.Shell.PropertySystem;

namespace starshipxac.Shell
{
    public class SortColumn
    {
        public SortColumn(ShellPropertyKey propertyKey, SortDirection direction)
        {
            Contract.Requires<ArgumentNullException>(propertyKey != null);

            this.PropertyKey = propertyKey;
            this.Direction = direction;
        }

        public ShellPropertyKey PropertyKey { get; }

        public SortDirection Direction { get; }

        public static bool operator ==(SortColumn col1, SortColumn col2)
        {
            if (ReferenceEquals(col1, col2))
            {
                return true;
            }
            if (ReferenceEquals(col1, null))
            {
                return false;
            }
            if (ReferenceEquals(col2, null))
            {
                return false;
            }

            return (col1.Direction == col2.Direction) && (col1.PropertyKey == col2.PropertyKey);
        }

        public static bool operator !=(SortColumn col1, SortColumn col2)
        {
            return !(col1 == col2);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(SortColumn))
            {
                return false;
            }
            return this == (SortColumn)obj;
        }

        public override int GetHashCode()
        {
            return this.Direction.GetHashCode() ^ this.PropertyKey.GetHashCode();
        }
    }
}