﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using starshipxac.Shell;
using starshipxac.Shell.Interop;
using starshipxac.Windows.Shell.FileSystem.Internal;
using starshipxac.Windows.Shell.Interop;

namespace starshipxac.Windows.Shell.FileSystem
{
    /// <summary>
    ///     <para>
    ///         ファイルのコピー、移動、削除の操作を行います。
    ///     </para>
    /// </summary>
    public class FileOperation : IDisposable
    {
        private bool disposed = false;

        private IFileOperation fileOperation;
        private FileOperationProgressSink progressSink;
        private readonly UInt32 cookie;

        private FileOperationOptions operationOptions = FileOperationOptions.NoConfirmMakeDirectory |
                                                        FileOperationOptions.ArrowUndo;

        private static readonly Type FileOperationType = Type.GetTypeFromCLSID(new Guid(ShellCLSID.FileOperation));

        [SuppressMessage("ReSharper", "InconsistentNaming")]
        private static readonly HRESULT COPYENGINE_E_USER_CANCELLED = (HRESULT)0x80270000;

        /// <summary>
        ///     <see cref="FileOperation" />クラスの新しいインスタンスを初期化します。
        /// </summary>
        public FileOperation()
            : this(null, null)
        {
        }

        /// <summary>
        ///     進捗通知クラスを指定して、
        ///     <see cref="FileOperation" />クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="progress">進捗通知クラスのインスタンス。</param>
        public FileOperation(FileOperationProgress progress)
            : this(progress, null)
        {
        }

        /// <summary>
        ///     オーナーウィンドウを指定して、
        ///     <see cref="FileOperation" />クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="ownerWindow">オーナーウィンドウ</param>
        public FileOperation(Window ownerWindow)
            : this(null, ownerWindow)
        {
        }

        /// <summary>
        ///     進捗通知クラスとオーナーウィンドウを指定して、
        ///     <see cref="FileOperation" />クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="progress">進捗通知クラス。</param>
        /// <param name="ownerWindow">オーナーウィンドウ。</param>
        public FileOperation(FileOperationProgress progress, Window ownerWindow)
        {
            this.fileOperation = (IFileOperation)Activator.CreateInstance(FileOperationType);

            if (progress != null)
            {
                this.progressSink = new FileOperationProgressSink(progress);
                this.cookie = this.fileOperation.Advise(this.progressSink);
            }

            if (ownerWindow != null)
            {
                var windowHelper = new WindowInteropHelper(ownerWindow);
                this.fileOperation.SetOwnerWindow(windowHelper.Handle);
            }
        }

        /// <summary>
        ///     Finalizer.
        /// </summary>
        ~FileOperation()
        {
            Dispose(false);
        }

        /// <summary>
        ///     Release all resources used by <see cref="FileOperation" /> class.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Release all resources used by <see cref="FileOperation" /> class
        ///     and optionally releases managed resources.
        /// </summary>
        /// <param name="disposing">
        ///     <c>true</c> to release both managed and unmanaged resources.
        ///     <c>false</c> to release only unmanaged resources.
        /// </param>
        public void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                }

                // Release unmanaged resources.
                if (this.progressSink != null)
                {
                    this.fileOperation.Unadvise(this.cookie);
                    this.progressSink = null;
                }

                Marshal.FinalReleaseComObject(this.fileOperation);
                this.fileOperation = null;

                this.disposed = true;
            }
        }

        /// <summary>
        ///     ファイル操作オプションを取得または設定します。
        /// </summary>
        public FileOperationOptions OperationOptions
        {
            get
            {
                return this.operationOptions;
            }
            set
            {
                ThrowIfDisposed();
                this.operationOptions = value;
                this.fileOperation.SetOperationFlags((UInt32)this.operationOptions);
            }
        }

        /// <summary>
        ///     ファイル操作が中止されたかどうかを判定する値を取得します。
        /// </summary>
        public bool AnyOperationsAborted
        {
            get
            {
                ThrowIfDisposed();
                return this.fileOperation.GetAnyOperationsAborted();
            }
        }

        #region Copy

        /// <summary>
        ///     <paramref name="source" />で指定したファイルまたはフォルダーを
        ///     <paramref name="destination" />で指定したフォルダーにコピーします。
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        public void Copy(string source, string destination)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(source));
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(destination));
            ThrowIfDisposed();

            using (var sourceItem = ShellItem.FromParsingName(source))
            using (var destinationItem = ShellItem.FromParsingName(destination))
            {
                this.fileOperation.CopyItem(
                    sourceItem.ShellItemInterface,
                    destinationItem.ShellItemInterface,
                    Path.GetFileName(sourceItem.GetParsingName()),
                    null);
            }
        }

        /// <summary>
        ///     <paramref name="source" />で指定したファイルまたはフォルダーを
        ///     <paramref name="destination" />で指定したフォルダーにコピーします。
        ///     <para>
        ///         コピー先のファイルまたはフォルダー名を<paramref name="copyName" />に変更します。
        ///     </para>
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="copyName"></param>
        public void Copy(string source, string destination, string copyName)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(source));
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(destination));
            ThrowIfDisposed();

            using (var sourceItem = ShellItem.FromParsingName(source))
            using (var destinationItem = ShellItem.FromParsingName(destination))
            {
                this.fileOperation.CopyItem(
                    sourceItem.ShellItemInterface,
                    destinationItem.ShellItemInterface,
                    copyName,
                    null);
            }
        }

        /// <summary>
        ///     <paramref name="sourceFile" />で指定したファイルを
        ///     <paramref name="destinationFile" />で指定したファイルにコピーします。
        /// </summary>
        /// <param name="sourceFile"></param>
        /// <param name="destinationFile"></param>
        public void Copy(ShellFile sourceFile, ShellFile destinationFile)
        {
            Contract.Requires<ArgumentNullException>(sourceFile != null);
            Contract.Requires<ArgumentNullException>(destinationFile != null);
            ThrowIfDisposed();

            this.fileOperation.CopyItem(sourceFile.ShellItem.ShellItemInterface,
                destinationFile.Folder.ShellItem.ShellItemInterface, destinationFile.Name,
                null);
        }

        /// <summary>
        ///     <paramref name="sourceFile" />で指定したファイルを
        ///     <paramref name="destinationFolder" />で指定したフォルダーにコピーします。
        /// </summary>
        /// <param name="sourceFile"></param>
        /// <param name="destinationFolder"></param>
        public void Copy(ShellFile sourceFile, ShellFolder destinationFolder)
        {
            Contract.Requires<ArgumentNullException>(sourceFile != null);
            Contract.Requires<ArgumentNullException>(destinationFolder != null);
            ThrowIfDisposed();

            var copyName = sourceFile.Name;
            this.fileOperation.CopyItem(sourceFile.ShellItem.ShellItemInterface,
                destinationFolder.ShellItem.ShellItemInterface, copyName,
                null);
        }

        /// <summary>
        ///     <paramref name="sourceFile" />で指定したファイルを
        ///     <paramref name="destinationFolder" />で指定したフォルダーにコピーします。
        ///     <para>
        ///         ファイル名を<see cref="copyName" />に変更します。
        ///     </para>
        /// </summary>
        /// <param name="sourceFile"></param>
        /// <param name="destinationFolder"></param>
        /// <param name="copyName"></param>
        public void Copy(ShellFile sourceFile, ShellFolder destinationFolder, string copyName)
        {
            Contract.Requires<ArgumentNullException>(sourceFile != null);
            Contract.Requires<ArgumentNullException>(destinationFolder != null);
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(copyName));
            ThrowIfDisposed();

            this.fileOperation.CopyItem(sourceFile.ShellItem.ShellItemInterface,
                destinationFolder.ShellItem.ShellItemInterface, copyName,
                null);
        }

        #endregion

        #region Move

        public void Move(string source, string destination)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(source));
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(destination));
            ThrowIfDisposed();

            using (var sourceItem = ShellItem.FromParsingName(source))
            using (var destinationItem = ShellItem.FromParsingName(destination))
            {
                this.fileOperation.MoveItem(
                    sourceItem.ShellItemInterface,
                    destinationItem.ShellItemInterface,
                    Path.GetFileName(sourceItem.GetParsingName()),
                    null);
            }
        }

        public void Move(string source, string destination, string newName)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(source));
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(destination));
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(newName));
            ThrowIfDisposed();

            using (var sourceItem = ShellItem.FromParsingName(source))
            using (var destinationItem = ShellItem.FromParsingName(destination))
            {
                this.fileOperation.MoveItem(
                    sourceItem.ShellItemInterface,
                    destinationItem.ShellItemInterface,
                    newName,
                    null);
            }
        }

        public void Move(ShellFile sourceFile, ShellFolder destinationFolder)
        {
            Contract.Requires<ArgumentNullException>(sourceFile != null);
            Contract.Requires<ArgumentNullException>(destinationFolder != null);
            ThrowIfDisposed();

            this.fileOperation.MoveItem(
                sourceFile.ShellItem.ShellItemInterface,
                destinationFolder.ShellItem.ShellItemInterface,
                sourceFile.Name,
                null);
        }

        public void Move(ShellFile sourceFile, ShellFolder destinationFolder, string newName)
        {
            Contract.Requires<ArgumentNullException>(sourceFile != null);
            Contract.Requires<ArgumentNullException>(destinationFolder != null);
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(newName));
            ThrowIfDisposed();

            this.fileOperation.MoveItem(
                sourceFile.ShellItem.ShellItemInterface,
                destinationFolder.ShellItem.ShellItemInterface,
                newName,
                null);
        }

        #endregion

        #region Delete

        public void Delete(string path)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(path));
            ThrowIfDisposed();

            using (var shellItem = ShellItem.FromParsingName(path))
            {
                this.fileOperation.DeleteItem(shellItem.ShellItemInterface, null);
            }
        }

        public void Delete(ShellObject shellObject)
        {
            Contract.Requires<ArgumentNullException>(shellObject != null);
            ThrowIfDisposed();

            this.fileOperation.DeleteItem(shellObject.ShellItem.ShellItemInterface, null);
        }

        #endregion

        #region Rename

        public void Rename(string path, string newName)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(path));
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(newName));
            ThrowIfDisposed();

            using (var shellItem = ShellItem.FromParsingName(path))
            {
                this.fileOperation.RenameItem(shellItem.ShellItemInterface, newName, null);
            }
        }

        public void Rename(ShellObject shellObject, string newName)
        {
            Contract.Requires<ArgumentNullException>(shellObject != null);
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(newName));
            ThrowIfDisposed();

            this.fileOperation.RenameItem(shellObject.ShellItem.ShellItemInterface, newName, null);
        }

        #endregion

        #region New

        public void New(string destination, FileAttributes fileAttributes, string newName, string templateName)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(destination));
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(newName));
            ThrowIfDisposed();

            using (var destinationItem = ShellItem.FromParsingName(destination))
            {
                this.fileOperation.NewItem(
                    destinationItem.ShellItemInterface,
                    fileAttributes,
                    newName,
                    templateName,
                    null);
            }
        }

        public void New(ShellFolder destinationContainer, FileAttributes fileAttributes, string newName, string templateName)
        {
            Contract.Requires<ArgumentNullException>(destinationContainer != null);
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(newName));
            ThrowIfDisposed();

            this.fileOperation.NewItem(
                destinationContainer.ShellItem.ShellItemInterface,
                fileAttributes,
                newName,
                templateName,
                null);
        }

        public void CreateFolder(string destination, string newFolderName)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(destination));
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(newFolderName));
            ThrowIfDisposed();

            using (var destinationItem = ShellItem.FromParsingName(destination))
            {
                this.fileOperation.NewItem(
                    destinationItem.ShellItemInterface,
                    FileAttributes.Directory,
                    newFolderName,
                    null,
                    null);
            }
        }

        public void CreateFolder(ShellFolder destinationFolder, string newFolderName)
        {
            Contract.Requires<ArgumentNullException>(destinationFolder != null);
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(newFolderName));
            ThrowIfDisposed();

            this.fileOperation.NewItem(
                destinationFolder.ShellItem.ShellItemInterface,
                FileAttributes.Directory,
                newFolderName,
                null,
                null);
        }

        #endregion

        /// <summary>
        ///     設定したファイル操作を実行します。
        /// </summary>
        /// <returns></returns>
        public bool PerformOperations()
        {
            ThrowIfDisposed();

            try
            {
                this.fileOperation.PerformOperations();
            }
            catch (COMException ex)
            {
                if (ex.ErrorCode == COPYENGINE_E_USER_CANCELLED)
                {
                    // キャンセル
                    return false;
                }
                else
                {
                    throw FileOperationException.FromCOMException(ex);
                }
            }

            return true;
        }

        #region Private Methods

        private void ThrowIfDisposed()
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        #endregion
    }
}