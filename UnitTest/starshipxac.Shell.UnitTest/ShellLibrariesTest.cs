﻿using System;
using System.Threading.Tasks;
using starshipxac.Shell.TestTools;
using Xunit;
using Xunit.Abstractions;

namespace starshipxac.Shell
{
    public class ShellLibrariesTest : IClassFixture<ShellTestConfig>
    {
        public ShellLibrariesTest(ShellTestConfig testConfig, ITestOutputHelper outputHelper)
        {
            this.TestConfig = testConfig;
            this.Output = outputHelper;
        }

        public ShellTestConfig TestConfig { get; }

        public ITestOutputHelper Output { get; }

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

                Assert.Equal(LibraryFolderTypes.Documents, actual.LibraryType);

                Dump(actual);
            });
        }

        [Fact]
        public async Task MusicLibraryTest()
        {
            await STATask.Run(() =>
            {
                var actual = ShellLibraries.MusicLibrary;

                Assert.Equal(LibraryFolderTypes.Music, actual.LibraryType);

                Dump(actual);
            });
        }

        [Fact]
        public async Task PictureLibraryTest()
        {
            await STATask.Run(() =>
            {
                var actual = ShellLibraries.PicturesLibrary;

                Assert.Equal(LibraryFolderTypes.Pictures, actual.LibraryType);

                Dump(actual);
            });
        }

        [Fact]
        public async Task VideosLibraryTest()
        {
            await STATask.Run(() =>
            {
                var actual = ShellLibraries.VideosLibrary;

                Assert.Equal(LibraryFolderTypes.Videos, actual.LibraryType);

                Dump(actual);
            });
        }

        [Fact]
        public async Task RecoredTVLibraryTest()
        {
            await STATask.Run(() =>
            {
                var actual = ShellLibraries.RecordedTVLibrary;

                Assert.Equal(LibraryFolderTypes.Videos, actual.LibraryType);

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
                        foreach (var item in library.EnumerateObjects())
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

        private void Dump(ShellLibrary shellLibrary)
        {
            try
            {
                this.Output.WriteLine("Name={0}", shellLibrary.Name);
                this.Output.WriteLine("DisplayName={0}", shellLibrary.DisplayName);
                this.Output.WriteLine("ParsingName={0}", shellLibrary.ParsingName);
                this.Output.WriteLine("LibraryFolders={0}", String.Join(", ", shellLibrary.EnumerateFolders()));
                //this.Output.WriteLine("LibraryType={0}", shellLibrary.LibraryType);
            }
            catch (Exception ex)
            {
                this.Output.WriteLine(ex.Message);
            }
            this.Output.WriteLine("");
        }
    }
}