using System;
using System.Collections.Concurrent;
using System.Diagnostics.Contracts;

namespace starshipxac.Shell.PropertySystem.Internal
{
    /// <summary>
    ///     <see cref="ShellPropertyDescription" />のキャッシュを保持します。
    /// </summary>
    internal class ShellPropertyDescriptionFactory
    {
        private static readonly ConcurrentDictionary<ShellPropertyKey, ShellPropertyDescription> Cache;

        static ShellPropertyDescriptionFactory()
        {
            Cache = new ConcurrentDictionary<ShellPropertyKey, ShellPropertyDescription>();
        }

        /// <summary>
        ///     指定したプロパティキーに一致する<see cref="ShellPropertyDescription" />を取得します。
        /// </summary>
        /// <param name="propertyKey">プロパティキー</param>
        /// <returns></returns>
        public static ShellPropertyDescription Create(ShellPropertyKey propertyKey)
        {
            Contract.Requires<ArgumentNullException>(propertyKey != null);

            return Cache.GetOrAdd(propertyKey,
                propKey => new ShellPropertyDescription(propKey.PropertyKeyNative));
        }
    }
}