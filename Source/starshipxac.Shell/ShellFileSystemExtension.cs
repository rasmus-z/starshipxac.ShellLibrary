using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;

namespace starshipxac.Shell
{
    /// <summary>
    ///     Define the extension method of the shell file system.
    /// </summary>
    public static class ShellFileSystemExtension
    {
        private static readonly char[] Separators =
        {
            Path.DirectorySeparatorChar,
            Path.AltDirectorySeparatorChar
        };

        private const string NonFileSystemKnownFolderRootString = "::";

        /// <summary>
        ///     Split <c>ParsingName</c>.
        /// </summary>
        /// <param name="parsingName">Parsing name.</param>
        /// <returns></returns>
        public static string[] SplitParsingName(this string parsingName)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(parsingName));
            Contract.Ensures(Contract.Result<string[]>() != null);
            Contract.Ensures(Contract.Result<string[]>().Length > 0);

            var result = new List<string>();

            var root = GetParsingNameRoot(parsingName);
            if (!String.IsNullOrEmpty(root))
            {
                result.Add(root);
                result.AddRange(parsingName.Substring(root.Length).Split(Separators, StringSplitOptions.RemoveEmptyEntries));
            }
            else
            {
                result.AddRange(parsingName.Split(Separators, StringSplitOptions.RemoveEmptyEntries));
            }

            return result.ToArray();
        }

        /// <summary>
        ///     Split <c>ParsingName</c>.
        /// </summary>
        /// <param name="shellObject"><see cref="ShellObject"/>.</param>
        /// <returns></returns>
        public static string[] SplitParsingName(this ShellObject shellObject)
        {
            Contract.Requires<ArgumentNullException>(shellObject != null);
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(shellObject.ParsingName));
            Contract.Ensures(Contract.Result<string[]>() != null);
            Contract.Ensures(Contract.Result<string[]>().Length > 0);

            return SplitParsingName(shellObject.ParsingName);
        }

        /// <summary>
        ///     Get a parent <c>ParsingName</c>.
        /// </summary>
        /// <param name="parsingName">Parsing name.</param>
        /// <returns></returns>
        public static string GetParentParsingName(string parsingName)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(parsingName));

            var parts = SplitParsingName(parsingName);
            if (parts.Length <= 1)
            {
                return null;
            }
            return Path.Combine(parts.Take(parts.Length - 1).ToArray());
        }

        /// <summary>
        ///     Determine if <c>ParsingName</c> is a root folder outside the file system.
        /// </summary>
        /// <param name="parsingName">Parsing name.</param>
        /// <returns></returns>
        public static bool IsNonFileSystemKnownFolderRoot(string parsingName)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(parsingName));

            return parsingName.StartsWith(NonFileSystemKnownFolderRootString);
        }

        /// <summary>
        ///     Get the root of <c>ParsingName</c>.
        /// </summary>
        /// <param name="parsingName">Parsing name.</param>
        /// <returns></returns>
        public static string GetParsingNameRoot(string parsingName)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(parsingName));

            if (IsNonFileSystemKnownFolderRoot(parsingName))
            {
                var index = parsingName.IndexOfAny(Separators);
                if (index < 0)
                {
                    return parsingName;
                }
                return parsingName.Substring(0, index);
            }
            return Path.GetPathRoot(parsingName);
        }
    }
}