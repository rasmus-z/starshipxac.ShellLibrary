using System;
using System.Collections.Concurrent;
using System.Diagnostics.Contracts;
using starshipxac.Shell.PropertySystem.Interop;

namespace starshipxac.Shell.PropertySystem.Internal
{
    /// <summary>
    ///     <see cref="ShellPropertyKey" />を作成します。
    /// </summary>
    internal static class ShellPropertyKeyFactory
    {
        private static readonly ConcurrentDictionary<string, ShellPropertyKey> NameCache;
        private static readonly ConcurrentDictionary<PROPERTYKEY, ShellPropertyKey> IdCache;

        static ShellPropertyKeyFactory()
        {
            NameCache = new ConcurrentDictionary<string, ShellPropertyKey>();
            IdCache = new ConcurrentDictionary<PROPERTYKEY, ShellPropertyKey>();
        }

        /// <summary>
        ///     標準名から<see cref="ShellPropertyKey" />を取得します。
        /// </summary>
        /// <param name="canonicalName"></param>
        /// <returns></returns>
        internal static ShellPropertyKey Create(string canonicalName)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(canonicalName));

            return NameCache.GetOrAdd(canonicalName,
                ShellPropertyKey.FromCanonicalName);
        }

        /// <summary>
        ///     フォーマットIDおよびプロパティIDから<see cref="ShellPropertyKey" />を取得します。
        /// </summary>
        /// <param name="formatId"></param>
        /// <param name="propId"></param>
        /// <returns></returns>
        internal static ShellPropertyKey Create(string formatId, UInt32 propId)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(formatId));

            return Create(new PROPERTYKEY(new Guid(formatId), propId));
        }

        /// <summary>
        ///     フォーマットIDおよびプロパティIDから<see cref="ShellPropertyKey" />を取得します。
        /// </summary>
        /// <param name="formatId"></param>
        /// <param name="propId"></param>
        /// <returns></returns>
        internal static ShellPropertyKey Create(Guid formatId, UInt32 propId)
        {
            return Create(new PROPERTYKEY(formatId, propId));
        }

        /// <summary>
        ///     <see cref="PROPERTYKEY" />から<see cref="ShellPropertyKey" />を取得します。
        /// </summary>
        /// <param name="propertyKey"></param>
        /// <returns></returns>
        internal static ShellPropertyKey Create(PROPERTYKEY propertyKey)
        {
            return IdCache.GetOrAdd(propertyKey,
                new ShellPropertyKey(propertyKey));
        }
    }
}