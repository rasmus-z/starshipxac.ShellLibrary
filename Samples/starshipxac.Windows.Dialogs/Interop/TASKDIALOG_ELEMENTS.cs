using System;
using System.Diagnostics.CodeAnalysis;

namespace starshipxac.Windows.Dialogs.Interop
{
    /// <summary>
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/bb787513(v=vs.85).aspx
    /// </remarks>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal enum TASKDIALOG_ELEMENTS : uint
    {
        TDE_CONTENT,
        TDE_EXPANDED_INFORMATION,
        TDE_FOOTER,
        TDE_MAIN_INSTRUCTION
    }
}