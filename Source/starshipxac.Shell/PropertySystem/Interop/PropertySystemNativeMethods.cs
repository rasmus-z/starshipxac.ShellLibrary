using System;
using System.Runtime.InteropServices;
using starshipxac.Shell.Interop;

namespace starshipxac.Shell.PropertySystem.Interop
{
	internal static class PropertySystemNativeMethods
	{
		[DllImport("propsys.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern HRESULT PSGetNameFromPropertyKey(
			ref PROPERTYKEY propkey,
			[Out] [MarshalAs(UnmanagedType.LPWStr)] out string ppszCanonicalName
			);

		[DllImport("propsys.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern HRESULT PSGetPropertyDescription(
			ref PROPERTYKEY propkey,
			ref Guid riid,
			[Out] [MarshalAs(UnmanagedType.Interface)] out IPropertyDescription ppv
			);

		[DllImport("propsys.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern HRESULT PSGetPropertyKeyFromName(
			[In] [MarshalAs(UnmanagedType.LPWStr)] string pszCanonicalName,
			out PROPERTYKEY propkey
			);

		[DllImport("propsys.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern HRESULT PSGetPropertyDescriptionListFromString(
			[In] [MarshalAs(UnmanagedType.LPWStr)] string pszPropList,
			[In] ref Guid riid,
			out IPropertyDescriptionList ppv
			);
	}
}