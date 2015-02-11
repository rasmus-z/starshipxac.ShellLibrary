using System;
using System.Runtime.InteropServices;

namespace starshipxac.Shell.Interop
{
	/// <summary>
	/// サムネイルキャッシュインターフェイスを定義します。
	/// </summary>
	/// <remarks>
	/// http://msdn.microsoft.com/en-us/library/windows/desktop/bb774628(v=vs.85).aspx
	/// </remarks>
	[ComImport]
	[Guid(ShellIID.IThumbnailCache)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IThumbnailCache
	{
		void GetThumbnail(
			[In] IShellItem pShellItem,
			[In] uint cxyRequestedThumbSize,
			[In] WTS_FLAGS flags,
			[Out] out ISharedBitmap ppvThumb,
			[Out] out WTS_CACHEFLAGS pOutFlags,
			[Out] WTS_THUMBNAILID pThumbnailID);

		void GetThumbnailByID(
			[In] WTS_THUMBNAILID thumbnailID,
			[In] uint cxyRequestedThumbSize,
			[Out] out ISharedBitmap ppvThumb,
			[Out] out WTS_CACHEFLAGS pOutFlags);
	}
}