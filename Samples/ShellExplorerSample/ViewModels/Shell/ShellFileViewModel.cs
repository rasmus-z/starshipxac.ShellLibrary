using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using Reactive.Bindings;
using starshipxac.Shell;
using starshipxac.Shell.Media.Imaging;
using starshipxac.Shell.PropertySystem;
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
        private ShellFileViewModel(ShellFile shellFile)
            : base(shellFile)
        {
            Contract.Requires<ArgumentNullException>(shellFile != null);

            #region Reactive Property

            this.DisplayName = new ReactiveProperty<string>(this.ShellFile.DisplayName);
            this.ItemTypeText = new ReactiveProperty<string>(
                new ShellProperty<string>(this.ShellFile, "System.ItemTypeText").Value);
            this.DateCreated = new ReactiveProperty<DateTime>(this.ShellFile.DateCreated);
            this.DateModified = new ReactiveProperty<DateTime>(this.ShellFile.DateModified);
            this.Thumbnail = new ReactiveProperty<ShellImageSource>();

            this.Path = new ReactiveProperty<string>(this.ShellFile.Path);

            #endregion
        }

        public static async Task<ShellFileViewModel> CreateAsync(ShellFile shellFile)
        {
            Contract.Requires<ArgumentNullException>(shellFile != null);

            var result = new ShellFileViewModel(shellFile);
            result.Thumbnail.Value = new ShellImageSource(await shellFile.GetThumbnailAsync(ThumbnailMode.ListView));

            return result;
        }

        public ShellFile ShellFile => (ShellFile)this.ShellObject;

        public override ReactiveProperty<string> DisplayName { get; }

        public override ReactiveProperty<string> ItemTypeText { get; }

        public override ReactiveProperty<DateTime> DateCreated { get; }

        public override ReactiveProperty<DateTime> DateModified { get; }

        public override ReactiveProperty<ShellImageSource> Thumbnail { get; }

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