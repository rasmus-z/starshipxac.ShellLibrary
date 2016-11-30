using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using starshipxac.Shell.PropertySystem.Interop;

namespace starshipxac.Shell.Interop
{
    /// <summary>
    ///     Stores information about how to sort a column that is displayed in the folder view.
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/bb759817(v=vs.85).aspx
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal struct SORTCOLUMN
    {
        public SORTCOLUMN(PROPERTYKEY propertyKey, SORTDIRECTION direction)
            : this()
        {
            this.propkey = propertyKey;
            this.direction = direction;
        }

        /// <summary>
        ///     The ID of the column by which the user will sort. A PROPERTYKEY structure.
        ///     For example, for the "Name" column, the property key is PKEY_ItemNameDisplay.
        /// </summary>
        public PROPERTYKEY propkey;

        /// <summary>
        ///     The direction in which the items are sorted. One of the following values.
        /// </summary>
        public SORTDIRECTION direction;
    }
}