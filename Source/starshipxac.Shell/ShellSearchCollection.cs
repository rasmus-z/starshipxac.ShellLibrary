using System;
using System.Diagnostics.Contracts;

namespace starshipxac.Shell
{
	public class ShellSearchCollection : ShellFolder
	{
		internal ShellSearchCollection(ShellItem shellItem)
			: base(shellItem)
		{
			Contract.Requires<ArgumentNullException>(shellItem != null);
		}
	}
}