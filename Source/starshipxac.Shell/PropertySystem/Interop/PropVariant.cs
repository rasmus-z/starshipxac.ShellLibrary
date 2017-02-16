using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using starshipxac.Shell.Properties;

namespace starshipxac.Shell.PropertySystem.Interop
{
    /// <summary>
    ///     <c>PROPVARIANT</c>を定義します。
    /// </summary>
    /// <remarks>
    ///     http://blogs.msdn.com/adamroot/pages/interop-with-propvariants-in-net.aspx
    /// </remarks>
    [StructLayout(LayoutKind.Explicit)]
    [SuppressMessage("ReSharper", "BitwiseOperatorOnEnumWithoutFlags")]
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
    internal sealed class PropVariant : IDisposable
    {
        #region Fields

        [FieldOffset(0)]
        private decimal decimalValue;

        [FieldOffset(0)]
        private ushort valueType;

        [FieldOffset(8)]
        private IntPtr ptr;

        [FieldOffset(8)]
        private Int32 int32Value;

        [FieldOffset(8)]
        private UInt32 uint32Value;

        [FieldOffset(8)]
        private byte byteValue;

        [FieldOffset(8)]
        private sbyte sbyteValue;

        [FieldOffset(8)]
        private short shortValue;

        [FieldOffset(8)]
        private ushort ushortValue;

        [FieldOffset(8)]
        private long longValue;

        [FieldOffset(8)]
        private ulong ulongValue;

        [FieldOffset(8)]
        private double doubleValue;

        [FieldOffset(8)]
        private float floatValue;

        [FieldOffset(12)]
        private IntPtr ptr2;

        #endregion

        /// <summary>
        ///     <see cref="PropVariant" />クラスの新しいインスタンスを初期化します。
        /// </summary>
        public PropVariant()
        {
        }

        /// <summary>
        ///     文字列を指定して、<see cref="PropVariant" />クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="value">文字列の値。</param>
        public PropVariant(string value)
        {
            Contract.Requires<ArgumentNullException>(value != null);

            this.valueType = (ushort)VarEnum.VT_LPWSTR;
            this.ptr = Marshal.StringToCoTaskMemUni(value);
        }

        public PropVariant(string[] values)
        {
            Contract.Requires<ArgumentNullException>(values != null);

            PropVariantNativeMethods.InitPropVariantFromStringVector(values, (uint)values.Length, this);
        }

        public PropVariant(bool value)
        {
            this.valueType = (ushort)VarEnum.VT_BOOL;
            this.int32Value = (value) ? -1 : 0;
        }

        public PropVariant(bool[] values)
        {
            Contract.Requires<ArgumentNullException>(values != null);

            PropVariantNativeMethods.InitPropVariantFromBooleanVector(values, (uint)values.Length, this);
        }

        public PropVariant(byte value)
        {
            this.valueType = (ushort)VarEnum.VT_UI1;
            this.byteValue = value;
        }

        public PropVariant(sbyte value)
        {
            this.valueType = (ushort)VarEnum.VT_I1;
            this.sbyteValue = value;
        }

        public PropVariant(short value)
        {
            this.valueType = (ushort)VarEnum.VT_I2;
            this.shortValue = value;
        }

        public PropVariant(short[] values)
        {
            Contract.Requires<ArgumentNullException>(values != null);

            PropVariantNativeMethods.InitPropVariantFromInt16Vector(values, (uint)values.Length, this);
        }

        public PropVariant(ushort value)
        {
            this.valueType = (ushort)VarEnum.VT_UI2;
            this.ushortValue = value;
        }

        public PropVariant(ushort[] values)
        {
            Contract.Requires<ArgumentNullException>(values != null);

            PropVariantNativeMethods.InitPropVariantFromUInt16Vector(values, (uint)values.Length, this);
        }

        public PropVariant(int value)
        {
            this.valueType = (ushort)VarEnum.VT_I4;
            this.int32Value = value;
        }

        public PropVariant(int[] values)
        {
            Contract.Requires<ArgumentNullException>(values != null);

            PropVariantNativeMethods.InitPropVariantFromInt32Vector(values, (uint)values.Length, this);
        }

        public PropVariant(uint value)
        {
            this.valueType = (ushort)VarEnum.VT_UI4;
            this.uint32Value = value;
        }

        public PropVariant(uint[] values)
        {
            Contract.Requires<ArgumentNullException>(values != null);

            PropVariantNativeMethods.InitPropVariantFromUInt32Vector(values, (uint)values.Length, this);
        }

        public PropVariant(long value)
        {
            this.valueType = (ushort)VarEnum.VT_I8;
            this.longValue = value;
        }

        public PropVariant(long[] values)
        {
            Contract.Requires<ArgumentNullException>(values != null);

            PropVariantNativeMethods.InitPropVariantFromInt64Vector(values, (uint)values.Length, this);
        }

        public PropVariant(ulong value)
        {
            this.valueType = (ushort)VarEnum.VT_UI8;
            this.ulongValue = value;
        }

        public PropVariant(ulong[] value)
        {
            Contract.Requires<ArgumentNullException>(value != null);

            PropVariantNativeMethods.InitPropVariantFromUInt64Vector(value, (uint)value.Length, this);
        }

        public PropVariant(float value)
        {
            this.valueType = (ushort)VarEnum.VT_R4;
            this.floatValue = value;
        }

        public PropVariant(float[] value)
        {
            Contract.Requires<ArgumentNullException>(value != null);

            this.valueType = (ushort)(VarEnum.VT_R4 | VarEnum.VT_VECTOR);
            this.int32Value = value.Length;

            this.ptr2 = Marshal.AllocCoTaskMem(value.Length*sizeof(float));
            Marshal.Copy(value, 0, this.ptr2, value.Length);
        }

        public PropVariant(double value)
        {
            this.valueType = (ushort)VarEnum.VT_R8;
            this.doubleValue = value;
        }

        public PropVariant(double[] values)
        {
            Contract.Requires<ArgumentNullException>(values != null);

            PropVariantNativeMethods.InitPropVariantFromDoubleVector(values, (uint)values.Length, this);
        }

        public PropVariant(DateTime value)
        {
            this.valueType = (ushort)VarEnum.VT_FILETIME;

            var ft = DateTimeToFileTime(value);
            PropVariantNativeMethods.InitPropVariantFromFileTime(ref ft, this);
        }

        public PropVariant(DateTime[] value)
        {
            Contract.Requires<ArgumentNullException>(value != null);

            var fileTimeArray = new System.Runtime.InteropServices.ComTypes.FILETIME[value.Length];
            for (var index = 0; index < value.Length; ++index)
            {
                fileTimeArray[index] = DateTimeToFileTime(value[index]);
            }
            PropVariantNativeMethods.InitPropVariantFromFileTimeVector(fileTimeArray, (uint)fileTimeArray.Length, this);
        }

        public PropVariant(decimal value)
        {
            // valueTypeと decimalValueは領域が重なっているので、先に decimalValueに値を設定する。
            this.decimalValue = value;
            this.valueType = (ushort)VarEnum.VT_DECIMAL;
        }

        public PropVariant(decimal[] value)
        {
            Contract.Requires<ArgumentNullException>(value != null);

            this.valueType = (ushort)(VarEnum.VT_DECIMAL | VarEnum.VT_VECTOR);
            this.int32Value = value.Length;

            this.ptr2 = Marshal.AllocCoTaskMem(value.Length*sizeof(decimal));
            foreach (var t in value)
            {
                var bits = decimal.GetBits(t);
                Marshal.Copy(bits, 0, this.ptr2, bits.Length);
            }
        }

        /// <summary>
        ///     指定した<paramref name="value" />から<see cref="PropVariant" />を作成します。
        /// </summary>
        /// <param name="value">オブジェクト。</param>
        /// <returns>作成した<see cref="PropVariant" />。</returns>
        public static PropVariant FromObject(object value)
        {
            if (value == null)
            {
                return new PropVariant();
            }

            var func = GetDynamicConstructor(value.GetType());
            return func(value);
        }

        ~PropVariant()
        {
            Dispose();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     現在の値の<see cref="VarEnum" />を取得または設定します。
        /// </summary>
        public VarEnum VarType
        {
            get
            {
                return (VarEnum)this.valueType;
            }
            set
            {
                this.valueType = (ushort)value;
            }
        }

        /// <summary>
        ///     値が<see cref="VarEnum.VT_EMPTY" />または <see cref="VarEnum.VT_NULL" />かどうかを判定する値を取得します。
        /// </summary>
        public bool IsNullOrEmpty
        {
            get
            {
                return (this.valueType == (ushort)VarEnum.VT_EMPTY) || (this.valueType == (ushort)VarEnum.VT_NULL);
            }
        }

        /// <summary>
        ///     値を取得します。
        /// </summary>
        public object Value
        {
            get
            {
                switch ((VarEnum)this.valueType)
                {
                    case VarEnum.VT_I1:
                        return this.sbyteValue;
                    case VarEnum.VT_UI1:
                        return this.byteValue;
                    case VarEnum.VT_I2:
                        return this.shortValue;
                    case VarEnum.VT_UI2:
                        return this.ushortValue;
                    case VarEnum.VT_I4:
                    case VarEnum.VT_INT:
                        return this.int32Value;
                    case VarEnum.VT_UI4:
                    case VarEnum.VT_UINT:
                        return this.uint32Value;
                    case VarEnum.VT_I8:
                        return this.longValue;
                    case VarEnum.VT_UI8:
                        return this.ulongValue;
                    case VarEnum.VT_R4:
                        return this.floatValue;
                    case VarEnum.VT_R8:
                        return this.doubleValue;
                    case VarEnum.VT_BOOL:
                        return this.int32Value == -1;
                    case VarEnum.VT_ERROR:
                        return this.longValue;
                    case VarEnum.VT_CY:
                        return this.decimalValue;
                    case VarEnum.VT_DATE:
                        return DateTime.FromOADate(this.doubleValue);
                    case VarEnum.VT_FILETIME:
                        return DateTime.FromFileTime(this.longValue);
                    case VarEnum.VT_BSTR:
                        return Marshal.PtrToStringBSTR(this.ptr);
                    case VarEnum.VT_BLOB:
                        return GetBlobData();
                    case VarEnum.VT_LPSTR:
                        return Marshal.PtrToStringAnsi(this.ptr);
                    case VarEnum.VT_LPWSTR:
                        return Marshal.PtrToStringUni(this.ptr);
                    case VarEnum.VT_UNKNOWN:
                        return Marshal.GetObjectForIUnknown(this.ptr);
                    case VarEnum.VT_DISPATCH:
                        return Marshal.GetObjectForIUnknown(this.ptr);
                    case VarEnum.VT_DECIMAL:
                        return this.decimalValue;
                    case VarEnum.VT_ARRAY | VarEnum.VT_UNKNOWN:
                        return CrackSingleDimSafeArray(this.ptr);
                    case (VarEnum.VT_VECTOR | VarEnum.VT_LPWSTR):
                        return GetVector<string>();
                    case (VarEnum.VT_VECTOR | VarEnum.VT_I2):
                        return GetVector<Int16>();
                    case (VarEnum.VT_VECTOR | VarEnum.VT_UI2):
                        return GetVector<UInt16>();
                    case (VarEnum.VT_VECTOR | VarEnum.VT_I4):
                        return GetVector<Int32>();
                    case (VarEnum.VT_VECTOR | VarEnum.VT_UI4):
                        return GetVector<UInt32>();
                    case (VarEnum.VT_VECTOR | VarEnum.VT_I8):
                        return GetVector<Int64>();
                    case (VarEnum.VT_VECTOR | VarEnum.VT_UI8):
                        return GetVector<UInt64>();
                    case (VarEnum.VT_VECTOR | VarEnum.VT_R4):
                        return GetVector<float>();
                    case (VarEnum.VT_VECTOR | VarEnum.VT_R8):
                        return GetVector<Double>();
                    case (VarEnum.VT_VECTOR | VarEnum.VT_BOOL):
                        return GetVector<Boolean>();
                    case (VarEnum.VT_VECTOR | VarEnum.VT_FILETIME):
                        return GetVector<DateTime>();
                    case (VarEnum.VT_VECTOR | VarEnum.VT_DECIMAL):
                        return GetVector<Decimal>();
                    default:
                        return null;
                }
            }
        }

        #region IUnknown Methods

        internal void SetIUnknown(object value)
        {
            this.valueType = (ushort)VarEnum.VT_UNKNOWN;
            this.ptr = Marshal.GetIUnknownForObject(value);
        }

        internal void SetSafeArray(Array array)
        {
            Contract.Requires<ArgumentNullException>(array != null);

            const ushort vtUnknown = 13;
            var psa = PropVariantNativeMethods.SafeArrayCreateVector(vtUnknown, 0, (uint)array.Length);
            var pvData = PropVariantNativeMethods.SafeArrayAccessData(psa);

            try
            {
                for (var index = 0; index < array.Length; ++index)
                {
                    var obj = array.GetValue(index);
                    var punk = (obj != null) ? Marshal.GetIUnknownForObject(obj) : IntPtr.Zero;
                    Marshal.WriteIntPtr(pvData, index*IntPtr.Size, punk);
                }
            }
            finally
            {
                PropVariantNativeMethods.SafeArrayUnaccessData(psa);
            }

            this.valueType = (ushort)VarEnum.VT_ARRAY | (ushort)VarEnum.VT_UNKNOWN;
            this.ptr = psa;
        }

        #endregion

        private static long GetFileTimeAsLong(ref System.Runtime.InteropServices.ComTypes.FILETIME value)
        {
            return (((long)value.dwHighDateTime) << 32) + value.dwLowDateTime;
        }

        private static System.Runtime.InteropServices.ComTypes.FILETIME DateTimeToFileTime(DateTime value)
        {
            var hFt = value.ToFileTime();
            return new System.Runtime.InteropServices.ComTypes.FILETIME
            {
                dwLowDateTime = (int)(hFt & 0xFFFFFFFF),
                dwHighDateTime = (int)(hFt >> 32)
            };
        }

        private object GetBlobData()
        {
            var blobData = new byte[this.int32Value];
            var pBlobData = this.ptr2;
            Marshal.Copy(pBlobData, blobData, 0, this.int32Value);

            return blobData;
        }

        public Array GetVector<T>()
        {
            var count = PropVariantNativeMethods.PropVariantGetElementCount(this);
            if (count <= 0)
            {
                return null;
            }

            lock (_padlock)
            {
                if (_vectorActions == null)
                {
                    _vectorActions = GenerateVectorActions();
                }
            }

            Action<PropVariant, Array, uint> action;
            if (!_vectorActions.TryGetValue(typeof(T), out action))
            {
                throw new InvalidCastException();
            }

            Array array = new T[count];
            for (uint index = 0; index < count; ++index)
            {
                action(this, array, index);
            }

            return array;
        }

        private static readonly Dictionary<Type, Func<object, PropVariant>> _cache =
            new Dictionary<Type, Func<object, PropVariant>>();

        private static readonly object _padlock = new object();

        private static Func<object, PropVariant> GetDynamicConstructor(Type type)
        {
            lock (_padlock)
            {
                Func<object, PropVariant> action;
                if (!_cache.TryGetValue(type, out action))
                {
                    var constructor = typeof(PropVariant).GetConstructor(new[] {type});
                    if (constructor == null)
                    {
                        throw new ArgumentException(ErrorMessages.PropVariantTypeNotSupported);
                    }

                    var arg = Expression.Parameter(typeof(object), "arg");
                    var create = Expression.New(constructor, Expression.Convert(arg, type));

                    action = Expression.Lambda<Func<object, PropVariant>>(create, arg).Compile();
                    _cache.Add(type, action);
                }
                return action;
            }
        }

        private static Array CrackSingleDimSafeArray(IntPtr psa)
        {
            var dim = PropVariantNativeMethods.SafeArrayGetDim(psa);
            if (dim != 1)
            {
                throw new ArgumentException(ErrorMessages.PropVariantMultiDimArray, "psa");
            }

            var lBound = PropVariantNativeMethods.SafeArrayGetLBound(psa, 1U);
            var uBound = PropVariantNativeMethods.SafeArrayGetUBound(psa, 1U);

            var array = new object[uBound - lBound + 1];
            for (var index = lBound; index <= uBound; ++index)
            {
                array[index - lBound] = PropVariantNativeMethods.SafeArrayGetElement(psa, ref index);
            }

            return array;
        }

        #region Vector Action Chache

        private static Dictionary<Type, Action<PropVariant, Array, uint>> _vectorActions = null;

        private static Dictionary<Type, Action<PropVariant, Array, uint>> GenerateVectorActions()
        {
            var cache = new Dictionary<Type, Action<PropVariant, Array, uint>>();

            cache.Add(typeof(Int16), (pv, array, i) =>
            {
                short val;
                PropVariantNativeMethods.PropVariantGetInt16Elem(pv, i, out val);
                array.SetValue(val, i);
            });

            cache.Add(typeof(UInt16), (pv, array, i) =>
            {
                ushort val;
                PropVariantNativeMethods.PropVariantGetUInt16Elem(pv, i, out val);
                array.SetValue(val, i);
            });

            cache.Add(typeof(Int32), (pv, array, i) =>
            {
                int val;
                PropVariantNativeMethods.PropVariantGetInt32Elem(pv, i, out val);
                array.SetValue(val, i);
            });

            cache.Add(typeof(UInt32), (pv, array, i) =>
            {
                uint val;
                PropVariantNativeMethods.PropVariantGetUInt32Elem(pv, i, out val);
                array.SetValue(val, i);
            });

            cache.Add(typeof(Int64), (pv, array, i) =>
            {
                long val;
                PropVariantNativeMethods.PropVariantGetInt64Elem(pv, i, out val);
                array.SetValue(val, i);
            });

            cache.Add(typeof(UInt64), (pv, array, i) =>
            {
                ulong val;
                PropVariantNativeMethods.PropVariantGetUInt64Elem(pv, i, out val);
                array.SetValue(val, i);
            });

            cache.Add(typeof(DateTime), (pv, array, i) =>
            {
                System.Runtime.InteropServices.ComTypes.FILETIME val;
                PropVariantNativeMethods.PropVariantGetFileTimeElem(pv, i, out val);

                var fileTime = GetFileTimeAsLong(ref val);

                array.SetValue(DateTime.FromFileTime(fileTime), i);
            });

            cache.Add(typeof(Boolean), (pv, array, i) =>
            {
                bool val;
                PropVariantNativeMethods.PropVariantGetBooleanElem(pv, i, out val);
                array.SetValue(val, i);
            });

            cache.Add(typeof(Double), (pv, array, i) =>
            {
                double val;
                PropVariantNativeMethods.PropVariantGetDoubleElem(pv, i, out val);
                array.SetValue(val, i);
            });

            cache.Add(typeof(Single), (pv, array, i) => // float
            {
                var val = new float[1];
                Marshal.Copy(pv.ptr2, val, (int)i, 1);
                array.SetValue(val[0], (int)i);
            });

            cache.Add(typeof(Decimal), (pv, array, i) =>
            {
                var val = new int[4];
                for (var a = 0; a < val.Length; a++)
                {
                    val[a] = Marshal.ReadInt32(pv.ptr2, (int)i*sizeof(decimal) + a*sizeof(int));
                }
                array.SetValue(new decimal(val), i);
            });

            cache.Add(typeof(String), (pv, array, i) =>
            {
                var val = string.Empty;
                PropVariantNativeMethods.PropVariantGetStringElem(ref pv, i, out val);
                array.SetValue(val, i);
            });

            return cache;
        }

        #endregion

        /// <summary>
        ///     <see cref="PropVariant" />の文字列表現を取得します。
        /// </summary>
        /// <returns><see cref="PropVariant" />の文字列表現。</returns>
        public override string ToString()
        {
            return String.Format(CultureInfo.InvariantCulture, "{0}: {1}", this.Value, this.VarType.ToString());
        }
    }
}