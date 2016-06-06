using System;
using System.Runtime.InteropServices;

namespace starshipxac.Shell.Interop
{
    /// <summary>
    ///     サムネイルイメージ取得インターフェイスを定義します。
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/bb774614(v=vs.85).aspx
    /// </remarks>
    [ComImport]
    [Guid(ShellIID.IThumbnailProvider)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IThumbnailProvider
    {
        void GetThumbnail(
            UInt32 cx,
            out IntPtr phbmp,
            out WTS_ALPHATYPE pdwAlpha);
    }
}