using System;

namespace starshipxac.Shell.Search.Interop
{
	/// <summary>
	/// 
	/// </summary>
	/// <remarks>
	/// http://msdn.microsoft.com/en-us/library/windows/desktop/aa965708(v=vs.85).aspx
	/// </remarks>
	internal enum STRUCTURED_QUERY_SINGLE_OPTION
	{
		SQSO_SCHEMA = 0,
		SQSO_LOCALE_WORD_BREAKING = (SQSO_SCHEMA + 1),
		SQSO_WORD_BREAKER = (SQSO_LOCALE_WORD_BREAKING + 1),
		SQSO_NATURAL_SYNTAX = (SQSO_WORD_BREAKER + 1),
		SQSO_AUTOMATIC_WILDCARD = (SQSO_NATURAL_SYNTAX + 1),
		SQSO_TRACE_LEVEL = (SQSO_AUTOMATIC_WILDCARD + 1),
		SQSO_LANGUAGE_KEYWORDS = (SQSO_TRACE_LEVEL + 1),
		SQSO_SYNTAX = (SQSO_LANGUAGE_KEYWORDS + 1),
		SQSO_TIME_ZONE = (SQSO_SYNTAX + 1),
		SQSO_IMPLICIT_CONNECTOR = (SQSO_TIME_ZONE + 1),
		SQSO_CONNECTOR_CASE = (SQSO_IMPLICIT_CONNECTOR + 1)
	}
}