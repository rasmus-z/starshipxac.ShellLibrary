using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using starshipxac.Shell.Properties;

namespace starshipxac.Shell.PropertySystem.Internal
{
    /// <summary>
    ///     <see cref="ShellProperty&lt;T&gt;" />を作成します。
    /// </summary>
    internal static class ShellPropertyFactory
    {
        /// <summary>
        ///     <c>ShellProperty</c>コンストラクタのキャッシュ。
        /// </summary>
        private static readonly ConcurrentDictionary<int, Func<ShellPropertyKey, ShellPropertyDescription, object, IShellProperty>>
            ConstructorCache;

        static ShellPropertyFactory()
        {
            ConstructorCache =
                new ConcurrentDictionary<int, Func<ShellPropertyKey, ShellPropertyDescription, object, IShellProperty>>();
        }

        /// <summary>
        ///     <see cref="ShellPropertyKey" />と<see cref="ShellObject" />を指定して、
        ///     <see cref="ShellProperty{T}" />のインスタンスを作成します。
        /// </summary>
        /// <param name="propertyKey"></param>
        /// <param name="shellObject"></param>
        /// <returns></returns>
        internal static IShellProperty CreateShellProperty(ShellPropertyKey propertyKey, ShellObject shellObject)
        {
            Contract.Requires<ArgumentNullException>(propertyKey != null);
            Contract.Requires<ArgumentNullException>(shellObject != null);

            return CreateGenericShellProperty(propertyKey, shellObject);
        }

        /// <summary>
        ///     <see cref="ShellPropertyKey" />と<see cref="ShellPropertyStore" />を指定して、
        ///     <see cref="ShellProperty{T}" />のインスタンスを作成します。
        /// </summary>
        /// <param name="propertyKey"></param>
        /// <param name="propertyStore"></param>
        /// <returns></returns>
        internal static IShellProperty CreateShellProperty(ShellPropertyKey propertyKey, ShellPropertyStore propertyStore)
        {
            Contract.Requires<ArgumentNullException>(propertyKey != null);
            Contract.Requires<ArgumentNullException>(propertyStore != null);

            return CreateGenericShellProperty(propertyKey, propertyStore);
        }

        /// <summary>
        ///     <see cref="ShellPropertyKey" />と元となるオブジェクトを指定して、
        ///     <see cref="ShellProperty{T}" />のインスタンスを作成します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyKey"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        private static IShellProperty CreateGenericShellProperty<T>(ShellPropertyKey propertyKey, T source)
        {
            Contract.Requires<ArgumentNullException>(propertyKey != null);

            var sourceType = (source is ShellObject) ? typeof(ShellObject) : typeof(T);
            var description = ShellPropertyDescriptionsCache.GetDescription(propertyKey);

            var type = typeof(ShellProperty<>).MakeGenericType(VarEnumToSystemType(description.VarEnumType));
            var hash = GetTypeHash(type, sourceType);

            var ctor = ConstructorCache.GetOrAdd(hash,
                hashValue =>
                {
                    Type[] argTypes = {typeof(ShellPropertyKey), typeof(ShellPropertyDescription), sourceType};
                    return CreateConstructorExpress(type, argTypes);
                });

            return ctor(propertyKey, description, source);
        }

        /// <summary>
        ///     <see cref="VarEnum" />を対応する .NETの型に変換します。
        /// </summary>
        /// <param name="varEnumType"><see cref="VarEnum" />。</param>
        /// <returns>.NETの型を表す<see cref="Type" />。</returns>
        [Pure]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        internal static Type VarEnumToSystemType(VarEnum varEnumType)
        {
            switch (varEnumType)
            {
                case (VarEnum.VT_EMPTY):
                case (VarEnum.VT_NULL):
                    return typeof(Object);
                case (VarEnum.VT_UI1):
                    return typeof(Byte?);
                case (VarEnum.VT_I2):
                    return typeof(Int16?);
                case (VarEnum.VT_UI2):
                    return typeof(UInt16?);
                case (VarEnum.VT_I4):
                    return typeof(Int32?);
                case (VarEnum.VT_UI4):
                    return typeof(UInt32?);
                case (VarEnum.VT_I8):
                    return typeof(Int64?);
                case (VarEnum.VT_UI8):
                    return typeof(UInt64?);
                case (VarEnum.VT_R8):
                    return typeof(Double?);
                case (VarEnum.VT_BOOL):
                    return typeof(Boolean?);
                case (VarEnum.VT_FILETIME):
                    return typeof(DateTime?);
                case (VarEnum.VT_CLSID):
                    return typeof(IntPtr?);
                case (VarEnum.VT_CF):
                    return typeof(IntPtr?);
                case (VarEnum.VT_BLOB):
                    return typeof(Byte[]);
                case (VarEnum.VT_LPWSTR):
                    return typeof(String);
                case (VarEnum.VT_UNKNOWN):
                    return typeof(IntPtr?);
                case (VarEnum.VT_STREAM):
                    return typeof(IStream);
                case (VarEnum.VT_VECTOR | VarEnum.VT_UI1):
                    return typeof(Byte[]);
                case (VarEnum.VT_VECTOR | VarEnum.VT_I2):
                    return typeof(Int16[]);
                case (VarEnum.VT_VECTOR | VarEnum.VT_UI2):
                    return typeof(UInt16[]);
                case (VarEnum.VT_VECTOR | VarEnum.VT_I4):
                    return typeof(Int32[]);
                case (VarEnum.VT_VECTOR | VarEnum.VT_UI4):
                    return typeof(UInt32[]);
                case (VarEnum.VT_VECTOR | VarEnum.VT_I8):
                    return typeof(Int64[]);
                case (VarEnum.VT_VECTOR | VarEnum.VT_UI8):
                    return typeof(UInt64[]);
                case (VarEnum.VT_VECTOR | VarEnum.VT_R8):
                    return typeof(Double[]);
                case (VarEnum.VT_VECTOR | VarEnum.VT_BOOL):
                    return typeof(Boolean[]);
                case (VarEnum.VT_VECTOR | VarEnum.VT_FILETIME):
                    return typeof(DateTime[]);
                case (VarEnum.VT_VECTOR | VarEnum.VT_CLSID):
                    return typeof(IntPtr[]);
                case (VarEnum.VT_VECTOR | VarEnum.VT_CF):
                    return typeof(IntPtr[]);
                case (VarEnum.VT_VECTOR | VarEnum.VT_LPWSTR):
                    return typeof(String[]);
                default:
                    return typeof(Object);
            }
        }

        private static Func<ShellPropertyKey, ShellPropertyDescription, object, IShellProperty> CreateConstructorExpress(Type type,
            Type[] argTypes)
        {
            Contract.Requires(type != null);
            Contract.Requires(argTypes != null);
            Contract.Requires(argTypes.Length >= 3);

            var ctorInfo = type.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                .FirstOrDefault(x => x.GetParameters().Select(p => p.ParameterType).SequenceEqual(argTypes));
            if (ctorInfo == null)
            {
                throw new ArgumentException(
                    String.Format(ErrorMessages.ShellPropertyFactoryConstructorNotFound, argTypes),
                    nameof(type));
            }

            var propertyKey = Expression.Parameter(argTypes[0], "propertyKey");
            var description = Expression.Parameter(argTypes[1], "description");
            var source = Expression.Parameter(typeof(object), "source");

            var create = Expression.New(ctorInfo, propertyKey, description, Expression.Convert(source, argTypes[2]));

            return Expression.Lambda<Func<ShellPropertyKey, ShellPropertyDescription, object, IShellProperty>>(
                create, propertyKey, description, source).Compile();
        }

        private static int GetTypeHash(params Type[] types)
        {
            return GetTypeHash((IEnumerable<Type>)types);
        }

        private static int GetTypeHash(IEnumerable<Type> types)
        {
            return types.Aggregate(0, (current, type) => current*31 + type.GetHashCode());
        }
    }
}