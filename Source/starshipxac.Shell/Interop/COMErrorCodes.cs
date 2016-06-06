using System;
using System.Diagnostics.CodeAnalysis;

namespace starshipxac.Shell.Interop
{
    /// <summary>
    ///     COMエラーコードを定義します。
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         http://msdn.microsoft.com/en-us/library/windows/desktop/dd542642(v=vs.85).aspx
    ///     </para>
    ///     <para>
    ///         Header File: WinError.h
    ///     </para>
    /// </remarks>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public static class COMErrorCodes
    {
        public static readonly HRESULT S_OK = (HRESULT)0;
        public static readonly HRESULT S_FALSE = (HRESULT)1;

        public static readonly HRESULT E_OUTOFMEMORY = (HRESULT)0x8007000E;
        public static readonly HRESULT E_INVALIDARG = (HRESULT)0x80070057;
        public static readonly HRESULT E_NOINTERFACE = (HRESULT)0x80004002;
        public static readonly HRESULT E_FAIL = (HRESULT)0x80004005;

        public static readonly HRESULT TYPE_E_ELEMENTNOTFOUND = (HRESULT)0x8002802B;

        public static readonly HRESULT MK_E_NOOBJECT = (HRESULT)0x800401E5;

        public static readonly HRESULT STG_E_ACCESSDENIED = (HRESULT)0x80030005;

        public static readonly HRESULT Cancelled = HRESULT.MakeHRESULT(
            HRESULT.Severity.SEVERITY_ERROR, HRESULT.Facility.FACILITY_WIN32, ErrorCodes.ERROR_CANCELLED);

        public static readonly HRESULT ResourceInUse = HRESULT.MakeHRESULT(
            HRESULT.Severity.SEVERITY_ERROR, HRESULT.Facility.FACILITY_WIN32, ErrorCodes.ERROR_BUSY);
    }
}