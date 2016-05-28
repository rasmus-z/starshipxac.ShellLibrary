using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using starshipxac.Windows.Shell.Dialogs.Interop;

namespace starshipxac.Windows.Shell.Dialogs
{
    /// <summary>
    ///     ファイルダイアログのファイル種別フィルターを保持します。
    /// </summary>
    public class FileTypeFilterCollection : Collection<FileTypeFilter>
    {
        public FileTypeFilterCollection()
        {
        }

        /// <summary>
        ///     コレクションにフィルターを追加します。
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
        ///     コレクションにフィルターを追加します。
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