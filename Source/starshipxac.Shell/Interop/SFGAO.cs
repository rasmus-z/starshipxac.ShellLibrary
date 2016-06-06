using System;
using System.Diagnostics.CodeAnalysis;

namespace starshipxac.Shell.Interop
{
    /// <summary>
    ///     シェルアイテム属性を定義します。
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/bb762589(v=vs.85).aspx
    /// </remarks>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal static class SFGAO
    {
        public static UInt32 SFGAO_CANCOPY = 0x00000001;

        public static UInt32 SFGAO_CANMOVE = 0x00000002;

        public static UInt32 SFGAO_CANLINK = 0x00000004;

        public static UInt32 SFGAO_STORAGE = 0x00000008;

        public static UInt32 SFGAO_CANRENAME = 0x00000010;

        public static UInt32 SFGAO_CANDELETE = 0x00000020;

        public static UInt32 SFGAO_HASPROPERTYSHEET = 0x00000040;

        public static UInt32 SFGAO_DROPTARGET = 0x00000100;

        public static UInt32 SFGAO_CAPABILITYMASK = 0x00000177;

        public static UInt32 SFGAO_SYSTEM = 0x00001000;

        public static UInt32 SFGAO_ENCRYPTED = 0x00002000;

        public static UInt32 SFGAO_ISSLOW = 0x00004000;

        public static UInt32 SFGAO_GHOSTED = 0x00008000;

        public static UInt32 SFGAO_LINK = 0x00010000;

        public static UInt32 SFGAO_SHARE = 0x00020000;

        public static UInt32 SFGAO_READONLY = 0x00040000;

        public static UInt32 SFGAO_HIDDEN = 0x00080000;

        public static UInt32 SFGAO_DISPLAYATTRMASK = 0x000FC000;

        public static UInt32 SFGAO_NONENUMERATED = 0x00100000;

        public static UInt32 SFGAO_NEWCONTENT = 0x00200000;

        public static UInt32 SFGAO_CANMONIKER = 0x00400000;

        public static UInt32 SFGAO_HASSTORAGE = 0x00400000;

        public static UInt32 SFGAO_STREAM = 0x00400000;

        public static UInt32 SFGAO_STORAGEANCESTOR = 0x00800000;

        public static UInt32 SFGAO_VALIDATE = 0x01000000;

        public static UInt32 SFGAO_REMOVABLE = 0x02000000;

        public static UInt32 SFGAO_COMPRESSED = 0x04000000;

        public static UInt32 SFGAO_BROWSABLE = 0x08000000;

        public static UInt32 SFGAO_FILESYSANCESTOR = 0x10000000;

        public static UInt32 SFGAO_FOLDER = 0x20000000;

        public static UInt32 SFGAO_FILESYSTEM = 0x40000000;

        public static UInt32 SFGAO_STORAGECAPMASK = 0x70C50008;

        public static UInt32 SFGAO_HASSUBFOLDER = 0x80000000;

        public static UInt32 SFGAO_CONTENTSMASK = 0x80000000;

        public static UInt32 SFGAO_PKEYSFGAOMASK = 0x81044000;
    }
}