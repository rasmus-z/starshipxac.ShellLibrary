[Japanese](README-ja.md)

starshipxac.ShellLibrary
==========================

This library that can be used with .NET Framework such as Windows [Shell API](https://msdn.microsoft.com/en-us/library/windows/desktop/ee663298), IFileDialog and so on.

## Operating environment

- Windows7/8.1/10
- .NETFramework4.5 or later


## Libraries

### starshipxac.Shell

- `ShellFile`: Define file information.
- `ShellFolder`: Define folder information.
- `ShellLibrary`: It holds the information of "library folder" that can be used from Windows 7.
- `ShellKnownFolder`: It holds information on standard folders such as desktop, document, download, etc.

### starshipxac.Windows.Shell

- starshipxac.Windows.Shell.Dialogs
  - `OpenFileSelector`: Dialog to select the file to open.
  - `SaveFileSelector`: Dialog for selecting files to save.
  - `FolderSelector`: Dialog for selecting a folder.

- starshipxac.Shell.Media.Imaging
  - `ShellThumbnail`: Get icon / thumbnail images of files and folders.


## Samples

### ShellExplorerSample

Tree view of folder and item list in folder.

### ShellFileDialogSample

Open file, Save As, Folder Selection and Display Custom File Dialog.

### ShellKnownFoldersSample

List of standard folders.


