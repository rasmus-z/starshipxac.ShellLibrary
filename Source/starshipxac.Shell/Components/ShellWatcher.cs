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
    ///     Monitor shell changes.
    /// </summary>
    public class ShellWatcher : IDisposable
    {
        private bool disposed = false;

        private readonly ShellObject shellObject;
        private readonly bool recursive;

        private readonly ShellChangeEventManager eventManager = new ShellChangeEventManager();
        private uint registrationId;

        /// <summary>
        ///     Initialize a new instance of the <see cref="ShellWatcher" /> class.
        /// </summary>
        /// <param name="shellObject">Monitoring to <see cref="ShellObject" />.</param>
        /// <param name="window">Notification window.</param>
        /// <param name="message">Notification window message.</param>
        /// <param name="recursive">Flag indicating whether to monitor recursively.</param>
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
                if (disposing)
                {
                }

                if (this.ListenerWindow != null)
                {
                    ShellWatcherManager.UnregisterAsync(this).Wait();
                }

                this.disposed = true;
            }
        }

        /// <summary>
        ///     Create the <see cref="ShellWatcher" />.
        /// </summary>
        /// <param name="shellObject">Monitoring to <see cref="ShellObject" />.</param>
        /// <param name="recursive">Flag indicating whether to monitor recursively.</param>
        /// <returns></returns>
        public static async Task<ShellWatcher> CreateAsync(ShellObject shellObject, bool recursive)
        {
            Contract.Requires<ArgumentNullException>(shellObject != null);
            return await ShellWatcherManager.RegisterAsync(shellObject, recursive);
        }

        /// <summary>
        ///     Get or set listener window.
        /// </summary>
        private WindowSource ListenerWindow { get; }

        /// <summary>
        ///     Get the notification window message.
        /// </summary>
        internal uint Message { get; }

        /// <summary>
        ///     Get a value that determines whether monitoring is in progress.
        /// </summary>
        public bool Running { get; private set; }

        /// <summary>
        ///     Start shell watcher.
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
                pidl = this.shellObject.ShellItem.PIDL
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
        ///     Stop shell watcher.
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
        ///     Process shell change notification events.
        /// </summary>
        /// <param name="args"></param>
        internal void ChangeNotificationEvent(ShellNotificationEventArgs args)
        {
            Contract.Requires<ArgumentNullException>(args != null);

            this.eventManager.Invoke(this, (uint)args.ChangeType, args);
        }

        #region Events

        public event EventHandler<ShellNotificationEventArgs> AllEvents
        {
            add
            {
                this.eventManager.AddHandler(ShellChangeTypes.AllEventsMask, value);
            }
            remove
            {
                this.eventManager.RemoveHandler(ShellChangeTypes.AllEventsMask, value);
            }
        }

        public event EventHandler<ShellNotificationEventArgs> GlobalEvents
        {
            add
            {
                this.eventManager.AddHandler(ShellChangeTypes.GlobalEventsMask, value);
            }
            remove
            {
                this.eventManager.RemoveHandler(ShellChangeTypes.GlobalEventsMask, value);
            }
        }

        public event EventHandler<ShellNotificationEventArgs> DiskEvents
        {
            add
            {
                this.eventManager.AddHandler(ShellChangeTypes.DiskEventsMask, value);
            }
            remove
            {
                this.eventManager.RemoveHandler(ShellChangeTypes.DiskEventsMask, value);
            }
        }

        public event EventHandler<ShellRenamedEventArgs> ItemRenamed
        {
            add
            {
                this.eventManager.AddHandler(ShellChangeTypes.ItemRename, value);
            }
            remove
            {
                this.eventManager.RemoveHandler(ShellChangeTypes.ItemRename, value);
            }
        }

        public event EventHandler<ShellChangedEventArgs> ItemCreated
        {
            add
            {
                this.eventManager.AddHandler(ShellChangeTypes.ItemCreate, value);
            }
            remove
            {
                this.eventManager.RemoveHandler(ShellChangeTypes.ItemCreate, value);
            }
        }

        public event EventHandler<ShellChangedEventArgs> ItemDeleted
        {
            add
            {
                this.eventManager.AddHandler(ShellChangeTypes.ItemDelete, value);
            }
            remove
            {
                this.eventManager.RemoveHandler(ShellChangeTypes.ItemDelete, value);
            }
        }

        public event EventHandler<ShellChangedEventArgs> Updated
        {
            add
            {
                this.eventManager.AddHandler(ShellChangeTypes.Update, value);
            }
            remove
            {
                this.eventManager.RemoveHandler(ShellChangeTypes.Update, value);
            }
        }

        public event EventHandler<ShellChangedEventArgs> DirectoryUpdated
        {
            add
            {
                this.eventManager.AddHandler(ShellChangeTypes.DirectoryContentsUpdate, value);
            }
            remove
            {
                this.eventManager.RemoveHandler(ShellChangeTypes.DirectoryContentsUpdate, value);
            }
        }

        public event EventHandler<ShellRenamedEventArgs> DirectoryRenamed
        {
            add
            {
                this.eventManager.AddHandler(ShellChangeTypes.DirectoryRename, value);
            }
            remove
            {
                this.eventManager.RemoveHandler(ShellChangeTypes.DirectoryRename, value);
            }
        }

        public event EventHandler<ShellChangedEventArgs> DirectoryCreated
        {
            add
            {
                this.eventManager.AddHandler(ShellChangeTypes.DirectoryCreate, value);
            }
            remove
            {
                this.eventManager.RemoveHandler(ShellChangeTypes.DirectoryCreate, value);
            }
        }

        public event EventHandler<ShellChangedEventArgs> DirectoryDeleted
        {
            add
            {
                this.eventManager.AddHandler(ShellChangeTypes.DirectoryDelete, value);
            }
            remove
            {
                this.eventManager.RemoveHandler(ShellChangeTypes.DirectoryDelete, value);
            }
        }

        public event EventHandler<ShellChangedEventArgs> MediaInserted
        {
            add
            {
                this.eventManager.AddHandler(ShellChangeTypes.MediaInsert, value);
            }
            remove
            {
                this.eventManager.RemoveHandler(ShellChangeTypes.MediaInsert, value);
            }
        }

        public event EventHandler<ShellChangedEventArgs> MediaRemoved
        {
            add
            {
                this.eventManager.AddHandler(ShellChangeTypes.MediaRemove, value);
            }
            remove
            {
                this.eventManager.RemoveHandler(ShellChangeTypes.MediaRemove, value);
            }
        }

        public event EventHandler<ShellChangedEventArgs> DriveAdded
        {
            add
            {
                this.eventManager.AddHandler(ShellChangeTypes.DriveAdd, value);
            }
            remove
            {
                this.eventManager.RemoveHandler(ShellChangeTypes.DriveAdd, value);
            }
        }

        public event EventHandler<ShellChangedEventArgs> DriveRemoved
        {
            add
            {
                this.eventManager.AddHandler(ShellChangeTypes.DriveRemove, value);
            }
            remove
            {
                this.eventManager.RemoveHandler(ShellChangeTypes.DriveRemove, value);
            }
        }

        public event EventHandler<ShellChangedEventArgs> FolderNetworkShared
        {
            add
            {
                this.eventManager.AddHandler(ShellChangeTypes.NetShare, value);
            }
            remove
            {
                this.eventManager.RemoveHandler(ShellChangeTypes.NetShare, value);
            }
        }

        public event EventHandler<ShellChangedEventArgs> FolderNetworkUnshared
        {
            add
            {
                this.eventManager.AddHandler(ShellChangeTypes.NetUnshare, value);
            }
            remove
            {
                this.eventManager.RemoveHandler(ShellChangeTypes.NetUnshare, value);
            }
        }

        public event EventHandler<ShellChangedEventArgs> ServerDisconnected
        {
            add
            {
                this.eventManager.AddHandler(ShellChangeTypes.ServerDisconnect, value);
            }
            remove
            {
                this.eventManager.RemoveHandler(ShellChangeTypes.ServerDisconnect, value);
            }
        }

        public event EventHandler<ShellChangedEventArgs> SystemImageChanged
        {
            add
            {
                this.eventManager.AddHandler(ShellChangeTypes.SystemImageUpdate, value);
            }
            remove
            {
                this.eventManager.RemoveHandler(ShellChangeTypes.SystemImageUpdate, value);
            }
        }

        public event EventHandler<ShellChangedEventArgs> FreeSpaceChanged
        {
            add
            {
                this.eventManager.AddHandler(ShellChangeTypes.FreeSpace, value);
            }
            remove
            {
                this.eventManager.RemoveHandler(ShellChangeTypes.FreeSpace, value);
            }
        }

        public event EventHandler<ShellChangedEventArgs> FileTypeAssociationChanged
        {
            add
            {
                this.eventManager.AddHandler(ShellChangeTypes.AssociationChange, value);
            }
            remove
            {
                this.eventManager.RemoveHandler(ShellChangeTypes.AssociationChange, value);
            }
        }

        #endregion
    }
}