using System;
using System.Diagnostics;
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

                actual.IsNotNull();
                actual.Path.Is(path);
                actual.ParsingName.Is(path);
                actual.Name.Is(TestConfig.TestDirectoryName);
                actual.Parent.ParsingName.Is(TestConfig.TestDirectory.Parent?.FullName);

                // Flags
                actual.IsFileSystem.IsTrue();
            });
        }

        [Fact]
        public async Task FolderPropertyTest()
        {
            await STATask.Run(() =>
            {
                var path = TestConfig.TestDirectory.FullName;
                var actual = ShellFactory.FromFolderPath(path);

                actual.Parent.IsNotNull();
                actual.Parent.ParsingName.Is(TestConfig.TestDirectory.Parent?.FullName);
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
                folder1.IsNotNull();

                var file1 = actual.Find(item => ShellTestConfig.CompareFileName(item.Name, "Test.txt"));
                file1.IsNotNull();
                file1.IsInstanceOf<ShellFile>();

                var file2 = actual.Find(Item => ShellTestConfig.CompareFileName(Item.Name, "Test2.txt"));
                file2.IsNotNull();
                file2.IsInstanceOf<ShellFile>();
            });
        }

        [Fact]
        public async Task GetFolderTest()
        {
            await STATask.Run(() =>
            {
                var desktop = ShellKnownFolders.Desktop;
                var actual = desktop.GetFolder();
                actual.IsNull();
            });
        }

        [Fact]
        public async Task EqualsTest1()
        {
            await STATask.Run(() =>
            {
                var folder1 = ShellFactory.FromFolderPath(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
                var folder2 = ShellFactory.FromFolderPath(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));

                folder1.IsNotSameReferenceAs(folder2);

                folder1.Equals(folder2).IsTrue();
                (folder1 == folder2).IsTrue();
                (folder1.Path == folder2.Path).IsTrue();
                (folder1.GetHashCode() == folder2.GetHashCode()).IsTrue();
            });
        }

        [Fact]
        public async Task EqualsTest2()
        {
            await STATask.Run(() =>
            {
                var folder1 = ShellFactory.FromFolderPath(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
                var folder2 = ShellFactory.FromFolderPath(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));

                folder1.IsNotSameReferenceAs(folder2);

                folder1.Equals(folder2).IsFalse();
                (folder1 == folder2).IsFalse();
                (folder1.Path == folder2.Path).IsFalse();
                (folder1.GetHashCode() == folder2.GetHashCode()).IsFalse();
            });
        }

        [Fact]
        public async Task NotEqualsTest1()
        {
            await STATask.Run(() =>
            {
                var folder1 = ShellFactory.FromFolderPath(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
                var folder2 = ShellFactory.FromFolderPath(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));

                folder1.IsNotSameReferenceAs(folder2);

                folder1.Equals(folder2).IsTrue();
                (folder1 == folder2).IsTrue();
                (folder1 != folder2).IsFalse();
                (folder1.Path == folder2.Path).IsTrue();
                (folder1.GetHashCode() == folder2.GetHashCode()).IsTrue();
            });
        }

        [Fact]
        public async Task NotEqualsTest2()
        {
            await STATask.Run(() =>
            {
                var folder1 = ShellFactory.FromFolderPath(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
                var folder2 = ShellFactory.FromFolderPath(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));

                folder1.IsNotSameReferenceAs(folder2);

                folder1.Equals(folder2).IsFalse();
                (folder1 == folder2).IsFalse();
                (folder1 != folder2).IsTrue();
                (folder1.Path == folder2.Path).IsFalse();
                (folder1.GetHashCode() == folder2.GetHashCode()).IsFalse();
            });
        }

        [Fact]
        public void EnumerateFoldersTest1()
        {
            var pictureFolder = ShellLibraries.PicturesLibrary;

            var childFolders1 = pictureFolder.EnumerateObjects()
                .OfType<ShellFolder>()
                .ToList();
            foreach (var child in childFolders1)
            {
                Debug.WriteLine(child);
            }
            Console.WriteLine();

            var folder = childFolders1.FirstOrDefault(x => x.Name == "2ch");
            folder.IsNotNull();

            var parentFolder = folder.GetFolder();
            var childFolders2 = parentFolder.EnumerateObjects()
                .OfType<ShellFolder>()
                .ToList();
            foreach (var child in childFolders2)
            {
                Debug.WriteLine(child);
            }
        }
    }
}