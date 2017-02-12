using System;

namespace starshipxac.Windows.Shell.Dialogs
{
    /// <summary>
    ///     Define file dialog result.
    /// </summary>
    public enum FileDialogResult
    {
        /// <summary>
        ///     Unexecuted state.
        /// </summary>
        None = 0,

        /// <summary>
        ///     OK or Save.
        /// </summary>
        Ok = 1,

        /// <summary>
        ///     Cancel.
        /// </summary>
        Cancel = 2,
    }
}