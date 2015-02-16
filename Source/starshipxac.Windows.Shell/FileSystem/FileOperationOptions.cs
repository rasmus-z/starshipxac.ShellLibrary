using System;
using starshipxac.Windows.Shell.Interop;

namespace starshipxac.Windows.Shell.FileSystem
{
    /// <summary>
    /// <para>
    /// ファイル操作のオプションを定義します。
    /// </para>
    /// </summary>
    [Flags]
    public enum FileOperationOptions : uint
    {
        /// <summary>
        /// 何も指定しません。
        /// </summary>
        None = 0,

        /// <summary>
        /// やり直しを実行できるようにします。
        /// </summary>
        ArrowUndo = FILEOP_FLAGS.FOF_ALLOWUNDO,

        /// <summary>
        /// 
        /// </summary>
        FilesOnly = FILEOP_FLAGS.FOF_FILESONLY,

        /// <summary>
        /// 
        /// </summary>
        MultiDestFiles = FILEOP_FLAGS.FOF_MULTIDESTFILES,

        /// <summary>
        /// 
        /// </summary>
        NoConfirmation = FILEOP_FLAGS.FOF_NOCONFIRMATION,

        /// <summary>
        /// 新しいダイアログの作成が必要な場合に、確認メッセージを表示しません。
        /// </summary>
        NoConfirmMakeDirectory = FILEOP_FLAGS.FOF_NOCONFIRMMKDIR,

        /// <summary>
        /// 
        /// </summary>
        NoConnectedElements = FILEOP_FLAGS.FOF_NO_CONNECTED_ELEMENTS,

        /// <summary>
        /// ファイルのセキュリティ属性情報をコピーしません。
        /// </summary>
        NoCopySecurityAttribute = FILEOP_FLAGS.FOF_NOCOPYSECURITYATTRIBS,

        /// <summary>
        /// エラーが発生した場合に、メッセージを表示しません。
        /// </summary>
        NoErrorUI = FILEOP_FLAGS.FOF_NOERRORUI,

        /// <summary>
        /// 
        /// </summary>
        NoRecursion = FILEOP_FLAGS.FOF_NORECURSION,

        /// <summary>
        /// 
        /// </summary>
        RenameOnCollision = FILEOP_FLAGS.FOF_RENAMEONCOLLISION,

        /// <summary>
        /// 進捗ダイアログを表示しません。
        /// </summary>
        Silent = FILEOP_FLAGS.FOF_SILENT,

        /// <summary>
        /// ファイル名を表示しない進捗ダイアログを表示します。
        /// </summary>
        SimpleProgress = FILEOP_FLAGS.FOF_SIMPLEPROGRESS,

        /// <summary>
        /// 
        /// </summary>
        WantNukeWarning = FILEOP_FLAGS.FOF_WANTNUKEWARNING,
    }
}