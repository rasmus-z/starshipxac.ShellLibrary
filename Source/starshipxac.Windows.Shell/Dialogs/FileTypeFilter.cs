using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using starshipxac.Windows.Shell.Dialogs.Interop;

namespace starshipxac.Windows.Shell.Dialogs
{
    /// <summary>
    /// �t�@�C���_�C�A���O�Ńt�@�C�����t�B���^�����O���邽�߂̊g���q�̃R���N�V������ێ����܂��B
    /// </summary>
    public class FileTypeFilter
    {
        private string displayName = null;
        private readonly List<string> extensions;

        /// <summary>
        /// �t�B���^�[���̂Ɗg���q�̈ꗗ���w�肵�āA
        /// <see cref="FileTypeFilter"/>�N���X�̐V�����C���X�^���X�����������܂��B
        /// </summary>
        /// <param name="filterName">�t�B���^�[���́B</param>
        /// <param name="extensionsString">�g���q�̈ꗗ�B</param>
        /// <remarks>
        /// <param name="extensionsString"/>�́A�g���q���Z�~�R����(';')�܂��̓J���}(',')�ŋ�؂��Ďw�肵�܂��B
        /// �g���q�́A�s���I�h('.')�܂��̓��C���h�J�[�h "*."��擪�ɂ��Ďw�肷�邩�A���������Ɏw��ł��܂��B
        /// </remarks>
        /// <example>
        /// <code>
        /// var filter = new CommonFileDialogFilter("�摜�t�@�C��", "*.bmp, *.jpg, *.gif, *.png");
        /// </code>
        /// </example>
        /// <exception cref="ArgumentNullException">
        /// <para><param name="filterName"/>��<c>null</c>�ł��B</para>
        /// <para>�܂���</para>
        /// <para><param name="extensionsString"/>��<c>null</c>�ł��B</para>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <para><param name="filterName"/>����̕�����ł��B</para>
        /// <para>�܂���</para>
        /// <para><param name="extensionsString"/>����̕�����ł��B</para>
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
        /// �t�B���^�[���̂��擾���܂��B
        /// </summary>
        public string FilterName { get; private set; }

        /// <summary>
        /// �g���q�̃R���N�V�������擾���܂��B
        /// </summary>
        public IReadOnlyList<string> Extensions
        {
            get
            {
                return this.extensions;
            }
        }

        /// <summary>
        /// �t�B���^�[�̕\�������擾�܂��͐ݒ肵�܂��B
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
        /// �w�肵���g���q������𐳋K�����܂��B
        /// </summary>
        /// <param name="extensionString">�g���q������B</param>
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
        /// �\���p�̊g���q�R���N�V�����̕�������쐬���܂��B
        /// </summary>
        /// <param name="extensions"></param>
        /// <returns></returns>
        private static string GetDisplayExtensionsString(IEnumerable<string> extensions)
        {
            Contract.Requires<ArgumentNullException>(extensions != null);

            return String.Join(", ", extensions.Select(x => String.Format("*.{0}", x)));
        }

        /// <summary>
        /// <see cref="FileTypeFilter"/>����ACOM API�Ŏg�p����<see cref="COMDLG_FILTERSPEC"/>���쐬���܂��B
        /// </summary>
        /// <returns>�쐬����<see cref="COMDLG_FILTERSPEC"/>�B</returns>
        /// 
        internal COMDLG_FILTERSPEC CreateFilterSpec()
        {
            var filters = String.Join(";", this.Extensions
                .Select(x => String.Format("*.{0}", x)));
            return new COMDLG_FILTERSPEC(this.DisplayName, filters);
        }

        /// <summary>
        /// <see cref="FileTypeFilter"/>�̕�����\�����擾���܂��B
        /// </summary>
        /// <returns><see cref="FileTypeFilter"/>�̕�����\���B</returns>
        public override string ToString()
        {
            return this.DisplayName;
        }
    }
}