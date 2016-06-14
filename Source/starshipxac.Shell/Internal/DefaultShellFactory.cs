using System;

namespace starshipxac.Shell.Internal
{
    /// <summary>
    ///     デフォルトの<see cref="ShellFactory" />を定義します。
    /// </summary>
    internal class DefaultShellFactory : ShellFactory
    {
        static DefaultShellFactory()
        {
            Default = new DefaultShellFactory();
        }

        public static ShellFactory Default { get; private set; }

        /// <summary>
        ///     <see cref="ShellFolder" />を作成します。
        /// </summary>
        /// <param name="shellItem"></param>
        /// <returns></returns>
        public override ShellFolder CreateShellFolder(ShellItem shellItem)
        {
            return new ShellFolder(shellItem);
        }

        /// <summary>
        ///     <see cref="ShellFile" />を作成します。
        /// </summary>
        /// <param name="shellItem"></param>
        /// <returns></returns>
        public override ShellFile CreateShellFile(ShellItem shellItem)
        {
            return new ShellFile(shellItem);
        }
    }
}