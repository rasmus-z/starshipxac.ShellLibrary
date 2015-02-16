using System;
using System.Threading.Tasks;
using starshipxac.Shell.TestTools;
using Xunit;

namespace starshipxac.Shell
{
    public class ShellLibrariesTest : IClassFixture<ShellTestConfig>
    {
        public ShellLibrariesTest(ShellTestConfig testConfig)
        {
            this.TestConfig = testConfig;
        }

        public ShellTestConfig TestConfig { get; private set; }

        [Fact]
        public async Task GetAllLibrariesTest()
        {
            await STATask.Run(() =>
            {
                foreach (var library in ShellLibraries.EnumerateLibraries())
                {
                    Dump(library);
                }
            });
        }

        [Fact]
        public async Task GetAllPublicLibrariesTest()
        {
            await STATask.Run(() =>
            {
                foreach (var library in ShellLibraries.EnumeratePublicLibraries())
                {
                    Dump(library);
                }
            });
        }

        [Fact]
        public async Task DocumentsLibraryTest()
        {
            await STATask.Run(() =>
            {
                var actual = ShellLibraries.DocumentsLibrary;

                Assert.Equal(LibraryFolderType.Documents, actual.LibraryType);

                Dump(actual);
            });
        }

        [Fact]
        public async Task MusicLibraryTest()
        {
            await STATask.Run(() =>
            {
                var actual = ShellLibraries.MusicLibrary;

                Assert.Equal(LibraryFolderType.Music, actual.LibraryType);

                Dump(actual);
            });
        }

        [Fact]
        public async Task PictureLibraryTest()
        {
            await STATask.Run(() =>
            {
                var actual = ShellLibraries.PicturesLibrary;

                Assert.Equal(LibraryFolderType.Pictures, actual.LibraryType);

                Dump(actual);
            });
        }

        [Fact]
        public async Task VideosLibraryTest()
        {
            await STATask.Run(() =>
            {
                var actual = ShellLibraries.VideosLibrary;

                Assert.Equal(LibraryFolderType.Videos, actual.LibraryType);

                Dump(actual);
            });
        }

        [Fact]
        public async Task RecoredTVLibraryTest()
        {
            await STATask.Run(() =>
            {
                var actual = ShellLibraries.RecordedTVLibrary;

                Assert.Equal(LibraryFolderType.Videos, actual.LibraryType);

                Dump(actual);
            });
        }

        [Fact]
        public async Task AllLibraryItemsTest()
        {
            await STATask.Run(() =>
            {
                try
                {
                    foreach (var library in ShellLibraries.EnumerateLibraries())
                    {
                        Console.WriteLine("Library: {0}", library.DisplayName);
                        foreach (var item in library.EnumerateItems())
                        {
                            Console.WriteLine("  {0}", item.DisplayName);
                        }
                    }
                }
                catch (ShellException ex)
                {
                    Console.WriteLine(ex);
                    Console.WriteLine("FileNotFoundExceptionが発生する場合は、プロジェクトのプロパティ -> ビルドの「32ビットを優先」のチェックを外す。");
                }
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