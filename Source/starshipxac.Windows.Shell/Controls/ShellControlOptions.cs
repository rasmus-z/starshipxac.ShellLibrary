using System;
using starshipxac.Shell.Interop;

namespace starshipxac.Windows.Shell.Controls
{
	[Flags]
	public enum ShellControlOptions : uint
	{
		CheckingForChildren = SHCONTF.SHCONTF_CHECKING_FOR_CHILDREN,
		Folders = SHCONTF.SHCONTF_FOLDERS,
		NonFolders = SHCONTF.SHCONTF_NONFOLDERS,
		IncludeHidden = SHCONTF.SHCONTF_INCLUDEHIDDEN,
		InitOnFirstNext = SHCONTF.SHCONTF_INIT_ON_FIRST_NEXT,
		NetPrinterSearch = SHCONTF.SHCONTF_NETPRINTERSRCH,
		Shareable = SHCONTF.SHCONTF_SHAREABLE,
		Storage = SHCONTF.SHCONTF_STORAGE,
		NavigationEnumeration = SHCONTF.SHCONTF_NAVIGATION_ENUM,
		FastItems = SHCONTF.SHCONTF_FASTITEMS,
		FlatList = SHCONTF.SHCONTF_FLATLIST,
		EnableAsync = SHCONTF.SHCONTF_ENABLE_ASYNC,
		IncludeSuperHidden = SHCONTF.SHCONTF_INCLUDESUPERHIDDEN,
	}
}