using System;
using System.Runtime.InteropServices;

namespace starshipxac.Shell.Interop
{
	/// <summary>
	/// シェルアイテムイメージファクトリインターフェイスを定義します。
	/// </summary>
	/// <remarks>
	/// http://msdn.microsoft.com/en-us/library/windows/desktop/bb761084(v=vs.85).aspx
	/// </remarks>
	[ComImport]
	[Guid("bcc18b79-ba16-442f-80c4-8a59c30c463b")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IShellItemImageFactory
	{
		[PreserveSig]
		HRESULT GetImage(
			[In] [MarshalAs(UnmanagedType.Struct)] SIZE size,
			[In] SIIGBF flags,
			[Out] out IntPtr phbm);
	}
}