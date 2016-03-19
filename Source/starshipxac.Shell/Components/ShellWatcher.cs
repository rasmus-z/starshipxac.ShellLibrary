using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using starshipxac.Shell.Components.Internal;
using starshipxac.Shell.Components.Interop;
using starshipxac.Shell.Internal;
using starshipxac.Shell.Properties;

namespace starshipxac.Shell.Components
{
    /// <summary>
    ///     シェル変更を監視します。
    /// </summary>
    public class ShellWatcher : IDisposable
    {
        private bool disposed = false;

        private readonly ShellObject shellObject;
        private readonly bool recursive;

        private readonly ShellChangeEventManager eventManager = new ShellChangeEventManager();
        private uint registrationId;

        /// <summary>
        ///     <see cref="ShellWatcher" />クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="shellObject">監視する<see cref="ShellObject" />。</param>
        /// <param name="window">通知ウィンドウ。</param>
        /// <param name="message">通知ウィンドウメッセージ。</param>
        /// <param name="recursive">再帰的に監視するかどうかを示すフラグ。</param>
        internal ShellWatcher(ShellObject shellObject, WindowSource window, uint message, bool recursive)
        {
            Contract.Requires<ArgumentNullException>(shellObject != null);
            Contract.Requires<ArgumentNullException>(window != null);

            this.shellObject = shellObject;
            this.ListenerWindow = window;
            this.Message = message;
            this.recursive = recursive;
        }

        ~ShellWatcher()
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
                this.disposed = true;

                if (disposing)
                {
                }

                if (this.ListenerWindow != null)
                {
                    ShellWatcherManager.UnregisterAsync(this).Wait();
                }
            }
        }

        /// <summary>
        ///     <see cref="ShellWatcher" />を作成します。
        /// </summary>
        /// <param name="shellObject"><see cref="ShellObject" />。</param>
        /// <param name="recursive">再帰的に監視するかどうかを示すフラグ。</param>
        /// <returns></returns>
        public static async Task<ShellWatcher> CreateAsync(ShellObject shellObject, bool recursive)
        {
            Contract.Requires<ArgumentNullException>(shellObject != null);
            return await ShellWatcherManager.RegisterAsync(shellObject, recursive);
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.shellObject != null);
            Contract.Invariant(this.ListenerWindow != null);
            Contract.Invariant(this.eventManager != null);
        }

        /// <summary>
        ///     通知ウィンドウを取得または設定します。
        /// </summary>
        private WindowSource ListenerWindow { get; }

        /// <summary>
        ///     通知ウィンドウメッセージを取得します。
        /// </summary>
        internal uint Message { get; }

        /// <summary>
        ///     監視が実行中かどうかを判定する値を取得します。
        /// </summary>
        public bool Running { get; private set; }

        /// <summary>
        ///     シェル監視を開始します。
        /// </summary>
        public void Start()
        {
            if (this.Running)
            {
                return;
            }

            var entry = new SHChangeNotifyEntry
            {
                fRecursive = this.recursive,
                pidl = this.shellObject.PIDL
            };

            const Int32 flags = SHCNRF.SHCNRF_ShellLevel |
                                SHCNRF.SHCNRF_InterruptLevel |
                                SHCNRF.SHCNRF_NewDelivery;

            this.registrationId = ShellWatcherNativeMethods.SHChangeNotifyRegister(
                this.ListenerWindow.Handle,
                flags,
                this.eventManager.RegisteredTypes,
                this.Message,
                1,
                ref entry);
            if (this.registrationId == 0)
            {
                throw new Win32Exception(ErrorMessages.ShellWatcherRegisterFailed);
            }

            this.Running = true;
        }

        /// <summary>
        ///     シェル監視を停止します。
        /// </summary>
        public void Stop()
        {
            if (!this.Running)
            {
                return;
            }

            if (this.registrationId > 0)
            {
                ShellWatcherNativeMethods.SHChangeNotifyDeregister(this.registrationId);
                this.registrationId = 0;
            }
        }

        /// <summary>
        ///     シェル変更通知イベントを処理します。
        /// </summary>
        /// <param name="args"></param>
        internal void ChangeNotificationEvent(ShellNotificationEventArgs args)
        {
            Contract.Requires<ArgumentNullException>(args != null);

            this.eventManager.Invoke(this, (uint)args.ChangeType, args);
        }

        private void ThrowIfRunnning()
        {
            if (this.Running)
            {
                throw new InvalidOperationException(ErrorMessages.ShellWatcherUnableToChangeEvents);
            }
        }

        #region Events

        public event EventHandler<ShellNotificationEventArgs> AllEvents
        {
            add
            {
                ThrowIfRunnning();
                this.eventManager.AddHandler(ShellChangeTypes.AllEventsMask, value);
            }
            remove
            {
                ThrowIfRunnning();
                this.eventManager.RemoveHandler(ShellChangeTypes.AllEventsMask, value);
            }
        }

        public event EventHandler<ShellNotificationEventArgs> GlobalEvents
        {
            add
            {
                ThrowIfRunnning();
                this.eventManager.AddHandler(ShellChangeTypes.GlobalEventsMask, value);
            }
            remove
            {
                ThrowIfRunnning();
                this.eventManager.RemoveHandler(ShellChangeTypes.GlobalEventsMask, value);
            }
        }

        public event EventHandler<ShellNotificationEventArgs> DiskEvents
        {
            add
            {
                ThrowIfRunnning();
                this.eventManager.AddHandler(ShellChangeTypes.DiskEventsMask, value);
            }
            remove
            {
                ThrowIfRunnning();
                this.eventManager.RemoveHandler(ShellChangeTypes.DiskEventsMask, value);
            }
        }

        public event EventHandler<ShellRenamedEventArgs> ItemRenamed
        {
            add
            {
                ThrowIfRunnning();
                this.eventManager.AddHandler(ShellChangeTypes.ItemRename, value);
            }
            remove
            {
                ThrowIfRunnning();
                this.eventManager.RemoveHandler(ShellChangeTypes.ItemRename, value);
            }
        }

        public event EventHandler<ShellChangedEventArgs> ItemCreated
        {
            add
            {
                ThrowIfRunnning();
                this.eventManager.AddHandler(ShellChangeTypes.ItemCreate, value);
            }
            remove
            {
                ThrowIfRunnning();
                this.eventManager.RemoveHandler(ShellChangeTypes.ItemCreate, value);
            }
        }

        public event EventHandler<ShellChangedEventArgs> ItemDeleted
        {
            add
            {
                ThrowIfRunnning();
                this.eventManager.AddHandler(ShellChangeTypes.ItemDelete, value);
            }
            remove
            {
                ThrowIfRunnning();
                this.eventManager.RemoveHandler(ShellChangeTypes.ItemDelete, value);
            }
        }

        public event EventHandler<ShellChangedEventArgs> Updated
        {
            add
            {
                ThrowIfRunnning();
                this.eventManager.AddHandler(ShellChangeTypes.Update, value);
            }
            remove
            {
                ThrowIfRunnning();
                this.eventManager.RemoveHandler(ShellChangeTypes.Update, value);
            }
        }

        public event EventHandler<ShellChangedEventArgs> DirectoryUpdated
        {
            add
            {
                ThrowIfRunnning();
                this.eventManager.AddHandler(ShellChangeTypes.DirectoryContentsUpdate, value);
            }
            remove
            {
                ThrowIfRunnning();
                this.eventManager.RemoveHandler(ShellChangeTypes.DirectoryContentsUpdate, value);
            }
        }

        public event EventHandler<ShellRenamedEventArgs> DirectoryRenamed
        {
            add
            {
                ThrowIfRunnning();
                this.eventManager.AddHandler(ShellChangeTypes.DirectoryRename, value);
            }
            remove
            {
                ThrowIfRunnning();
                this.eventManager.RemoveHandler(ShellChangeTypes.DirectoryRename, value);
            }
        }

        public event EventHandler<ShellChangedEventArgs> DirectoryCreated
        {
            add
            {
                ThrowIfRunnning();
                this.eventManager.AddHandler(ShellChangeTypes.DirectoryCreate, value);
            }
            remove
            {
                ThrowIfRunnning();
                this.eventManager.RemoveHandler(ShellChangeTypes.DirectoryCreate, value);
            }
        }

        public event EventHandler<ShellChangedEventArgs> DirectoryDeleted
        {
            add
            {
                ThrowIfRunnning();
                this.eventManager.AddHandler(ShellChangeTypes.DirectoryDelete, value);
            }
            remove
            {
                ThrowIfRunnning();
                this.eventManager.RemoveHandler(ShellChangeTypes.DirectoryDelete, value);
            }
        }

        public event EventHandler<ShellChangedEventArgs> MediaInserted
        {
            add
            {
                ThrowIfRunnning();
                this.eventManager.AddHandler(ShellChangeTypes.MediaInsert, value);
            }
            remove
            {
                ThrowIfRunnning();
                this.eventManager.RemoveHandler(ShellChangeTypes.MediaInsert, value);
            }
        }

        public event EventHandler<ShellChangedEventArgs> MediaRemoved
        {
            add
            {
                ThrowIfRunnning();
                this.eventManager.AddHandler(ShellChangeTypes.MediaRemove, value);
            }
            remove
            {
                ThrowIfRunnning();
                this.eventManager.RemoveHandler(ShellChangeTypes.MediaRemove, value);
            }
        }

        public event EventHandler<ShellChangedEventArgs> DriveAdded
        {
            add
            {
                ThrowIfRunnning();
                this.eventManager.AddHandler(ShellChangeTypes.DriveAdd, value);
            }
            remove
            {
                ThrowIfRunnning();
                this.eventManager.RemoveHandler(ShellChangeTypes.DriveAdd, value);
            }
        }

        public event EventHandler<ShellChangedEventArgs> DriveRemoved
        {
            add
            {
                ThrowIfRunnning();
                this.eventManager.AddHandler(ShellChangeTypes.DriveRemove, value);
            }
            remove
            {
                ThrowIfRunnning();
                this.eventManager.RemoveHandler(ShellChangeTypes.DriveRemove, value);
            }
        }

        public event EventHandler<ShellChangedEventArgs> FolderNetworkShared
        {
            add
            {
                ThrowIfRunnning();
                this.eventManager.AddHandler(ShellChangeTypes.NetShare, value);
            }
            remove
            {
                ThrowIfRunnning();
                this.eventManager.RemoveHandler(ShellChangeTypes.NetShare, value);
            }
        }

        public event EventHandler<ShellChangedEventArgs> FolderNetworkUnshared
        {
            add
            {
                ThrowIfRunnning();
                this.eventManager.AddHandler(ShellChangeTypes.NetUnshare, value);
            }
            remove
            {
                ThrowIfRunnning();
                this.eventManager.RemoveHandler(ShellChangeTypes.NetUnshare, value);
            }
        }

        public event EventHandler<ShellChangedEventArgs> ServerDisconnected
        {
            add
            {
                ThrowIfRunnning();
                this.eventManager.AddHandler(ShellChangeTypes.ServerDisconnect, value);
            }
            remove
            {
                ThrowIfRunnning();
                this.eventManager.RemoveHandler(ShellChangeTypes.ServerDisconnect, value);
            }
        }

        public event EventHandler<ShellChangedEventArgs> SystemImageChanged
        {
            add
            {
                ThrowIfRunnning();
                this.eventManager.AddHandler(ShellChangeTypes.SystemImageUpdate, value);
            }
            remove
            {
                ThrowIfRunnning();
                this.eventManager.RemoveHandler(ShellChangeTypes.SystemImageUpdate, value);
            }
        }

        public event EventHandler<ShellChangedEventArgs> FreeSpaceChanged
        {
            add
            {
                ThrowIfRunnning();
                this.eventManager.AddHandler(ShellChangeTypes.FreeSpace, value);
            }
            remove
            {
                ThrowIfRunnning();
                this.eventManager.RemoveHandler(ShellChangeTypes.FreeSpace, value);
            }
        }

        public event EventHandler<ShellChangedEventArgs> FileTypeAssociationChanged
        {
            add
            {
                ThrowIfRunnning();
                this.eventManager.AddHandler(ShellChangeTypes.AssociationChange, value);
            }
            remove
            {
                ThrowIfRunnning();
                this.eventManager.RemoveHandler(ShellChangeTypes.AssociationChange, value);
            }
        }

        #endregion
    }
}