using System;
using System.Linq;
using System.Threading.Tasks;
using starshipxac.Shell.TestTools;
using Xunit;
using Xunit.Abstractions;

namespace starshipxac.Shell
{
    public class ShellNonFileSystemItemTest : IClassFixture<ShellTestConfig>
    {
        public ShellNonFileSystemItemTest(ShellTestConfig testConfig, ITestOutputHelper output)
        {
            this.TestConfig = testConfig;
            this.Output = output;
        }

        public ShellTestConfig TestConfig { get; }

        private ITestOutputHelper Output { get; }

        /// <summary>
        /// 「すべてのコントロールパネル項目」テスト
        /// </summary>
        [Fact]
        public async Task ControlPanelTest()
        {
            await STATask.Run(() =>
            {
                var controlPanel = ShellKnownFolderFactory.FromKnownFolderId(new Guid("82A74AEB-AEB4-465C-A014-D097EE346D63"));

                Assert.NotNull(controlPanel);

                foreach (var item in controlPanel.EnumerateFiles().Cast<ShellNonFileSystemItem>())
                {
                    Dump(item);
                }
            });
        }

        private void Dump(ShellNonFileSystemItem item)
        {
            Output.WriteLine($"Name = {item.Name}");
            Output.WriteLine($"DisplayName = {item.DisplayName}");
            Output.WriteLine($"ParsingName = {item.ParsingName}");
            if (item.Folder != null)
            {
                Output.WriteLine($"Folder Type = {item.Folder.GetType()}");
                Output.WriteLine($"Folder = {item.Folder}");
            }
            Output.WriteLine("----");
        }
    }
}