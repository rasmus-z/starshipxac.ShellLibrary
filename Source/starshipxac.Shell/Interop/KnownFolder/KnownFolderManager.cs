using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace starshipxac.Shell.Interop.KnownFolder
{
	[ComImport]
	[Guid(ShellCLSID.KnownFolderManager)]
	internal class KnownFolderManager : IKnownFolderManager
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		public virtual extern void FolderIdFromCsidl(int csidl,
			[Out] out Guid knownFolderID);

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		public virtual extern void FolderIdToCsidl(
			[In] [MarshalAs(UnmanagedType.LPStruct)] Guid id,
			[Out] out int csidl);

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		public virtual extern void GetFolderIds(
			[Out] out IntPtr folders,
			[Out] out UInt32 count);

		[PreserveSig]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		public virtual extern HRESULT GetFolder(
			[In] [MarshalAs(UnmanagedType.LPStruct)] Guid id,
			[Out] [MarshalAs(UnmanagedType.Interface)] out IKnownFolder knownFolder);

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		public virtual extern void GetFolderByName(
			string canonicalName,
			[Out] [MarshalAs(UnmanagedType.Interface)] out IKnownFolder knownFolder);

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		public virtual extern void RegisterFolder(
			[In] [MarshalAs(UnmanagedType.LPStruct)] Guid knownFolderGuid,
			[In] ref KNOWNFOLDER_DEFINITION pKFD);

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		public virtual extern void UnregisterFolder(
			[In] [MarshalAs(UnmanagedType.LPStruct)] Guid knownFolderGuid);

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		public virtual extern void FindFolderFromPath(
			[In] [MarshalAs(UnmanagedType.LPWStr)] string path,
			[In] int mode,
			[Out] [MarshalAs(UnmanagedType.Interface)] out IKnownFolder knownFolder);

		[PreserveSig]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		public virtual extern HRESULT FindFolderFromIDList(IntPtr pidl,
			[Out] [MarshalAs(UnmanagedType.Interface)] out IKnownFolder knownFolder);

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		public virtual extern void Redirect();
	}
}