using System;
using System.Diagnostics.Contracts;
using Codeplex.Reactive;
using starshipxac.Shell;

namespace ShellExplorerSample.ViewModels.Shell
{
    /// <summary>
    /// ファイル情報の<c>ViewModel</c>を定義します。
    /// </summary>
    public class ShellFileViewModel : ShellObjectViewModel
    {
        /// <summary>
        /// <see cref="ShellFile"/>および親フォルダーを指定して、
        /// <see cref="ShellFileViewModel"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="shellFile"></param>
        /// <param name="parentFolder"></param>
        public ShellFileViewModel(ShellFile shellFile, ShellFolderViewModel parentFolder)
            : base(shellFile, parentFolder)
        {
            Contract.Requires<ArgumentNullException>(shellFile != null);
            Contract.Requires<ArgumentNullException>(parentFolder != null);

            this.ShellFile = shellFile;
            this.Path = new ReactiveProperty<string>(this.ShellFile.Path);
        }

        public ShellFile ShellFile { get; private set; }

        /// <summary>
        /// ファイルのパスを取得します。
        /// </summary>
        public ReactiveProperty<string> Path { get; private set; }

        public override string ToString()
        {
            return String.Format("{0}: {{ Path={1} }}",
                this.GetType().Name,
                this.Path.Value);
        }
    }
}