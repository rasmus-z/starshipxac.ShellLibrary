using System;
using System.Runtime.InteropServices;
using starshipxac.Windows.Dialogs.Interop;

namespace starshipxac.Windows.Dialogs.Internal
{
    internal sealed class TaskDialogTextElements
    {
        private readonly IntPtr[] elementTexts;

        public TaskDialogTextElements()
        {
            this.elementTexts = new IntPtr[Enum.GetNames(typeof(TASKDIALOG_ELEMENTS)).Length];
        }

        internal IntPtr CreateText(TASKDIALOG_ELEMENTS element, string text)
        {
            var ptr = Marshal.StringToHGlobalUni(text);
            this.elementTexts[(int)element] = ptr;
            return ptr;
        }

        internal void FreeText(TASKDIALOG_ELEMENTS element)
        {
            var index = (int)element;
            Marshal.FreeHGlobal(this.elementTexts[index]);
            this.elementTexts[index] = IntPtr.Zero;
        }

        internal void FreeAllText()
        {
            for (var index = 0; index < this.elementTexts.Length; ++index)
            {
                if (this.elementTexts[index] != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(this.elementTexts[index]);
                    this.elementTexts[index] = IntPtr.Zero;
                }
            }
        }
    }
}