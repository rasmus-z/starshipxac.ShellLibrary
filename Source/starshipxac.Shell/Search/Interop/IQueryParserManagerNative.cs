using System;
using System.Runtime.InteropServices;

namespace starshipxac.Shell.Search.Interop
{
	[ComImport]
	[Guid(SearchIID.IQueryParserManager)]
	[CoClass(typeof(QueryParserManagerCoClass))]
	internal interface IQueryParserManagerNative : IQueryParserManager
	{
	}
}