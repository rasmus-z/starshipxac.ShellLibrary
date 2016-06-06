using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace starshipxac.Shell.Interop
{
    /// <summary>
    ///     <c>HRESULT</c>を定義します。
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/cc231198.aspx
    /// </remarks>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public struct HRESULT
    {
        /// <summary>
        ///     <see cref="HRESULT" />を初期化します。
        /// </summary>
        /// <param name="hr"><c>HRESULT</c>の値。</param>
        public HRESULT(int hr)
            : this()
        {
            this.Value = hr;
        }

        /// <summary>
        ///     <see cref="HRESULT" />を初期化します。
        /// </summary>
        /// <param name="hr"><c>HRESULT</c>の値。</param>
        public HRESULT(uint hr)
            : this()
        {
            this.Value = unchecked((int)hr);
        }

        /// <summary>
        ///     <see cref="HRESULT" />を初期化します。
        /// </summary>
        /// <param name="severity"></param>
        /// <param name="facility"></param>
        /// <param name="code"></param>
        public HRESULT(int severity, int facility, int code)
            : this()
        {
            this.Value = unchecked((int)(((uint)severity << 31) | ((uint)facility << 16) | (uint)code));
        }

        /// <summary>
        ///     <see cref="HRESULT" />を作成します。
        /// </summary>
        /// <param name="severity"></param>
        /// <param name="facility"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static HRESULT MakeHRESULT(int severity, int facility, int code)
        {
            return new HRESULT(severity, facility, code);
        }

        /// <summary>
        ///     <see cref="HRESULT" />を作成します。
        /// </summary>
        /// <param name="severity"></param>
        /// <param name="facility"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static HRESULT MakeHRESULT(Severity severity, Facility facility, int code)
        {
            return new HRESULT((int)severity, (int)facility, code);
        }

        /// <summary>
        ///     <see cref="HRESULT" />の値を取得します。
        /// </summary>
        public int Value { get; }

        public enum Severity
        {
            SEVERITY_SUCCESS = 0,
            SEVERITY_ERROR = 1,
        }

        public enum Facility
        {
            FACILITY_WIN32 = 7,
            FACILITY_WINDOWS = 8,
        }

        /// <summary>
        ///     <see cref="HRESULT" />のコード部分を取得します。
        /// </summary>
        /// <param name="hresult"></param>
        /// <returns></returns>
        public static int Code(HRESULT hresult)
        {
            return hresult & 0xFFFF;
        }

        /// <summary>
        ///     指定した<see cref="HRESULT" />が成功を示す値かどうかを判定します。
        /// </summary>
        /// <param name="hresult"></param>
        /// <returns></returns>
        public static bool Succeeded(HRESULT hresult)
        {
            return hresult >= 0;
        }

        /// <summary>
        ///     指定した<see cref="HRESULT" />が成功を示す値かどうかを判定します。
        /// </summary>
        /// <param name="hresult"></param>
        /// <returns></returns>
        public static bool Succeeded(int hresult)
        {
            return hresult >= 0;
        }

        /// <summary>
        ///     指定した<see cref="HRESULT" />が失敗を示す値かどうかを判定します。
        /// </summary>
        /// <param name="hresult"></param>
        /// <returns></returns>
        public static bool Failed(HRESULT hresult)
        {
            return hresult < 0;
        }

        /// <summary>
        ///     指定した<see cref="HRESULT" />が失敗を示す値かどうかを判定します。
        /// </summary>
        /// <param name="hresult"></param>
        /// <returns></returns>
        public static bool Failed(int hresult)
        {
            return hresult < 0;
        }

        /// <summary>
        ///     指定した<see cref="HRESULT" />が失敗を示す値の場合、例外を送出します。
        /// </summary>
        /// <param name="hresult"></param>
        public static void ThrowIfFailed(HRESULT hresult)
        {
            if (Failed(hresult))
            {
                Marshal.ThrowExceptionForHR(hresult);
            }
        }

        /// <summary>
        ///     指定した<see cref="HRESULT" />に対応する<see cref="Exception" />オブジェクトを取得します。
        /// </summary>
        /// <param name="hresult"></param>
        /// <returns></returns>
        public static Exception GetException(HRESULT hresult)
        {
            return Marshal.GetExceptionForHR(hresult);
        }

        public static explicit operator HRESULT(int hr)
        {
            return new HRESULT(hr);
        }

        public static explicit operator HRESULT(uint hr)
        {
            return new HRESULT(hr);
        }

        public static implicit operator int(HRESULT hr)
        {
            return hr.Value;
        }

        public static implicit operator uint(HRESULT hr)
        {
            return (uint)hr.Value;
        }

        public static bool operator ==(HRESULT x, HRESULT y)
        {
            return x.Value == y.Value;
        }

        public static bool operator !=(HRESULT x, HRESULT y)
        {
            return !(x == y);
        }

        public static bool operator ==(HRESULT x, uint y)
        {
            return (uint)x.Value == y;
        }

        public static bool operator !=(HRESULT x, uint y)
        {
            return !(x == y);
        }

        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                if (obj is HRESULT)
                {
                    return this.Value == ((HRESULT)obj).Value;
                }
                if (obj is int)
                {
                    return this.Value == (int)obj;
                }
                if (obj is uint)
                {
                    return this.Value == (uint)obj;
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        public override string ToString()
        {
            return $"0x{this.Value:x}";
        }
    }
}