using System;
using System.Diagnostics.CodeAnalysis;

namespace starshipxac.Windows.Shell.Controls.Interop
{
    /// <summary>
    /// Windowsコントロールの<c>GUID</c>を定義します。
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal class ControlGuid
    {
        /// <summary>
        /// イメージリスト<c>GUID</c>。
        /// </summary>
        public static readonly Guid IID_IImageList = new Guid(ControlIID.IImageList);
    }
}