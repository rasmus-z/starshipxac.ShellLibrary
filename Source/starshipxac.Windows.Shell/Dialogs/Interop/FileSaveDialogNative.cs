using System;
using System.Runtime.InteropServices;
using starshipxac.Shell.Interop;

namespace starshipxac.Windows.Shell.Dialogs.Interop
{
    [ComImport]
    [Guid(ShellIID.IFileSaveDialog)]
    [CoClass(typeof(FileSaveDialogCoClass))]
    internal interface FileSaveDialogNative : IFileSaveDialog
    {
    }
}