﻿using System;
using System.Threading.Tasks;
using starshipxac.Shell.TestTools;
using Xunit;

namespace starshipxac.Shell
{
    public class ShellItemTest : IClassFixture<ShellTestConfig>
    {
        [Fact]
        public async Task FromParsingNameTest1()
        {
            await STATask.Run(() =>
            {
                const string parsingName = @"::{031E4825-7B94-4DC3-B131-E946B44C8DD5}\Documents.library-ms";

                var actual = ShellItem.FromParsingName(parsingName);

                Assert.NotNull(actual);
                Assert.Equal(parsingName, actual.GetParsingName());
                Assert.Equal(".library-ms", actual.GetItemType());

                Console.WriteLine("ParsingName = {0}", actual.GetParsingName());
                Console.WriteLine("ItemType = {0}", actual.GetItemType());

                var folder = ShellFactory.FromShellItem(actual);
                Console.WriteLine("folder.ParsingName = {0}", folder.ParsingName);
                Console.WriteLine("folder.Type = {0}", folder.GetType());
            });
        }

        //[Fact]
        //public void HashCodeTest()
        //{
        //    var item1 = ShellItem.FromParsingName(@"C:\Windows");
        //    var item2 = ShellItem.FromParsingName(@"C:\Windows");

        //    (item1.GetHashCode() == item2.GetHashCode()).IsTrue();
        //}
    }
}