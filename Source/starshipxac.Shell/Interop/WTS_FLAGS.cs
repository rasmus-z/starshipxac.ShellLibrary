using System;
using System.Diagnostics.CodeAnalysis;

namespace starshipxac.Shell.Interop
{
    /// <summary>
    ///     Values used by <see cref="IThumbnailCache.GetThumbnail(IShellItem, uint, WTS_FLAGS, out ISharedBitmap, out WTS_CACHEFLAGS, WTS_THUMBNAILID)"/> to specify options for the extraction and display of the thumbnail image.
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/hh707152(v=vs.85).aspx
    /// </remarks>
    [Flags]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal enum WTS_FLAGS
    {
        /// <summary>
        ///     None of the following options are set.
        /// </summary>
        WTS_NONE = 0,

        /// <summary>
        ///     Extract the thumbnail if it is not cached.
        /// </summary>
        WTS_EXTRACT = 0,

        /// <summary>
        ///     Only return the thumbnail if it is cached.
        /// </summary>
        WTS_INCACHEONLY = 0x1,

        /// <summary>
        ///     If not cached, only extract the thumbnail if it is embedded in EXIF format, typically 96x96.
        /// </summary>
        WTS_FASTEXTRACT = 0x2,

        /// <summary>
        ///     Ignore cache and extract thumbnail from source file.
        /// </summary>
        WTS_FORCEEXTRACTION = 0x4,

        /// <summary>
        ///     The thumbnail has an extended lifetime.
        ///     Use for volumes that might go offline, like non-fixed disks.
        /// </summary>
        WTS_SLOWRECLAIM = 0x8,

        /// <summary>
        ///     Extract but do not add the thumbnail to the cache.
        /// </summary>
        WTS_EXTRACTDONOTCACHE = 0x20,

        /// <summary>
        ///     If the specific thumbnail size requested in the <c>cxyRequestedThumbSize</c> parameter is not available, resize the thumbnail to the requested size.
        ///     If possible, a larger bitmap is reduced in scale, preserving its aspect ratio, to the width and height required.
        ///     If the only available cached thumbnail is smaller than the requested size, then it is scaled up using the nearest-neighbor algorithm.
        /// </summary>
        WTS_SCALETOREQUESTEDSIZE = 0x40,

        /// <summary>
        ///     Do not extract a thumbnail embedded in the metadata of an EXIF image.
        /// </summary>
        WTS_SKIPFASTEXTRACT = 0x80,

        /// <summary>
        ///     Ensures that the thumbnail handler is loaded in the same process as the caller.
        ///     When this flag is not specified, the handler is loaded into a surrogate process to protect the caller from unexpected crashes caused by the processing of the target file.
        ///     Use this value when debugging thumbnail extractors.
        /// </summary>
        WTS_EXTRACTINPROC = 0x100,

        /// <summary>
        ///     If necessary, crop the bitmap's dimensions so that is square.
        ///     The length of the shortest side becomes the length of all sides.
        /// </summary>
        WTS_CROPTOSQUARE = 0x200,

        /// <summary>
        ///     Create a surrogate for this instance of the cache rather than using the shared DLLHost surrogate.
        /// </summary>
        WTS_INSTANCESURROGATE = 0x400,

        /// <summary>
        ///     Require extractions to take place in the surrogate.
        /// </summary>
        WTS_REQUIRESURROGATE = 0x800,

        /// <summary>
        ///     Pass the <c>WTSCF_APPSTYLE</c> flag to <c>IThumbnailSettings::SetContext</c>, if the provider supports it. 
        /// </summary>
        WTS_APPSTYLE = 0x2000,

        /// <summary>
        ///     Stretch and crop the bitmap so that its height is 0.7 times its width.
        /// </summary>
        WTS_WIDETHUMBNAILS = 0x4000,

        /// <summary>
        ///     Return from the ideal cache snap size only.
        ///     The returned image might be larger, but it will be pulled from the correct cache entry. 
        /// </summary>
        WTS_IDEALCACHESIZEONLY = 0x8000,

        /// <summary>
        ///     If necessary, stretch the image so that the height and width fit the given size.
        /// </summary>
        WTS_SCALEUP = 0x10000
    }
}