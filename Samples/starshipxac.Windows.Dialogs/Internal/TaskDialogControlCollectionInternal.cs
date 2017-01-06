using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using starshipxac.Windows.Dialogs.Controls;
using starshipxac.Windows.Dialogs.Interop;

namespace starshipxac.Windows.Dialogs.Internal
{
    /// <summary>
    ///     タスクダイアログネイティブコントロールのメモリを保持します。
    /// </summary>
    internal class TaskDialogControlCollectionInternal<TControl> : IDisposable, IEnumerable<TControl>
        where TControl : TaskDialogButtonBase
    {
        private bool disposed = false;

        /// <summary>
        ///     ネイティブコントロールハンドルを指定して、
        ///     <see cref="TaskDialogControlCollectionInternal&lt;TControl&gt;" />クラスの新しいインスタンスを初期化します。
        /// </summary>
        public TaskDialogControlCollectionInternal(IReadOnlyList<TControl> controls)
        {
            Contract.Requires<ArgumentNullException>(controls != null);

            this.Controls = controls;

            var buttonArray = CreateNativeButtonArray(this.Controls);
            this.Handle = AllocateButtons(buttonArray);
        }

        ~TaskDialogControlCollectionInternal()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                }

                Release();

                this.disposed = true;
            }
        }

        /// <summary>
        ///     ネイティブコントロールハンドルを取得します。
        /// </summary>
        internal IntPtr Handle { get; private set; }

        internal IReadOnlyList<TControl> Controls { get; }

        public int Count => this.Controls.Count;

        /// <summary>
        ///     ネイティブコントロールハンドルを解放します。
        /// </summary>
        public void Release()
        {
            if (this.Handle != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(this.Handle);
                this.Handle = IntPtr.Zero;
            }
        }

        public IEnumerator<TControl> GetEnumerator()
        {
            return this.Controls.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        ///     ネイティブボタン配列を作成します。
        /// </summary>
        /// <param name="controls">ボタンコントロールのコレクション。</param>
        /// <returns>作成したネイティブボタン配列。</returns>
        private static TASKDIALOG_BUTTON[] CreateNativeButtonArray(IReadOnlyCollection<TaskDialogButtonBase> controls)
        {
            Contract.Requires<ArgumentNullException>(controls != null);

            var result = new TASKDIALOG_BUTTON[controls.Count];
            var index = 0;
            foreach (var control in controls)
            {
                result[index] = new TASKDIALOG_BUTTON(control.Id, control.GetButtonText());
                ++index;
            }

            return result;
        }

        /// <summary>
        ///     ネイティブボタン配列メモリを確保します。
        /// </summary>
        /// <param name="nativeControls">ネイティブボタン配列。</param>
        /// <returns>確保したネイティブボタン配列メモリ。</returns>
        private static IntPtr AllocateButtons(TASKDIALOG_BUTTON[] nativeControls)
        {
            Contract.Requires(nativeControls != null);

            var result = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(TASKDIALOG_BUTTON))*nativeControls.Length);

            var pos = result;
            foreach (var button in nativeControls)
            {
                Marshal.StructureToPtr(button, pos, false);
                if (IntPtr.Size == 4)
                {
                    // 32bit
                    pos = (IntPtr)((int)pos + Marshal.SizeOf(button));
                }
                else
                {
                    // 64bit
                    pos = (IntPtr)(pos.ToInt64() + Marshal.SizeOf(button));
                }
            }

            return result;
        }
    }
}