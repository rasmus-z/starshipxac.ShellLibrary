using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;

namespace starshipxac.Shell
{
    /// <summary>
    ///     シェルファイルシステムの拡張メソッドを定義します。
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
        ///     <c>ParsingName</c>を分割します。
        /// </summary>
        /// <param name="parsingName"></param>
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
        ///     <c>ParsingName</c>を分割します。
        /// </summary>
        /// <param name="shellObject"></param>
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
        ///     親の<c>ParsingName</c>を取得します。
        /// </summary>
        /// <param name="parsingName"></param>
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
        ///     <c>ParsingName</c>が、ファイルシステム外のルートフォルダーかどうかを判定します。
        /// </summary>
        /// <param name="parsingName"></param>
        /// <returns></returns>
        public static bool IsNonFileSystemKnownFolderRoot(string parsingName)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(parsingName));

            return parsingName.StartsWith(NonFileSystemKnownFolderRootString);
        }

        /// <summary>
        ///     <c>ParsingName</c>のルートを取得します。
        /// </summary>
        /// <param name="parsingName"></param>
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