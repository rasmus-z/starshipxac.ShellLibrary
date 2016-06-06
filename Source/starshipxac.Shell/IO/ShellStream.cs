using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using starshipxac.Shell.IO.Interop;
using STATSTG = System.Runtime.InteropServices.ComTypes.STATSTG;

namespace starshipxac.Shell.IO
{
    /// <summary>
    ///     シェルストリームを定義します。
    /// </summary>
    public class ShellStream : Stream
    {
        private bool disposed = false;

        /// <summary>
        ///     <see cref="ShellStream" />クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="shellFile">ストリームで読み込みまたは書き込みを行うファイルの<see cref="ShellFile" />。</param>
        public ShellStream(ShellFile shellFile)
        {
            Contract.Requires<ArgumentNullException>(shellFile != null);

            this.ShellFile = shellFile;
            this.StreamInterface = this.ShellFile.ShellItem.GetStream();
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (!this.disposed)
                {
                    this.disposed = true;

                    // IStream解放
                    Flush();
                    Marshal.ReleaseComObject(this.StreamInterface);
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        [ContractInvariantMethod]
        private new void ObjectInvariant()
        {
            Contract.Invariant(this.ShellFile != null);
            Contract.Invariant(this.StreamInterface != null);
        }

        /// <summary>
        ///     <see cref="ShellFile" />を取得または設定します。
        /// </summary>
        private ShellFile ShellFile { get; }

        /// <summary>
        ///     <see cref="IStream" />を取得または設定します。
        /// </summary>
        private IStream StreamInterface { get; }

        /// <summary>
        ///     ストリームが読み込み可能かどうかを判定する値を取得します。
        /// </summary>
        public override bool CanRead => true;

        /// <summary>
        ///     ストリームがシーク可能かどうかを判定する値を取得または設定します。
        /// </summary>
        public override bool CanSeek => true;

        /// <summary>
        ///     ストリームが書き込み可能かどうかを判定する値を取得します。
        /// </summary>
        public override bool CanWrite => true;

        /// <summary>
        ///     ストリームのバイト長を取得します。
        /// </summary>
        public override long Length => this.Stat.cbSize;

        /// <summary>
        ///     現在のストリームの位置を取得します。
        /// </summary>
        public override long Position
        {
            get
            {
                var ptr = Marshal.AllocHGlobal(sizeof(Int32));
                try
                {
                    this.StreamInterface.Seek(0, (int)STREAM_SEEK.STREAM_SEEK_CUR, ptr);
                    var result = Marshal.ReadInt32(ptr);
                    return result;
                }
                finally
                {
                    Marshal.FreeHGlobal(ptr);
                }
            }
            set
            {
                Seek(value, SeekOrigin.Begin);
            }
        }

        /// <summary>
        ///     ストリームの統計情報を取得します。
        /// </summary>
        private STATSTG Stat
        {
            get
            {
                STATSTG result;
                this.StreamInterface.Stat(out result, 2);
                return result;
            }
        }

        public override void Flush()
        {
            //this.StreamInterface.Commit((int)STGC.STGC_DEFAULT);
        }

        /// <summary>
        ///     ストリームをシークします。
        /// </summary>
        /// <param name="offset">シークするオフセット。</param>
        /// <param name="origin">シーク開始位置。</param>
        /// <returns>変更後のシークポインターの位置。</returns>
        public override long Seek(long offset, SeekOrigin origin)
        {
            var ptr = Marshal.AllocHGlobal(sizeof(Int32));
            try
            {
                if (origin == SeekOrigin.Begin)
                {
                    this.StreamInterface.Seek(offset, (int)STREAM_SEEK.STREAM_SEEK_SET, ptr);
                }
                else if (origin == SeekOrigin.Current)
                {
                    this.StreamInterface.Seek(offset, (int)STREAM_SEEK.STREAM_SEEK_CUR, ptr);
                }
                else if (origin == SeekOrigin.End)
                {
                    this.StreamInterface.Seek(offset, (int)STREAM_SEEK.STREAM_SEEK_END, ptr);
                }

                return Marshal.ReadInt32(ptr);
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
        }

        /// <summary>
        ///     ストリームオブジェクトのサイズを設定します。
        /// </summary>
        /// <param name="value">新しいストリームオブジェクトのサイズ。</param>
        public override void SetLength(long value)
        {
            this.StreamInterface.SetSize(value);
        }

        /// <summary>
        ///     ストリームオブジェクトから指定したバイト数を読み込みます。
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public override int Read(byte[] buffer, int offset, int count)
        {
            var ptr = Marshal.AllocHGlobal(sizeof(Int32));
            try
            {
                var buff = new byte[count];
                this.StreamInterface.Read(buff, count, ptr);
                var result = Marshal.ReadInt32(ptr);
                Array.Copy(buff, 0, buffer, offset, result);
                return result;
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
        }

        /// <summary>
        ///     ストリームオブジェクトに指定したバイト数を書き込みます。
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        public override void Write(byte[] buffer, int offset, int count)
        {
            var ptr = Marshal.AllocHGlobal(sizeof(Int32));
            try
            {
                var buff = new byte[count];
                Array.Copy(buffer, offset, buff, 0, count);
                this.StreamInterface.Write(buff, count, IntPtr.Zero);
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
        }
    }
}