using System;
using System.Runtime.InteropServices;
using starshipxac.Shell.Interop;

namespace starshipxac.Shell.PropertySystem.Interop
{
    /// <summary>
    /// <c>IPropertyStoreCapabilities</c>インターフェイスを定義します。
    /// </summary>
    /// <remarks>
    /// http://msdn.microsoft.com/en-us/library/windows/desktop/bb761452(v=vs.85).aspx
    /// </remarks>
    [ComImport]
    [Guid(PropertySystemIID.IPropertyStoreCapabilities)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IPropertyStoreCapabilities
    {
        HRESULT IsPropertyWritable([In] ref PROPERTYKEY propertyKey);
    }
}