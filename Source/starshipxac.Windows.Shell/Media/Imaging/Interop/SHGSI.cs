using System;
using System.Diagnostics.CodeAnalysis;
using starshipxac.Shell.Interop;

namespace starshipxac.Windows.Shell.Media.Imaging.Interop
{
    /// <summary>
    ///     Define stock icon flags.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         http://msdn.microsoft.com/en-us/library/windows/desktop/bb762542(v=vs.85).aspx
    ///     </para>
    /// </remarks>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal static class SHGSI
    {
        /// <summary>
        ///     Get icon pass and index(Always enabled).
        /// </summary>
        public const UInt32 SHGSI_ICONLOCATION = 0;

        /// <summary>
        ///     Get icon.
        /// </summary>
        public const UInt32 SHGSI_ICON = SHGFI.SHGFI_ICON;

        /// <summary>
        ///     Get system icon index.
        /// </summary>
        public const UInt32 SHGSI_SYSICONINDEX = SHGFI.SHGFI_SYSICONINDEX;

        /// <summary>
        ///     Get link overlay.
        /// </summary>
        public const UInt32 SHGSI_LINKOVERLAY = SHGFI.SHGFI_LINKOVERLAY;

        /// <summary>
        ///     Get selected icon.
        /// </summary>
        public const UInt32 SHGSI_SELECTED = SHGFI.SHGFI_SELECTED;

        /// <summary>
        ///     Get large icon.
        /// </summary>
        public const UInt32 SHGSI_LARGEICON = SHGFI.SHGFI_LARGEICON;

        /// <summary>
        ///     Get small icon.
        /// </summary>
        public const UInt32 SHGSI_SMALLICON = SHGFI.SHGFI_SMALLICON;

        /// <summary>
        ///     Get shell icon size.
        /// </summary>
        public const UInt32 SHGSI_SHELLICONSIZE = SHGFI.SHGFI_SHELLICONSIZE;
    }
}