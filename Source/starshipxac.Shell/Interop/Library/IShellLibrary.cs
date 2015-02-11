using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace starshipxac.Shell.Interop.Library
{
	/// <summary>
	/// シェルライブラリインターフェイスを定義します。
	/// </summary>
	/// <remarks>
	/// http://msdn.microsoft.com/en-us/library/windows/desktop/dd391719(v=vs.85).aspx
	/// </remarks>
	[ComImport]
	[Guid(ShellIID.IShellLibrary)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IShellLibrary
	{
		[PreserveSig]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		HRESULT LoadLibraryFromItem(
			[In] [MarshalAs(UnmanagedType.Interface)] IShellItem library,
			[In] UInt32 grfMode);

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void LoadLibraryFromKnownFolder(
			[In] ref Guid knownfidLibrary,
			[In] UInt32 grfMode);

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void AddFolder([In] [MarshalAs(UnmanagedType.Interface)] IShellItem location);

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void RemoveFolder([In] [MarshalAs(UnmanagedType.Interface)] IShellItem location);

		[PreserveSig]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		HRESULT GetFolders(
			[In] LIBRARYFOLDERFILTER lff,
			[In] ref Guid riid,
			[MarshalAs(UnmanagedType.Interface)] out IShellItemArray ppv);

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void ResolveFolder(
			[In] [MarshalAs(UnmanagedType.Interface)] IShellItem folderToResolve,
			[In] uint timeout,
			[In] ref Guid riid,
			[MarshalAs(UnmanagedType.Interface)] out IShellItem ppv);

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void GetDefaultSaveFolder(
			[In] DEFAULTSAVEFOLDERTYPE dsft,
			[In] ref Guid riid,
			[MarshalAs(UnmanagedType.Interface)] out IShellItem ppv);

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void SetDefaultSaveFolder(
			[In] DEFAULTSAVEFOLDERTYPE dsft,
			[In] [MarshalAs(UnmanagedType.Interface)] IShellItem si);

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void GetOptions(
			out LIBRARYOPTIONFLAGS lofOptions);

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void SetOptions(
			[In] LIBRARYOPTIONFLAGS lofMask,
			[In] LIBRARYOPTIONFLAGS lofOptions);

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void GetFolderType(out Guid ftid);

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void SetFolderType([In] ref Guid ftid);

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void GetIcon([MarshalAs(UnmanagedType.LPWStr)] out string icon);

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void SetIcon([In] [MarshalAs(UnmanagedType.LPWStr)] string icon);

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void Commit();

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void Save(
			[In] [MarshalAs(UnmanagedType.Interface)] IShellItem folderToSaveIn,
			[In] [MarshalAs(UnmanagedType.LPWStr)] string libraryName,
			[In] LIBRARYSAVEFLAGS lsf,
			[MarshalAs(UnmanagedType.Interface)] out IShellItem2 savedTo);

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void SaveInKnownFolder(
			[In] ref Guid kfidToSaveIn,
			[In] [MarshalAs(UnmanagedType.LPWStr)] string libraryName,
			[In] LIBRARYSAVEFLAGS lsf,
			[MarshalAs(UnmanagedType.Interface)] out IShellItem2 savedTo);
	};
}