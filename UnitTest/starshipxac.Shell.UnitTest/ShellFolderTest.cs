using System;
using System.Linq;
using System.Threading.Tasks;
using starshipxac.Shell.TestTools;
using Xunit;

namespace starshipxac.Shell
{
    public class ShellFolderTest : IClassFixture<ShellTestConfig>
    {
        public ShellFolderTest(ShellTestConfig testConfig)
        {
            this.TestConfig = testConfig;
        }

        public ShellTestConfig TestConfig { get; }

        [Fact]
        public async Task FromFolderPathTest()
        {
            await STATask.Run(() =>
            {
                var path = TestConfig.TestDirectory.FullName;
                var actual = ShellFactory.FromFolderPath(path);

                Assert.NotNull(actual);
                Assert.Equal(path, actual.Path);
                Assert.Equal(path, actual.ParsingName);
                Assert.Equal(TestConfig.TestDirectoryName, actual.Name);
                Assert.Equal(TestConfig.TestDirectory.Parent?.FullName, actual.Parent.ParsingName);

                // Flags
                Assert.True(actual.IsFileSystem);
            });
        }

        [Fact]
        public async Task FolderPropertyTest()
        {
            await STATask.Run(() =>
            {
                var path = TestConfig.TestDirectory.FullName;
                var actual = ShellFactory.FromFolderPath(path);

                Assert.NotNull(actual.Parent);
                Assert.Equal(TestConfig.TestDirectory.Parent?.FullName, actual.Parent.ParsingName);
            });
        }

        [Fact]
        public async Task GetItemsTest()
        {
            await STATask.Run(() =>
            {
                var path = TestConfig.TestDirectory.FullName;
                var folder = ShellFactory.FromFolderPath(path);
                var actual = folder.EnumerateObjects().ToList();

                var folder1 = actual.Find(item => ShellTestConfig.CompareFileName(item.Name, "Pictures"));
                Assert.NotNull(folder1);

                var file1 = actual.Find(item => ShellTestConfig.CompareFileName(item.Name, "Test.txt"));
                Assert.NotNull(file1);
                Assert.IsType<ShellFile>(file1);

                var file2 = actual.Find(Item => ShellTestConfig.CompareFileName(Item.Name, "Test2.txt"));
                Assert.NotNull(file2);
                Assert.IsType<ShellFile>(file2);
            });
        }

        [Fact]
        public async Task EqualsTest1()
        {
            await STATask.Run(() =>
            {
                var folder1 = ShellFactory.FromFolderPath(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
                var folder2 = ShellFactory.FromFolderPath(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));

                Assert.NotSame(folder1, folder2);

                Assert.True(folder1.Equals(folder2));
                Assert.True(folder1 == folder2);

                Assert.True(folder1.Path == folder2.Path);
                Assert.Equal(folder1.GetHashCode(), folder2.GetHashCode());
            });
        }

        [Fact]
        public async Task EqualsTest2()
        {
            await STATask.Run(() =>
            {
                var folder1 = ShellFactory.FromFolderPath(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
                var folder2 = ShellFactory.FromFolderPath(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));

                Assert.NotSame(folder1, folder2);

                Assert.False(folder1.Path == folder2.Path);
                Assert.False(folder1.Equals(folder2));
                Assert.False(folder1 == folder2);
                Assert.NotEqual(folder1.GetHashCode(), folder2.GetHashCode());
            });
        }

        [Fact]
        public async Task NotEqualsTest1()
        {
            await STATask.Run(() =>
            {
                var folder1 = ShellFactory.FromFolderPath(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
                var folder2 = ShellFactory.FromFolderPath(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));

                Assert.NotSame(folder1, folder2);
                Assert.True(folder1.Path == folder2.Path);
                Assert.True(folder1.Equals(folder2));
                Assert.False(folder1 != folder2);
                Assert.Equal(folder1.GetHashCode(), folder2.GetHashCode());
            });
        }

        [Fact]
        public async Task NotEqualsTest2()
        {
            await STATask.Run(() =>
            {
                var folder1 = ShellFactory.FromFolderPath(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
                var folder2 = ShellFactory.FromFolderPath(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));

                Assert.NotSame(folder1, folder2);
                Assert.False(folder1.Path == folder2.Path);
                Assert.False(folder1.Equals(folder2));
                Assert.True(folder1 != folder2);
                Assert.NotEqual(folder1.GetHashCode(), folder2.GetHashCode());
            });
        }
    }
}