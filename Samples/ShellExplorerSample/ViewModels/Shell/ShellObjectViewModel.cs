using System;
using System.Diagnostics.Contracts;
using Livet;
using Reactive.Bindings;
using starshipxac.Shell;
using starshipxac.Shell.Media.Imaging;

namespace ShellExplorerSample.ViewModels.Shell
{
    /// <summary>
    ///     <see cref="ShellObject" />の<c>ViewModel</c>クラスを定義します。
    /// </summary>
    public abstract class ShellObjectViewModel : ViewModel
    {
        /// <summary>
        ///     <see cref="ShellObject" />および親フォルダーの<c>ViewModel</c>を指定して、
        ///     <see cref="ShellObjectViewModel" />クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="shellObject"></param>
        protected ShellObjectViewModel(ShellObject shellObject)
        {
            this.ShellObject = shellObject;

            if (this.ShellObject == null)
            {
                this.Thumbnail = new ReactiveProperty<ShellThumbnailImageSource>();
                this.DetailThumbnail = new ReactiveProperty<ShellThumbnailImageSource>();
            }
            else
            {
                this.Thumbnail = new ReactiveProperty<ShellThumbnailImageSource>(
                    new ShellThumbnailImageSource(this.ShellObject, ThumbnailMode.ListView));
                this.DetailThumbnail = new ReactiveProperty<ShellThumbnailImageSource>(
                    new ShellThumbnailImageSource(this.ShellObject, ThumbnailMode.ListView));
            }
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.DisplayName != null);
        }

        public ShellObject ShellObject { get; }

        /// <summary>
        ///     表示名を取得します。
        /// </summary>
        public abstract ReactiveProperty<string> DisplayName { get; }

        /// <summary>
        ///     アイテム種別のテキストを取得します。
        /// </summary>
        public abstract ReactiveProperty<string> ItemTypeText { get; }

        /// <summary>
        ///     作成日時を取得します。
        /// </summary>
        public abstract ReactiveProperty<DateTime> DateCreated { get; }

        /// <summary>
        ///     更新日時を取得します。
        /// </summary>
        public abstract ReactiveProperty<DateTime> DateModified { get; }

        /// <summary>
        ///     サムネイルイメージを取得します。
        /// </summary>
        public ReactiveProperty<ShellThumbnailImageSource> Thumbnail { get; }

        public ReactiveProperty<ShellThumbnailImageSource> DetailThumbnail { get; }

        public override string ToString()
        {
            Contract.Ensures(Contract.Result<string>() != null);

            return $"{this.GetType().Name}: {{ DisplayName={this.DisplayName.Value} }}";
        }
    }
}