using System;

namespace starshipxac.Windows.Shell.Controls.Explorers.Interop
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// http://msdn.microsoft.com/en-us/library/windows/desktop/bb762510(v=vs.85).aspx
    /// </remarks>
    public enum FOLDERVIEWMODE
    {
        FVM_AUTO = -1,
        FVM_FIRST = 1,
        FVM_ICON = 1,
        FVM_SMALLICON = 2,
        FVM_LIST = 3,
        FVM_DETAILS = 4,
        FVM_THUMBNAIL = 5,
        FVM_TILE = 6,
        FVM_THUMBSTRIP = 7,
        FVM_CONTENT = 8,
        FVM_LAST = 8
    }
}