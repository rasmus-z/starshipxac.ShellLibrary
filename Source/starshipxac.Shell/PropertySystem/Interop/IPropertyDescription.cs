using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using starshipxac.Shell.Interop;
using starshipxac.Shell.Search.Interop;

namespace starshipxac.Shell.PropertySystem.Interop
{
    /// <summary>
    /// <c>IPropertyDescription</c>インターフェイスを定義します。
    /// </summary>
    /// <remarks>
    /// http://msdn.microsoft.com/en-us/library/windows/desktop/bb761561(v=vs.85).aspx
    /// </remarks>
    [ComImport]
    [Guid(PropertySystemIID.IPropertyDescription)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IPropertyDescription
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetPropertyKey(out PROPERTYKEY pkey);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetCanonicalName([MarshalAs(UnmanagedType.LPWStr)] out string ppszName);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT GetPropertyType(out VarEnum pvartype);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [PreserveSig]
        HRESULT GetDisplayName(out IntPtr ppszName);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT GetEditInvitation(out IntPtr ppszInvite);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT GetTypeFlags([In] PROPDESC_TYPE_FLAGS mask, out PROPDESC_TYPE_FLAGS ppdtFlags);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT GetViewFlags(out PROPDESC_VIEW_FLAGS ppdvFlags);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT GetDefaultColumnWidth(out uint pcxChars);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT GetDisplayType(out PROPDESC_DISPLAYTYPE pdisplaytype);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT GetColumnState(out SHCOLSTATE pcsFlags);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT GetGroupingRange(out PROPDESC_GROUPING_RANGE pgr);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetRelativeDescriptionType(out PROPDESC_RELATIVEDESCRIPTION_TYPE prdt);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetRelativeDescription([In] PropVariant propvar1, [In] PropVariant propvar2,
            [MarshalAs(UnmanagedType.LPWStr)] out string ppszDesc1, [MarshalAs(UnmanagedType.LPWStr)] out string ppszDesc2);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT GetSortDescription(out PROPDESC_SORTDESCRIPTION psd);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT GetSortDescriptionLabel([In] bool fDescending, out IntPtr ppszDescription);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT GetAggregationType(out PROPDESC_AGGREGATION_TYPE paggtype);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT GetConditionType(out PROPDESC_CONDITION_TYPE pcontype, out CONDITION_OPERATION popDefault);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT GetEnumTypeList([In] ref Guid riid, [Out] [MarshalAs(UnmanagedType.Interface)] out IPropertyEnumTypeList ppv);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void CoerceToCanonicalValue([In] [Out] PropVariant propvar);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT FormatForDisplay([In] PropVariant propvar, [In] ref PROPDESC_FORMAT_FLAGS pdfFlags,
            [MarshalAs(UnmanagedType.LPWStr)] out string ppszDisplay);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT IsValueCanonical([In] PropVariant propvar);
    }
}