using System;

// ReSharper disable InconsistentNaming

namespace starshipxac.Windows.Shell.Interop
{
    /// <summary>
    ///     ファイル操作フラグを定義します。
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/bb759795(v=vs.85).aspx
    /// </remarks>
    [Flags]
    internal enum FILEOP_FLAGS : uint
    {
        FOF_MULTIDESTFILES = 0x1,
        FOF_CONFIRMMOUSE = 0x2,
        FOF_SILENT = 0x4,
        FOF_RENAMEONCOLLISION = 0x8,
        FOF_NOCONFIRMATION = 0x10,
        FOF_WANTMAPPINGHANDLE = 0x20,
        FOF_ALLOWUNDO = 0x40,
        FOF_FILESONLY = 0x80,
        FOF_SIMPLEPROGRESS = 0x100,
        FOF_NOCONFIRMMKDIR = 0x200,
        FOF_NOERRORUI = 0x400,
        FOF_NOCOPYSECURITYATTRIBS = 0x800,
        FOF_NORECURSION = 0x1000,
        FOF_NO_CONNECTED_ELEMENTS = 0x2000,
        FOF_WANTNUKEWARNING = 0x4000,
        FOF_NORECURSEREPARSE = 0x8000
    }
}