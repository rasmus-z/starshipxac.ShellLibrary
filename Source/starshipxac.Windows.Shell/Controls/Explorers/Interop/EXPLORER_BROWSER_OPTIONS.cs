using System;

namespace starshipxac.Windows.Shell.Controls.Explorers.Interop
{
	/// <summary>
	/// 
	/// </summary>
	/// <remarks>
	/// http://msdn.microsoft.com/en-us/library/windows/desktop/bb762501(v=vs.85).aspx
	/// </remarks>
	[Flags]
	internal enum EXPLORER_BROWSER_OPTIONS
	{
		EBO_NONE = 0x00000000,

		EBO_NAVIGATEONCE = 0x00000001,

		EBO_SHOWFRAMES = 0x00000002,
	
		EBO_ALWAYSNAVIGATE = 0x00000004,
		
		EBO_NOTRAVELLOG = 0x00000008,
		
		EBO_NOWRAPPERWINDOW = 0x00000010,
		
		EBO_HTMLSHAREPOINTVIEW = 0x00000020,

		EBO_NOBORDER = 0x00000040,

		EBO_NOPERSISTVIEWSTATE = 0x0000080,
	}
}