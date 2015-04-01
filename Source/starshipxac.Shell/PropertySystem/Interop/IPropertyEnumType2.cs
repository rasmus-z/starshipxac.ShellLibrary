using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace starshipxac.Shell.PropertySystem.Interop
{
    /// <summary>
    /// <c>IPropertyEnumType2</c>インターフェイスを定義します。
    /// </summary>
    /// <remarks>
    /// http://msdn.microsoft.com/en-us/library/windows/desktop/dd378274(v=vs.85).aspx
    /// </remarks>
    [ComImport]
    [Guid(PropertySystemIID.IPropertyEnumType2)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IPropertyEnumType2 : IPropertyEnumType
    {
#pragma warning disable 108
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetEnumType([Out] out PROPENUMTYPE penumtype);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetValue([Out] PropVariant ppropvar);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetRangeMinValue([Out] PropVariant ppropvar);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetRangeSetValue([Out] PropVariant ppropvar);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetDisplayText([Out] [MarshalAs(UnmanagedType.LPWStr)] out string ppszDisplay);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetImageReference([Out] [MarshalAs(UnmanagedType.LPWStr)] out string ppszImageRes);
#pragma warning restore 108
    }
}