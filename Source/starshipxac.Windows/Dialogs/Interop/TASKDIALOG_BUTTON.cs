using System;
using System.Runtime.InteropServices;

namespace starshipxac.Windows.Dialogs.Interop
{
    /// <summary>
    /// タスクダイアログボタン構造体を定義します。
    /// </summary>
    /// <remarks>
    /// http://msdn.microsoft.com/en-us/library/windows/desktop/bb787475(v=vs.85).aspx
    /// </remarks>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 4)]
    internal struct TASKDIALOG_BUTTON
    {
        public TASKDIALOG_BUTTON(int buttonId, string text)
        {
            this.buttonId = buttonId;
            this.buttonText = text;
        }

        public int buttonId;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string buttonText;
    }
}