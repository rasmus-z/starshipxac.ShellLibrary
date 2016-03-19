using System;

namespace starshipxac.Windows.Shell.FileSystem
{
    [Flags]
    public enum TransferSourceFlags
    {
        Normal = 0,
        FailExist = 0,
        RenameExist = 0x1,
        OverwriteExist = 0x2,
        AllowDecryption = 0x4,
        NoSecurity = 0x8,
        CopyCreationTime = 0x10,
        CopyWriteTime = 0x20,
        UseFullAccess = 0x40,
        DeleteRecycleIfPossible = 0x80,
        CopyHardLink = 0x100,
        CopyLocalizedName = 0x200,
        MoveAsCopyDelete = 0x400,
        SuspendShellEvents = 0x800
    }
}