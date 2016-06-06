using System;
using System.Diagnostics.CodeAnalysis;

namespace starshipxac.Shell.Interop
{
    /// <summary>
    ///     ストレージ作成モードを定義します。
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/aa380337(v=vs.85).aspx
    /// </remarks>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal static class STGM
    {
        // Access

        public const UInt32 STGM_READ = 0x00000000;

        public const UInt32 STGM_WRITE = 0x00000001;

        public const UInt32 STGM_READWRITE = 0x00000002;

        // Sharing

        public const UInt32 STGM_SHARE_DENY_NONE = 0x00000040;

        public const UInt32 STGM_SHARE_DENY_READ = 0x00000030;

        public const UInt32 STGM_SHARE_DENY_WRITE = 0x00000020;

        public const UInt32 STGM_SHARE_DENY_EXCLUSIVE = 0x00000010;

        public const UInt32 STGM_SHARE_DENY_PRIORITY = 0x00040000;

        // Creation

        public const UInt32 STGM_CREATE = 0x00001000;

        public const UInt32 STGM_CONVERT = 0x00020000;

        public const UInt32 STGM_FAILIFTHERE = 0x00000000;

        // Transactioning

        public const UInt32 STGM_DIRECT = 0x00000000;

        public const UInt32 STGM_TRANSACTED = 0x00010000;

        // Transactioning Performance

        public const UInt32 STGM_NOSCRATCH = 0x00100000;

        public const UInt32 STGM_NOSNAPSHOT = 0x00200000;

        // Direct SWMR and Simple

        public const UInt32 STGM_SIMPLE = 0x08000000;

        public const UInt32 STGM_DIRECT_SWMR = 0x00400000;

        // Delete On Release

        public const UInt32 STGM_DELETEONRELEASE = 0x04000000;
    };
}