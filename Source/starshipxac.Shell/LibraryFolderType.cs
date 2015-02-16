using System;
using starshipxac.Shell.Interop;

namespace starshipxac.Shell
{
    /// <summary>
    /// ���C�u�����t�H���_�[��ʂ��`���܂��B
    /// </summary>
    public static class LibraryFolderType
    {
        /// <summary>
        /// �S��
        /// </summary>
        public static Guid Generic
        {
            get
            {
                return KnownFolderTypes.FOLDERTYPEID_Generic;
            }
        }

        /// <summary>
        /// �h�L�������g�t�H���_�[
        /// </summary>
        public static Guid Documents
        {
            get
            {
                return KnownFolderTypes.FOLDERTYPEID_Documents;
            }
        }

        /// <summary>
        /// �~���[�W�b�N�t�H���_�[
        /// </summary>
        public static Guid Music
        {
            get
            {
                return KnownFolderTypes.FOLDERTYPEID_Music;
            }
        }

        /// <summary>
        /// �s�N�`���[�t�H���_�[
        /// </summary>
        public static Guid Pictures
        {
            get
            {
                return KnownFolderTypes.FOLDERTYPEID_Pictures;
            }
        }

        /// <summary>
        /// �r�f�I�t�H���_�[
        /// </summary>
        public static Guid Videos
        {
            get
            {
                return KnownFolderTypes.FOLDERTYPEID_Videos;
            }
        }
    }
}