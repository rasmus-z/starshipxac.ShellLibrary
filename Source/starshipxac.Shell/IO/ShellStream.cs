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
    ///     Define shell stream class.
    /// </summary>
    public class ShellStream : Stream
    {
        private bool disposed = false;

        /// <summary>
        ///     Initialize a instance of the <see cref="ShellStream" /> class.
        /// </summary>
        /// <param name="shellFile"><See cref="ShellFile" /> in the file to read or write in the stream.</param>
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
                    // Release IStream
                    Flush();
                    Marshal.ReleaseComObject(this.StreamInterface);

                    this.disposed = true;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        /// <summary>
        ///     Get the <see cref="ShellFile" />.
        /// </summary>
        private ShellFile ShellFile { get; }

        /// <summary>
        ///     Get the <see cref="IStream" />.
        /// </summary>
        private IStream StreamInterface { get; }

        /// <summary>
        ///     Get a value that determines if the stream is readable.
        /// </summary>
        public override bool CanRead => true;

        /// <summary>
        ///     Get or sets a value that determines if the stream is seekable.
        /// </summary>
        public override bool CanSeek => true;

        /// <summary>
        ///     Get a value that determines if the stream is writable.
        /// </summary>
        public override bool CanWrite => true;

        /// <summary>
        ///     Get the byte length of the stream.
        /// </summary>
        public override long Length => this.Stat.cbSize;

        /// <summary>
        ///     Get the position of the current stream.
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
        ///     Get the stream statistics.
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
        ///     Seek the stream.
        /// </summary>
        /// <param name="offset">Offset to seek.</param>
        /// <param name="origin">The seek start position.</param>
        /// <returns>The position of the seek pointer after the change.</returns>
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
        ///     Set the size of the stream object.
        /// </summary>
        /// <param name="value">The size of the new stream object.</param>
        public override void SetLength(long value)
        {
            this.StreamInterface.SetSize(value);
        }

        /// <summary>
        ///     Reads the specified number of bytes from the stream object.
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
        ///     Writes the specified number of bytes to the stream object.
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