using System;
using System.IO;
using System.Threading.Tasks;
using starshipxac.ShellTest;
using Xunit;

namespace starshipxac.Shell
{
	public class ShellFileTest : IClassFixture<ShellTestConfig>
	{
		public ShellFileTest(ShellTestConfig testConfig)
		{
			this.TestConfig = testConfig;
		}

		public ShellTestConfig TestConfig { get; private set; }

		[Fact]
		public async Task FromFilePathTest()
		{
			await STATask.Run(() =>
			{
				const string filename = @"Test.txt";
				var path = Path.Combine(TestConfig.TestDirectory.FullName, filename);
				var actual = ShellFactory.FromFilePath(path);

				// Path
				Assert.NotNull(actual);
				Assert.Equal(path, actual.Path);
				Assert.Equal(path, actual.ParsingName);
				Assert.Equal(filename, actual.Name);
				Assert.Equal(".txt", actual.Extension);

				// Flags
				Assert.True(actual.IsFileSystem);
			});
		}

		[Fact]
		public async Task FromFilePathErrorTest()
		{
			await STATask.Run(() =>
			{
				const string filename = @"xxxx.txt";
				var path = Path.Combine(TestConfig.TestDirectory.FullName, filename);

				var ex = Assert.Throws<FileNotFoundException>(() => ShellFactory.FromFilePath(path));
				Assert.Equal(path, ex.FileName);
			});
		}

		[Fact]
		public async Task ParentPropertyTest()
		{
			await STATask.Run(() =>
			{
				const string fileName = @"Test.txt";
				var path = Path.Combine(TestConfig.TestDirectory.FullName, fileName);
				var actual = ShellFactory.FromFilePath(path);

				Assert.NotNull(actual.Parent);
				Assert.IsType<ShellFolder>(actual.Parent);
				Assert.Equal(TestConfig.TestDirectory.FullName, actual.Parent.ParsingName);
			});
		}

		[Fact]
		public async Task SizePropertyTest()
		{
			await STATask.Run(() =>
			{
				const string fileName = @"Test.txt";
				var path = Path.Combine(TestConfig.TestDirectory.FullName, fileName);
				var actual = ShellFactory.FromFilePath(path);

				Console.WriteLine(actual.Size);
				Assert.True(0 < actual.Size);
			});
		}

		[Fact]
		public async Task GetDisplayNameTest()
		{
			await STATask.Run(() =>
			{
				const string fileName = @"Test.txt";
				var path = Path.Combine(TestConfig.TestDirectory.FullName, fileName);
				var actual = ShellFactory.FromFilePath(path);

				Assert.NotNull(actual);
				Assert.Equal(fileName, actual.GetDisplayName(DisplayNameTypes.Default));
				Assert.Equal(fileName, actual.GetDisplayName(DisplayNameTypes.RelativeToParent));
				Assert.Equal(fileName, actual.GetDisplayName(DisplayNameTypes.RelativeToParentAddressBar));
				Assert.Equal(path, actual.GetDisplayName(DisplayNameTypes.RelativeToDesktop));
				Assert.Equal(fileName, actual.GetDisplayName(DisplayNameTypes.RelativeToParentEditing));
				Assert.Equal(path, actual.GetDisplayName(DisplayNameTypes.RelativeToDesktopEditing));
				Assert.Equal(path, actual.GetDisplayName(DisplayNameTypes.FileSystemPath));
			});
		}

		[Fact]
		public async Task EqualsTest()
		{
			await STATask.Run(() =>
			{
				var shellFile1 = ShellFactory.FromFilePath(Path.Combine(TestConfig.TestDirectory.FullName, @"Test.txt"));
				var shellFile2 = ShellFactory.FromFilePath(Path.Combine(TestConfig.TestDirectory.FullName, @"Test.txt"));

				Assert.True(shellFile1.Equals(shellFile2));
			});
		}

		[Fact]
		public async Task NotEqualsTest()
		{
			await STATask.Run(() =>
			{
				var shellFile1 = ShellFactory.FromFilePath(Path.Combine(TestConfig.TestDirectory.FullName, @"Test.txt"));
				var shellFile2 = ShellFactory.FromFilePath(Path.Combine(TestConfig.TestDirectory.FullName, @"Test2.txt"));

				Assert.False(shellFile1.Equals(shellFile2));
			});
		}

		[Fact]
		public async Task EqualsObjectTest()
		{
			await STATask.Run(() =>
			{
				var shellFile1 = ShellFactory.FromFilePath(Path.Combine(TestConfig.TestDirectory.FullName, @"Test.txt"));
				Object shellFile2 = ShellFactory.FromFilePath(Path.Combine(TestConfig.TestDirectory.FullName, @"Test.txt"));

				Assert.True(shellFile1.Equals(shellFile2));
			});
		}

		[Fact]
		public async Task NotEqualsTest1()
		{
			await STATask.Run(() =>
			{
				var shellFile1 = ShellFactory.FromFilePath(Path.Combine(TestConfig.TestDirectory.FullName, @"Test.txt"));
				Object shellFile2 = ShellFactory.FromFilePath(Path.Combine(TestConfig.TestDirectory.FullName, @"Test2.txt"));

				Assert.False(shellFile1.Equals(shellFile2));
			});
		}

		[Fact]
		public async Task NotEqualsTest2()
		{
			await STATask.Run(() =>
			{
				var shellFile1 = ShellFactory.FromFilePath(Path.Combine(TestConfig.TestDirectory.FullName, @"Test.txt"));
				Object shellFile2 = "abcdefg";

				Assert.False(shellFile1.Equals(shellFile2));
			});
		}

		[Fact]
		public async Task OperatorEqualsTest1()
		{
			await STATask.Run(() =>
			{
				var shellFile1 = ShellFactory.FromFilePath(Path.Combine(TestConfig.TestDirectory.FullName, @"Test.txt"));
				var shellFile2 = ShellFactory.FromFilePath(Path.Combine(TestConfig.TestDirectory.FullName, @"Test.txt"));

				Assert.True(shellFile1 == shellFile2);
			});
		}

		[Fact]
		public async Task OperatorEqualsTest2()
		{
			await STATask.Run(() =>
			{
				var shellFile1 = ShellFactory.FromFilePath(Path.Combine(TestConfig.TestDirectory.FullName, @"Test.txt"));
				var shellFile2 = ShellFactory.FromFilePath(Path.Combine(TestConfig.TestDirectory.FullName, @"Test2.txt"));

				Assert.False(shellFile1 == shellFile2);
			});
		}

		[Fact]
		public async Task OperatorNotEqualsTest1()
		{
			await STATask.Run(() =>
			{
				var shellFile1 = ShellFactory.FromFilePath(Path.Combine(TestConfig.TestDirectory.FullName, @"Test.txt"));
				var shellFile2 = ShellFactory.FromFilePath(Path.Combine(TestConfig.TestDirectory.FullName, @"Test2.txt"));

				Assert.True(shellFile1 != shellFile2);
			});
		}

		[Fact]
		public async Task OperatorNotEqualsTest2()
		{
			await STATask.Run(() =>
			{
				var shellFile1 = ShellFactory.FromFilePath(Path.Combine(TestConfig.TestDirectory.FullName, @"Test.txt"));
				var shellFile2 = ShellFactory.FromFilePath(Path.Combine(TestConfig.TestDirectory.FullName, @"Test.txt"));

				Assert.False(shellFile1 != shellFile2);
			});
		}

		[Fact]
		public async Task GetHashCodeTest()
		{
			await STATask.Run(() =>
			{
				const string fileName = @"Test.txt";
				var path = Path.Combine(TestConfig.TestDirectory.FullName, fileName);
				var actual = ShellFactory.FromFilePath(path);

				Assert.NotEqual(0, actual.GetHashCode());
			});
		}

		[Fact]
		public async Task ToStringTest()
		{
			await STATask.Run(() =>
			{
				const string fileName = @"Test.txt";
				var path = Path.Combine(TestConfig.TestDirectory.FullName, fileName);
				var actual = ShellFactory.FromFilePath(path);

				Console.WriteLine(actual.ToString());
				Assert.NotEqual(-1, actual.ToString().IndexOf(path, StringComparison.InvariantCultureIgnoreCase));
			});
		}
	}
}