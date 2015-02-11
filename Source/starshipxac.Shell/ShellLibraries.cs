using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace starshipxac.Shell
{
	/// <summary>
	/// シェル標準ライブラリを取得します。
	/// </summary>
	public static class ShellLibraries
	{
		/// <summary>
		/// すべてのライブラリを取得します。
		/// </summary>
		public static IEnumerable<ShellLibrary> AllLibraries
		{
			get
			{
				return ShellKnownFolders.Libraries.GetItems().OfType<ShellLibrary>();
			}
		}

		/// <summary>
		/// すべてのパブリックライブラリを取得します。
		/// </summary>
		public static IEnumerable<ShellLibrary> AllPublicLibraries
		{
			get
			{
				return ShellKnownFolders.PublicLibraries.GetItems().OfType<ShellLibrary>();
			}
		}

		/// <summary>
		/// ドキュメントライブラリを取得します。
		/// </summary>
		public static ShellLibrary DocumentsLibrary
		{
			get
			{
				Contract.Ensures(Contract.Result<ShellLibrary>() != null);
				return ShellLibraryFactory.Load(ShellKnownFolderFactory.FromCanonicalName("DocumentsLibrary"));
			}
		}

		/// <summary>
		/// ミュージックライブラリを取得します。
		/// </summary>
		public static ShellLibrary MusicLibrary
		{
			get
			{
				Contract.Ensures(Contract.Result<ShellLibrary>() != null);
				return ShellLibraryFactory.Load(ShellKnownFolderFactory.FromCanonicalName("MusicLibrary"));
			}
		}

		/// <summary>
		/// ピクチャライブラリを取得します。
		/// </summary>
		public static ShellLibrary PicturesLibrary
		{
			get
			{
				Contract.Ensures(Contract.Result<ShellLibrary>() != null);
				return ShellLibraryFactory.Load(ShellKnownFolderFactory.FromCanonicalName("PicturesLibrary"));
			}
		}

		/// <summary>
		/// ビデオライブラリを取得します。
		/// </summary>
		public static ShellLibrary VideosLibrary
		{
			get
			{
				Contract.Ensures(Contract.Result<ShellLibrary>() != null);
				return ShellLibraryFactory.Load(ShellKnownFolderFactory.FromCanonicalName("VideosLibrary"));
			}
		}

		/// <summary>
		/// 録画されたTVライブラリを取得します。
		/// </summary>
		public static ShellLibrary RecordedTVLibrary
		{
			get
			{
				Contract.Ensures(Contract.Result<ShellLibrary>() != null);
				return ShellLibraryFactory.Load(ShellKnownFolderFactory.FromCanonicalName("RecordedTVLibrary"));
			}
		}
	}
}