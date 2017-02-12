using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using starshipxac.Windows.Shell.Dialogs.Interop;

namespace starshipxac.Windows.Shell.Dialogs
{
    /// <summary>
    ///     Define file type filter.
    /// </summary>
    public class FileTypeFilter
    {
        private string displayName = null;
        private readonly List<string> extensions;

        /// <summary>
        ///     <para>
        ///         Initialize a new instance of the <see cref="FileTypeFilter" /> class
        ///         to the specified filter name and extensions string.
        ///     </para>
        ///     <para>
        ///         フィルター名称と拡張子の一覧を指定して、
        ///         <see cref="FileTypeFilter" />クラスの新しいインスタンスを初期化します。
        ///     </para>
        /// </summary>
        /// <param name="filterName">Filter name.</param>
        /// <param name="extensionsString">extensions.</param>
        /// <remarks>
        ///     <para>
        ///         <Paramref name="extensionsString" /> specifies extensions separated by semicolons (';') or commas (',').
        ///         The extension can be specified by prefixing a period ('.') or wildcard '*.' or without specifying anything.
        ///     </para>
        ///     <para>
        ///         <paramref name="extensionsString" />は、拡張子をセミコロン(';')またはカンマ(',')で区切って指定します。
        ///         拡張子は、ピリオド('.')またはワイルドカード "*."を先頭につけて指定するか、何もつけずに指定できます。
        ///     </para>
        /// </remarks>
        /// <example>
        ///     <code>
        ///         var filter = new CommonFileDialogFilter("Picture file", "*.bmp, *.jpg, *.gif, *.png");
        ///     </code>
        /// </example>
        /// <exception cref="ArgumentException">
        ///     <para><paramref name="filterName" /> is <c>null</c> or empty string.</para>
        ///     or
        ///     <para><paramref name="extensionsString" /> is <c>null</c> or empty string.</para>
        /// </exception>
        public FileTypeFilter(string filterName, string extensionsString)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(filterName));
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(extensionsString));

            this.FilterName = filterName;
            this.extensions = extensionsString.Split(',', ';')
                .Select(NormalizeExtensionString)
                .ToList();
        }

        /// <summary>
        ///     Initialize a new instance of the <see cref="FileTypeFilter" /> class
        ///     to the specified filter name and collection of extension.
        /// </summary>
        /// <param name="filterName">Filter name.</param>
        /// <param name="extensions">Collection of extension.</param>
        /// <exception cref="ArgumentException"><paramref name="filterName" /> is <c>null</c> or empty string.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="extensions" /> is <c>null</c>.</exception>
        public FileTypeFilter(string filterName, IEnumerable<string> extensions)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(filterName));
            Contract.Requires<ArgumentNullException>(extensions != null);

            this.FilterName = filterName;
            this.extensions = extensions
                .Select(NormalizeExtensionString)
                .ToList();
        }

        /// <summary>
        ///     Initialize a new instance of the <see cref="FileTypeFilter" /> class
        ///     to the specified filter name and array of extension.
        /// </summary>
        /// <param name="filterName">Filter name.</param>
        /// <param name="extensions">Array of extension.</param>
        /// <exception cref="ArgumentException"><paramref name="filterName" /> is <c>null</c> or empty string.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="extensions" /> is <c>null</c>.</exception>
        public FileTypeFilter(string filterName, params string[] extensions)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(filterName));
            Contract.Requires<ArgumentNullException>(extensions != null);

            this.FilterName = filterName;
            this.extensions = extensions
                .Select(NormalizeExtensionString)
                .ToList();
        }

        /// <summary>
        ///     Get the filter name.
        /// </summary>
        public string FilterName { get; }

        /// <summary>
        ///     Get the collection of extension.
        /// </summary>
        public IReadOnlyList<string> Extensions => this.extensions;

        /// <summary>
        ///     Get the display name of filter.
        /// </summary>
        public string DisplayName
        {
            get
            {
                if (this.displayName == null)
                {
                    this.displayName = String.Format(System.Globalization.CultureInfo.InvariantCulture,
                        "{0} ({1})",
                        this.FilterName, GetDisplayExtensionsString(this.Extensions));
                }
                return this.displayName;
            }
            set
            {
                this.displayName = String.IsNullOrWhiteSpace(value) ? String.Empty : value;
            }
        }

        /// <summary>
        ///     Normalize the specified extension string.
        /// </summary>
        /// <param name="extensionString">Extension string.</param>
        /// <returns></returns>
        private static string NormalizeExtensionString(string extensionString)
        {
            Contract.Requires(extensionString != null);

            var result = extensionString.Trim();
            result = result.Replace("*.", String.Empty);
            result = result.Replace(".", String.Empty);
            return result;
        }

        /// <summary>
        ///     Create an extension collection string for display.
        /// </summary>
        /// <param name="extensions"></param>
        /// <returns></returns>
        private static string GetDisplayExtensionsString(IEnumerable<string> extensions)
        {
            Contract.Requires<ArgumentNullException>(extensions != null);

            return String.Join(", ", extensions.Select(x => $"*.{x}"));
        }

        /// <summary>
        ///     <para>
        ///         Create <see cref="COMDLG_FILTERSPEC" /> for use with the COM API from <see cref="FileTypeFilter" />.
        ///     </para>
        ///     <para>
        ///         <see cref="FileTypeFilter" />から、COM APIで使用する<see cref="COMDLG_FILTERSPEC" />を作成します。
        ///     </para>
        /// </summary>
        /// <returns><see cref="COMDLG_FILTERSPEC" />.</returns>
        internal COMDLG_FILTERSPEC CreateFilterSpec()
        {
            var filters = String.Join(";", this.Extensions.Select(x => $"*.{x}"));
            return new COMDLG_FILTERSPEC(this.DisplayName, filters);
        }

        /// <summary>
        ///     Get the string representation of <see cref="FileTypeFilter" />.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.DisplayName;
        }
    }
}