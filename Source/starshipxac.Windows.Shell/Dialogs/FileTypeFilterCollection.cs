using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using starshipxac.Windows.Shell.Dialogs.Interop;

namespace starshipxac.Windows.Shell.Dialogs
{
    /// <summary>
    ///     Define file type filter collection.
    /// </summary>
    public class FileTypeFilterCollection : Collection<FileTypeFilter>
    {
        /// <summary>
        ///     Initialize a new instance of the <see cref="FileTypeFilterCollection" /> class.
        /// </summary>
        public FileTypeFilterCollection()
        {
        }

        /// <summary>
        ///     Add a filter to the collection.
        /// </summary>
        /// <param name="filterName">Filter name.</param>
        /// <param name="extensionsString">Extensions string.</param>
        public void AddFilter(string filterName, string extensionsString)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(filterName));
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(extensionsString));

            Add(new FileTypeFilter(filterName, extensionsString));
        }

        /// <summary>
        ///     Add a filter to the collection.
        /// </summary>
        /// <param name="filterName">Filter name.</param>
        /// <param name="extensions">Collection of extension.</param>
        public void AddFilter(string filterName, IEnumerable<string> extensions)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(filterName));
            Contract.Requires<ArgumentNullException>(extensions != null);

            Add(new FileTypeFilter(filterName, extensions));
        }

        /// <summary>
        ///     Add a filter to the collection.
        /// </summary>
        /// <param name="filterName">Filter name.</param>
        /// <param name="extensions">Array of extension.</param>
        public void AddFilter(string filterName, params string[] extensions)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(filterName));
            Contract.Requires<ArgumentNullException>(extensions != null);

            Add(new FileTypeFilter(filterName, extensions));
        }

        /// <summary>
        ///     Add filters to the collection.
        /// </summary>
        /// <param name="filters">Collection of <see cref="FileTypeFilter" />.</param>
        public void AddRange(IEnumerable<FileTypeFilter> filters)
        {
            Contract.Requires<ArgumentNullException>(filters != null);

            foreach (var filter in filters)
            {
                Add(filter);
            }
        }

        /// <summary>
        ///     Add filters to the collection.
        /// </summary>
        /// <param name="filters">Array of <see cref="FileTypeFilter" />.</param>
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