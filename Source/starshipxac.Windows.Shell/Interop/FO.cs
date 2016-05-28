using System;

// ReSharper disable InconsistentNaming

namespace starshipxac.Windows.Shell.Interop
{
    /// <summary>
    ///     ファイル操作を定義します。
    /// </summary>
    /// <remarks>
    ///     http://msdn.microsoft.com/en-us/library/windows/desktop/bb759795(v=vs.85).aspx
    /// </remarks>
    internal static class FO
    {
        /// <summary>
        ///     ファイル移動。
        /// </summary>
        public const UInt32 FO_MOVE = 0x01;

        /// <summary>
        ///     ファイルコピー。
        /// </summary>
        public const UInt32 FO_COPY = 0x02;

        /// <summary>
        ///     ファイル削除。
        /// </summary>
        public const UInt32 FO_DELETE = 0x03;

        /// <summary>
        ///     ファイル名変更。
        /// </summary>
        public const UInt32 FO_RENAME = 0x04;
    }
}