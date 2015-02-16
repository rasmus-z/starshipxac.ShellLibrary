using System;
using System.Runtime.InteropServices;

namespace starshipxac.Shell.Interop
{
    [ComImport]
    [ClassInterface(ClassInterfaceType.None)]
    [TypeLibType(TypeLibTypeFlags.FCanCreate)]
    [Guid(ShellCLSID.ShellLibrary)]
    internal class ShellLibraryCoClass
    {
    }

    [ComImport]
    [Guid(ShellCLSID.ShellLink)]
    [ClassInterface(ClassInterfaceType.None)]
    internal class ShellLinkCoClass
    {
    }

    [ComImport]
    [ClassInterface(ClassInterfaceType.None)]
    [TypeLibType(TypeLibTypeFlags.FCanCreate)]
    [Guid(ShellCLSID.SearchFolderItemFactory)]
    internal class SearchFolderItemFactoryCoClass
    {
    }
}