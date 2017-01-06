using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace starshipxac.Windows.Dialogs.Interop
{
    [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal struct TASKDIALOGCONFIG_ICON
    {
        internal TASKDIALOGCONFIG_ICON(int iconHandle)
        {
            this.iconHandle = iconHandle;
            this.spacer = IntPtr.Zero;
        }

        [FieldOffset(0)]
        public readonly int iconHandle;

        [FieldOffset(0)]
        public readonly IntPtr spacer;

        /// <summary>
        ///     アイコンハンドルを取得します。
        /// </summary>
        public int IconHandle
        {
            get
            {
                return this.iconHandle;
            }
        }
    }
}