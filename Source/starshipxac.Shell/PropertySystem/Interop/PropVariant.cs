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
    internal sealed class PropVariant : IDisposable
    {
        #region Fields

        // ReSharper disable FieldCanBeMadeReadOnly.Local

        [FieldOffset(0)]
        private decimal _decimal;

        [FieldOffset(0)]
        private ushort _valueType;

        [FieldOffset(8)]
        private IntPtr _ptr;

        [FieldOffset(8)]
        private Int32 _int32;

        [FieldOffset(8)]
        private UInt32 _uint32;

        [FieldOffset(8)]
        private byte _byte;

        [FieldOffset(8)]
        private sbyte _sbyte;

        [FieldOffset(8)]
        private short _short;

        [FieldOffset(8)]
        private ushort _ushort;

        [FieldOffset(8)]
        private long _long;

        [FieldOffset(8)]
        private ulong _ulong;

        [FieldOffset(8)]
        private double _double;

        [FieldOffset(8)]
        private float _float;

        [FieldOffset(12)]
        private IntPtr _ptr2;

        // ReSharper restore FieldCanBeMadeReadOnly.Local

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

            this._valueType = (ushort)VarEnum.VT_LPWSTR;
            this._ptr = Marshal.StringToCoTaskMemUni(value);
        }

        public PropVariant(string[] values)
        {
            Contract.Requires<ArgumentNullException>(values != null);

            PropVariantNativeMethods.InitPropVariantFromStringVector(values, (uint)values.Length, this);
        }

        public PropVariant(bool value)
        {
            this._valueType = (ushort)VarEnum.VT_BOOL;
            this._int32 = (value) ? -1 : 0;
        }

        public PropVariant(bool[] values)
        {
            Contract.Requires<ArgumentNullException>(values != null);

            PropVariantNativeMethods.InitPropVariantFromBooleanVector(values, (uint)values.Length, this);
        }

        public PropVariant(byte value)
        {
            this._valueType = (ushort)VarEnum.VT_UI1;
            this._byte = value;
        }

        public PropVariant(sbyte value)
        {
            this._valueType = (ushort)VarEnum.VT_I1;
            this._sbyte = value;
        }

        public PropVariant(short value)
        {
            this._valueType = (ushort)VarEnum.VT_I2;
            this._short = value;
        }

        public PropVariant(short[] values)
        {
            Contract.Requires<ArgumentNullException>(values != null);

            PropVariantNativeMethods.InitPropVariantFromInt16Vector(values, (uint)values.Length, this);
        }

        public PropVariant(ushort value)
        {
            this._valueType = (ushort)VarEnum.VT_UI2;
            this._ushort = value;
        }

        public PropVariant(ushort[] values)
        {
            Contract.Requires<ArgumentNullException>(values != null);

            PropVariantNativeMethods.InitPropVariantFromUInt16Vector(values, (uint)values.Length, this);
        }

        public PropVariant(int value)
        {
            this._valueType = (ushort)VarEnum.VT_I4;
            this._int32 = value;
        }

        public PropVariant(int[] values)
        {
            Contract.Requires<ArgumentNullException>(values != null);

            PropVariantNativeMethods.InitPropVariantFromInt32Vector(values, (uint)values.Length, this);
        }

        public PropVariant(uint value)
        {
            this._valueType = (ushort)VarEnum.VT_UI4;
            this._uint32 = value;
        }

        public PropVariant(uint[] values)
        {
            Contract.Requires<ArgumentNullException>(values != null);

            PropVariantNativeMethods.InitPropVariantFromUInt32Vector(values, (uint)values.Length, this);
        }

        public PropVariant(long value)
        {
            this._valueType = (ushort)VarEnum.VT_I8;
            this._long = value;
        }

        public PropVariant(long[] values)
        {
            Contract.Requires<ArgumentNullException>(values != null);

            PropVariantNativeMethods.InitPropVariantFromInt64Vector(values, (uint)values.Length, this);
        }

        public PropVariant(ulong value)
        {
            this._valueType = (ushort)VarEnum.VT_UI8;
            this._ulong = value;
        }

        public PropVariant(ulong[] value)
        {
            Contract.Requires<ArgumentNullException>(value != null);

            PropVariantNativeMethods.InitPropVariantFromUInt64Vector(value, (uint)value.Length, this);
        }

        public PropVariant(float value)
        {
            this._valueType = (ushort)VarEnum.VT_R4;
            this._float = value;
        }

        public PropVariant(float[] value)
        {
            Contract.Requires<ArgumentNullException>(value != null);

            this._valueType = (ushort)(VarEnum.VT_R4 | VarEnum.VT_VECTOR);
            this._int32 = value.Length;

            this._ptr2 = Marshal.AllocCoTaskMem(value.Length*sizeof(float));
            Marshal.Copy(value, 0, this._ptr2, value.Length);
        }

        public PropVariant(double value)
        {
            this._valueType = (ushort)VarEnum.VT_R8;
            this._double = value;
        }

        public PropVariant(double[] values)
        {
            Contract.Requires<ArgumentNullException>(values != null);

            PropVariantNativeMethods.InitPropVariantFromDoubleVector(values, (uint)values.Length, this);
        }

        public PropVariant(DateTime value)
        {
            this._valueType = (ushort)VarEnum.VT_FILETIME;

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
            // _valueTypeと _decimalは領域が重なっているので、先に _decimalに値を設定する。
            this._decimal = value;
            this._valueType = (ushort)VarEnum.VT_DECIMAL;
        }

        public PropVariant(decimal[] value)
        {
            Contract.Requires<ArgumentNullException>(value != null);

            this._valueType = (ushort)(VarEnum.VT_DECIMAL | VarEnum.VT_VECTOR);
            this._int32 = value.Length;

            this._ptr2 = Marshal.AllocCoTaskMem(value.Length*sizeof(decimal));
            foreach (var t in value)
            {
                var bits = decimal.GetBits(t);
                Marshal.Copy(bits, 0, this._ptr2, bits.Length);
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
                return (VarEnum)this._valueType;
            }
            set
            {
                this._valueType = (ushort)value;
            }
        }

        /// <summary>
        ///     値が<see cref="VarEnum.VT_EMPTY" />または <see cref="VarEnum.VT_NULL" />かどうかを判定する値を取得します。
        /// </summary>
        public bool IsNullOrEmpty
        {
            get
            {
                return (this._valueType == (ushort)VarEnum.VT_EMPTY) || (this._valueType == (ushort)VarEnum.VT_NULL);
            }
        }

        /// <summary>
        ///     値を取得します。
        /// </summary>
        public object Value
        {
            get
            {
                switch ((VarEnum)this._valueType)
                {
                    case VarEnum.VT_I1:
                        return this._sbyte;
                    case VarEnum.VT_UI1:
                        return this._byte;
                    case VarEnum.VT_I2:
                        return this._short;
                    case VarEnum.VT_UI2:
                        return this._ushort;
                    case VarEnum.VT_I4:
                    case VarEnum.VT_INT:
                        return this._int32;
                    case VarEnum.VT_UI4:
                    case VarEnum.VT_UINT:
                        return this._uint32;
                    case VarEnum.VT_I8:
                        return this._long;
                    case VarEnum.VT_UI8:
                        return this._ulong;
                    case VarEnum.VT_R4:
                        return this._float;
                    case VarEnum.VT_R8:
                        return this._double;
                    case VarEnum.VT_BOOL:
                        return this._int32 == -1;
                    case VarEnum.VT_ERROR:
                        return this._long;
                    case VarEnum.VT_CY:
                        return this._decimal;
                    case VarEnum.VT_DATE:
                        return DateTime.FromOADate(this._double);
                    case VarEnum.VT_FILETIME:
                        return DateTime.FromFileTime(this._long);
                    case VarEnum.VT_BSTR:
                        return Marshal.PtrToStringBSTR(this._ptr);
                    case VarEnum.VT_BLOB:
                        return GetBlobData();
                    case VarEnum.VT_LPSTR:
                        return Marshal.PtrToStringAnsi(this._ptr);
                    case VarEnum.VT_LPWSTR:
                        return Marshal.PtrToStringUni(this._ptr);
                    case VarEnum.VT_UNKNOWN:
                        return Marshal.GetObjectForIUnknown(this._ptr);
                    case VarEnum.VT_DISPATCH:
                        return Marshal.GetObjectForIUnknown(this._ptr);
                    case VarEnum.VT_DECIMAL:
                        return this._decimal;
                    case VarEnum.VT_ARRAY | VarEnum.VT_UNKNOWN:
                        return CrackSingleDimSafeArray(this._ptr);
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
            this._valueType = (ushort)VarEnum.VT_UNKNOWN;
            this._ptr = Marshal.GetIUnknownForObject(value);
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

            this._valueType = (ushort)VarEnum.VT_ARRAY | (ushort)VarEnum.VT_UNKNOWN;
            this._ptr = psa;
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
            var blobData = new byte[this._int32];
            var pBlobData = this._ptr2;
            Marshal.Copy(pBlobData, blobData, 0, this._int32);

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
                Marshal.Copy(pv._ptr2, val, (int)i, 1);
                array.SetValue(val[0], (int)i);
            });

            cache.Add(typeof(Decimal), (pv, array, i) =>
            {
                var val = new int[4];
                for (var a = 0; a < val.Length; a++)
                {
                    val[a] = Marshal.ReadInt32(pv._ptr2, (int)i*sizeof(decimal) + a*sizeof(int));
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