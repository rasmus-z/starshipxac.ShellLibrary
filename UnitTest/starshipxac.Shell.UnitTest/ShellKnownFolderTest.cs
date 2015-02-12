using System;
using System.Threading.Tasks;
using starshipxac.Shell.TestConfiguration;
using Xunit;

namespace starshipxac.Shell
{
	public class ShellKnownFolderTest : IClassFixture<ShellTestConfig>
	{
		public ShellKnownFolderTest(ShellTestConfig testConfig)
		{
			this.TestConfig = testConfig;
		}

		public ShellTestConfig TestConfig { get; private set; }

		/// <summary>
		/// 「コンピューター」テスト
		/// </summary>
		[Fact]
		public async Task ComputerTest()
		{
			await STATask.Run(() =>
			{
				var actual = ShellKnownFolders.Computer;

				Assert.NotNull(actual);
				Assert.False(actual.IsFileSystem);

				Assert.False(actual.PathExists);
				Assert.Empty(actual.Path);
				Assert.Empty(actual.RelativePath);

				Assert.Equal(KnownFolderCategories.Virtual, actual.Category);
			});
		}

		/// <summary>
		/// 「すべてのコントロールパネル項目」テスト
		/// </summary>
		[Fact]
		public async Task ControlPanelTest()
		{
			await STATask.Run(() =>
			{
				var actual = ShellKnownFolderFactory.FromKnownFolderId(new Guid("82A74AEB-AEB4-465C-A014-D097EE346D63"));

				Assert.NotNull(actual);
				Assert.False(actual.IsFileSystem);

				Assert.False(actual.PathExists);
				Assert.Empty(actual.Path);
				Assert.Empty(actual.RelativePath);

				Assert.Equal(KnownFolderCategories.Virtual, actual.Category);

				// Parentは「コントロールパネル」
				Assert.NotNull(actual.Parent);
				Assert.NotNull(actual.ParentFolder);
			});
		}

		/// <summary>
		/// 「インターネット」フォルダー
		/// </summary>
		[Fact]
		public async Task InternetTest()
		{
			await STATask.Run(() =>
			{
				var actual = ShellKnownFolderFactory.FromKnownFolderId(new Guid("4D9F7874-4E0C-4904-967B-40B0D20C3E4B"));

				Assert.NotNull(actual);
				Assert.False(actual.IsFileSystem);

				Assert.False(actual.PathExists);
				Assert.Empty(actual.Path);
				Assert.Empty(actual.RelativePath);

				Assert.Equal(KnownFolderCategories.Virtual, actual.Category);
			});
		}

		/// <summary>
		/// 「デスクトップ」テスト
		/// </summary>
		[Fact]
		public async Task DesktopTest()
		{
			await STATask.Run(() =>
			{
				var folder = ShellKnownFolders.Desktop;

				Assert.NotNull(folder);
				Assert.True(folder.IsFileSystem);

				Assert.True(folder.PathExists);

				Assert.Equal(KnownFolderCategories.PerUser, folder.Category);

				Assert.Null(folder.Parent);
				Assert.Null(folder.ParentFolder);
			});
		}
	}
}