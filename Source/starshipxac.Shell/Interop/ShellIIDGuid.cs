using System;

namespace starshipxac.Shell.Interop
{
    internal static class ShellIIDGuid
    {
        public static Guid IShellItem2 = new Guid(ShellIID.IShellItem2);

        public static Guid IShellFolder = new Guid(ShellIID.IShellFolder);

        public static Guid IStream = new Guid(ShellIID.IStream);

        public static Guid IStreamAsync = new Guid(ShellIID.IStreamAsync);
    }
}