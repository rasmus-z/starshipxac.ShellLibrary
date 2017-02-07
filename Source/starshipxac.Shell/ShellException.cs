using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using starshipxac.Shell.Interop;

namespace starshipxac.Shell
{
    /// <summary>
    ///     Define <c>Shell</c> exception class.
    /// </summary>
    [Serializable]
    public class ShellException : ExternalException
    {
        /// <summary>
        ///     Initialize a new instance of the <see cref="ShellException" /> class.
        /// </summary>
        public ShellException()
        {
        }

        /// <summary>
        ///     Initialize a new instance of the <see cref="ShellException" /> class
        ///     to the specified message.
        /// </summary>
        /// <param name="message">Message.</param>
        public ShellException(string message)
            : base(message)
        {
        }

        /// <summary>
        ///     Initialize a new instance of the <see cref="ShellException" /> class
        ///     to the specified message and internal <see cref="Exception"/>.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="innerException"><see cref="Exception"/> that occurred before.</param>
        public ShellException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        ///     Initialize a new instance of the <see cref="ShellException" /> class
        ///     to the specified message and error code.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="errorCode">Error code.</param>
        public ShellException(string message, int errorCode)
            : base(message, errorCode)
        {
        }

        /// <summary>
        ///     Initialize a new instance of the <see cref="ShellException" /> class
        ///     to the specified message and <see cref="HRESULT"/>.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="errorCode"><see cref="HRESULT" /> Error code.</param>
        internal ShellException(string message, HRESULT errorCode)
            : this(message, (int)errorCode)
        {
        }

        /// <summary>
        ///     Initialize a new instance of the <see cref="ShellException" /> class
        ///     to the specified serialize data.
        /// </summary>
        /// <param name="info"><see cref="SerializationInfo" />.</param>
        /// <param name="context"><see cref="StreamingContext" />.</param>
        protected ShellException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        [SuppressMessage("ReSharper", "InconsistentNaming")]
        internal static Exception FromHRESULT(HRESULT hr)
        {
            var exception = Marshal.GetExceptionForHR(hr);
            return new ShellException(exception.Message, exception);
        }
    }
}