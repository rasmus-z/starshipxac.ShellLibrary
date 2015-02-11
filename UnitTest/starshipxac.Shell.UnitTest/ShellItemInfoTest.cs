using System;
using System.Threading.Tasks;
using starshipxac.ShellTest;
using Xunit;

namespace starshipxac.Shell
{
	public class ShellItemInfoTest : IClassFixture<ShellTestConfig>
	{
		[Fact]
		public async Task FromParsingNameTest1()
		{
			await STATask.Run(() =>
			{
				const string parsingName = @"::{031E4825-7B94-4DC3-B131-E946B44C8DD5}\Documents.library-ms";

				var actual = ShellItem.FromParsingName(parsingName);

				Assert.NotNull(actual);
				Assert.Equal(parsingName, actual.ParsingName);
				Assert.Equal(".library-ms", actual.ItemType);

				Console.WriteLine("ParsingName = {0}", actual.ParsingName);
				Console.WriteLine("ItemType = {0}", actual.ItemType);

				var folder = ShellFactory.Instance.Create(actual);
				Console.WriteLine("folder.ParsingName = {0}", folder.ParsingName);
				Console.WriteLine("folder.Type = {0}", folder.GetType());
			});
		}
	}
}