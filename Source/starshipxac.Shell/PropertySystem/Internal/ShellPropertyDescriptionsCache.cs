using System;
using System.Collections.Concurrent;
using System.Diagnostics.Contracts;

namespace starshipxac.Shell.PropertySystem.Internal
{
    /// <summary>
    ///     <see cref="ShellPropertyDescription" />のキャッシュを保持します。
    /// </summary>
    internal class ShellPropertyDescriptionsCache
    {
        private static readonly ConcurrentDictionary<ShellPropertyKey, ShellPropertyDescription> Cache;

        static ShellPropertyDescriptionsCache()
        {
            Cache = new ConcurrentDictionary<ShellPropertyKey, ShellPropertyDescription>();
        }

        /// <summary>
        ///     指定したプロパティキーに一致する<see cref="ShellPropertyDescription" />を取得します。
        /// </summary>
        /// <param name="propertyKey"></param>
        /// <returns></returns>
        public static ShellPropertyDescription GetDescription(ShellPropertyKey propertyKey)
        {
            Contract.Requires<ArgumentNullException>(propertyKey != null);

            return Cache.GetOrAdd(propertyKey,
                propKey => new ShellPropertyDescription(propKey.PropertyKeyNative));
        }
    }
}