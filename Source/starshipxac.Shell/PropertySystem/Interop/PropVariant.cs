using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Runtime.InteropServices;
using starshipxac.Shell.Interop;
using static starshipxac.Shell.PropertySystem.Interop.PropVariantNativeMethods;
using FILETIME = System.Runtime.InteropServices.ComTypes.FILETIME;

namespace starshipxac.Shell.PropertySystem.Interop
{
    /// <summary>
    ///     Implement the native <c>PROPVARIANT</c> structure.
    /// </summary>
    /// <remarks>
    ///     http://blogs.msdn.com/adamroot/pages/interop-with-propvariants-in-net.aspx
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    [SuppressMessage("ReSharper", "UseObjectOrCollectionInitializer")]
    internal struct PropVariant
    {
        #region Fields

        [FieldOffset(0)] internal ushort varType;
        [FieldOffset(2)] internal ushort wReserved1;
        [FieldOffset(4)] internal ushort wReserved2;
        [FieldOffset(6)] internal ushort wReserved3;

        /// <summary>
        ///     <c>CHAR cVal</c>
        /// </summary>
        [FieldOffset(8)] internal sbyte cVal;

        /// <summary>
        ///     <c>UCHAR bVal</c>
        /// </summary>
        [FieldOffset(8)] internal byte bVal;

        /// <summary>
        ///     <c>SHORT iVal</c>
        /// </summary>
        [FieldOffset(8)] internal Int16 iVal;

        /// <summary>
        ///     USHORT uiVal
        /// </summary>
        [FieldOffset(8)] internal UInt16 uiVal;

        [FieldOffset(8)] internal Int32 intVal;

        [FieldOffset(8)] internal UInt32 uintVal;

        /// <summary>
        ///     <c>LONG lVal</c>
        /// </summary>
        [FieldOffset(8)] internal Int64 lVal;

        /// <summary>
        ///     <c>ULONG ulVal</c>
        /// </summary>
        [FieldOffset(8)] internal UInt64 ulVal;

        /// <summary>
        ///     <c>FLOAT fltVal</c>
        /// </summary>
        [FieldOffset(8)] internal float fltVal;

        /// <summary>
        ///     <c>DOUBLE dblVal</c>
        /// </summary>
        [FieldOffset(8)] internal double dblVal;

        [FieldOffset(8)] internal short boolVal;

        [FieldOffset(8)] internal IntPtr pclsidVal;

        [FieldOffset(8)] internal IntPtr pszVal;

        [FieldOffset(8)] internal IntPtr pwszVal;

        [FieldOffset(8)] internal IntPtr punkVal;

        [FieldOffset(8)] internal PROPARRAY ca;

        [FieldOffset(8)] internal FILETIME filetime;

        #endregion

        /// <summary>
        ///     文字列を指定して、<see cref="PropVariant" />クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="value">文字列の値。</param>
        public static PropVariant FromString(string value)
        {
            Contract.Requires<ArgumentNullException>(value != null);

            PropVariant result;
            InitPropVariantFromString(value, out result);

            return result;
        }

        public static PropVariant FromStringArray(string[] values)
        {
            Contract.Requires<ArgumentNullException>(values != null);

            PropVariant result;
            var hr = InitPropVariantFromStringVector(values, (uint)values.Length, out result);
            if (HRESULT.Failed(hr))
            {
                throw ShellException.FromHRESULT(hr);
            }

            return result;
        }

        /// <summary>
        ///     Create a new instance of the <see cref="PropVariant" /> structure.
        /// </summary>
        /// <param name="value"></param>
        /// <returns><see cref="PropVariant" />(<c>VT_VECTOR</c> | <c>VT_LPWSTR</c>)</returns>
        public static PropVariant FromStringAs(string value)
        {
            Contract.Requires<ArgumentNullException>(value != null);

            PropVariant result;
            var hr = InitPropVariantFromStringAsVector(value, out result);
            if (HRESULT.Failed(hr))
            {
                throw ShellException.FromHRESULT(hr);
            }

            return result;
        }

        public static PropVariant FromInt8(sbyte value)
        {
            var result = default(PropVariant);
            result.cVal = value;
            result.varType = (ushort)VarEnum.VT_I1;

            return result;
        }

        public static PropVariant FromUInt8(byte value)
        {
            var result = default(PropVariant);
            result.bVal = value;
            result.varType = (ushort)VarEnum.VT_UI1;

            return result;
        }

        public static PropVariant FromInt16(Int16 value)
        {
            PropVariant result;
            InitPropVariantFromInt16(value, out result);

            return result;
        }

        public static PropVariant FromInt16Array(Int16[] values)
        {
            Contract.Requires<ArgumentNullException>(values != null);

            PropVariant result;
            var hr = InitPropVariantFromInt16Vector(values, (uint)values.Length, out result);
            if (HRESULT.Failed(hr))
            {
                throw ShellException.FromHRESULT(hr);
            }

            return result;
        }

        public static PropVariant FromUInt16(UInt16 value)
        {
            PropVariant result;
            InitPropVariantFromUInt16(value, out result);

            return result;
        }

        public static PropVariant FromUInt16Array(ushort[] values)
        {
            Contract.Requires<ArgumentNullException>(values != null);

            PropVariant result;
            var hr = InitPropVariantFromUInt16Vector(values, (uint)values.Length, out result);
            if (HRESULT.Failed(hr))
            {
                throw ShellException.FromHRESULT(hr);
            }

            return result;
        }

        public static PropVariant FromInt32(Int32 value)
        {
            PropVariant result;
            InitPropVariantFromInt32(value, out result);

            return result;
        }

        public static PropVariant FromInt32Array(Int32[] values)
        {
            Contract.Requires<ArgumentNullException>(values != null);

            PropVariant result;
            var hr = InitPropVariantFromInt32Vector(values, (uint)values.Length, out result);
            if (HRESULT.Failed(hr))
            {
                throw ShellException.FromHRESULT(hr);
            }

            return result;
        }

        public static PropVariant FromUInt32(UInt32 value)
        {
            PropVariant result;
            InitPropVariantFromUInt32(value, out result);

            return result;
        }

        public static PropVariant FromUInt32Array(uint[] values)
        {
            Contract.Requires<ArgumentNullException>(values != null);

            PropVariant result;
            var hr = InitPropVariantFromUInt32Vector(values, (uint)values.Length, out result);
            if (HRESULT.Failed(hr))
            {
                throw ShellException.FromHRESULT(hr);
            }

            return result;
        }

        public static PropVariant FromInt64(Int64 value)
        {
            PropVariant result;
            InitPropVariantFromInt64(value, out result);

            return result;
        }

        public static PropVariant FromInt64Array(Int64[] values)
        {
            Contract.Requires<ArgumentNullException>(values != null);

            PropVariant result;
            var hr = InitPropVariantFromInt64Vector(values, (uint)values.Length, out result);
            if (HRESULT.Failed(hr))
            {
                throw ShellException.FromHRESULT(hr);
            }

            return result;
        }

        public static PropVariant FromUInt64(UInt64 value)
        {
            PropVariant result;
            InitPropVariantFromUInt64(value, out result);

            return result;
        }

        public static PropVariant FromUInt64Array(UInt64[] value)
        {
            Contract.Requires<ArgumentNullException>(value != null);

            PropVariant result;
            var hr = InitPropVariantFromUInt64Vector(value, (uint)value.Length, out result);
            if (HRESULT.Failed(hr))
            {
                throw ShellException.FromHRESULT(hr);
            }

            return result;
        }

        public static PropVariant FromSingle(Single value)
        {
            var result = default(PropVariant);
            result.fltVal = value;
            result.varType = (ushort)VarEnum.VT_R4;

            return result;
        }

        public static PropVariant FromDouble(Double value)
        {
            PropVariant result;
            InitPropVariantFromDouble(value, out result);

            return result;
        }

        public static PropVariant FromDoubleArray(double[] values)
        {
            Contract.Requires<ArgumentNullException>(values != null);

            PropVariant result;
            var hr = InitPropVariantFromDoubleVector(values, (uint)values.Length, out result);
            if (HRESULT.Failed(hr))
            {
                throw ShellException.FromHRESULT(hr);
            }

            return result;
        }

        public static PropVariant FromBoolean(bool value)
        {
            PropVariant result;
            InitPropVariantFromBoolean(value, out result);

            return result;
        }

        public static PropVariant FromBooleanArray(bool[] values)
        {
            Contract.Requires<ArgumentNullException>(values != null);

            PropVariant result;
            var hr = InitPropVariantFromBooleanVector(values, (uint)values.Length, out result);
            if (HRESULT.Failed(hr))
            {
                throw ShellException.FromHRESULT(hr);
            }

            return result;
        }

        public static PropVariant FromDateTime(DateTime value)
        {
            PropVariant result;
            FILETIME filetime;
            DateTimeToFileTime(value, out filetime);
            var hr = InitPropVariantFromFileTime(ref filetime, out result);
            if (HRESULT.Failed(hr))
            {
                throw ShellException.FromHRESULT(hr);
            }

            return result;
        }

        public static PropVariant FromDateTimeArray(DateTime[] value)
        {
            Contract.Requires<ArgumentNullException>(value != null);

            PropVariant result;
            var fileTimeArray = new FILETIME[value.Length];
            for (var index = 0; index < value.Length; ++index)
            {
                DateTimeToFileTime(value[index], out fileTimeArray[index]);
            }
            var hr = InitPropVariantFromFileTimeVector(fileTimeArray, (uint)fileTimeArray.Length, out result);
            if (HRESULT.Failed(hr))
            {
                throw ShellException.FromHRESULT(hr);
            }

            return result;
        }

        public static PropVariant FromGuid(Guid value)
        {
            var result = default(PropVariant);
            var guid = value.ToByteArray();
            result.pclsidVal = Marshal.AllocCoTaskMem(guid.Length);
            Marshal.Copy(guid, 0, result.pclsidVal, guid.Length);
            result.varType = (ushort)VarEnum.VT_CLSID;

            return result;
        }

        /// <summary>
        ///     指定した<paramref name="value" />から<see cref="PropVariant" />を作成します。
        ///     Create a new instance of the <see cref="PropVariant" /> struct
        ///     to the specified <see cref="object" />.
        /// </summary>
        /// <param name="value">オブジェクト。</param>
        /// <returns>作成した<see cref="PropVariant" />。</returns>
        public static PropVariant FromObject(object value)
        {
            if (value == null)
            {
                var result = new PropVariant();
                result.varType = (ushort)VarEnum.VT_EMPTY;
                return result;
            }
            else
            {
                #region Create Value

                if (value is string)
                {
                    return FromString((string)value);
                }
                else if (value is string[])
                {
                    return FromStringArray((string[])value);
                }
                else if (value is sbyte)
                {
                    return FromInt8((sbyte)value);
                }
                else if (value is byte)
                {
                    return FromUInt8((byte)value);
                }
                else if (value is Int16)
                {
                    return FromInt16((Int16)value);
                }
                else if (value is UInt16)
                {
                    return FromUInt16((UInt16)value);
                }
                else if (value is Int32)
                {
                    return FromInt32((Int32)value);
                }
                else if (value is UInt32)
                {
                    return FromUInt32((UInt32)value);
                }
                else if (value is Int64)
                {
                    return FromInt64((Int64)value);
                }
                else if (value is UInt64)
                {
                    return FromUInt64((UInt64)value);
                }
                else if (value is float)
                {
                    return FromSingle((float)value);
                }
                else if (value is double)
                {
                    return FromDouble((double)value);
                }
                else if (value is bool)
                {
                    return FromBoolean((bool)value);
                }
                else if (value is Guid)
                {
                    return FromGuid((Guid)value);
                }

                #endregion
            }

            throw new InvalidOperationException();
        }

        /// <summary>
        ///     値が<see cref="System.Runtime.InteropServices.VarEnum.VT_EMPTY" />または
        ///     <see cref="System.Runtime.InteropServices.VarEnum.VT_NULL" />かどうかを判定する値を取得します。
        /// </summary>
        public bool IsNullOrEmpty => (this.varType == (ushort)VarEnum.VT_EMPTY) ||
                                     (this.varType == (ushort)VarEnum.VT_NULL);

        public string GetStringValue()
        {
            var result = default(string);
            if (this.varType == (ushort)VarEnum.VT_LPSTR)
            {
                result = Marshal.PtrToStringAnsi(this.pszVal);
            }
            else if (this.varType == (ushort)VarEnum.VT_LPWSTR)
            {
                result = Marshal.PtrToStringUni(this.pwszVal);
            }
            return result;
        }

        public string[] GetStringArrayValue()
        {
            var result = new string[this.ca.cElems];
            if (this.varType == ((ushort)VarEnum.VT_VECTOR | (ushort)VarEnum.VT_LPSTR))
            {
                for (var index = 0; index < result.Length; ++index)
                {
                    var ptr = Marshal.ReadIntPtr(this.ca.pElems, index * IntPtr.Size);
                    result[index] = Marshal.PtrToStringAnsi(ptr);
                }
            }
            else if (this.varType == ((ushort)VarEnum.VT_VECTOR | (ushort)VarEnum.VT_LPWSTR))
            {
                for (var index = 0; index < result.Length; ++index)
                {
                    var ptr = Marshal.ReadIntPtr(this.ca.pElems, index * IntPtr.Size);
                    result[index] = Marshal.PtrToStringUni(ptr);
                }
            }
            return result;
        }

        public sbyte GetInt8Value()
        {
            return this.cVal;
        }

        public sbyte[] GetInt8ArrayValue()
        {
            var result = new sbyte[this.ca.cElems];
            for (var index = 0; index < result.Length; ++index)
            {
                result[index] = (sbyte)Marshal.ReadByte(this.ca.pElems, index);
            }
            return result;
        }

        public byte GetUInt8Value()
        {
            return this.bVal;
        }

        public byte[] GetUInt8ArrayValue()
        {
            var result = new byte[this.ca.cElems];
            Marshal.Copy(this.ca.pElems, result, 0, result.Length);
            return result;
        }

        public Int16 GetInt16Value()
        {
            return this.iVal;
        }

        public Int16[] GetInt16ArrayValue()
        {
            var result = new Int16[this.ca.cElems];
            //Marshal.Copy(this.ca.pElems, result, 0, result.Length);
            for (var index = 0u; index < result.Length; ++index)
            {
                PropVariantGetInt16Elem(this, index, out result[index]);
            }
            return result;
        }

        public UInt16 GetUInt16Value()
        {
            return this.uiVal;
        }

        public UInt16[] GetUInt16ArrayValue()
        {
            var result = new UInt16[this.ca.cElems];
            for (var index = 0u; index < result.Length; ++index)
            {
                PropVariantGetUInt16Elem(this, index, out result[index]);
            }
            return result;
        }

        public Int32 GetInt32Value()
        {
            return this.intVal;
        }

        public Int32[] GetInt32ArrayValue()
        {
            var result = new Int32[this.ca.cElems];
            for (var index = 0u; index < this.ca.cElems; ++index)
            {
                PropVariantGetInt32Elem(this, index, out result[index]);
            }
            return result;
        }

        public UInt32 GetUInt32Value()
        {
            return this.uintVal;
        }

        public UInt32[] GetUInt32ArrayValue()
        {
            var result = new UInt32[this.ca.cElems];
            for (var index = 0u; index < result.Length; ++index)
            {
                PropVariantGetUInt32Elem(this, index, out result[index]);
            }
            return result;
        }

        public Int64 GetInt64Value()
        {
            return this.lVal;
        }

        public Int64[] GetInt64ArrayValue()
        {
            var result = new Int64[this.ca.cElems];
            for (var index = 0u; index < result.Length; ++index)
            {
                PropVariantGetInt64Elem(this, index, out result[index]);
            }
            return result;
        }

        public UInt64 GetUInt64Value()
        {
            return this.ulVal;
        }

        public UInt64[] GetUInt64ArrayValue()
        {
            var result = new UInt64[this.ca.cElems];
            for (var index = 0u; index < result.Length; ++index)
            {
                PropVariantGetUInt64Elem(this, index, out result[index]);
            }
            return result;
        }

        public Single GetSingleValue()
        {
            return this.fltVal;
        }

        public Single[] GetSingleArrayValue()
        {
            var result = new Single[this.ca.cElems];
            Marshal.Copy(this.ca.pElems, result, 0, result.Length);
            return result;
        }

        public Double GetDoubleValue()
        {
            return this.dblVal;
        }

        public Double[] GetDoubleArrayValue()
        {
            var result = new Double[this.ca.cElems];
            for (var index = 0u; index < result.Length; ++index)
            {
                PropVariantGetDoubleElem(this, index, out result[index]);
            }
            return result;
        }

        public bool GetBooleanValue()
        {
            return this.boolVal != 0;
        }

        public bool[] GetBooleanArrayValue()
        {
            var result = new bool[this.ca.cElems];
            for (var index = 0u; index < result.Length; ++index)
            {
                PropVariantGetBooleanElem(this, index, out result[index]);
            }
            return result;
        }

        public DateTime GetDateTimeValue()
        {
            DateTime result;
            FileTimeToDateTime(ref this.filetime, out result);
            return result;
        }

        public DateTime[] GetDateTimeArrayValue()
        {
            var result = new DateTime[this.ca.cElems];
            for (var index = 0u; index < result.Length; ++index)
            {
                FILETIME value;
                PropVariantGetFileTimeElem(this, index, out value);
                FileTimeToDateTime(ref value, out result[index]);
            }
            return result;
        }

        public byte[] GetBlobValue()
        {
            var result = new byte[this.ca.cElems];
            Marshal.Copy(ca.pElems, result, 0, (int)this.ca.cElems);
            return result;
        }

        public Guid GetGuidValue()
        {
            var guid = new byte[16];
            Marshal.Copy(this.pclsidVal, guid, 0, 16);
            return new Guid(guid);
        }

        public Guid[] GetGuidArrayValue()
        {
            var result = new Guid[this.ca.cElems];
            for (var index = 0; index < this.ca.cElems; ++index)
            {
                var guid = new byte[16];
                Marshal.Copy(this.ca.pElems, guid, index * 16, 16);
                result[index] = new Guid(guid);
            }
            return result;
        }

        /// <summary>
        ///     値を取得します。
        /// </summary>
        public object GetValue()
        {
            switch (this.varType)
            {
                case (ushort)VarEnum.VT_EMPTY:
                    return null;

                case (ushort)VarEnum.VT_I1:
                    return GetInt8Value();

                case (ushort)VarEnum.VT_UI1:
                    return GetUInt8Value();

                case (ushort)VarEnum.VT_I2:
                    return GetInt16Value();

                case (ushort)VarEnum.VT_UI2:
                    return GetUInt16Value();

                case (ushort)VarEnum.VT_I4:
                case (ushort)VarEnum.VT_INT:
                    return GetInt32Value();

                case (ushort)VarEnum.VT_UI4:
                case (ushort)VarEnum.VT_UINT:
                    return GetUInt32Value();

                case (ushort)VarEnum.VT_I8:
                    return GetInt64Value();

                case (ushort)VarEnum.VT_UI8:
                    return GetUInt64Value();

                case (ushort)VarEnum.VT_R4:
                    return GetSingleValue();

                case (ushort)VarEnum.VT_R8:
                    return GetDoubleValue();

                case (ushort)VarEnum.VT_BOOL:
                    return GetBooleanValue();

                case (ushort)VarEnum.VT_CLSID:
                    return GetGuidValue();

                case (ushort)VarEnum.VT_DATE:
                    return DateTime.FromOADate(this.dblVal);

                case (ushort)VarEnum.VT_FILETIME:
                    return GetDateTimeValue();

                case (ushort)VarEnum.VT_BLOB:
                    return GetBlobValue();

                case (ushort)VarEnum.VT_LPSTR:
                case (ushort)VarEnum.VT_LPWSTR:
                    return GetStringValue();

                case (ushort)VarEnum.VT_UNKNOWN:
                    return Marshal.GetObjectForIUnknown(this.punkVal);

                case (ushort)VarEnum.VT_VECTOR | (ushort)VarEnum.VT_I1:
                    return GetInt8ArrayValue();

                case (ushort)VarEnum.VT_VECTOR | (ushort)VarEnum.VT_UI1:
                    return GetUInt8ArrayValue();

                case (ushort)VarEnum.VT_VECTOR | (ushort)VarEnum.VT_I2:
                    return GetInt16ArrayValue();

                case (ushort)VarEnum.VT_VECTOR | (ushort)VarEnum.VT_UI2:
                    return GetUInt16ArrayValue();

                case (ushort)VarEnum.VT_VECTOR | (ushort)VarEnum.VT_I4:
                    return GetInt32ArrayValue();

                case (ushort)VarEnum.VT_VECTOR | (ushort)VarEnum.VT_UI4:
                    return GetUInt32ArrayValue();

                case (ushort)VarEnum.VT_VECTOR | (ushort)VarEnum.VT_I8:
                    return GetInt64ArrayValue();

                case (ushort)VarEnum.VT_VECTOR | (ushort)VarEnum.VT_UI8:
                    return GetUInt64ArrayValue();

                case (ushort)VarEnum.VT_VECTOR | (ushort)VarEnum.VT_R4:
                    return GetSingleArrayValue();

                case (ushort)VarEnum.VT_VECTOR | (ushort)VarEnum.VT_R8:
                    return GetDoubleArrayValue();

                case (ushort)VarEnum.VT_VECTOR | (ushort)VarEnum.VT_BOOL:
                    return GetBooleanArrayValue();

                case (ushort)VarEnum.VT_VECTOR | (ushort)VarEnum.VT_FILETIME:
                    return GetDateTimeArrayValue();

                case (ushort)VarEnum.VT_VECTOR | (ushort)VarEnum.VT_LPSTR:
                case (ushort)VarEnum.VT_VECTOR | (ushort)VarEnum.VT_LPWSTR:
                    return GetStringArrayValue();

                default:
                    return null;
            }
        }

        public T GetValue<T>()
        {
            return (T)GetValue();
        }

        public void Clear()
        {
            var tmp = this;
            PropVariantClear(ref tmp);

            this.varType = (ushort)VarEnum.VT_EMPTY;
        }

        private static void DateTimeToFileTime(DateTime value, out FILETIME filetime)
        {
            var hFt = value.ToFileTime();
            filetime.dwLowDateTime = (int)(hFt & 0xFFFFFFFF);
            filetime.dwHighDateTime = (int)(hFt >> 32);
        }

        private static void FileTimeToDateTime(ref FILETIME value, out DateTime dateTime)
        {
            var filetime = ((long)(value.dwHighDateTime) << 32) | (uint)value.dwLowDateTime;
            dateTime = DateTime.FromFileTime(filetime);
        }

        /// <summary>
        ///     <see cref="PropVariant" />の文字列表現を取得します。
        /// </summary>
        /// <returns><see cref="PropVariant" />の文字列表現。</returns>
        public override string ToString()
        {
            return String.Format(CultureInfo.InvariantCulture, "{0}: {1}",
                this.GetValue(),
                ((VarEnum)this.varType).ToString());
        }
    }
}