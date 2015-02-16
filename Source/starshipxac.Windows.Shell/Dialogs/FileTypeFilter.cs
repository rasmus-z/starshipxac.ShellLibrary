using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using starshipxac.Windows.Shell.Dialogs.Interop;

namespace starshipxac.Windows.Shell.Dialogs
{
    /// <summary>
    /// ファイルダイアログでファイルをフィルタリングするための拡張子のコレクションを保持します。
    /// </summary>
    public class FileTypeFilter
    {
        private string displayName = null;
        private readonly List<string> extensions;

        /// <summary>
        /// フィルター名称と拡張子の一覧を指定して、
        /// <see cref="FileTypeFilter"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="filterName">フィルター名称。</param>
        /// <param name="extensionsString">拡張子の一覧。</param>
        /// <remarks>
        /// <param name="extensionsString"/>は、拡張子をセミコロン(';')またはカンマ(',')で区切って指定します。
        /// 拡張子は、ピリオド('.')またはワイルドカード "*."を先頭につけて指定するか、何もつけずに指定できます。
        /// </remarks>
        /// <example>
        /// <code>
        /// var filter = new CommonFileDialogFilter("画像ファイル", "*.bmp, *.jpg, *.gif, *.png");
        /// </code>
        /// </example>
        /// <exception cref="ArgumentNullException">
        /// <para><param name="filterName"/>が<c>null</c>です。</para>
        /// <para>または</para>
        /// <para><param name="extensionsString"/>が<c>null</c>です。</para>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <para><param name="filterName"/>が空の文字列です。</para>
        /// <para>または</para>
        /// <para><param name="extensionsString"/>が空の文字列です。</para>
        /// </exception>
        public FileTypeFilter(string filterName, string extensionsString)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(filterName));
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(extensionsString));

            this.FilterName = filterName;
            this.extensions = new List<string>(extensionsString
                .Split(',', ';')
                .Select(NormalizeExtensionString));
        }

        public FileTypeFilter(string filterName, IEnumerable<string> extensions)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(filterName));
            Contract.Requires<ArgumentNullException>(extensions != null);

            this.FilterName = filterName;
            this.extensions = new List<string>(extensions
                .Select(NormalizeExtensionString));
        }

        public FileTypeFilter(string filterName, params string[] extensions)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(filterName));
            Contract.Requires<ArgumentNullException>(extensions != null);

            this.FilterName = filterName;
            this.extensions = new List<string>(extensions
                .Select(NormalizeExtensionString));
        }

        [ContractInvariantMethod]
        private void CommonFileDialogFilterInvaliant()
        {
            Contract.Invariant(this.FilterName != null);
            Contract.Invariant(this.extensions != null);
        }

        /// <summary>
        /// フィルター名称を取得します。
        /// </summary>
        public string FilterName { get; private set; }

        /// <summary>
        /// 拡張子のコレクションを取得します。
        /// </summary>
        public IReadOnlyList<string> Extensions
        {
            get
            {
                return this.extensions;
            }
        }

        /// <summary>
        /// フィルターの表示名を取得または設定します。
        /// </summary>
        public string DisplayName
        {
            get
            {
                if (this.displayName == null)
                {
                    this.displayName = string.Format(System.Globalization.CultureInfo.InvariantCulture,
                        "{0} ({1})",
                        this.FilterName, GetDisplayExtensionsString(this.Extensions));
                }
                return this.displayName;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    this.displayName = String.Empty;
                }
                else
                {
                    this.displayName = value;
                }
            }
        }

        /// <summary>
        /// 指定した拡張子文字列を正規化します。
        /// </summary>
        /// <param name="extensionString">拡張子文字列。</param>
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
        /// 表示用の拡張子コレクションの文字列を作成します。
        /// </summary>
        /// <param name="extensions"></param>
        /// <returns></returns>
        private static string GetDisplayExtensionsString(IEnumerable<string> extensions)
        {
            Contract.Requires<ArgumentNullException>(extensions != null);

            return String.Join(", ", extensions.Select(x => String.Format("*.{0}", x)));
        }

        /// <summary>
        /// <see cref="FileTypeFilter"/>から、COM APIで使用する<see cref="COMDLG_FILTERSPEC"/>を作成します。
        /// </summary>
        /// <returns>作成した<see cref="COMDLG_FILTERSPEC"/>。</returns>
        /// 
        internal COMDLG_FILTERSPEC CreateFilterSpec()
        {
            var filters = String.Join(";", this.Extensions
                .Select(x => String.Format("*.{0}", x)));
            return new COMDLG_FILTERSPEC(this.DisplayName, filters);
        }

        /// <summary>
        /// <see cref="FileTypeFilter"/>の文字列表現を取得します。
        /// </summary>
        /// <returns><see cref="FileTypeFilter"/>の文字列表現。</returns>
        public override string ToString()
        {
            return this.DisplayName;
        }
    }
}