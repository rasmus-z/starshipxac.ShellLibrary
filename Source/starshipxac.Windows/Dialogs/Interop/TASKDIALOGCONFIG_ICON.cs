using System;
using System.Runtime.InteropServices;

namespace starshipxac.Windows.Dialogs.Interop
{
    [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
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
        /// �A�C�R���n���h�����擾���܂��B
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