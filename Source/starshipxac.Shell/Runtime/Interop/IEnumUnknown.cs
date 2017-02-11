using System;
using System.Runtime.InteropServices;
using starshipxac.Shell.Interop;

namespace starshipxac.Shell.Runtime.Interop
{
    /// <summary>
    ///     Enumerates objects with the IUnknown interface.
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/ms683764(v=vs.85).aspx
    /// </remarks>
    [ComImport]
    [Guid(ComIID.IEnumUnknown)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IEnumUnknown
    {
        /// <summary>
        ///     Retrieves the specified number of items in the enumeration sequence.
        /// </summary>
        /// <param name="requestedNumber"></param>
        /// <param name="buffer"></param>
        /// <param name="fetchedNumber"></param>
        /// <returns></returns>
        [PreserveSig]
        HRESULT Next(UInt32 requestedNumber, ref IntPtr buffer, ref UInt32 fetchedNumber);

        /// <summary>
        ///     Skips over the specified number of items in the enumeration sequence.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        [PreserveSig]
        HRESULT Skip(UInt32 number);

        /// <summary>
        ///     Resets the enumeration sequence to the beginning.
        /// </summary>
        /// <returns></returns>
        [PreserveSig]
        HRESULT Reset();

        /// <summary>
        ///     Creates a new enumerator that contains the same enumeration state as the current one.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        [PreserveSig]
        HRESULT Clone(out IEnumUnknown result);
    }
}