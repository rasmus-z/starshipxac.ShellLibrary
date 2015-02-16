using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using starshipxac.Windows.Shell.Dialogs.Interop;

namespace starshipxac.Windows.Shell.Dialogs
{
    /// <summary>
    /// �t�@�C���_�C�A���O�̃t�@�C����ʃt�B���^�[��ێ����܂��B
    /// </summary>
    public class FileTypeFilterCollection : Collection<FileTypeFilter>
    {
        public FileTypeFilterCollection()
        {
        }

        /// <summary>
        /// �R���N�V�����Ƀt�B���^�[��ǉ����܂��B
        /// </summary>
        /// <param name="filterName"></param>
        /// <param name="extensionStrings"></param>
        public void AddFilter(string filterName, string extensionStrings)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(filterName));
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(extensionStrings));

            Add(new FileTypeFilter(filterName, extensionStrings));
        }

        /// <summary>
        /// �R���N�V�����Ƀt�B���^�[��ǉ����܂��B
        /// </summary>
        /// <param name="filterName"></param>
        /// <param name="extensions"></param>
        public void AddFilter(string filterName, IEnumerable<string> extensions)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(filterName));
            Contract.Requires<ArgumentNullException>(extensions != null);

            Add(new FileTypeFilter(filterName, extensions));
        }

        public void AddFilter(string filterName, params string[] extensions)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(filterName));
            Contract.Requires<ArgumentNullException>(extensions != null);

            Add(new FileTypeFilter(filterName, extensions));
        }

        public void AddRange(IEnumerable<FileTypeFilter> filters)
        {
            Contract.Requires<ArgumentNullException>(filters != null);

            foreach (var filter in filters)
            {
                Add(filter);
            }
        }

        public void AddRange(params FileTypeFilter[] filters)
        {
            Contract.Requires<ArgumentNullException>(filters != null);

            AddRange((IEnumerable<FileTypeFilter>)filters);
        }

        internal IEnumerable<COMDLG_FILTERSPEC> EnumerateFilterSpecs()
        {
            return this.Select(filter => filter.CreateFilterSpec());
        }
    }
}