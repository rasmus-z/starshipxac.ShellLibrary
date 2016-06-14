using System;
using System.IO;

namespace starshipxac.Shell.TestTools
{
    /// <summary>
    ///     テスト情報を保持します。
    /// </summary>
    public class ShellTestConfig : IDisposable
    {
        public ShellTestConfig()
        {
            var dir = new DirectoryInfo(Directory.GetCurrentDirectory());
            while (!CompareFileName(dir.Name, "UnitTest"))
            {
                dir = dir.Parent;
            }
            this.TestDirectory = new DirectoryInfo(Path.Combine(dir.FullName, this.TestDirectoryName));
        }

        public void Dispose()
        {
        }

        /// <summary>
        ///     テスト用ファイルを格納するディレクトリの名前を取得します。
        /// </summary>
        public string TestDirectoryName => "TestFolder";

        /// <summary>
        ///     テスト用ファイルを格納するディレクトリの情報を取得します。
        /// </summary>
        public DirectoryInfo TestDirectory { get; private set; }

        /// <summary>
        ///     ファイル名を比較します。
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool CompareFileName(string x, string y)
        {
            return String.Compare(x, y, StringComparison.InvariantCultureIgnoreCase) == 0;
        }
    }
}