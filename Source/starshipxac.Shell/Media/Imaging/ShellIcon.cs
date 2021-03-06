﻿using System;
using System.Diagnostics.Contracts;
using starshipxac.Shell.Interop;

namespace starshipxac.Shell.Media.Imaging
{
    /// <summary>
    ///     Define shell icon.
    /// </summary>
    public sealed class ShellIcon : IDisposable
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

        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                }

                // Release unmanaged resources.
                Win32Api.DestroyIcon(this.Handle);
                this.Handle = IntPtr.Zero;

                this.disposed = true;
            }
        }

        /// <summary>
        ///     Get the icon handle.
        /// </summary>
        internal IntPtr Handle { get; private set; }
    }
}