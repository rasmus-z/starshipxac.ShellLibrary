using System;
using starshipxac.Shell.TestConfiguration;
using Xunit;

namespace starshipxac.Shell
{
	public class ShellLinkTest : IClassFixture<ShellTestConfig>
	{
		public ShellLinkTest(ShellTestConfig testConfig)
		{
			this.TestConfig = testConfig;
		}

		public ShellTestConfig TestConfig { get; private set; }

		//[Fact]
		//public async Task PathPropertyTest()
		//{
		//	await STATask.Run(() =>
		//	{
		//		const string filename = "Test.txt - ShortCut.lnk";
		//		var path = Path.Combine(TestConfig.TestDirectory.FullName, filename);

		//		var actual = (ShellLink)ShellFactory.FromParsingName(path);

		//		Assert.Equal(path, actual.Path);
		//	});
		//}

		//[Fact]
		//public async Task NamePropertyTest()
		//{
		//	await STATask.Run(() =>
		//	{
		//		const string filename = "Test.txt - ShortCut.lnk";
		//		var path = Path.Combine(TestConfig.TestDirectory.FullName, filename);

		//		var actual = (ShellLink)ShellFactory.FromParsingName(path);

		//		Assert.Equal("Test.txt - ShortCut.lnk", actual.Name);
		//	});
		//}

		//[Fact]
		//public async Task DisplayNamePropertyTest()
		//{
		//	await STATask.Run(() =>
		//	{
		//		const string filename = "Test.txt - ShortCut.lnk";
		//		var path = Path.Combine(TestConfig.TestDirectory.FullName, filename);

		//		var actual = (ShellLink)ShellFactory.FromParsingName(path);

		//		Assert.Equal("Test.txt - ShortCut", actual.DisplayName);
		//	});
		//}

		//[Fact]
		//public async Task TargetLocationPropertyTest()
		//{
		//	await STATask.Run(() =>
		//	{
		//		const string filename = "Test.txt - ShortCut.lnk";
		//		var path = Path.Combine(TestConfig.TestDirectory.FullName, filename);

		//		var actual = (ShellLink)ShellFactory.FromParsingName(path);

		//		Assert.Equal(
		//			Path.Combine(TestConfig.TestDirectory.FullName, "Test.txt"),
		//			actual.TargetLocation);
		//	});
		//}

		//[Fact]
		//public async Task TargetShellObjectPropertyTest()
		//{
		//	await STATask.Run(() =>
		//	{
		//		const string filename = "Test.txt - ShortCut.lnk";
		//		var path = Path.Combine(TestConfig.TestDirectory.FullName, filename);

		//		var actual = (ShellLink)ShellFactory.FromParsingName(path);

		//		Assert.NotNull(actual.Target);
		//		Assert.Equal(
		//			ShellFactory.FromParsingName(Path.Combine(TestConfig.TestDirectory.FullName, "Test.txt")),
		//			actual.Target);
		//	});
		//}

		//[Fact]
		//public async Task ArgumentsPropertyTest()
		//{
		//	await STATask.Run(() =>
		//	{
		//		const string filename = "Test.txt - ShortCut.lnk";
		//		var path = Path.Combine(TestConfig.TestDirectory.FullName, filename);

		//		var actual = (ShellLink)ShellFactory.FromParsingName(path);

		//		Assert.Equal("arg1 arg2", actual.Arguments);
		//	});
		//}

		//[Fact]
		//public async Task CommentsPropertyTest()
		//{
		//	await STATask.Run(() =>
		//	{
		//		const string filename = "Test.txt - ShortCut.lnk";
		//		var path = Path.Combine(TestConfig.TestDirectory.FullName, filename);

		//		var actual = (ShellLink)ShellFactory.FromParsingName(path);

		//		Assert.Equal("comment", actual.Comment);
		//	});
		//}
	}
}