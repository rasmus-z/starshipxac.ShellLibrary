using System;
using System.Diagnostics.CodeAnalysis;

namespace starshipxac.Windows.Interop
{
    /// <summary>
    ///     読み込むイメージの種別を定義します。
    /// </summary>
    /// <remarks>
    ///     <code>http://msdn.microsoft.com/en-us/library/windows/desktop/ms648045(v=vs.85).aspx</code>
    /// </remarks>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal static class ImageTypes
    {
        public const UInt32 IMAGE_BITMAP = 0;
        public const UInt32 IMAGE_ICON = 1;
        public const UInt32 IMAGE_CURSOR = 2;
    }
}