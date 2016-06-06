using System;
using System.Diagnostics.CodeAnalysis;

namespace starshipxac.Shell.Search.Interop
{
    /// <summary>
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/aa965706(v=vs.85).aspx
    /// </remarks>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal enum STRUCTURED_QUERY_MULTIOPTION
    {
        SQMO_VIRTUAL_PROPERTY = 0,
        SQMO_DEFAULT_PROPERTY = (SQMO_VIRTUAL_PROPERTY + 1),
        SQMO_GENERATOR_FOR_TYPE = (SQMO_DEFAULT_PROPERTY + 1),
        SQMO_MAP_PROPERTY = (SQMO_GENERATOR_FOR_TYPE + 1)
    }
}