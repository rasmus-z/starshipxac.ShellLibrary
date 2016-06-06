using System;
using System.Diagnostics.CodeAnalysis;

namespace starshipxac.Windows.Shell.Dialogs.Interop
{
    /// <summary>
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/bb762504(v=vs.85).aspx
    /// </remarks>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal enum FDE_OVERWRITE_RESPONSE
    {
        FDEOR_DEFAULT = 0,
        FDEOR_ACCEPT = 1,
        FDEOR_REFUSE = 2,
    }
}