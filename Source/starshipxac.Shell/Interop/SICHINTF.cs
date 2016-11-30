using System;
using System.Diagnostics.CodeAnalysis;

namespace starshipxac.Shell.Interop
{
    /// <summary>
    ///     Used to determine how to compare two Shell items.
    ///     <see cref="IShellItem.Compare(IShellItem, SICHINTF, out int)"/> uses this enumerated type.
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/bb762543(v=vs.85).aspx
    /// </remarks>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal enum SICHINTF
    {
        /// <summary>
        ///     This relates to the iOrder parameter of the <see cref="IShellItem.Compare(IShellItem, SICHINTF, out int)"/> interface and
        ///     indicates that the comparison is based on the display in a folder view.
        /// </summary>
        SICHINT_DISPLAY = 0x00000000,

        /// <summary>
        ///     Exact comparison of two instances of a Shell item.
        /// </summary>
        SICHINT_ALLFIELDS = unchecked((int)0x80000000),

        /// <summary>
        ///     This relates to the iOrder parameter of the <see cref="IShellItem.Compare(IShellItem, SICHINTF, out int)"/> interface and
        ///     indicates that the comparison is based on a canonical name.
        /// </summary>
        SICHINT_CANONICAL = 0x10000000,

        /// <summary>
        ///     If the Shell items are not the same, test the file system paths.
        /// </summary>
        SICHINT_TEST_FILESYSPATH_IF_NOT_EQUAL = 0x20000000,
    }
}