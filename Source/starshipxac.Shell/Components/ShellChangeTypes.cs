using System;

namespace starshipxac.Shell.Components
{
    /// <summary>
    ///     Define shell change notification event types.
    /// </summary>
    [Flags]
    public enum ShellChangeTypes : uint
    {
        /// <summary>
        ///     None.
        /// </summary>
        None = 0,

        /// <summary>
        ///     Rename.
        /// </summary>
        ItemRename = 0x00000001,

        /// <summary>
        ///     Create.
        /// </summary>
        ItemCreate = 0x00000002,

        /// <summary>
        ///     Delete.
        /// </summary>
        ItemDelete = 0x00000004,

        /// <summary>
        ///     Create directory.
        /// </summary>
        DirectoryCreate = 0x00000008,

        /// <summary>
        ///     Delete directory.
        /// </summary>
        DirectoryDelete = 0x00000010,

        /// <summary>
        ///     Insert media.
        /// </summary>
        MediaInsert = 0x00000020,

        /// <summary>
        ///     Remove media.
        /// </summary>
        MediaRemove = 0x00000040,

        /// <summary>
        ///     Remove drive.
        /// </summary>
        DriveRemove = 0x00000080,

        /// <summary>
        ///     Add drive.
        /// </summary>
        DriveAdd = 0x00000100,

        /// <summary>
        ///     Net share.
        /// </summary>
        NetShare = 0x00000200,

        /// <summary>
        ///     Net unshare.
        /// </summary>
        NetUnshare = 0x00000400,

        /// <summary>
        ///     Change attributes.
        /// </summary>
        AttributesChange = 0x00000800,

        /// <summary>
        ///     Update directory contents.
        /// </summary>
        DirectoryContentsUpdate = 0x00001000,

        /// <summary>
        ///     Update.
        /// </summary>
        Update = 0x00002000,

        /// <summary>
        ///     Disconnect server.
        /// </summary>
        ServerDisconnect = 0x00004000,

        /// <summary>
        ///     Update system image.
        /// </summary>
        SystemImageUpdate = 0x00008000,

        /// <summary>
        ///     Rename directory.
        /// </summary>
        DirectoryRename = 0x00020000,

        /// <summary>
        ///     Free space.
        /// </summary>
        FreeSpace = 0x00040000,

        /// <summary>
        ///     Exntended event.
        /// </summary>
        ExtendedEvent = 0x04000000,

        /// <summary>
        ///     Change association.
        /// </summary>
        AssociationChange = 0x08000000,

        /// <summary>
        ///     Disk events mask.
        /// </summary>
        DiskEventsMask = 0x0002381F,

        /// <summary>
        ///     Global events mask.
        /// </summary>
        GlobalEventsMask = 0x0C0581E0,

        /// <summary>
        ///     All events mask.
        /// </summary>
        AllEventsMask = 0x7FFFFFFF,

        /// <summary>
        ///     System event mask.
        /// </summary>
        FromInterrupt = 0x80000000
    }
}