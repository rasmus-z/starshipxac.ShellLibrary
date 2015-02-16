using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace starshipxac.Shell.PropertySystem.Interop
{
    /// <summary>
    /// <c>IPropertyEnumTypeList</c>インターフェイスを定義します。
    /// </summary>
    /// <remarks>
    /// http://msdn.microsoft.com/en-us/library/windows/desktop/bb761483(v=vs.85).aspx
    /// </remarks>
    [ComImport]
    [Guid(PropertySystemIID.IPropertyEnumTypeList)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IPropertyEnumTypeList
    {
#pragma warning disable 108
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetCount([Out] out uint pctypes);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetAt(
            [In] uint itype,
            [In] ref Guid riid,
            [Out] [MarshalAs(UnmanagedType.Interface)] out IPropertyEnumType ppv);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetConditionAt(
            [In] uint index,
            [In] ref Guid riid,
            out IntPtr ppv);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void FindMatchingIndex(
            [In] PropVariant propvarCmp,
            [Out] out uint pnIndex);
#pragma warning restore 108
    }
}