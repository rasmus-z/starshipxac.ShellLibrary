using System;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;

namespace starshipxac.Shell.Media.Imaging
{
    /// <summary>
    ///     シェルアイコンを定義します。
    /// </summary>
    public class ShellIcon : IDisposable
    {
        private bool disposed = false;

        internal ShellIcon(IntPtr hIcon)
        {
            Contract.Requires(hIcon != IntPtr.Zero);

            this.Handle = hIcon;
        }

        ~ShellIcon()
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
                DestroyIcon(this.Handle);

                this.disposed = true;
            }
        }

        /// <summary>
        ///     アイコンハンドルを取得します。
        /// </summary>
        internal IntPtr Handle { get; }

        #region Native Methods

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool DestroyIcon(IntPtr hIcon);

        #endregion
    }
}