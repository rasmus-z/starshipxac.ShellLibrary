﻿using System;
using starshipxac.Windows.Shell.Dialogs.Interop;

namespace starshipxac.Windows.Shell.Dialogs
{
    /// <summary>
    ///     Define file dialog options.
    /// </summary>
    [Flags]
    public enum FileDialogOptions : uint
    {
        None = 0,

        SelectFolers = FILEOPENDIALOGOPTIONS.FOS_PICKFOLDERS,

        MultiSelect = FILEOPENDIALOGOPTIONS.FOS_ALLOWMULTISELECT,
        ExpandMode = FILEOPENDIALOGOPTIONS.FOS_DEFAULTNOMINIMODE,
        AppendDefaultExtension = FILEOPENDIALOGOPTIONS.FOS_STRICTFILETYPES,

        EnsureReadOnly = FILEOPENDIALOGOPTIONS.FOS_NOREADONLYRETURN,
        PathMustExist = FILEOPENDIALOGOPTIONS.FOS_PATHMUSTEXIST,
        FileMustExist = FILEOPENDIALOGOPTIONS.FOS_FILEMUSTEXIST,

        OverwritePrompt = FILEOPENDIALOGOPTIONS.FOS_OVERWRITEPROMPT,
        CreatePrompt = FILEOPENDIALOGOPTIONS.FOS_CREATEPROMPT,

        RestoreDirectory = FILEOPENDIALOGOPTIONS.FOS_NOCHANGEDIR,

        ForceFileSystem = FILEOPENDIALOGOPTIONS.FOS_FORCEFILESYSTEM,
        AllNonStotageItems = FILEOPENDIALOGOPTIONS.FOS_ALLNONSTORAGEITEMS,

        ShowPlacesList = FILEOPENDIALOGOPTIONS.FOS_HIDEPINNEDPLACES,
        ShowHiddenItems = FILEOPENDIALOGOPTIONS.FOS_FORCESHOWHIDDEN,
        NavigateToShortcut = FILEOPENDIALOGOPTIONS.FOS_NODEREFERENCELINKS,
    }
}