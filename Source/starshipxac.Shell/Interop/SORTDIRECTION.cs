using System;
using System.Diagnostics.CodeAnalysis;

namespace starshipxac.Shell.Interop
{
    /// <summary>
    ///     ソート方向を定義します。
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/bb759817(v=vs.85).aspx
    /// </remarks>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum SORTDIRECTION
    {
        SORT_DESCENDING = -1,
        SORT_ASCENDING = 1
    }
}