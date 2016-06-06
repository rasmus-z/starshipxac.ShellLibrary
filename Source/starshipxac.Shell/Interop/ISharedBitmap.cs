using System;
using System.Runtime.InteropServices;

namespace starshipxac.Shell.Interop
{
    /// <summary>
    ///     ビットマップアクセスインターフェイスを定義します。
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/bb775173(v=vs.85).aspx
    /// </remarks>
    [ComImport]
    [Guid(ShellIID.ISharedBitmap)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface ISharedBitmap
    {
        void GetSharedBitmap([Out] out IntPtr phbm);

        void GetSize([Out] out SIZE p);

        //void GetFormat([Out] out ThumbnailAlphaType pat);

        //void InitializeBitmap([In] IntPtr hbm, [In] ThumbnailAlphaType wtsAT);

        void Detach([Out] out IntPtr phbm);
    }
}