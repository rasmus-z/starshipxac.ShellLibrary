﻿using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Reactive.Linq;
using System.Windows.Data;
using Livet;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using ShellExplorerSample.ViewModels.Shell;

namespace ShellExplorerSample.ViewModels
{
    /// <summary>
    ///     Shell Explorer Sample View Model.
    /// </summary>
    /// <remarks>
    ///     Gitライブラリフォルダーの<see cref="starshipxac.Shell.ShellFolder.EnumerateObjects" />メソッドで
    ///     <c>FileNotFoundException</c>が発生する場合は、
    ///     プロジェクトのプロパティ -> ビルドの「32ビットの優先」のチェックを外す。
    /// </remarks>
    public class MainWindowViewModel : ViewModel
    {
        /// <summary>
        ///     <see cref="MainWindowViewModel" />クラスの新しいインスタンスを初期化します。
        /// </summary>
        public MainWindowViewModel()
        {
            ShellViewModelFactory.CreateFactory();

            #region Reactive Property

            this.RootFolder = ShellViewModelFactory.CreateRoot();

            this.ShellItems = new ReactiveCollection<ShellObjectViewModel>();
            this.ShellItemCollectionView = CollectionViewSource.GetDefaultView(this.ShellItems);

            this.RootFolder.SelectedFolder
                .Where(folder => folder != null)
                .Subscribe(CreateShellItems)
                .AddTo(this.CompositeDisposable);

            #endregion
        }

        /// <summary>
        ///     <c>ContentRendered</c>イベントが発生すると呼ばれます。
        /// </summary>
        public void Initialize()
        {
            this.RootFolder.Initialize();
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.RootFolder != null);
            Contract.Invariant(this.ShellItems != null);
            Contract.Invariant(this.ShellItemCollectionView != null);
        }

        public ShellRootViewModel RootFolder { get; }

        /// <summary>
        ///     選択中のフォルダーに含まれるファイルまたはフォルダーのコレクションを取得します。
        /// </summary>
        public ReactiveCollection<ShellObjectViewModel> ShellItems { get; }

        public ICollectionView ShellItemCollectionView { get; }

        private void CreateShellItems(ShellFolderViewModel folder)
        {
            this.ShellItems.ClearOnScheduler();

            foreach (var item in folder.EnumerateItems())
            {
                this.ShellItems.AddOnScheduler(item);
            }
        }
    }
}