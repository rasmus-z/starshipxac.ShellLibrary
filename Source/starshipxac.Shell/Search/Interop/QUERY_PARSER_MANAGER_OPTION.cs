using System;

namespace starshipxac.Shell.Search.Interop
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// http://msdn.microsoft.com/en-us/library/windows/desktop/aa965700(v=vs.85).aspx
    /// </remarks>
    internal enum QUERY_PARSER_MANAGER_OPTION
    {
        QPMO_SCHEMA_BINARY_NAME = 0,
        QPMO_PRELOCALIZED_SCHEMA_BINARY_PATH = (QPMO_SCHEMA_BINARY_NAME + 1),
        QPMO_UNLOCALIZED_SCHEMA_BINARY_PATH = (QPMO_PRELOCALIZED_SCHEMA_BINARY_PATH + 1),
        QPMO_LOCALIZED_SCHEMA_BINARY_PATH = (QPMO_UNLOCALIZED_SCHEMA_BINARY_PATH + 1),
        QPMO_APPEND_LCID_TO_LOCALIZED_PATH = (QPMO_LOCALIZED_SCHEMA_BINARY_PATH + 1),
        QPMO_LOCALIZER_SUPPORT = (QPMO_APPEND_LCID_TO_LOCALIZED_PATH + 1)
    }
}