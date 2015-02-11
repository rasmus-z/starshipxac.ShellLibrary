using System;
using System.Collections.Concurrent;
using System.Diagnostics.Contracts;
using starshipxac.Shell.PropertySystem.Interop;

namespace starshipxac.Shell.PropertySystem.Internal
{
	/// <summary>
	/// <see cref="ShellPropertyKey"/>を作成します。
	/// </summary>
	internal static class ShellPropertyKeyFactory
	{
		private static readonly ConcurrentDictionary<string, ShellPropertyKey> nameCache;
		private static readonly ConcurrentDictionary<PROPERTYKEY, ShellPropertyKey> idCache;

		static ShellPropertyKeyFactory()
		{
			nameCache = new ConcurrentDictionary<string, ShellPropertyKey>();
			idCache = new ConcurrentDictionary<PROPERTYKEY, ShellPropertyKey>();
		}

		/// <summary>
		/// 標準名から<see cref="ShellPropertyKey"/>を取得します。
		/// </summary>
		/// <param name="canonicalName"></param>
		/// <returns></returns>
		internal static ShellPropertyKey Get(string canonicalName)
		{
			Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(canonicalName));

			return nameCache.GetOrAdd(canonicalName,
				ShellPropertyKey.FromCanonicalName);
		}

		/// <summary>
		/// フォーマットIDおよびプロパティIDから<see cref="ShellPropertyKey"/>を取得します。
		/// </summary>
		/// <param name="formatId"></param>
		/// <param name="propId"></param>
		/// <returns></returns>
		internal static ShellPropertyKey Get(string formatId, UInt32 propId)
		{
			Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(formatId));

			return Get(new PROPERTYKEY(new Guid(formatId), propId));
		}

		/// <summary>
		/// フォーマットIDおよびプロパティIDから<see cref="ShellPropertyKey"/>を取得します。
		/// </summary>
		/// <param name="formatId"></param>
		/// <param name="propId"></param>
		/// <returns></returns>
		internal static ShellPropertyKey Get(Guid formatId, UInt32 propId)
		{
			return Get(new PROPERTYKEY(formatId, propId));
		}

		/// <summary>
		/// <see cref="PROPERTYKEY"/>から<see cref="ShellPropertyKey"/>を取得します。
		/// </summary>
		/// <param name="propertyKey"></param>
		/// <returns></returns>
		internal static ShellPropertyKey Get(PROPERTYKEY propertyKey)
		{
			return idCache.GetOrAdd(propertyKey,
				new ShellPropertyKey(propertyKey));
		}
	}
}