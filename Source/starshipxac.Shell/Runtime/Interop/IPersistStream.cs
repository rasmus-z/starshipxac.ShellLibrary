using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using starshipxac.Shell.Interop;

namespace starshipxac.Shell.Runtime.Interop
{
    /// <summary>
    ///     Enables the saving and loading of objects that use a simple serial stream for their storage needs.
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/ms690091(v=vs.85).aspx
    /// </remarks>
    [ComImport]
    [Guid(ComIID.IPersistStream)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IPersistStream
    {
        [PreserveSig]
        void GetClassID(out Guid pClassID);

        /// <summary>
        ///     Determines whether an object has changed since it was last saved to its stream.
        /// </summary>
        /// <returns></returns>
        [PreserveSig]
        HRESULT IsDirty();

        /// <summary>
        ///     Initializes an object from the stream where it was saved previously.
        /// </summary>
        /// <param name="stm"></param>
        /// <returns></returns>
        [PreserveSig]
        HRESULT Load([In] [MarshalAs(UnmanagedType.Interface)] IStream stm);

        /// <summary>
        ///     Saves an object to the specified stream.
        /// </summary>
        /// <param name="stm"></param>
        /// <param name="fRemember"></param>
        /// <returns></returns>
        [PreserveSig]
        HRESULT Save([In] [MarshalAs(UnmanagedType.Interface)] IStream stm, bool fRemember);

        /// <summary>
        ///     Retrieves the size of the stream needed to save the object.
        /// </summary>
        /// <param name="cbSize"></param>
        /// <returns></returns>
        [PreserveSig]
        HRESULT GetSizeMax(out ulong cbSize);
    }
}