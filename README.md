starshipxac.ShellLibrary
==========================

Windowsの [Shell API](https://msdn.microsoft.com/en-us/library/windows/desktop/ee663298) , IFileDialogなどを .NETFrameworkで使用できるライブラリです。

動作環境:

- Windows7/8.1
- .NETFramework4.5

## ライブラリ

- **starshipxac.Shell**
  - <code>ShellFile</code>: ファイルの情報を保持します。
  - <code>ShellFolder</code>: フォルダーの情報を保持します。
  - <code>ShellLibrary</code>: Windows7より使用できる「ライブラリフォルダー」の情報を保持します。
  - <code>ShellKnownFolder</code>: デスクトップ、ドキュメント、ダウンロードなどの標準フォルダーの情報を保持します。


- **starshipxac.Shell.Windows**
  - starshipxac.Shell.Windows.Dialogs
    - <code>OpenFileSelector</code>: 開くファイルを選択するダイアログ。
    - <code>SaveFileSelector</code>: 保存するファイルを選択するダイアログ。
    - <code>FolderSelector</code>: フォルダーを選択するダイアログ。
  - starshipxac.Shell.Media.Imaging
    - <code>ShellThumbnail</code>: ファイルやフォルダーのアイコン/サムネイル画像を取得します。
