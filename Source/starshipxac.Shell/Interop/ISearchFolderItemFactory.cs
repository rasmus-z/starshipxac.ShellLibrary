using System;
using System.Runtime.InteropServices;
using starshipxac.Shell.PropertySystem.Interop;
using starshipxac.Shell.Search.Interop;

namespace starshipxac.Shell.Interop
{
	/// <summary>
	/// フォルダー検索アイテムファクトリインターフェイスを定義します。
	/// </summary>
	/// <remarks>
	/// http://msdn.microsoft.com/en-us/library/windows/desktop/bb775176(v=vs.85).aspx
	/// </remarks>
	[ComImport]
	[Guid(ShellIID.ISearchFolderItemFactory)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface ISearchFolderItemFactory
	{
		[PreserveSig]
		HRESULT SetDisplayName([In] [MarshalAs(UnmanagedType.LPWStr)] string pszDisplayName);

		[PreserveSig]
		HRESULT SetFolderTypeID([In] Guid ftid);

		[PreserveSig]
		HRESULT SetFolderLogicalViewMode([In] FOLDERLOGICALVIEWMODE flvm);

		[PreserveSig]
		HRESULT SetIconSize([In] int iIconSize);

		[PreserveSig]
		HRESULT SetVisibleColumns([In] uint cVisibleColumns, [In] [MarshalAs(UnmanagedType.LPArray)] PROPERTYKEY[] rgKey);

		[PreserveSig]
		HRESULT SetSortColumns([In] uint cSortColumns, [In] [MarshalAs(UnmanagedType.LPArray)] SORTCOLUMN[] rgSortColumns);

		[PreserveSig]
		HRESULT SetGroupColumn([In] ref PROPERTYKEY keyGroup);

		[PreserveSig]
		HRESULT SetStacks([In] uint cStackKeys, [In] [MarshalAs(UnmanagedType.LPArray)] PROPERTYKEY[] rgStackKeys);

		[PreserveSig]
		HRESULT SetScope([In] [MarshalAs(UnmanagedType.Interface)] IShellItemArray ppv);

		[PreserveSig]
		HRESULT SetCondition([In] ICondition pCondition);

		[PreserveSig]
		HRESULT GetShellItem(ref Guid riid, [Out] [MarshalAs(UnmanagedType.Interface)] out IShellItem ppv);

		[PreserveSig]
		HRESULT GetIDList([Out] IntPtr ppidl);
	};
}