using System;
using System.Diagnostics.CodeAnalysis;

namespace starshipxac.Shell.Interop.Library
{
    /// <summary>
    ///     Defines options for filtering folder items. 
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/dd378448(v=vs.85).aspx
    /// </remarks>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal enum LIBRARYFOLDERFILTER
    {
        /// <summary>
        ///     Return only file system items. 
        /// </summary>
        LFF_FORCEFILESYSTEM = 1,

        /// <summary>
        ///     Return items that can be bound to an IStorage object. 
        /// </summary>
        LFF_STORAGEITEMS = 2,

        /// <summary>
        ///     Return all items. 
        /// </summary>
        LFF_ALLITEMS = 3
    };
}