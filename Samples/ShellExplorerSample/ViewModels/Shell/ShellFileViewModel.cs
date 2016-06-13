using System;
using System.Diagnostics.Contracts;
using Reactive.Bindings;
using starshipxac.Shell;
using starshipxac.Windows.Shell.Media.Imaging;

namespace ShellExplorerSample.ViewModels.Shell
{
    /// <summary>
    ///     ファイル情報の<c>ViewModel</c>を定義します。
    /// </summary>
    public class ShellFileViewModel : ShellObjectViewModel
    {
        /// <summary>
        ///     <see cref="ShellFile" />および親フォルダーを指定して、
        ///     <see cref="ShellFileViewModel" />クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="shellFile"></param>
        /// <param name="thumbnailFactory"></param>
        public ShellFileViewModel(ShellFile shellFile, ShellThumbnailFactory thumbnailFactory)
            : base(shellFile, thumbnailFactory)
        {
            Contract.Requires<ArgumentNullException>(shellFile != null);
            Contract.Requires<ArgumentNullException>(thumbnailFactory != null);

            #region Reactive Property

            this.DisplayName = new ReactiveProperty<string>(this.ShellFile.DisplayName);
            this.ItemTypeText = new ReactiveProperty<string>(
                this.ShellFile.Properties.Create<string>("System.ItemTypeText").Value);
            this.DateCreated = new ReactiveProperty<DateTime>(this.ShellFile.DateCreated);
            this.DateModified = new ReactiveProperty<DateTime>(this.ShellFile.DateModified);
            this.Thumbnail = new ReactiveProperty<ShellThumbnail>(
                new ShellThumbnail(this.ShellFile, this.ThumbnailFactory));

            this.Path = new ReactiveProperty<string>(this.ShellFile.Path);

            #endregion
        }

        public ShellFile ShellFile => (ShellFile)this.ShellObject;

        public override ReactiveProperty<string> DisplayName { get; }

        public override ReactiveProperty<string> ItemTypeText { get; }

        public override ReactiveProperty<DateTime> DateCreated { get; }

        public override ReactiveProperty<DateTime> DateModified { get; }

        public override ReactiveProperty<ShellThumbnail> Thumbnail { get; }

        /// <summary>
        ///     ファイルのパスを取得します。
        /// </summary>
        public ReactiveProperty<string> Path { get; }

        public override string ToString()
        {
            return $"{this.GetType().Name}: {{ Path={this.Path.Value} }}";
        }
    }
}