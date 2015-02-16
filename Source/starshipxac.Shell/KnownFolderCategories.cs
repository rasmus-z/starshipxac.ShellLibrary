using System;
using starshipxac.Shell.Interop.KnownFolder;

namespace starshipxac.Shell
{
    /// <summary>
    /// �W���t�H���_�[�J�e�S���[���`���܂��B
    /// </summary>
    public enum KnownFolderCategories
    {
        /// <summary>
        /// ����`�̃t�H���_�[�J�e�S���[�B
        /// </summary>
        None = 0,

        /// <summary>
        /// ���z�t�H���_�[�B
        /// </summary>
        Virtual = KF_CATEGORY.KF_CATEGORY_VIRTUAL,

        /// <summary>
        /// �Œ�t�H���_�[�B
        /// </summary>
        Fixed = KF_CATEGORY.KF_CATEGORY_FIXED,

        /// <summary>
        /// ���ʃt�H���_�[�B
        /// </summary>
        Common = KF_CATEGORY.KF_CATEGORY_COMMON,

        /// <summary>
        /// ���[�U�[���Ƃ̃t�H���_�[�B
        /// </summary>
        PerUser = KF_CATEGORY.KF_CATEGORY_PERUSER
    }
}