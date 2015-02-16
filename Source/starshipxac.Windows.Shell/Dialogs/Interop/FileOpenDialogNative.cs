using System;
using System.Runtime.InteropServices;
using starshipxac.Shell.Interop;

namespace starshipxac.Windows.Shell.Dialogs.Interop
{
    [ComImport]
    [Guid(ShellIID.IFileOpenDialog)]
    [CoClass(typeof(FileOpenDialogCoClass))]
    internal interface FileOpenDialogNative : IFileOpenDialog
    {
    }
}