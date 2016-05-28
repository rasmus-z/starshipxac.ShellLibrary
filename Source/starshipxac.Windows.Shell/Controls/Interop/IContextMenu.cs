using System;
using System.Runtime.InteropServices;
using System.Text;
using starshipxac.Shell.Interop;

namespace starshipxac.Windows.Shell.Controls.Interop
{
    [ComImport]
    [Guid(ShellIID.IContextMenu)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IContextMenu
    {
        [PreserveSig]
        int QueryContextMenu(
            IntPtr hMenu,
            UInt32 indexMenu,
            Int32 idCmdFirst,
            Int32 idCmdLast,
            UInt32 uFlags);

        [PreserveSig]
        void InvokeCommand(IntPtr pici);

        [PreserveSig]
        void GetCommandString(
            UIntPtr idcmd,
            UInt32 uflags,
            UIntPtr reserved,
            StringBuilder pszName,
            UInt32 cchMax);
    }
}