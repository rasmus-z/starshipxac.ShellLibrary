using System;
using System.Diagnostics.Contracts;

namespace starshipxac.Shell
{
    public sealed class ShellSearchConnector : ShellSearchCollection
    {
        internal ShellSearchConnector(ShellItem shellItem)
            : base(shellItem)
        {
            Contract.Requires<ArgumentNullException>(shellItem != null);
        }

        internal static readonly string FileExtension = ".searchconnector-ms";
    }
}