using System;
using System.Threading.Tasks;
using starshipxac.Shell.TestConfiguration;
using Xunit;

namespace starshipxac.Shell
{
	public class ShellLibraryTest : IClassFixture<ShellTestConfig>
	{
		public ShellLibraryTest(ShellTestConfig testConfig)
		{
			this.TestConfig = testConfig;
		}

		public ShellTestConfig TestConfig { get; private set; }

		[Fact]
		public async Task LoadKnownFolderTest()
		{
			await STATask.Run(() =>
			{
				var actual = ShellLibraryFactory.Load(ShellKnownFolderFactory.FromCanonicalName("PicturesLibrary"));

				Assert.NotNull(actual);
				Assert.Equal(LibraryFolderType.Pictures, actual.LibraryType);

				Dump(actual);
			});
		}

		[Fact]
		public async Task LibrariesKnownFolderPropertyTest()
		{
			await STATask.Run(() =>
			{
				foreach (var library in ShellKnownFolders.Libraries.EnumerateFiles())
				{
					Assert.NotNull(library);
					Assert.IsType<ShellLibrary>(library);

					Console.WriteLine("Name = {0}", library.Name);
					Console.WriteLine("ParsingName = {0}", library.ParsingName);
				}
			});
		}

		[Fact]
		public async Task GetDisplayNameTest()
		{
			await STATask.Run(() =>
			{
				var actual = ShellLibraries.PicturesLibrary;

				Assert.NotNull(actual);

				Console.WriteLine("Default=" + actual.GetDisplayName(DisplayNameTypes.Default));
				Console.WriteLine("RelativeToParent=" + actual.GetDisplayName(DisplayNameTypes.RelativeToParent));
				Console.WriteLine("RelativeTOParentAddressBar=" + actual.GetDisplayName(DisplayNameTypes.RelativeToParentAddressBar));
				Console.WriteLine("RelativeToDesktop=" + actual.GetDisplayName(DisplayNameTypes.RelativeToDesktop));
				Console.WriteLine("RelativeToParentEditing=" + actual.GetDisplayName(DisplayNameTypes.RelativeToParentEditing));
				Console.WriteLine("RelativeToDesktopEditing=" + actual.GetDisplayName(DisplayNameTypes.RelativeToDesktopEditing));
				Assert.Throws<ShellException>(() => actual.GetDisplayName(DisplayNameTypes.FileSystemPath));
				Assert.Throws<ShellException>(() => actual.GetDisplayName(DisplayNameTypes.Url));
			});
		}

		[Fact]
		public async Task DocumentsLibraryTest()
		{
			await STATask.Run(() =>
			{
				var actual = ShellLibraries.DocumentsLibrary;

				Assert.NotNull(actual);
				Assert.Equal(LibraryFolderType.Documents, actual.LibraryType);
				Assert.Equal("Documents.library-ms", actual.Name);
				Assert.Equal(@"::{031E4825-7B94-4DC3-B131-E946B44C8DD5}\Documents.library-ms", actual.ParsingName);

				Dump(actual);
			});
		}

		[Fact]
		public async Task MusicLibraryTest()
		{
			await STATask.Run(() =>
			{
				var actual = ShellLibraries.MusicLibrary;

				Assert.NotNull(actual);
				Assert.Equal(LibraryFolderType.Music, actual.LibraryType);
				Assert.Equal("Music.library-ms", actual.Name);
				Assert.Equal(@"::{031E4825-7B94-4DC3-B131-E946B44C8DD5}\Music.library-ms", actual.ParsingName);

				Dump(actual);
			});
		}

		[Fact]
		public async Task PictureLibraryTest()
		{
			await STATask.Run(() =>
			{
				var actual = ShellLibraries.PicturesLibrary;

				Assert.NotNull(actual);
				Assert.Equal(LibraryFolderType.Pictures, actual.LibraryType);
				Assert.Equal("Pictures.library-ms", actual.Name);
				Assert.Equal(@"::{031E4825-7B94-4DC3-B131-E946B44C8DD5}\Pictures.library-ms", actual.ParsingName);

				Dump(actual);
			});
		}

		[Fact]
		public async Task VideosLibraryTest()
		{
			await STATask.Run(() =>
			{
				var actual = ShellLibraries.VideosLibrary;

				Assert.NotNull(actual);
				Assert.Equal(LibraryFolderType.Videos, actual.LibraryType);
				Assert.Equal("Videos.library-ms", actual.Name);
				Assert.Equal(@"::{031E4825-7B94-4DC3-B131-E946B44C8DD5}\Videos.library-ms", actual.ParsingName);

				Dump(actual);
			});
		}

		private static void Dump(ShellLibrary shellLibrary)
		{
			Console.WriteLine("Name={0}", shellLibrary.Name);
			Console.WriteLine("DisplayName={0}", shellLibrary.DisplayName);
			Console.WriteLine("ParsingName={0}", shellLibrary.ParsingName);
			Console.WriteLine("LibraryFolder={0}", shellLibrary.LibraryType);
			Console.WriteLine("LibraryType={0}", shellLibrary.LibraryType);
			Console.WriteLine();
		}
	}
}