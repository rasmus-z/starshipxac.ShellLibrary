using System;
using System.Diagnostics.CodeAnalysis;

namespace starshipxac.Shell.Runtime.Interop
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal static class ComIID
    {
        public const string IPersistStream = "00000109-0000-0000-C000-000000000046";
        public const string IPersist = "0000010c-0000-0000-C000-000000000046";
        public const string IEnumUnknown = "00000100-0000-0000-C000-000000000046";
    }
}