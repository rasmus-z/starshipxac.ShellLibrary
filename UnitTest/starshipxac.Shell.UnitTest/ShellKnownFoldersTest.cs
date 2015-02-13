using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using starshipxac.Shell.TestConfiguration;
using Xunit;

namespace starshipxac.Shell
{
	public class ShellKnownFoldersTest : IClassFixture<ShellTestConfig>
	{
		public ShellKnownFoldersTest(ShellTestConfig testConfig)
		{
			this.TestConfig = testConfig;
		}

		public ShellTestConfig TestConfig { get; private set; }

		[Fact]
		public async Task AllFoldersTest()
		{
			await STATask.Run(() =>
			{
				foreach (var folder in ShellKnownFolders.AllFolders)
				{
					Dump(folder);
				}
			});
		}

		[Fact]
		public async Task ApplicationDataTest()
		{
			await STATask.Run(() =>
			{
				var actual = ShellKnownFolders.ApplicationData;

				Assert.NotNull(actual);
				Assert.Equal("AppData", actual.CanonicalName);
				Assert.Equal(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), actual.ParsingName);

				Dump(actual);
			});
		}

		[Fact]
		public async Task ComputerTest()
		{
			await STATask.Run(() =>
			{
				var actual = ShellKnownFolders.Computer;

				Assert.NotNull(actual);
				Assert.Equal("MyComputerFolder", actual.CanonicalName);

				Dump(actual);
			});
		}

		[Fact]
		public async Task DesktopTest()
		{
			await STATask.Run(() =>
			{
				var actual = ShellKnownFolders.Desktop;

				Assert.NotNull(actual);
				Assert.Equal("Desktop", actual.CanonicalName);
				Assert.Equal(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), actual.ParsingName);
				Assert.True(actual.Path.Any());

				Dump(actual);
			});
		}

		[Fact]
		public async Task DocumentsTest()
		{
			await STATask.Run(() =>
			{
				var actual = ShellKnownFolders.Documents;

				Assert.NotNull(actual);
				Assert.Equal("Personal", actual.CanonicalName);
				Assert.Equal(Environment.GetFolderPath(Environment.SpecialFolder.Personal), actual.ParsingName);

				Dump(actual);
			});
		}

		[Fact]
		public async Task DownloadsTest()
		{
			await STATask.Run(() =>
			{
				var actual = ShellKnownFolders.Downloads;

				Assert.NotNull(actual);
				Assert.Equal("Downloads", actual.CanonicalName);
				Assert.Equal(
					Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), @"Downloads"),
					actual.ParsingName);

				Dump(actual);
			});
		}

		[Fact]
		public async Task FavoritesTest()
		{
			await STATask.Run(() =>
			{
				var actual = ShellKnownFolders.Favorites;

				Assert.NotNull(actual);
				Assert.Equal("Favorites", actual.CanonicalName);
				Assert.Equal(Environment.GetFolderPath(Environment.SpecialFolder.Favorites), actual.ParsingName);

				Dump(actual);
			});
		}

		[Fact]
		public async Task GamesTest()
		{
			await STATask.Run(() =>
			{
				var actual = ShellKnownFolders.Games;

				Assert.NotNull(actual);
				Assert.Equal("Games", actual.CanonicalName);

				Dump(actual);
			});
		}

		[Fact]
		public async Task GameTasksTest()
		{
			await STATask.Run(() =>
			{
				var actual = ShellKnownFolders.GameTasks;

				Assert.NotNull(actual);
				Assert.Equal("GameTasks", actual.CanonicalName);
				Assert.Equal(
					Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Microsoft\Windows\GameExplorer"),
					actual.ParsingName);

				Dump(actual);
			});
		}

		[Fact]
		public async Task HomeGroupTest()
		{
			await STATask.Run(() =>
			{
				var actual = ShellKnownFolders.HomeGroup;

				Assert.NotNull(actual);
				Assert.Equal("HomeGroupFolder", actual.CanonicalName);

				Dump(actual);
			});
		}

		[Fact]
		public async Task HomeGroupCurrentUserTest()
		{
			await STATask.Run(() =>
			{
				var actual = ShellKnownFolders.HomeGroupCurrentUser;

				Assert.NotNull(actual);
				Assert.Equal("HomeGroupCurrentUserFolder", actual.CanonicalName);

				Dump(actual);
			});
		}

		[Fact]
		public async Task LibrariesTest()
		{
			await STATask.Run(() =>
			{
				var actual = ShellKnownFolders.Libraries;

				Assert.NotNull(actual);
				Assert.Equal("Libraries", actual.CanonicalName);
				Assert.Equal(@"::{031E4825-7B94-4DC3-B131-E946B44C8DD5}", actual.ParsingName);

				Dump(actual);
			});
		}

		[Fact]
		public async Task LinksTest()
		{
			await STATask.Run(() =>
			{
				var actual = ShellKnownFolders.Links;

				Assert.NotNull(actual);
				Assert.Equal("Links", actual.CanonicalName);
				Assert.Equal(
					Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), @"Links"),
					actual.ParsingName);

				Dump(actual);
			});
		}

		[Fact]
		public async Task LocalApplicationDataTest()
		{
			await STATask.Run(() =>
			{
				var actual = ShellKnownFolders.LocalApplicationData;

				Assert.NotNull(actual);
				Assert.Equal("Local AppData", actual.CanonicalName);
				Assert.Equal(
					Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
					actual.ParsingName);

				Dump(actual);
			});
		}

		[Fact]
		public async Task MusicTest()
		{
			await STATask.Run(() =>
			{
				var actual = ShellKnownFolders.Music;

				Assert.NotNull(actual);
				Assert.Equal("My Music", actual.CanonicalName);
				Assert.Equal(
					Environment.GetFolderPath(Environment.SpecialFolder.MyMusic),
					actual.ParsingName);

				Dump(actual);
			});
		}

		[Fact]
		public async Task NetworkTest()
		{
			await STATask.Run(() =>
			{
				var actual = ShellKnownFolders.Network;

				Assert.NotNull(actual);
				Assert.Equal("NetworkPlacesFolder", actual.CanonicalName);

				Dump(actual);
			});
		}

		[Fact]
		public async Task NetworkShortcutsTest()
		{
			await STATask.Run(() =>
			{
				var actual = ShellKnownFolders.NetworkSortcuts;

				Assert.NotNull(actual);
				Assert.Equal("NetHood", actual.CanonicalName);
				Assert.Equal(
					Environment.GetFolderPath(Environment.SpecialFolder.NetworkShortcuts),
					actual.ParsingName);

				Dump(actual);
			});
		}

		[Fact]
		public async Task PersonalTest()
		{
			await STATask.Run(() =>
			{
				var actual = ShellKnownFolders.Personal;

				Assert.NotNull(actual);
				Assert.Equal("Personal", actual.CanonicalName);
				Assert.Equal(Environment.GetFolderPath(Environment.SpecialFolder.Personal), actual.ParsingName);

				Dump(actual);
			});
		}

		[Fact]
		public async Task PicturesTest()
		{
			await STATask.Run(() =>
			{
				var actual = ShellKnownFolders.Pictures;

				Assert.NotNull(actual);
				Assert.Equal("My Pictures", actual.CanonicalName);
				Assert.Equal(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), actual.ParsingName);

				Dump(actual);
			});
		}

		[Fact]
		public async Task PlaylistsTest()
		{
			await STATask.Run(() =>
			{
				var actual = ShellKnownFolders.Playlists;

				Assert.NotNull(actual);
				Assert.Equal("Playlists", actual.CanonicalName);
				Assert.Equal(
					Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic), @"Playlists"),
					actual.ParsingName);

				Dump(actual);
			});
		}

		[Fact]
		public async Task PrintersTest()
		{
			await STATask.Run(() =>
			{
				var actual = ShellKnownFolders.Printers;

				Assert.NotNull(actual);
				Assert.Equal("PrintersFolder", actual.CanonicalName);

				Dump(actual);
			});
		}

		[Fact]
		public async Task ProfileTest()
		{
			await STATask.Run(() =>
			{
				var actual = ShellKnownFolders.Profile;

				Assert.NotNull(actual);
				Assert.Equal("Profile", actual.CanonicalName);
				Assert.Equal(
					Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
					actual.ParsingName);

				Dump(actual);
			});
		}

		[Fact]
		public async Task ProgramDataTest()
		{
			await STATask.Run(() =>
			{
				var actual = ShellKnownFolders.ProgramData;

				Assert.NotNull(actual);
				Assert.Equal("Common AppData", actual.CanonicalName);
				Assert.Equal(
					Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
					actual.ParsingName);

				Dump(actual);
			});
		}

		[Fact]
		public async Task ProgramsTest()
		{
			await STATask.Run(() =>
			{
				var actual = ShellKnownFolders.Programs;

				Assert.NotNull(actual);
				Assert.Equal("Programs", actual.CanonicalName);
				Assert.Equal(
					Environment.GetFolderPath(Environment.SpecialFolder.Programs),
					actual.ParsingName);

				Dump(actual);
			});
		}

		[Fact]
		public async Task PublicTest()
		{
			await STATask.Run(() =>
			{
				var actual = ShellKnownFolders.Public;

				Assert.NotNull(actual);
				Assert.Equal("Public", actual.CanonicalName);
				Assert.True(actual.ParsingName.EndsWith(@"\Users\Public"));

				Dump(actual);
			});
		}

		[Fact]
		public async Task PublicDesktopTest()
		{
			await STATask.Run(() =>
			{
				var actual = ShellKnownFolders.PublicDesktop;

				Assert.NotNull(actual);
				Assert.Equal("Common Desktop", actual.CanonicalName);
				Assert.Equal(
					Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory),
					actual.ParsingName);

				Dump(actual);
			});
		}

		[Fact]
		public async Task PublicDocumentsTest()
		{
			await STATask.Run(() =>
			{
				var actual = ShellKnownFolders.PublicDocuments;

				Assert.NotNull(actual);
				Assert.Equal("Common Documents", actual.CanonicalName);
				Assert.Equal(
					Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments),
					actual.ParsingName);

				Dump(actual);
			});
		}

		[Fact]
		public async Task PublicDownloadsTest()
		{
			await STATask.Run(() =>
			{
				var actual = ShellKnownFolders.PublicDownloads;

				Assert.NotNull(actual);
				Assert.Equal("CommonDownloads", actual.CanonicalName);
				Assert.Equal(
					Path.Combine(ShellKnownFolders.Public.ParsingName, @"Downloads"),
					actual.ParsingName);

				Dump(actual);
			});
		}

		[Fact]
		public async Task PublicGameTasksTest()
		{
			await STATask.Run(() =>
			{
				var actual = ShellKnownFolders.PublicGameTasks;

				Assert.NotNull(actual);
				Assert.Equal("PublicGameTasks", actual.CanonicalName);
				Assert.Equal(
					Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), @"Microsoft\Windows\GameExplorer"),
					actual.ParsingName);

				Dump(actual);
			});
		}

		[Fact]
		public async Task PublicLibrariesTest()
		{
			await STATask.Run(() =>
			{
				var actual = ShellKnownFolders.PublicLibraries;

				Assert.NotNull(actual);
				Assert.Equal("PublicLibraries", actual.CanonicalName);
				Assert.Equal(
					Path.Combine(ShellKnownFolders.Public.ParsingName, @"Libraries"),
					actual.ParsingName);

				Dump(actual);
			});
		}

		[Fact]
		public async Task PublicMusicTest()
		{
			await STATask.Run(() =>
			{
				var actual = ShellKnownFolders.PublicMusic;

				Assert.NotNull(actual);
				Assert.Equal("CommonMusic", actual.CanonicalName);
				Assert.Equal(
					Environment.GetFolderPath(Environment.SpecialFolder.CommonMusic),
					actual.ParsingName);

				Dump(actual);
			});
		}

		[Fact]
		public async Task PublicPicturesTest()
		{
			await STATask.Run(() =>
			{
				var actual = ShellKnownFolders.PublicPictures;

				Assert.NotNull(actual);
				Assert.Equal("CommonPictures", actual.CanonicalName);
				Assert.Equal(
					Environment.GetFolderPath(Environment.SpecialFolder.CommonPictures),
					actual.ParsingName);

				Dump(actual);
			});
		}

		[Fact]
		public async Task PublicVideoTest()
		{
			await STATask.Run(() =>
			{
				var actual = ShellKnownFolders.PublicVideos;

				Assert.NotNull(actual);
				Assert.Equal("CommonVideo", actual.CanonicalName);
				Assert.Equal(
					Environment.GetFolderPath(Environment.SpecialFolder.CommonVideos),
					actual.ParsingName);

				Dump(actual);
			});
		}

		[Fact]
		public async Task RecycleBinTest()
		{
			await STATask.Run(() =>
			{
				var actual = ShellKnownFolders.RecycleBin;

				Assert.NotNull(actual);
				Assert.Equal("RecycleBinFolder", actual.CanonicalName);

				Dump(actual);
			});
		}

		[Fact]
		public async Task SavedGamesTest()
		{
			await STATask.Run(() =>
			{
				var actual = ShellKnownFolders.SavedGames;

				Assert.NotNull(actual);
				Assert.Equal("SavedGames", actual.CanonicalName);
				Assert.Equal(
					Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Saved Games"),
					actual.ParsingName);

				Dump(actual);
			});
		}

		[Fact]
		public async Task SavedSearchedTest()
		{
			await STATask.Run(() =>
			{
				var actual = ShellKnownFolders.Searches;

				Assert.NotNull(actual);
				Assert.Equal("Searches", actual.CanonicalName);
				Assert.Equal(
					Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Searches"),
					actual.ParsingName);

				Dump(actual);
			});
		}

		[Fact]
		public async Task SearchHistoryTest()
		{
			await STATask.Run(() =>
			{
				var actual = ShellKnownFolders.SearchHistory;

				Assert.NotNull(actual);
				Assert.Equal("SearchHistoryFolder", actual.CanonicalName);
				Assert.Equal(
					Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
						@"Microsoft\Windows\ConnectedSearch\History"),
					actual.ParsingName);

				Dump(actual);
			});
		}

		[Fact]
		public async Task SearchHomeTest()
		{
			await STATask.Run(() =>
			{
				var actual = ShellKnownFolders.SearchHome;

				Assert.NotNull(actual);
				Assert.Equal("SearchHomeFolder", actual.CanonicalName);

				Dump(actual);
			});
		}

		[Fact]
		public async Task SendToTest()
		{
			await STATask.Run(() =>
			{
				var actual = ShellKnownFolders.SendTo;

				Assert.NotNull(actual);
				Assert.Equal("SendTo", actual.CanonicalName);
				Assert.Equal(
					Environment.GetFolderPath(Environment.SpecialFolder.SendTo),
					actual.ParsingName);

				Dump(actual);
			});
		}

		[Fact]
		public async Task StartupTest()
		{
			await STATask.Run(() =>
			{
				var actual = ShellKnownFolders.Startup;

				Assert.NotNull(actual);
				Assert.Equal("Startup", actual.CanonicalName);
				Assert.Equal(
					Environment.GetFolderPath(Environment.SpecialFolder.Startup),
					actual.ParsingName);

				Dump(actual);
			});
		}

		[Fact]
		public async Task UserProfilesTest()
		{
			await STATask.Run(() =>
			{
				var actual = ShellKnownFolders.UserProfiles;

				Assert.NotNull(actual);
				Assert.Equal("UserProfiles", actual.CanonicalName);

				Dump(actual);
			});
		}

		[Fact]
		public async Task UserProfileTest()
		{
			await STATask.Run(() =>
			{
				var actual = ShellKnownFolders.UserProfile;

				Assert.NotNull(actual);
				Assert.Equal("UsersFilesFolder", actual.CanonicalName);
				Assert.Equal(
					Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
					actual.ParsingName);

				Dump(actual);
			});
		}

		[Fact]
		public async Task VideosTest()
		{
			await STATask.Run(() =>
			{
				var actual = ShellKnownFolders.Videos;

				Assert.NotNull(actual);
				Assert.Equal("My Video", actual.CanonicalName);
				Assert.Equal(
					Environment.GetFolderPath(Environment.SpecialFolder.MyVideos),
					actual.ParsingName);

				Dump(actual);
			});
		}

		[Fact]
		public async Task CreateFromCanonicalNameTest()
		{
			await STATask.Run(() =>
			{
				var actual = ShellKnownFolderFactory.FromCanonicalName("DocumentsLibrary");

				Assert.NotNull(actual);
				Assert.Equal("DocumentsLibrary", actual.CanonicalName);

				Dump(actual);
			});
		}

		[Fact]
		public async Task GetComputerFoldersTest()
		{
			await STATask.Run(() =>
			{
				foreach (var folder in ShellKnownFolders.Computer.EnumerateFolders())
				{
					Console.WriteLine("{0}", folder.DisplayName);

					try
					{
						foreach (var item in folder.EnumerateFolders())
						{
							Console.WriteLine("  {0}", item.DisplayName);
						}
					}
					catch (DirectoryNotFoundException ex)
					{
						Console.WriteLine("{0}: {1}", ex.GetType().Name, ex.Message);
					}
				}
			});
		}

		[Fact]
		public async Task ThisPCDesktopFolderTest()
		{
			await STATask.Run(() =>
			{
				var actual = ShellKnownFolderFactory.FromCanonicalName("ThisPCDesktopFolder");

				Assert.NotNull(actual);
				Assert.Equal("ThisPCDesktopFolder", actual.CanonicalName);
				Assert.Equal(KnownFolderCategories.Virtual, actual.Category);

				Dump(actual);
			});
		}

		private static void Dump(ShellKnownFolder folder)
		{
			Console.WriteLine("CanonicalName={0}", folder.CanonicalName);
			Console.WriteLine("DisplayName={0}", folder.DisplayName);
			Console.WriteLine("ParsingName={0}", folder.ParsingName);
			Console.WriteLine("Category={0}", folder.Category);
			Console.WriteLine("Path={0}", folder.Path);
			Console.WriteLine();
		}
	}
}