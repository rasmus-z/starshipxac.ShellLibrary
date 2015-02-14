using System;
using System.Diagnostics.Contracts;
using System.Windows;
using starshipxac.Shell;
using starshipxac.Windows.Shell.Media.Imaging;

namespace ShellExplorerSample.ViewModels.Shell
{
	public static class ShellViewModelFactory
	{
		static ShellViewModelFactory()
		{
			// 注意！！
			// ここで初期化すると、スレッドが違うせいか、特定のフォルダーの取得で
			// InvalidOperationExceptionが発生する。

			//ContainerThumbnailFactory = new ShellThumbnailFactory(new Size(32, 32));
			//ThumbnailFactory = new ShellThumbnailFactory(new Size(64, 64));
		}

		public static ShellThumbnailFactory ContainerThumbnailFactory { get; private set; }
		public static ShellThumbnailFactory ThumbnailFactory { get; private set; }

		/// <summary>
		/// <see cref="ShellViewModelFactory"/>を初期化します。
		/// </summary>
		public static void Initialize()
		{
			ContainerThumbnailFactory = new ShellThumbnailFactory(new Size(32, 32));
			ThumbnailFactory = new ShellThumbnailFactory(new Size(64, 64));
		}

		public static ShellObjectViewModel Create(ShellObject shellObject, ShellFolderViewModel parentFolder)
		{
			Contract.Requires<ArgumentNullException>(shellObject != null);
			Contract.Requires<ArgumentNullException>(parentFolder != null);
			Contract.Ensures(Contract.Result<ShellObjectViewModel>() != null);

			if (shellObject is ShellFolder)
			{
				return new ShellFolderViewModel((ShellFolder)shellObject, parentFolder);
			}
			else if (shellObject is ShellFile)
			{
				return new ShellFileViewModel((ShellFile)shellObject, parentFolder);
			}
			else
			{
				return new ShellObjectViewModel(shellObject, parentFolder);
			}
		}

		public static ShellFolderViewModel CreateRoot(ShellFolder rootFolder)
		{
			Contract.Requires<ArgumentNullException>(rootFolder != null);

			return new ShellFolderViewModel(rootFolder, ContainerThumbnailFactory);
		}
	}
}