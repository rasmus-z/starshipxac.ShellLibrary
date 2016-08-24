﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using starshipxac.Shell.Interop;

namespace starshipxac.Shell.Media.Imaging
{
    /// <summary>
    ///     <c>ShellObject</c>のサムネイルイメージを作成するための情報を保持します。
    /// </summary>
    public sealed class ShellThumbnail : IDisposable
    {
        private bool disposed = false;

        internal ShellThumbnail(ShellItem shellItem, IntPtr imageHandle, double width, double height)
        {
            Contract.Requires<ArgumentNullException>(shellItem != null);
            Contract.Requires<ArgumentOutOfRangeException>(0.0 <= width);
            Contract.Requires<ArgumentOutOfRangeException>(0.0 <= height);

            this.OriginalWidth = width;
            this.OriginalHeight = height;
            this.ImageHandle = imageHandle;

            var sfi = new SHFILEINFO();
            const uint flags = SHGFI.SHGFI_PIDL |
                               SHGFI.SHGFI_ICON |
                               SHGFI.SHGFI_SYSICONINDEX |
                               SHGFI.SHGFI_OVERLAYINDEX;
            ShellNativeMethods.SHGetFileInfo(shellItem.PIDL, 0, ref sfi, flags);

            this.IconIndex = sfi.iIcon & 0x00FFFFFF;
            this.OverlayIndex = (sfi.iIcon >> 24) & 0xFF;
        }

        ~ShellThumbnail()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        [SuppressMessage("ReSharper", "UnusedParameter.Local")]
        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                Win32Api.DeleteObject(this.ImageHandle);

                this.disposed = true;
            }
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(0.0 <= this.OriginalWidth);
            Contract.Invariant(0.0 <= this.OriginalHeight);
        }

        /// <summary>
        ///     イメージハンドルを取得します。
        /// </summary>
        internal IntPtr ImageHandle { get; }

        /// <summary>
        ///     イメージの幅を取得します。
        /// </summary>
        public double OriginalWidth { get; }

        /// <summary>
        ///     イメージの高さを取得します。
        /// </summary>
        public double OriginalHeight { get; }

        /// <summary>
        ///     アイコンインデックスを取得します。
        /// </summary>
        internal int IconIndex { get; }

        /// <summary>
        ///     オーバーレイアイコンインデックスを取得します。
        /// </summary>
        internal int OverlayIndex { get; }
    }
}