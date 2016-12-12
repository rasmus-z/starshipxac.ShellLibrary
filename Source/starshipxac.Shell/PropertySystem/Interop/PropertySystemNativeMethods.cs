using System;
using System.Runtime.InteropServices;
using starshipxac.Shell.Interop;

namespace starshipxac.Shell.PropertySystem.Interop
{
    /// <summary>
    ///     Windows Property System functions.
    /// </summary>
    /// <remarks>
    ///     https://msdn.microsoft.com/en-us/library/windows/desktop/ff728864(v=vs.85).aspx
    /// </remarks>
    internal static class PropertySystemNativeMethods
    {
        /// <summary>
        ///     Retrieves the canonical name of the property, given its <see cref="PROPERTYKEY"/>.
        /// </summary>
        /// <param name="propkey"></param>
        /// <param name="ppszCanonicalName"></param>
        /// <returns></returns>
        /// <remarks>
        ///     https://msdn.microsoft.com/en-us/library/windows/desktop/bb776502(v=vs.85).aspx
        /// </remarks>
        [DllImport("propsys.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern HRESULT PSGetNameFromPropertyKey(
            ref PROPERTYKEY propkey,
            [Out] [MarshalAs(UnmanagedType.LPWStr)] out string ppszCanonicalName);

        /// <summary>
        ///     Gets an instance of a property description interface for a property specified by a <see cref="PROPERTYKEY"/> structure.
        /// </summary>
        /// <param name="propkey"></param>
        /// <param name="riid"></param>
        /// <param name="ppv"></param>
        /// <returns></returns>
        /// <remarks>
        ///     https://msdn.microsoft.com/en-us/library/windows/desktop/bb776503(v=vs.85).aspx
        /// </remarks>
        [DllImport("propsys.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern HRESULT PSGetPropertyDescription(
            ref PROPERTYKEY propkey,
            ref Guid riid,
            [Out] [MarshalAs(UnmanagedType.Interface)] out IPropertyDescription ppv);

        /// <summary>
        ///     Gets the property key for a canonical property name.
        /// </summary>
        /// <param name="pszCanonicalName"></param>
        /// <param name="propkey"></param>
        /// <returns></returns>
        /// <remarks>
        ///     https://msdn.microsoft.com/en-us/library/windows/desktop/bb762081(v=vs.85).aspx
        /// </remarks>
        [DllImport("propsys.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern HRESULT PSGetPropertyKeyFromName(
            [In] [MarshalAs(UnmanagedType.LPWStr)] string pszCanonicalName,
            out PROPERTYKEY propkey);

        /// <summary>
        ///     Gets an instance of a property description list interface for a specified property list.
        /// </summary>
        /// <param name="pszPropList"></param>
        /// <param name="riid"></param>
        /// <param name="ppv"></param>
        /// <returns></returns>
        /// <remarks>
        ///     https://msdn.microsoft.com/en-us/library/windows/desktop/bb762079(v=vs.85).aspx
        /// </remarks>
        [DllImport("propsys.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern HRESULT PSGetPropertyDescriptionListFromString(
            [In] [MarshalAs(UnmanagedType.LPWStr)] string pszPropList,
            [In] ref Guid riid,
            out IPropertyDescriptionList ppv);
    }
}