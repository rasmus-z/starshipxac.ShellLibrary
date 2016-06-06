using System;
using System.Diagnostics.Contracts;
using starshipxac.Shell.PropertySystem;

namespace starshipxac.Shell
{
    /// <summary>
    ///     シェルファイル情報を定義します。
    /// </summary>
    public class ShellFile : ShellObject
    {
        private string extension;
        private ShellProperty<UInt64?> sizeProperty;

        /// <summary>
        ///     <see cref="ShellItem" />を指定して、<see cref="ShellFile" />クラスの新しいインスタンスを取得します。
        /// </summary>
        /// <param name="shellItem"><see cref="ShellItem" />。</param>
        protected internal ShellFile(ShellItem shellItem)
            : base(shellItem)
        {
            Contract.Requires<ArgumentNullException>(shellItem != null);
        }

        /// <summary>
        ///     ファイルのパスを取得します。
        /// </summary>
        public virtual string Path
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                return this.ParsingName;
            }
        }

        /// <summary>
        ///     ファイルの拡張子を取得します。
        /// </summary>
        public string Extension
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                if (this.extension == null)
                {
                    this.extension = System.IO.Path.GetExtension(this.Path);
                }
                return this.extension;
            }
        }

        /// <summary>
        ///     ストリームかどうかを判定する値を取得します。
        /// </summary>
        public bool IsStream => this.ShellItem.IsStream;

        /// <summary>
        ///     ファイルサイズを取得します。
        /// </summary>
        public UInt64 Size
        {
            get
            {
                if (this.sizeProperty == null)
                {
                    this.sizeProperty = this.Properties.Create<UInt64?>("System.Size");
                }

                return this.sizeProperty.Value.GetValueOrDefault(0);
            }
        }
    }
}