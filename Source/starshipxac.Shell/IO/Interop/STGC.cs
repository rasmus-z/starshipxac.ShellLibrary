using System;
using System.Diagnostics.CodeAnalysis;

namespace starshipxac.Shell.IO.Interop
{
    /// <summary>
    ///     The STGC enumeration constants specify the conditions for performing the commit operation
    ///     in the <c>IStorage.Commit</c> and <c>IStream.Commit</c> methods.
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/aa380320(v=vs.85).aspx
    /// </remarks>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal enum STGC
    {
        STGC_DEFAULT = 0,
        STGC_OVERWRITE = 1,
        STGC_ONLYIFCURRENT = 2,
        STGC_DANGEROUSLYCOMMITMERELYTODISKCACHE = 4,
        STGC_CONSOLIDATE = 8
    }
}