using System;
using System.Diagnostics.CodeAnalysis;

namespace starshipxac.Shell.Interop.Library
{
    /// <summary>
    ///     ライブラリ規定保存フォルダー種別を定義します。
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/dd378443(v=vs.85).aspx
    /// </remarks>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal enum DEFAULTSAVEFOLDERTYPE
    {
        DSFT_DETECT = 1,
        DSFT_PRIVATE = 2,
        DSFT_PUBLIC = 3
    };
}