using System;
using System.Diagnostics.Contracts;

namespace starshipxac.Shell
{
    public class ShellSavedSearchCollection : ShellSearchCollection
    {
        internal ShellSavedSearchCollection(ShellItem shellItem)
            : base(shellItem)
        {
            Contract.Requires<ArgumentNullException>(shellItem != null);
        }

        internal static readonly string FileExtension = ".search-ms";
    }
}