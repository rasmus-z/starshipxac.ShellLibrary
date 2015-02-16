using System;
using System.Diagnostics.Contracts;
using starshipxac.Shell.PropertySystem;

namespace starshipxac.Shell
{
    /// <summary>
    /// ショートカットファイル情報を定義します。
    /// </summary>
    public class ShellLink : ShellFile
    {
        private ShellProperty<string> titleProperty;
        private ShellProperty<string> linkTargetParsingPathProperty;
        private ShellProperty<string> linkArgumentsProperty;
        private ShellProperty<string> linkCommentProperty;
        private ShellProperty<string> commentProperty;

        internal static readonly string FileExtension = ".lnk";

        /// <summary>
        /// <see cref="ShellLink"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="shellItem"><see cref="ShellItem"/>。</param>
        internal ShellLink(ShellItem shellItem)
            : base(shellItem)
        {
            Contract.Requires<ArgumentNullException>(shellItem != null);
        }

        /// <summary>
        /// 対象のパスを取得または設定します。
        /// </summary>
        public string TargetLocation
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                if (this.linkTargetParsingPathProperty == null)
                {
                    this.linkTargetParsingPathProperty = this.Properties.Create<string>("{B9B4B3FC-2B51-4A42-B5D8-324146AFCF25}", 2);
                }
                return this.linkTargetParsingPathProperty.Value;
            }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                if (this.linkTargetParsingPathProperty == null)
                {
                    this.linkTargetParsingPathProperty = this.Properties.Create<string>("{B9B4B3FC-2B51-4A42-B5D8-324146AFCF25}", 2);
                }
                this.linkTargetParsingPathProperty.Value = value;
            }
        }

        /// <summary>
        /// 対象の<see cref="ShellObject"/>を取得します。
        /// </summary>
        public ShellObject Target
        {
            get
            {
                Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(this.TargetLocation));
                Contract.Ensures(Contract.Result<ShellObject>() != null);
                return ShellFactory.FromParsingName(this.TargetLocation);
            }
        }

        /// <summary>
        ///  ショートカットファイルのタイトルを取得します。
        /// </summary>
        public string Title
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                if (this.titleProperty == null)
                {
                    this.titleProperty = this.Properties.Create<string>("{F29F85E0-4FF9-1068-AB91-08002B27B3D9}", 2);
                }
                return this.titleProperty.GetValue(String.Empty);
            }
        }

        /// <summary>
        /// ショートカットファイルに設定されている引数を取得します。
        /// </summary>
        public string Arguments
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                if (this.linkArgumentsProperty == null)
                {
                    this.linkArgumentsProperty = this.Properties.Create<string>("{436F2667-14E2-4FEB-B30A-146C53B5B674}", 100);
                }
                return this.linkArgumentsProperty.GetValue(String.Empty);
            }
        }

        /// <summary>
        /// ショートカットファイルに設定されているコメントを取得します。
        /// </summary>
        public string Comment
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                if (this.linkCommentProperty == null)
                {
                    this.linkCommentProperty = this.Properties.Create<string>("{B9B4B3FC-2B51-4A42-B5D8-324146AFCF25}", 5);
                }
                if (this.linkCommentProperty.Value != null)
                {
                    return this.linkCommentProperty.Value;
                }

                if (this.commentProperty == null)
                {
                    this.commentProperty = this.Properties.Create<string>("{F29F85E0-4FF9-1068-AB91-08002B27B3D9}", 6);
                }
                return this.commentProperty.GetValue(String.Empty);
            }
        }
    }
}