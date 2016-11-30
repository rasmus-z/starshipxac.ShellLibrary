using System;
using System.Diagnostics.CodeAnalysis;

namespace starshipxac.Shell.Interop
{
    /// <summary>
    ///     The STGM constants are flags that indicate conditions for creating and deleting the object and access modes for the object.
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/aa380337(v=vs.85).aspx
    /// </remarks>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal static class STGM
    {
        // Access
        /// <summary>
        ///     Indicates that the object is read-only, meaning that modifications cannot be made.
        /// </summary>
        public const UInt32 STGM_READ = 0x00000000;

        /// <summary>
        ///     Enables you to save changes to the object, but does not permit access to its data.
        ///     The provided implementations of the <c>IPropertyStorage</c> and <c>IPropertySetStorage</c> interfaces do not support this write-only mode.
        /// </summary>
        public const UInt32 STGM_WRITE = 0x00000001;

        /// <summary>
        ///     Enables access and modification of object data.
        ///     For example, if a stream object is created or opened in this mode, it is possible to call both <c>IStream.Read</c> and <c>IStream.Write</c>.
        ///     Be aware that this constant is not a simple binary <c>OR</c> operation of the <see cref="STGM_WRITE"/> and <see cref="STGM_READ"/> elements.
        /// </summary>
        public const UInt32 STGM_READWRITE = 0x00000002;

        // Sharing
        /// <summary>
        ///     Specifies that subsequent openings of the object are not denied read or write access.
        ///     If no flag from the sharing group is specified, this flag is assumed.
        /// </summary>
        public const UInt32 STGM_SHARE_DENY_NONE = 0x00000040;

        /// <summary>
        ///     Prevents others from subsequently opening the object in <see cref="STGM_READ"/> mode.
        ///     It is typically used on a root storage object.
        /// </summary>
        public const UInt32 STGM_SHARE_DENY_READ = 0x00000030;

        /// <summary>
        ///     Prevents others from subsequently opening the object for <see cref="STGM_WRITE"/> or <see cref="STGM_READWRITE"/> access.
        ///     In transacted mode, sharing of <see cref="STGM_SHARE_DENY_WRITE"/> or <see cref="STGM_SHARE_EXCLUSIVE"/> can significantly improve performance because they do not require snapshots.
        /// </summary>
        public const UInt32 STGM_SHARE_DENY_WRITE = 0x00000020;

        /// <summary>
        ///     Prevents others from subsequently opening the object in any mode.
        ///     Be aware that this value is not a simple bitwise <c>OR</c> operation of the <see cref="STGM_SHARE_DENY_READ"/> and <see cref="STGM_SHARE_DENY_WRITE"/> values.
        ///     In transacted mode, sharing of <see cref="STGM_SHARE_DENY_WRITE"/> or <see cref="STGM_SHARE_EXCLUSIVE"/> can significantly improve performance because they do not require snapshots.
        /// </summary>
        public const UInt32 STGM_SHARE_EXCLUSIVE = 0x00000010;

        /// <summary>
        ///     Opens the storage object with exclusive access to the most recently committed version.
        ///     Thus, other users cannot commit changes to the object while you have it open in priority mode.
        ///     You gain performance benefits for copy operations, but you prevent others from committing changes.
        ///     Limit the time that objects are open in priority mode.
        ///     You must specify <see cref="STGM_DIRECT"/> and <see cref="STGM_READ"/> with priority mode, and you cannot specify <see cref="STGM_DELETEONRELEASE"/>.
        ///     <see cref="STGM_DELETEONRELEASE"/> is only valid when creating a root object, such as with <c>StgCreateStorageEx</c>.
        ///     It is not valid when opening an existing root object, such as with <c>StgOpenStorageEx</c>.
        ///     It is also not valid when creating or opening a subelement, such as with <c>IStorage.OpenStorage</c>.
        /// </summary>
        public const UInt32 STGM_PRIORITY = 0x00040000;

        // Creation
        /// <summary>
        ///     Indicates that an existing storage object or stream should be removed before the new object replaces it.
        ///     A new object is created when this flag is specified only if the existing object has been successfully removed. 
        /// </summary>
        public const UInt32 STGM_CREATE = 0x00001000;

        /// <summary>
        ///     Creates the new object while preserving existing data in a stream named "Contents".
        ///     In the case of a storage object or a byte array, the old data is formatted into a stream regardless of whether the existing file or byte array currently contains a layered storage object.
        ///     This flag can only be used when creating a root storage object.
        ///     It cannot be used within a storage object;
        ///     for example, in IStorage::CreateStream.
        ///     It is also not valid to use this flag and the <see cref="STGM_DELETEONRELEASE"/> flag simultaneously.
        /// </summary>
        public const UInt32 STGM_CONVERT = 0x00020000;

        /// <summary>
        ///     Causes the create operation to fail if an existing object with the specified name exists.
        ///     In this case, <c>STG_E_FILEALREADYEXISTS</c> is returned.
        ///     This is the default creation mode;
        ///     that is, if no other create flag is specified, <see cref="STGM_FAILIFTHERE"/> is implied.
        /// </summary>
        public const UInt32 STGM_FAILIFTHERE = 0x00000000;

        // Transactioning
        /// <summary>
        ///     Indicates that, in direct mode, each change to a storage or stream element is written as it occurs.
        ///     This is the default if neither <see cref="STGM_DIRECT"/> nor <see cref="STGM_TRANSACTED"/> is specified.
        /// </summary>
        public const UInt32 STGM_DIRECT = 0x00000000;

        /// <summary>
        ///     Indicates that, in transacted mode, changes are buffered and written only if an explicit commit operation is called.
        ///     To ignore the changes, call the Revert method in the <c>IStream</c>, <c>IStorage</c>, or <c>IPropertyStorage</c> interface.
        ///     The COM compound file implementation of <c>IStorage</c> does not support transacted streams, which means that streams can be opened only in direct mode, and you cannot revert changes to them, however transacted storages are supported.
        ///     The compound file, stand-alone, and NTFS file system implementations of <c>IPropertySetStorage</c> similarly do not support transacted, simple property sets because these property sets are stored in streams.
        ///     However, transactioning of nonsimple property sets, which can be created by specifying the <c>PROPSETFLAG_NONSIMPLE</c> flag in the grfFlags parameter of <c>IPropertySetStorage.Create</c>, are supported.
        /// </summary>
        public const UInt32 STGM_TRANSACTED = 0x00010000;

        // Transactioning Performance
        /// <summary>
        ///     Indicates that, in transacted mode, a temporary scratch file is usually used to save modifications until the Commit method is called.
        ///     Specifying <see cref="STGM_NOSCRATCH"/> permits the unused portion of the original file to be used as work space instead of creating a new file for that purpose.
        ///     This does not affect the data in the original file, and in certain cases can result in improved performance.
        ///     It is not valid to specify this flag without also specifying <see cref="STGM_TRANSACTED"/>, and this flag may only be used in a root open.
        ///     For more information about NoScratch mode, see the Remarks section.
        /// </summary>
        public const UInt32 STGM_NOSCRATCH = 0x00100000;

        /// <summary>
        ///     This flag is used when opening a storage object with <see cref="STGM_TRANSACTED"/> and without <see cref="STGM_SHARE_EXCLUSIVE"/> or <see cref="STGM_SHARE_DENY_WRITE"/>.
        ///     In this case, specifying <see cref="STGM_NOSNAPSHOT"/> prevents the system-provided implementation from creating a snapshot copy of the file.
        ///     Instead, changes to the file are written to the end of the file.
        ///     Unused space is not reclaimed unless consolidation is performed during the commit, and there is only one current writer on the file.
        ///     When the file is opened in no snapshot mode, another open operation cannot be performed without specifying <see cref="STGM_NOSNAPSHOT"/>.
        ///     This flag may only be used in a root open operation.
        ///     For more information about NoSnapshot mode, see the Remarks section.
        /// </summary>
        public const UInt32 STGM_NOSNAPSHOT = 0x00200000;

        // Direct SWMR and Simple
        /// <summary>
        ///     Provides a faster implementation of a compound file in a limited, but frequently used, case.
        /// </summary>
        public const UInt32 STGM_SIMPLE = 0x08000000;

        /// <summary>
        ///     Supports direct mode for single-writer, multireader file operations.
        /// </summary>
        public const UInt32 STGM_DIRECT_SWMR = 0x00400000;

        // Delete On Release
        /// <summary>
        ///     Indicates that the underlying file is to be automatically destroyed when the root storage object is released.
        ///     This feature is most useful for creating temporary files.
        ///     This flag can only be used when creating a root object, such as with <c>StgCreateStorageEx</c>.
        ///     It is not valid when opening a root object, such as with <c>StgOpenStorageEx</c>, or when creating or opening a subelement, such as with <c>IStorage.CreateStream</c>.
        ///     It is also not valid to use this flag and the <see cref="STGM_CONVERT"/> flag simultaneously.
        /// </summary>
        public const UInt32 STGM_DELETEONRELEASE = 0x04000000;
    };
}