﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Runtime.InteropServices;
using starshipxac.Shell.Internal;
using starshipxac.Shell.Interop;

namespace starshipxac.Shell
{
	/// <summary>
	/// シェルフォルダー情報を定義します。
	/// </summary>
	public class ShellFolder : ShellObject
	{
		/// <summary>
		/// <see cref="ShellItem"/>を指定して、<see cref="ShellFolder"/>クラスの新しいインスタンスを初期化します。
		/// </summary>
		/// <param name="shellItem"><see cref="ShellItem"/>。</param>
		internal ShellFolder(ShellItem shellItem)
			: base(shellItem)
		{
			Contract.Requires<ArgumentNullException>(shellItem != null);

			this.ShellFolderInterface = shellItem.GetShellFolder();
		}

		/// <summary>
		/// <see cref="ShellFolder"/>によって使用されているすべてのリソースを解放し、
		/// オプションでマネージリソースも解放します。
		/// </summary>
		/// <param name="disposing">
		/// マネージリソースとアンマネージリソースの両方を解放する場合は<c>true</c>。
		/// アンマネージリソースだけを解放する場合は<c>false</c>。
		/// </param>
		protected override void Dispose(bool disposing)
		{
			try
			{
				// アンマネージリソース解放
				Marshal.ReleaseComObject(this.ShellFolderInterface);
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		[ContractInvariantMethod]
		private void ObjectInvariant()
		{
			Contract.Invariant(this.ShellFolderInterface != null);
		}

		/// <summary>
		/// <see cref="IShellFolder"/>を取得します。
		/// </summary>
		internal IShellFolder ShellFolderInterface { get; private set; }

		/// <summary>
		/// ファイルシステム上のパスを取得します。
		/// </summary>
		public virtual string Path
		{
			get
			{
				return this.ParsingName;
			}
		}

		/// <summary>
		/// ファイルシステム上のパスが存在するかどうかを判定する値を取得します。
		/// </summary>
		public virtual bool PathExists
		{
			get
			{
				return this.IsFileSystem && Directory.Exists(this.Path);
			}
		}

		/// <summary>
		/// 親フォルダー情報を取得します。
		/// </summary>
		public ShellFolder ParentFolder
		{
			get
			{
				return this.Parent as ShellFolder;
			}
		}

		/// <summary>
		/// <see cref="ShellFolder"/>内のアイテムコレクションを取得します。
		/// </summary>
		/// <returns><see cref="ShellFolder"/>内のアイテムコレクション。</returns>
		public virtual IEnumerable<ShellObject> GetItems()
		{
			Contract.Ensures(Contract.Result<IEnumerable<ShellObject>>() != null);
			return new ShellItems(new ShellFolderEnumerator(this));
		}

		/// <summary>
		/// <see cref="ShellFolder"/>内のファイルコレクションを取得します。
		/// </summary>
		/// <returns><see cref="ShellFolder"/>内のファイルコレクション。</returns>
		public virtual IEnumerable<ShellObject> GetFiles()
		{
			Contract.Ensures(Contract.Result<IEnumerable<ShellObject>>() != null);
			return new ShellItems(new ShellFolderEnumerator(this, SHCONTF.SHCONTF_NONFOLDERS));
		}

		/// <summary>
		/// <see cref="ShellFolder"/>内のフォルダーコレクションを取得します。
		/// </summary>
		/// <returns><see cref="ShellFolder"/>内のフォルダーコレクション。</returns>
		public virtual IEnumerable<ShellFolder> GetFolders()
		{
			Contract.Ensures(Contract.Result<IEnumerable<ShellFolder>>() != null);
			return new ShellFolders(new ShellFolderEnumerator(this, SHCONTF.SHCONTF_FOLDERS));
		}
	}
}