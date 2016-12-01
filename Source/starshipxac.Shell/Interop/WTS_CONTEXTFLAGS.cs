using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace starshipxac.Shell.Interop
{
    /// <summary>
    ///     Specifies the context of a thumbnail extraction.
    ///     Used by <c>IThumbnailSettings::SetContext</c>.
    /// </summary>
    /// <remarks>
    ///     https://msdn.microsoft.com/en-us/library/windows/desktop/hh707151(v=vs.85).aspx
    /// </remarks>
    public enum WTS_CONTEXTFLAGS
    {
        /// <summary>
        ///     None of the following options are set.
        ///     Set in response to <see cref="WTS_FLAGS.WTS_NONE."/>.
        /// </summary>
        WTSCF_DEFAULT = 0x00000000,

        /// <summary>
        ///     Provide a thumbnail suitable to the Windows Store app UX guidelines.
        ///     Set in response to <see cref="WTS_FLAGS.WTS_APPSTYLE"/>.
        /// </summary>
        WTSCF_APPSTYLE = 0x00000001,

        /// <summary>
        ///     If necessary, crop the bitmap's dimensions so that is square.
        ///     The length of the shortest side becomes the length of all sides.
        ///     Set in response to <see cref="WTS_FLAGS.WTS_CROPTOSQUARE"/>.
        /// </summary>
        WTSCF_SQUARE = 0x00000002,

        /// <summary>
        ///     Stretch and crop the bitmap so that its height is 0.7 times its width.
        ///     Set in response to <see cref="WTS_FLAGS.WTS_WIDETHUMBNAILS"/>.
        /// </summary>
        WTSCF_WIDE = 0x00000004,

        /// <summary>
        ///     If not cached, only extract the thumbnail if it is embedded in EXIF format, typically 96x96.
        ///     Set in response to <see cref="WTS_FLAGS.WTS_FASTEXTRACT"/>.
        /// </summary>
        WTSCF_FAST = 0x00000008,
    }
}
