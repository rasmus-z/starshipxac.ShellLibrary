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
		public static ShellKnownFolder libraries = null;
		public static ShellKnownFolder publicLibraries = null;

		/// <summary>
		/// すべてのライブラリを取得します。
		/// </summary>
		public static IEnumerable<ShellLibrary> AllLibraries
		{
			get
			{
				if (libraries == null)
				{
					libraries = ShellKnownFolders.Libraries;
				}
				return libraries.GetItems().OfType<ShellLibrary>();
			}
		}

		/// <summary>
		/// すべてのパブリックライブラリを取得します。
		/// </summary>
		public static IEnumerable<ShellLibrary> AllPublicLibraries
		{
			get
			{
				if (publicLibraries == null)
				{
					publicLibraries = ShellKnownFolders.PublicLibraries;
				}
				return publicLibraries.GetItems().OfType<ShellLibrary>();
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