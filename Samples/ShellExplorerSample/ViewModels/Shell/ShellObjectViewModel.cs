using System;
using System.Diagnostics.Contracts;
using Codeplex.Reactive;
using Livet;
using starshipxac.Shell;
using starshipxac.Windows.Shell.Media.Imaging;

namespace ShellExplorerSample.ViewModels.Shell
{
    /// <summary>
    /// <see cref="ShellObject"/>の<c>ViewModel</c>クラスを定義します。
    /// </summary>
    public class ShellObjectViewModel : ViewModel
    {
        /// <summary>
        /// <see cref="ShellObject"/>および親フォルダーの<c>ViewModel</c>を指定して、
        /// <see cref="ShellObjectViewModel"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="shellObject"></param>
        /// <param name="parentFolder"></param>
        public ShellObjectViewModel(ShellObject shellObject, ShellFolderViewModel parentFolder)
        {
            Contract.Requires<ArgumentNullException>(shellObject != null);
            Contract.Requires<ArgumentNullException>(parentFolder != null);

            this.ShellObject = shellObject;
            this.ParentFolder = parentFolder;
            this.ThumbnailFactory = this.ParentFolder.ThumbnailFactory;

            InitializeReactiveProperties();
        }

        /// <summary>
        /// <see cref="ShellObject"/>と<see cref="ShellThumbnailFactory"/>を指定して、
        /// <see cref="ShellObjectViewModel"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="shellObject"></param>
        /// <param name="thumbnailFactory"></param>
        protected ShellObjectViewModel(ShellObject shellObject, ShellThumbnailFactory thumbnailFactory)
        {
            Contract.Requires<ArgumentNullException>(shellObject != null);
            Contract.Requires<ArgumentNullException>(thumbnailFactory != null);

            this.ShellObject = shellObject;
            this.ParentFolder = null;
            this.ThumbnailFactory = thumbnailFactory;

            InitializeReactiveProperties();
        }

        /// <summary>
        /// 親フォルダーを指定して、
        /// <see cref="ShellObjectViewModel"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="parentFolder"></param>
        protected ShellObjectViewModel(ShellFolderViewModel parentFolder)
        {
            Contract.Requires<ArgumentNullException>(parentFolder != null);

            this.ShellObject = null;
            this.ParentFolder = parentFolder;
            this.ThumbnailFactory = parentFolder.ThumbnailFactory;

            InitializeReactiveProperties();
        }

        #region ReactiveProperty

        /// <summary>
        /// <c>ReactiveProperty</c>を初期化します。
        /// </summary>
        private void InitializeReactiveProperties()
        {
            if (this.ShellObject == null)
            {
                this.ParsingName = new ReactiveProperty<string>(String.Empty);
                this.DisplayName = new ReactiveProperty<string>(String.Empty);
                this.DateCreated = new ReactiveProperty<DateTime>(DateTime.MinValue);
                this.DateModified = new ReactiveProperty<DateTime>(DateTime.MinValue);
            }
            else
            {
                this.ParsingName = new ReactiveProperty<string>(":::");
                this.DisplayName = new ReactiveProperty<string>(this.ShellObject.DisplayName);
                var itemTypeTextProperty = this.ShellObject.Properties.Create<string>("System.ItemTypeText");
                this.ItemTypeText = new ReactiveProperty<string>(itemTypeTextProperty.Value);
                
                if (this.ThumbnailFactory != null)
                {
                    this.Thumbnail = new ReactiveProperty<ShellThumbnail>(
                        new ShellThumbnail(this.ShellObject, this.ThumbnailFactory));
                }
            }
        }

        #endregion

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.ParsingName != null);
        }

        public ShellObject ShellObject { get; private set; }

        public ShellFolderViewModel ParentFolder { get; private set; }

        public ShellThumbnailFactory ThumbnailFactory { get; private set; }

        /// <summary>
        /// 解析名を取得します。
        /// </summary>
        public ReactiveProperty<string> ParsingName { get; protected set; }

        /// <summary>
        /// 表示名を取得します。
        /// </summary>
        public ReactiveProperty<string> DisplayName { get; private set; }

        /// <summary>
        /// アイテム種別のテキストを取得します。
        /// </summary>
        public ReactiveProperty<string> ItemTypeText { get; private set; }

        /// <summary>
        /// 作成日時を取得します。
        /// </summary>
        public ReactiveProperty<DateTime> DateCreated { get; private set; }

        /// <summary>
        /// 更新日時を取得します。
        /// </summary>
        public ReactiveProperty<DateTime> DateModified { get; private set; }

        /// <summary>
        /// サムネイルイメージを取得します。
        /// </summary>
        public ReactiveProperty<ShellThumbnail> Thumbnail { get; private set; }

        public override string ToString()
        {
            return String.Format("{0}: {{ ParsingName={1} }}",
                this.GetType().Name,
                this.DisplayName.Value);
        }
    }
}