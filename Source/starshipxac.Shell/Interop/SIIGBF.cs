using System;
using System.Diagnostics.CodeAnalysis;

namespace starshipxac.Shell.Interop
{
    /// <summary>
    ///     <see cref="IShellItemImageFactory.GetImage(SIZE, SIIGBF, out IntPtr)"/> flags.
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/bb761082(v=vs.85).aspx
    /// </remarks>
    [Flags]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal enum SIIGBF
    {
        /// <summary>
        ///     Shrink the bitmap as necessary to fit, preserving its aspect ratio.
        /// </summary>
        SIIGBF_RESIZETOFIT = 0x00000000,

        /// <summary>
        ///     Passed by callers if they want to stretch the returned image themselves.
        ///     For example, if the caller passes an icon size of 80x80, a 96x96 thumbnail could be returned.
        ///     This action can be used as a performance optimization if the caller expects that they will need to stretch the image.
        ///     Note that the Shell implementation of <see cref="IShellItemImageFactory"/> performs a GDI stretch blit.
        ///     If the caller wants a higher quality image stretch than provided through that mechanism, they should pass this flag and perform the stretch themselves.
        /// </summary>
        SIIGBF_BIGGERSIZEOK = 0x00000001,

        /// <summary>
        ///     Return the item only if it is already in memory.
        ///     Do not access the disk even if the item is cached.
        ///     Note that this only returns an already-cached icon and can fall back to a per-class icon if an item has a per-instance icon that has not been cached.
        ///     Retrieving a thumbnail, even if it is cached, always requires the disk to be accessed, so GetImage should not be called from the UI thread without passing <see cref="SIIGBF_MEMORYONLY"/>.
        /// </summary>
        SIIGBF_MEMORYONLY = 0x00000002,

        /// <summary>
        ///     Return only the icon, never the thumbnail.
        /// </summary>
        SIIGBF_ICONONLY = 0x00000004,

        /// <summary>
        ///     Return only the thumbnail, never the icon.
        ///     Note that not all items have thumbnails, so <see cref="SIIGBF_THUMBNAILONLY"/> will cause the method to fail in these cases.
        /// </summary>
        SIIGBF_THUMBNAILONLY = 0x00000008,

        /// <summary>
        ///     Allows access to the disk, but only to retrieve a cached item.
        ///     This returns a cached thumbnail if it is available.
        ///     If no cached thumbnail is available, it returns a cached per-instance icon but does not extract a thumbnail or icon.
        /// </summary>
        SIIGBF_INCACHEONLY = 0x00000010,

        /// <summary>
        ///     If necessary, crop the bitmap to a square.
        /// </summary>
        SIIGBF_CROPTOSQUARE = 0x00000020,

        /// <summary>
        ///     Stretch and crop the bitmap to a 0.7 aspect ratio.
        /// </summary>
        SIIGBF_WIDETHUMBNAILS = 0x00000040,

        /// <summary>
        ///     If returning an icon, paint a background using the associated app's registered background color.
        /// </summary>
        SIIGBF_ICONBACKGROUND = 0x00000080,

        /// <summary>
        ///     If necessary, stretch the bitmap so that the height and width fit the given size.
        /// </summary>
        SIIGBF_SCALEUP = 0x00000100,
    }
}