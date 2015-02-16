using System;

namespace starshipxac.Shell.Interop
{
    /// <summary>
    /// プロパティストア取得フラグを定義します。
    /// </summary>
    /// <remarks>
    /// http://msdn.microsoft.com/en-us/library/windows/desktop/bb762582(v=vs.85).aspx
    /// </remarks>
    [Flags]
    public enum GETPROPERTYSTOREFLAGS
    {
        GPS_DEFAULT = 0,

        GPS_HANDLERPROPERTIESONLY = 0x1,

        GPS_READWRITE = 0x2,

        GPS_TEMPORARY = 0x4,

        GPS_FASTPROPERTIESONLY = 0x8,

        GPS_OPENSLOWITEM = 0x10,

        GPS_DELAYCREATION = 0x20,

        GPS_BESTEFFORT = 0x40,

        GPS_NO_OPLOCK = 0x80,

        GPS_PREFERQUERYPROPERTIES = 0x100,

        GPS_MASK_VALID = 0x1ff,

        GPS_EXTRINSICPROPERTIES = 0x00000200,

        GPS_EXTRINSICPROPERTIESONLY = 0x00000400,
    }
}