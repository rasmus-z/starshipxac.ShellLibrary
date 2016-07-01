using System;
using System.Collections.Concurrent;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using starshipxac.Shell.Interop;
using starshipxac.Shell.Properties;

namespace starshipxac.Shell.PropertySystem.Internal
{
    /// <summary>
    ///     <see cref="ShellProperty{T}"/>を作成します。
    /// </summary>
    internal static class ShellPropertyFactory
    {
        /// <summary>
        ///     <c>ShellProperty</c>コンストラクタのキャッシュ。
        /// </summary>
        private static readonly ConcurrentDictionary<int, Func<ShellPropertyKey, ShellPropertyDescription, object, IShellProperty>> ConstructorCache;

        static ShellPropertyFactory()
        {
            ConstructorCache = new ConcurrentDictionary<int, Func<ShellPropertyKey, ShellPropertyDescription, object, IShellProperty>>();
        }

        /// <summary>
        ///     <see cref="ShellPropertyKey" />と<see cref="ShellObject" />を指定して、
        ///     <see cref="ShellProperty{T}" />のインスタンスを作成します。
        /// </summary>
        /// <param name="propertyKey"></param>
        /// <param name="shellObject"></param>
        /// <returns></returns>
        public static IShellProperty Create(ShellPropertyKey propertyKey, ShellObject shellObject)
        {
            Contract.Requires<ArgumentNullException>(propertyKey != null);
            Contract.Requires<ArgumentNullException>(shellObject != null);
            Contract.Ensures(Contract.Result<IShellProperty>() != null);

            var description = ShellPropertyDescriptionFactory.Create(propertyKey);
            var sourceType = typeof(ShellObject);
            var type = typeof(ShellProperty<>).MakeGenericType(VarEnumToSystemType(description.VarEnumType));

            var ctor = ConstructorCache.GetOrAdd(
                GetTypeHash(type, sourceType),
                hashValue =>
                {
                    Type[] argTypes = { typeof(ShellPropertyKey), typeof(ShellPropertyDescription), sourceType };
                    return CreateConstructorExpression(type, argTypes);
                });

            return ctor(propertyKey, description, shellObject);
        }

        /// <summary>
        ///     <see cref="ShellPropertyKey" />と<see cref="ShellPropertyStore" />を指定して、
        ///     <see cref="ShellProperty{T}" />のインスタンスを作成します。
        /// </summary>
        /// <param name="propertyKey"></param>
        /// <param name="propertyStore"></param>
        /// <returns></returns>
        public static IShellProperty Create(ShellPropertyKey propertyKey, ShellPropertyStore propertyStore)
        {
            Contract.Requires<ArgumentNullException>(propertyKey != null);
            Contract.Requires<ArgumentNullException>(propertyStore != null);
            Contract.Ensures(Contract.Result<IShellProperty>() != null);

            var description = ShellPropertyDescriptionFactory.Create(propertyKey);
            var sourceType = typeof(ShellPropertyStore);
            var type = typeof(ShellProperty<>).MakeGenericType(VarEnumToSystemType(description.VarEnumType));

            var ctor = ConstructorCache.GetOrAdd(
                GetTypeHash(type, sourceType),
                hashValue =>
                {
                    Type[] argTypes = { typeof(ShellPropertyKey), typeof(ShellPropertyDescription), sourceType };
                    return CreateConstructorExpression(type, argTypes);
                });

            return ctor(propertyKey, description, propertyStore);
        }

        /// <summary>
        ///     <see cref="VARENUM" />を対応する .NETの型に変換します。
        /// </summary>
        /// <param name="varEnumType"><see cref="VARENUM" />。</param>
        /// <returns>.NETの型を表す<see cref="Type" />。</returns>
        [Pure]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        internal static Type VarEnumToSystemType(VARENUM varEnumType)
        {
            switch (varEnumType)
            {
                case (VARENUM.VT_EMPTY):
                case (VARENUM.VT_NULL):
                    return typeof(Object);
                case (VARENUM.VT_UI1):
                    return typeof(Byte?);
                case (VARENUM.VT_I2):
                    return typeof(Int16?);
                case (VARENUM.VT_UI2):
                    return typeof(UInt16?);
                case (VARENUM.VT_I4):
                    return typeof(Int32?);
                case (VARENUM.VT_UI4):
                    return typeof(UInt32?);
                case (VARENUM.VT_I8):
                    return typeof(Int64?);
                case (VARENUM.VT_UI8):
                    return typeof(UInt64?);
                case (VARENUM.VT_R8):
                    return typeof(Double?);
                case (VARENUM.VT_BOOL):
                    return typeof(Boolean?);
                case (VARENUM.VT_FILETIME):
                    return typeof(DateTime?);
                case (VARENUM.VT_CLSID):
                    return typeof(IntPtr?);
                case (VARENUM.VT_CF):
                    return typeof(IntPtr?);
                case (VARENUM.VT_BLOB):
                    return typeof(Byte[]);
                case (VARENUM.VT_LPWSTR):
                    return typeof(String);
                case (VARENUM.VT_UNKNOWN):
                    return typeof(IntPtr?);
                case (VARENUM.VT_STREAM):
                    return typeof(IStream);
                case (VARENUM.VT_VECTOR | VARENUM.VT_UI1):
                    return typeof(Byte[]);
                case (VARENUM.VT_VECTOR | VARENUM.VT_I2):
                    return typeof(Int16[]);
                case (VARENUM.VT_VECTOR | VARENUM.VT_UI2):
                    return typeof(UInt16[]);
                case (VARENUM.VT_VECTOR | VARENUM.VT_I4):
                    return typeof(Int32[]);
                case (VARENUM.VT_VECTOR | VARENUM.VT_UI4):
                    return typeof(UInt32[]);
                case (VARENUM.VT_VECTOR | VARENUM.VT_I8):
                    return typeof(Int64[]);
                case (VARENUM.VT_VECTOR | VARENUM.VT_UI8):
                    return typeof(UInt64[]);
                case (VARENUM.VT_VECTOR | VARENUM.VT_R8):
                    return typeof(Double[]);
                case (VARENUM.VT_VECTOR | VARENUM.VT_BOOL):
                    return typeof(Boolean[]);
                case (VARENUM.VT_VECTOR | VARENUM.VT_FILETIME):
                    return typeof(DateTime[]);
                case (VARENUM.VT_VECTOR | VARENUM.VT_CLSID):
                    return typeof(IntPtr[]);
                case (VARENUM.VT_VECTOR | VARENUM.VT_CF):
                    return typeof(IntPtr[]);
                case (VARENUM.VT_VECTOR | VARENUM.VT_LPWSTR):
                    return typeof(String[]);
                default:
                    return typeof(Object);
            }
        }

        private static Func<ShellPropertyKey, ShellPropertyDescription, object, IShellProperty> CreateConstructorExpression(
            Type type, Type[] argTypes)
        {
            Contract.Requires(type != null);
            Contract.Requires(argTypes != null);
            Contract.Requires(argTypes.Length >= 3);

            var ctorInfo = type.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                .FirstOrDefault(x => x.GetParameters().Select(p => p.ParameterType).SequenceEqual(argTypes));
            if (ctorInfo == null)
            {
                throw new ArgumentException(
                    String.Format(ErrorMessages.ShellPropertyFactoryConstructorNotFound, argTypes.Cast<object>()),
                    nameof(type));
            }

            var propertyKey = Expression.Parameter(argTypes[0], "propertyKey");
            var description = Expression.Parameter(argTypes[1], "description");
            var source = Expression.Parameter(typeof(object), "source");

            var create = Expression.New(ctorInfo, propertyKey, description, Expression.Convert(source, argTypes[2]));

            return Expression
                .Lambda<Func<ShellPropertyKey, ShellPropertyDescription, object, IShellProperty>>(create, propertyKey, description, source)
                .Compile();
        }

        private static int GetTypeHash(Type propertyType, Type sourceType)
        {
            return propertyType.GetHashCode() ^ sourceType.GetHashCode();
        }
    }
}