[English](README.md)

starshipxac.ShellLibrary
==========================

Windows�� [Shell API](https://msdn.microsoft.com/en-us/library/windows/desktop/ee663298) , IFileDialog�Ȃǂ� .NETFramework�Ŏg�p�ł��郉�C�u�����ł��B

�����:

- Windows7/8.1/10
- .NETFramework4.5�ȏ�


## ���C�u����

### starshipxac.Shell

- `ShellFile`: �t�@�C���̏���ێ����܂��B
- `ShellFolder`: �t�H���_�[�̏���ێ����܂��B
- `ShellLibrary`: Windows7���g�p�ł���u���C�u�����t�H���_�[�v�̏���ێ����܂��B
- `ShellKnownFolder`: �f�X�N�g�b�v�A�h�L�������g�A�_�E�����[�h�Ȃǂ̕W���t�H���_�[�̏���ێ����܂��B

### starshipxac.Shell.Windows

- starshipxac.Shell.Windows.Dialogs
  - `OpenFileSelector`: �J���t�@�C����I������_�C�A���O�B
    ![OpenFolderSelector dialog](Documents/Images/OpenFileSelector.png)
  - `SaveFileSelector`: �ۑ�����t�@�C����I������_�C�A���O�B
  - `FolderSelector`: �t�H���_�[��I������_�C�A���O�B
- starshipxac.Shell.Media.Imaging
  - `ShellThumbnail`: �t�@�C����t�H���_�[�̃A�C�R��/�T���l�C���摜���擾���܂��B
    ![Known Folders](Documents/Images/ShellKnownFoldersSample.png)


## �T���v��

### ShellExplorerSample

�t�H���_�[�̃c���[�\���ƁA�t�H���_�[���̃A�C�e���ꗗ�\���B

### ShellFileDialogSample

�t�@�C�����J���A���O�����ĕۑ��A�t�H���_�[�I������уJ�X�^���t�@�C���_�C�A���O�̕\���B

### ShellKnownFoldersSample

�W���t�H���_�[�̈ꗗ�B
