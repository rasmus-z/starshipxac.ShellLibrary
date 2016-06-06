using System;
using System.Diagnostics.CodeAnalysis;

namespace starshipxac.Shell.Interop
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal static class ShellIIDGuid
    {
        public static Guid IShellItem2 = new Guid(ShellIID.IShellItem2);

        public static Guid IShellFolder = new Guid(ShellIID.IShellFolder);

        public static Guid IStream = new Guid(ShellIID.IStream);

        public static Guid IStreamAsync = new Guid(ShellIID.IStreamAsync);
    }
}