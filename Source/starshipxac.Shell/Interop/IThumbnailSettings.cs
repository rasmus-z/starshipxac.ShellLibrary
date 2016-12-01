using System;
using System.Runtime.InteropServices;

namespace starshipxac.Shell.Interop
{
    /// <summary>
    ///     Provides a method that enables a thumbnail provider to determine the user context of a thumbnail request.
    /// </summary>
    /// <remarks>
    ///     https://msdn.microsoft.com/en-us/library/windows/desktop/hh707043(v=vs.85).aspx
    /// </remarks>
    [ComImport]
    [Guid(ShellIID.IThumbnailSettings)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IThumbnailSettings
    {
        /// <summary>
        ///     Enables a thumbnail provider to return a thumbnail specific to the user's context.
        /// </summary>
        /// <param name="dwContext"></param>
        /// <returns></returns>
        HRESULT SetContext(
            [In] WTS_CONTEXTFLAGS dwContext);
    }
}
