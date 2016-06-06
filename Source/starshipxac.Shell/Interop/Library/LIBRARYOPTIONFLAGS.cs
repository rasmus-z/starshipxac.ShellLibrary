using System;
using System.Diagnostics.CodeAnalysis;

namespace starshipxac.Shell.Interop.Library
{
    /// <summary>
    ///     ライブラリオプションフラグを定義します。
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/dd378450(v=vs.85).aspx
    /// </remarks>
    [Flags]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal enum LIBRARYOPTIONFLAGS
    {
        LOF_DEFAULT = 0,
        LOF_PINNEDTONAVPANE = 0x1,
        LOF_MASK_ALL = 0x1
    };
}