using System;

namespace starshipxac.Shell.Interop
{
    /// <summary>
    /// アイテム列挙フラグを定義します。
    /// </summary>
    /// <remarks>
    /// http://msdn.microsoft.com/en-us/library/windows/desktop/bb762539(v=vs.85).aspx
    /// </remarks>
    [Flags]
    internal enum SHCONTF : uint
    {
        SHCONTF_CHECKING_FOR_CHILDREN = 0x0010,
        SHCONTF_FOLDERS = 0x0020,
        SHCONTF_NONFOLDERS = 0x0040,
        SHCONTF_INCLUDEHIDDEN = 0x0080,
        SHCONTF_INIT_ON_FIRST_NEXT = 0x0100,
        SHCONTF_NETPRINTERSRCH = 0x0200,
        SHCONTF_SHAREABLE = 0x0400,
        SHCONTF_STORAGE = 0x0800,
        SHCONTF_NAVIGATION_ENUM = 0x1000,
        SHCONTF_FASTITEMS = 0x2000,
        SHCONTF_FLATLIST = 0x4000,
        SHCONTF_ENABLE_ASYNC = 0x8000,
        SHCONTF_INCLUDESUPERHIDDEN = 0x10000
    }
}