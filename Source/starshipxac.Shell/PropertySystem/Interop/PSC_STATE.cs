using System;

namespace starshipxac.Shell.PropertySystem.Interop
{
    /// <summary>
    ///プロパティストアキャッシュ状態を定義します。
    /// </summary>
    /// <remarks>
    /// http://msdn.microsoft.com/en-us/library/windows/desktop/bb762531(v=vs.85).aspx
    /// </remarks>
    internal enum PSC_STATE
    {
        PSC_NORMAL = 0,
        PSC_NOTINSOURCE = 1,
        PSC_DIRTY = 2,
        PSC_READONLY = 3
    }
}