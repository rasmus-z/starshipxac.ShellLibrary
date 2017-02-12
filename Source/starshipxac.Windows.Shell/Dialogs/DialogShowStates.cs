using System;

namespace starshipxac.Windows.Shell.Dialogs
{
    /// <summary>
    ///     Define dialog show states.
    /// </summary>
    public enum DialogShowStates
    {
        /// <summary>
        ///     Previous show dialog.
        /// </summary>
        PreShow,

        /// <summary>
        ///     Dialog showing.
        /// </summary>
        Showing,

        /// <summary>
        ///     Dialog closing.
        /// </summary>
        Closing,

        /// <summary>
        ///     Dialog closed.
        /// </summary>
        Closed
    }
}