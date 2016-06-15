using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using starshipxac.Shell.Interop;

namespace starshipxac.Shell
{
    /// <summary>
    ///     <c>Shell</c>例外クラスを定義します。
    /// </summary>
    [Serializable]
    public class ShellException : ExternalException
    {
        /// <summary>
        ///     <see cref="ShellException" />クラスの新しいインスタンスを初期化します。
        /// </summary>
        public ShellException()
        {
        }

        /// <summary>
        ///     メッセージを指定して、<see cref="ShellException" />クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="message">メッセージ。</param>
        public ShellException(string message)
            : base(message)
        {
        }

        /// <summary>
        ///     メッセージと<see cref="Exception" />を指定して、<see cref="ShellException" />クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="message">メッセージ。</param>
        /// <param name="innerException">以前に発生した<see cref="Exception" />。</param>
        public ShellException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        ///     メッセージとエラーコードを指定して、<see cref="ShellException" />クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="message">メッセージ。</param>
        /// <param name="errorCode">エラーコード。</param>
        public ShellException(string message, int errorCode)
            : base(message, errorCode)
        {
        }

        /// <summary>
        ///     メッセージと<see cref="HRESULT" />を指定して、<see cref="ShellException" />クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="message">メッセージ。</param>
        /// <param name="errorCode"><see cref="HRESULT" />エラーコード。</param>
        internal ShellException(string message, HRESULT errorCode)
            : this(message, (int)errorCode)
        {
        }

        /// <summary>
        ///     シリアル化したデータを指定して、
        ///     <see cref="ShellException" />クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="info"><see cref="SerializationInfo" />。</param>
        /// <param name="context"><see cref="StreamingContext" />。</param>
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