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
    /// �^�X�N�_�C�A���O�l�C�e�B�u�R���g���[���̃�������ێ����܂��B
    /// </summary>
    internal class TaskDialogControlCollectionInternal<TControl> : IDisposable, IEnumerable<TControl>
        where TControl : TaskDialogButtonBase
    {
        private bool disposed = false;

        /// <summary>
        /// �l�C�e�B�u�R���g���[���n���h�����w�肵�āA
        /// <see cref="TaskDialogControlCollectionInternal&lt;TControl&gt;"/>�N���X�̐V�����C���X�^���X�����������܂��B
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
        /// �l�C�e�B�u�R���g���[���n���h�����擾���܂��B
        /// </summary>
        internal IntPtr Handle { get; private set; }

        internal IReadOnlyList<TControl> Controls { get; private set; }

        public int Count
        {
            get
            {
                return this.Controls.Count;
            }
        }

        /// <summary>
        /// �l�C�e�B�u�R���g���[���n���h����������܂��B
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
        /// �l�C�e�B�u�{�^���z����쐬���܂��B
        /// </summary>
        /// <param name="controls">�{�^���R���g���[���̃R���N�V�����B</param>
        /// <returns>�쐬�����l�C�e�B�u�{�^���z��B</returns>
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
        /// �l�C�e�B�u�{�^���z�񃁃������m�ۂ��܂��B
        /// </summary>
        /// <param name="nativeControls">�l�C�e�B�u�{�^���z��B</param>
        /// <returns>�m�ۂ����l�C�e�B�u�{�^���z�񃁃����B</returns>
        private static IntPtr AllocateButtons(TASKDIALOG_BUTTON[] nativeControls)
        {
            Contract.Requires(nativeControls != null);

            var result = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(TASKDIALOG_BUTTON)) * nativeControls.Length);

            var pos = result;
            foreach (var button in nativeControls)
            {
                Marshal.StructureToPtr(button, pos, false);
                pos = pos + Marshal.SizeOf(button);
            }

            return result;
        }
    }
}