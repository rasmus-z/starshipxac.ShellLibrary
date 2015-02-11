using System;

namespace starshipxac.Shell.PropertySystem.Interop
{
	/// <summary>
	/// 
	/// </summary>
	/// <remarks>
	/// http://msdn.microsoft.com/en-us/library/windows/desktop/bb762527(v=vs.85).aspx
	/// </remarks>
	internal enum PROPDESC_TYPE_FLAGS
	{
		PDTF_DEFAULT = 0,
		PDTF_MULTIPLEVALUES = 0x1,
		PDTF_ISINNATE = 0x2,
		PDTF_ISGROUP = 0x4,
		PDTF_CANGROUPBY = 0x8,
		PDTF_CANSTACKBY = 0x10,
		PDTF_ISTREEPROPERTY = 0x20,
		PDTF_INCLUDEINFULLTEXTQUERY = 0x40,
		PDTF_ISVIEWABLE = 0x80,
		PDTF_ISQUERYABLE = 0x100,
		PDTF_CANBEPURGED = 0x200,
		PDTF_SEARCHRAWVALUE = 0x400,
		PDTF_ISSYSTEMPROPERTY = unchecked((int)0x80000000),
		PDTF_MASK_ALL = unchecked((int)0x800007ff),
	}
}