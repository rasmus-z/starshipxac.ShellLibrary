using System;

namespace starshipxac.Shell.Interop.Library
{
    /// <summary>
    /// ライブラリフォルダーフィルターを定義します。
    /// </summary>
    /// <remarks>
    /// http://msdn.microsoft.com/en-us/library/windows/desktop/dd378448(v=vs.85).aspx
    /// </remarks>
    internal enum LIBRARYFOLDERFILTER
    {
        LFF_FORCEFILESYSTEM = 1,
        LFF_STORAGEITEMS = 2,
        LFF_ALLITEMS = 3
    };
}