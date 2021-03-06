﻿using System;
using System.Diagnostics.CodeAnalysis;
using starshipxac.Shell.Search.Interop;

namespace starshipxac.Shell.Search
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum QueryParserManagerOptions
    {
        SchemaBinaryName = QUERY_PARSER_MANAGER_OPTION.QPMO_SCHEMA_BINARY_NAME,

        PreLocalizedSchemaBinaryPath = QUERY_PARSER_MANAGER_OPTION.QPMO_PRELOCALIZED_SCHEMA_BINARY_PATH,

        UnlocalizedSchemaBinaryPath = QUERY_PARSER_MANAGER_OPTION.QPMO_UNLOCALIZED_SCHEMA_BINARY_PATH,

        LocalizedSchemaBinaryPath = QUERY_PARSER_MANAGER_OPTION.QPMO_UNLOCALIZED_SCHEMA_BINARY_PATH,

        AppendLCIDToLocalizedPath = QUERY_PARSER_MANAGER_OPTION.QPMO_APPEND_LCID_TO_LOCALIZED_PATH,

        LocalizerSupport = QUERY_PARSER_MANAGER_OPTION.QPMO_LOCALIZER_SUPPORT,
    }
}