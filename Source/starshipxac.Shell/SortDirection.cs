using System;
using starshipxac.Shell.Interop;

namespace starshipxac.Shell
{
    /// <summary>
    /// �A�C�e���̃\�[�g�������`���܂��B
    /// </summary>
    public enum SortDirection
    {
        /// <summary>
        /// �f�t�H���g�̃\�[�g�����B
        /// </summary>
        Default = 0,

        /// <summary>
        /// �t���̃\�[�g�����B
        /// </summary>
        Descending = SORTDIRECTION.SORT_DESCENDING,

        /// <summary>
        /// �����̃\�[�g�����B
        /// </summary>
        Ascending = SORTDIRECTION.SORT_ASCENDING,
    }
}