using System;
// ReSharper disable InconsistentNaming

namespace starshipxac.Windows.Shell.Controls.Explorers.Interop
{
    internal static class ExplorerBrowserGuid
    {
        public static readonly Guid IExplorerBrowser = new Guid(ExplorerBrowserIIDGuid.IExplorerBrowser);

        public static readonly Guid IExplorerPaneVisibility = new Guid(ExplorerBrowserIIDGuid.IExplorerPaneVisibility);

        public static readonly Guid ICommDlgBrowser = new Guid(ExplorerBrowserIIDGuid.ICommDlgBrowser);
        public static readonly Guid ICommDlgBrowser2 = new Guid(ExplorerBrowserIIDGuid.ICommDlgBrowser2);
        public static readonly Guid ICommDlgBrowser3 = new Guid(ExplorerBrowserIIDGuid.ICommDlgBrowser3);
    }
}