using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using starshipxac.Shell.Interop;
using starshipxac.Shell.Search.Interop;

namespace starshipxac.Shell.PropertySystem.Interop
{
    /// <summary>
    ///     <c>IPropertyDescription2</c>インターフェイスを定義します。
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/dd378278(v=vs.85).aspx
    /// </remarks>
    [ComImport]
    [Guid(PropertySystemIID.IPropertyDescription2)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IPropertyDescription2 : IPropertyDescription
    {
#pragma warning disable 108
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetPropertyKey(out PROPERTYKEY pkey);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetCanonicalName([MarshalAs(UnmanagedType.LPWStr)] out string ppszName);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetPropertyType(out VarEnum pvartype);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetDisplayName([MarshalAs(UnmanagedType.LPWStr)] out string ppszName);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetEditInvitation([MarshalAs(UnmanagedType.LPWStr)] out string ppszInvite);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetTypeFlags([In] PROPDESC_TYPE_FLAGS mask, out PROPDESC_TYPE_FLAGS ppdtFlags);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetViewFlags(out PROPDESC_VIEW_FLAGS ppdvFlags);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetDefaultColumnWidth(out uint pcxChars);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetDisplayType(out PROPDESC_DISPLAYTYPE pdisplaytype);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetColumnState(out uint pcsFlags);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetGroupingRange(out PROPDESC_GROUPING_RANGE pgr);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetRelativeDescriptionType(out PROPDESC_RELATIVEDESCRIPTION_TYPE prdt);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetRelativeDescription(
            [In] PropVariant propvar1,
            [In] PropVariant propvar2,
            [MarshalAs(UnmanagedType.LPWStr)] out string ppszDesc1,
            [MarshalAs(UnmanagedType.LPWStr)] out string ppszDesc2);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetSortDescription(out PROPDESC_SORTDESCRIPTION psd);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetSortDescriptionLabel([In] int fDescending, [MarshalAs(UnmanagedType.LPWStr)] out string ppszDescription);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetAggregationType(out PROPDESC_AGGREGATION_TYPE paggtype);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetConditionType(
            out PROPDESC_CONDITION_TYPE pcontype,
            out CONDITION_OPERATION popDefault);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetEnumTypeList([In] ref Guid riid, out IntPtr ppv);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void CoerceToCanonicalValue([In] [Out] PropVariant ppropvar);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void FormatForDisplay([In] PropVariant propvar, [In] ref PROPDESC_FORMAT_FLAGS pdfFlags,
            [MarshalAs(UnmanagedType.LPWStr)] out string ppszDisplay);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT IsValueCanonical([In] PropVariant propvar);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetImageReferenceForValue(
            [In] PropVariant propvar,
            [Out] [MarshalAs(UnmanagedType.LPWStr)] out string ppszImageRes);
#pragma warning restore 108
    }
}