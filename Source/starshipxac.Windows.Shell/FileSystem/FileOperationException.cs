using System;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace starshipxac.Windows.Shell.FileSystem
{
    /// <summary>
    ///     <para>
    ///         ファイル操作中に発生するエラーを表します。
    ///     </para>
    /// </summary>
    [Serializable]
    public class FileOperationException : Exception
    {
        /// <summary>
        ///     <para>
        ///         エラーメッセージを指定して、
        ///         <see cref="FileOperationException" />クラスの新しいインスタンスを初期化します。
        ///     </para>
        /// </summary>
        /// <param name="message">エラーメッセージ。</param>
        public FileOperationException(string message)
            : base(message)
        {
        }

        /// <summary>
        ///     <para>
        ///         エラーメッセージと、この例外の原因である内部発生への参照を指定して、
        ///         <see cref="FileOperationException" />クラスの新しいインスタンスを初期化します。
        ///     </para>
        /// </summary>
        /// <param name="message">エラーメッセージ。</param>
        /// <param name="innerException">この例外の原因である内部発生への参照。</param>
        public FileOperationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        ///     シリアル化したデータを使用して、
        ///     <see cref="FileOperationException" />クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public FileOperationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        ///     COM例外を指定して、<see cref="FileOperationException" />クラスのインスタンスを作成します。
        /// </summary>
        /// <param name="exception">COM例外。</param>
        /// <returns>作成した<see cref="FileOperationException" />クラスのインスタンス。</returns>
        public static FileOperationException FromCOMException(COMException exception)
        {
            Contract.Requires<ArgumentNullException>(exception != null);

            return new FileOperationException(exception.Message, exception);
        }
    }
}