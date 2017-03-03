using System;
using System.Runtime.InteropServices;
using starshipxac.Shell.Interop;
using FILETIME = System.Runtime.InteropServices.ComTypes.FILETIME;

namespace starshipxac.Shell.PropertySystem.Interop
{
    /// <summary>
    /// </summary>
    /// <remarks>
    ///     https://msdn.microsoft.com/en-us/library/windows/desktop/bb762286(v=vs.85).aspx
    /// </remarks>
    internal static class PropVariantNativeMethods
    {
        [DllImport("Propsys.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern void ClearPropVariantArray([In] ref PropVariant rgPropVar, uint vVars);

        [DllImport("Propsys.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern HRESULT FreePropVariantArray(uint cVariants, [In] [Out] ref PropVariant rgvars);

        // Boolean
        internal static HRESULT InitPropVariantFromBoolean(
            bool fVal,
            out PropVariant ppropvar )
        {
            ppropvar = default(PropVariant);
            ppropvar.boolVal = (short)((fVal) ? -1 : 0);
            ppropvar.varType = (ushort)VarEnum.VT_BOOL;

            return COMErrorCodes.S_OK;
        }

        [DllImport("Propsys.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern HRESULT InitPropVariantFromBooleanVector(
            [In] bool[] prgf,
            uint cElems,
            [Out] out PropVariant ppropvar);

        // Buffer
        [DllImport("Propsys.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern HRESULT InitPropVariantFromBuffer(
            [In] byte[] pv,
            uint cb,
            [Out] out PropVariant ppropvar);

        // Double
        internal static HRESULT InitPropVariantFromDouble(
            double dblVal,
            out PropVariant ppropvar)
        {
            ppropvar = default(PropVariant);
            ppropvar.dblVal = dblVal;
            ppropvar.varType = (ushort)VarEnum.VT_R8;

            return COMErrorCodes.S_OK;
        }

        [DllImport("Propsys.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern HRESULT InitPropVariantFromDoubleVector(
            [In] double[] prgn,
            uint cElems,
            [Out] out PropVariant ppropvar);

        // FILETIME
        [DllImport("Propsys.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern HRESULT InitPropVariantFromFileTime(
            [In] ref FILETIME pftIn,
            [Out] out PropVariant ppropvar);

        [DllImport("Propsys.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern HRESULT InitPropVariantFromFileTimeVector(
            [In] FILETIME[] prgft,
            uint cElems,
            [Out] out PropVariant ppropvar);

        // Int16
        internal static HRESULT InitPropVariantFromInt16(
            Int16 nVal,
            out PropVariant ppropvar)
        {
            ppropvar = default(PropVariant);
            ppropvar.iVal = nVal;
            ppropvar.varType = (ushort)VarEnum.VT_I2;

            return COMErrorCodes.S_OK;
        }

        [DllImport("Propsys.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern HRESULT InitPropVariantFromInt16Vector(
            [In] Int16[] prgn,
            uint cElems,
            [Out] out PropVariant ppropvar);

        // Int32
        internal static HRESULT InitPropVariantFromInt32(
            Int32 lVal,
            out PropVariant ppropvar)
        {
            ppropvar = default(PropVariant);
            ppropvar.intVal = lVal;
            ppropvar.varType = (ushort)VarEnum.VT_I4;

            return COMErrorCodes.S_OK;
        }

        [DllImport("Propsys.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern HRESULT InitPropVariantFromInt32Vector(
            [In] Int32[] prgn,
            uint cElems,
            [Out] out PropVariant ppropvar);

        // Int64
        internal static HRESULT InitPropVariantFromInt64(
            Int64 llVal,
            out PropVariant ppropvar)
        {
            ppropvar = default(PropVariant);
            ppropvar.lVal = llVal;
            ppropvar.varType = (ushort)VarEnum.VT_I8;

            return COMErrorCodes.S_OK;
        }

        [DllImport("Propsys.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern HRESULT InitPropVariantFromInt64Vector(
            [In] Int64[] prgn,
            uint cElems,
            [Out] out PropVariant ppropvar);

        // PropVariant
        [DllImport("Propsys.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern HRESULT InitPropVariantFromPropVariantVectorElem(
            [In] ref PropVariant propvarIn,
            uint iElem,
            [Out] out PropVariant ppropvar);

        // Resource
        [DllImport("Propsys.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern HRESULT InitPropVariantFromResource(
            [In] IntPtr hinst,
            uint id,
            [Out] out PropVariant ppropvar);

        // String
        internal static HRESULT InitPropVariantFromString(
            string psz,
            out PropVariant ppropvar)
        {
            ppropvar = default(PropVariant);
            ppropvar.pszVal = Marshal.StringToCoTaskMemUni(psz);
            ppropvar.varType = (ushort)VarEnum.VT_LPWSTR;

            return COMErrorCodes.S_OK;
        }

        /// <summary>
        ///     Initializes a <see cref="PropVariant" /> structure from a specified string.
        ///     The string is parsed as a semi-colon delimited list (for example: "A;B;C").
        /// </summary>
        /// <param name="psz"></param>
        /// <param name="ppropvar"></param>
        /// <returns></returns>
        /// <remarks>
        ///     Creates a VT_VECTOR | VT_LPWSTR propvariant.
        ///     It parses the source string as a semicolon list of values. The string "a; b; c" creates a vector with three values.
        ///     Leading and trailing whitespace are removed, and empty values are omitted.
        /// </remarks>
        [DllImport("Propsys.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern HRESULT InitPropVariantFromStringAsVector(
            [In] string psz,
            [Out] out PropVariant ppropvar);

        [DllImport("Propsys.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern HRESULT InitPropVariantFromStringVector(
            [In] string[] prgsz,
            uint cElems,
            [Out] out PropVariant ppropvar);

        // UInt16
        internal static HRESULT InitPropVariantFromUInt16(
            UInt16 uiVal,
            out PropVariant ppropvar)
        {
            ppropvar = default(PropVariant);
            ppropvar.uiVal = uiVal;
            ppropvar.varType = (ushort)VarEnum.VT_UI2;

            return COMErrorCodes.S_OK;
        }

        [DllImport("Propsys.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern HRESULT InitPropVariantFromUInt16Vector(
            [In] UInt16[] prgn,
            uint cElems,
            [Out] out PropVariant ppropvar);

        // UInt32
        internal static HRESULT InitPropVariantFromUInt32(
            UInt32 ulVal,
            out PropVariant ppropvar)
        {
            ppropvar = default(PropVariant);
            ppropvar.uintVal = ulVal;
            ppropvar.varType = (ushort)VarEnum.VT_UI4;

            return COMErrorCodes.S_OK;
        }

        [DllImport("Propsys.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern HRESULT InitPropVariantFromUInt32Vector(
            [In] UInt32[] prgn,
            uint cElems,
            [Out] out PropVariant ppropvar);

        // UInt64
        internal static HRESULT InitPropVariantFromUInt64(
            UInt64 ullVal,
            out PropVariant ppropvar)
        {
            ppropvar = default(PropVariant);
            ppropvar.ulVal = ullVal;
            ppropvar.varType = (ushort)VarEnum.VT_UI8;

            return COMErrorCodes.S_OK;
        }

        [DllImport("Propsys.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern HRESULT InitPropVariantFromUInt64Vector(
            [In] UInt64[] prgn,
            uint cElems,
            [Out] out PropVariant ppropvar);

        // PropVariant Vector
        [DllImport("Propsys.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern HRESULT InitPropVariantVectorFromPropVariant(
            [In] ref PropVariant propvarSingle,
            [Out] out PropVariant[] propvarVector);


        [DllImport("Propsys.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern HRESULT PropVariantChangeType(
            [Out] out PropVariant ppropvarDest,
            [In] ref PropVariant propvarSrc,
            [In] int flags,
            [In] ushort vt);

        [DllImport("Ole32.dll")]
        internal static extern HRESULT PropVariantClear([In] ref PropVariant pvar);

        [DllImport("Ole32.dll")]
        internal static extern HRESULT PropVariantCopy([Out] out PropVariant pDst, [In] ref PropVariant pSrc);


        [DllImport("Propsys.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern HRESULT PropVariantGetBooleanElem(
            [In] PropVariant propVar,
            [In] uint iElem,
            [Out] [MarshalAs(UnmanagedType.Bool)] out bool pfVal);

        [DllImport("Propsys.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern HRESULT PropVariantGetDoubleElem(
            [In] PropVariant propVar,
            [In] uint iElem,
            [Out] out double pnVal);

        /// <summary>
        ///     Retrieves the element count of a <see cref="PropVariant" /> structure.
        /// </summary>
        /// <param name="propVar"></param>
        /// <returns></returns>
        [DllImport("Propsys.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.I4)]
        internal static extern int PropVariantGetElementCount([In] PropVariant propVar);

        [DllImport("Propsys.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern HRESULT PropVariantGetFileTimeElem(
            [In] PropVariant propVar,
            [In] uint iElem,
            [Out] [MarshalAs(UnmanagedType.Struct)] out FILETIME pftVal);

        [DllImport("Propsys.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern HRESULT PropVariantGetInt16Elem(
            [In] PropVariant propVar,
            [In] uint iElem,
            [Out] out Int16 pnVal);

        [DllImport("Propsys.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern HRESULT PropVariantGetInt32Elem(
            [In] PropVariant propVar,
            [In] uint iElem,
            [Out] out Int32 pnVal);

        [DllImport("Propsys.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern HRESULT PropVariantGetInt64Elem(
            [In] PropVariant propVar,
            [In] uint iElem,
            [Out] out Int64 pnVal);

        [DllImport("Propsys.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern HRESULT PropVariantGetStringElem(
            [In] ref PropVariant propVar,
            [In] uint iElem,
            [Out] [MarshalAs(UnmanagedType.LPWStr)] out string ppszVal);

        [DllImport("Propsys.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern HRESULT PropVariantGetUInt16Elem(
            [In] PropVariant propVar,
            [In] uint iElem,
            [Out] out UInt16 pnVal);

        [DllImport("Propsys.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern HRESULT PropVariantGetUInt32Elem(
            [In] PropVariant propVar,
            [In] uint iElem,
            [Out] out UInt32 pnVal);

        [DllImport("Propsys.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern HRESULT PropVariantGetUInt64Elem(
            [In] PropVariant propVar,
            [In] uint iElem,
            [Out] out UInt64 pnVal);
    }
}