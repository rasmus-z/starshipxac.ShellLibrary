using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using starshipxac.Shell.Interop;

namespace starshipxac.Windows.Shell.Controls.Explorers.Interop
{
    /// <summary>
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         ICommDlgBrowser: http://msdn.microsoft.com/en-us/library/windows/desktop/bb776141(v=vs.85).aspx
    ///     </para>
    ///     <para>
    ///         ICommDlgBrowser2: http://msdn.microsoft.com/en-us/library/windows/desktop/bb776136(v=vs.85).aspx
    ///     </para>
    ///     <para>
    ///         ICommDlgBrowser3: http://msdn.microsoft.com/en-us/library/windows/desktop/bb776127(v=vs.85).aspx
    ///     </para>
    /// </remarks>
    [ComImport]
    [Guid(ExplorerBrowserIIDGuid.ICommDlgBrowser3)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface ICommDlgBrowser3
    {
        // ICommDlgBrowser
        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT OnDefaultCommand(IntPtr ppshv);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT OnStateChange(IntPtr ppshv, UInt32 uChange);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT IncludeObject(IntPtr ppshv, IntPtr pidl);

        // ICommDlgBrowser2
        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT GetDefaultMenuText(IShellView shellView, IntPtr buffer, int bufferMaxLength);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT GetViewFlags([Out] out uint pdwFlags);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT Notify(IntPtr pshv, UInt32 notifyType);

        // ICommDlgBrowser3
        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT GetCurrentFilter(StringBuilder pszFileSpec, int cchFileSpec);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT OnColumnClicked(IShellView ppshv, int iColumn);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        HRESULT OnPreViewCreated(IShellView ppshv);
    }
}