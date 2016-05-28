using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using starshipxac.Shell.Interop;
// ReSharper disable InconsistentNaming

namespace starshipxac.Windows.Shell.Dialogs.Interop
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// http://msdn.microsoft.com/en-us/library/windows/desktop/bb775912(v=vs.85).aspx
    /// </remarks>
    [ComImport]
    [Guid(ShellIID.IFileDialogCustomize)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IFileDialogCustomize
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void EnableOpenDropDown([In] UInt32 dwIDCtl);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void AddMenu(
            [In] UInt32 dwIDCtl,
            [In] [MarshalAs(UnmanagedType.LPWStr)] string pszLabel);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void AddPushButton(
            [In] UInt32 dwIDCtl,
            [In] [MarshalAs(UnmanagedType.LPWStr)] string pszLabel);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void AddComboBox([In] UInt32 dwIDCtl);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void AddRadioButtonList([In] UInt32 dwIDCtl);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void AddCheckButton(
            [In] UInt32 dwIDCtl,
            [In] [MarshalAs(UnmanagedType.LPWStr)] string pszLabel,
            [In] bool bChecked);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void AddEditBox(
            [In] UInt32 dwIDCtl,
            [In] [MarshalAs(UnmanagedType.LPWStr)] string pszText);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void AddSeparator([In] UInt32 dwIDCtl);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void AddText(
            [In] UInt32 dwIDCtl,
            [In] [MarshalAs(UnmanagedType.LPWStr)] string pszText);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void SetControlLabel(
            [In] UInt32 dwIDCtl,
            [In] [MarshalAs(UnmanagedType.LPWStr)] string pszLabel);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetControlState(
            [In] UInt32 dwIDCtl,
            [Out] out CDCONTROLSTATEF pdwState);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void SetControlState(
            [In] UInt32 dwIDCtl,
            [In] CDCONTROLSTATEF dwState);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetEditBoxText(
            [In] UInt32 dwIDCtl,
            [MarshalAs(UnmanagedType.LPWStr)] out string ppszText);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void SetEditBoxText(
            [In] UInt32 dwIDCtl,
            [In] [MarshalAs(UnmanagedType.LPWStr)] string pszText);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetCheckButtonState(
            [In] UInt32 dwIDCtl,
            [Out] out bool pbChecked);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void SetCheckButtonState(
            [In] UInt32 dwIDCtl,
            [In] bool bChecked);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void AddControlItem(
            [In] UInt32 dwIDCtl,
            [In] UInt32 dwIDItem,
            [In] [MarshalAs(UnmanagedType.LPWStr)] string pszLabel);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void RemoveControlItem(
            [In] UInt32 dwIDCtl,
            [In] UInt32 dwIDItem);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void RemoveAllControlItems([In] UInt32 dwIDCtl);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetControlItemState(
            [In] UInt32 dwIDCtl,
            [In] UInt32 dwIDItem,
            [Out] out CDCONTROLSTATEF pdwState);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void SetControlItemState(
            [In] UInt32 dwIDCtl,
            [In] UInt32 dwIDItem,
            [In] CDCONTROLSTATEF dwState);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetSelectedControlItem(
            [In] UInt32 dwIDCtl,
            [Out] out UInt32 pdwIDItem);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void SetSelectedControlItem(
            [In] UInt32 dwIDCtl,
            [In] UInt32 dwIDItem);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void StartVisualGroup(
            [In] UInt32 dwIDCtl,
            [In] [MarshalAs(UnmanagedType.LPWStr)] string pszLabel);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void EndVisualGroup();

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void MakeProminent([In] UInt32 dwIDCtl);
    }
}