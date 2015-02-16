using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Windows;
using starshipxac.Shell;
using starshipxac.Shell.Interop;
using starshipxac.Windows.Shell.Controls.Explorers.Interop;
using starshipxac.Windows.Shell.Properties;

namespace starshipxac.Windows.Shell.Controls.Explorers
{
    public class ExplorerBrowser : ExplorerBrowserBase
    {
        private ShellObject currentNavigationTarget;
        private FOLDERVIEWMODE? initialViewMode;

        private ObservableCollection<ShellObject> history;

        public ExplorerBrowser()
        {
            this.PropertyBagName = typeof(ExplorerBrowser).FullName;

            this.history = new ObservableCollection<ShellObject>();
        }

        #region Properties

        #region ViewMode Property

        public ExplorerFolderViewModes ViewMode
        {
            get
            {
                return (ExplorerFolderViewModes)GetValue(ViewModeProperty);
            }
            set
            {
                SetValue(ViewModeProperty, value);
            }
        }

        public static readonly DependencyProperty ViewModeProperty = DependencyProperty.Register(
            "ViewMode", typeof(ExplorerFolderViewModes), typeof(ExplorerBrowser),
            new PropertyMetadata(ExplorerFolderViewModes.Auto, OnViewModePropertyChanged));

        private static void OnViewModePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ExplorerBrowser;
            if (control != null)
            {
                if (control.ExplorerBrowserNative == null)
                {
                    control.initialViewMode = (FOLDERVIEWMODE)e.NewValue;
                }
                else
                {
                    control.FolderSettings.ViewMode = (FOLDERVIEWMODE)e.NewValue;
                }
            }
        }

        #endregion

        #region AlignLeft Property

        public bool AlignLeft
        {
            get
            {
                return (bool)GetValue(AlignLeftProperty);
            }
            set
            {
                SetValue(AlignLeftProperty, value);
            }
        }

        public static readonly DependencyProperty AlignLeftProperty = DependencyProperty.Register(
            "AlignLeft", typeof(bool), typeof(ExplorerBrowser),
            new PropertyMetadata(false, OnAlignLeftPropertyChanged));

        private static void OnAlignLeftPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ExplorerBrowser;
            if (control != null)
            {
                control.SetFolderSettingsFlag(FOLDERFLAGS.FWF_ALIGNLEFT, (bool)e.NewValue);
            }
        }

        #endregion

        #region AutoArrange Property

        public bool AutoArrange
        {
            get
            {
                return (bool)GetValue(AutoArrangeProperty);
            }
            set
            {
                SetValue(AutoArrangeProperty, value);
            }
        }

        public static readonly DependencyProperty AutoArrangeProperty = DependencyProperty.Register(
            "AutoArrange", typeof(bool), typeof(ExplorerBrowser),
            new PropertyMetadata(false, OnAutoArrangePropertyChanged));

        private static void OnAutoArrangePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ExplorerBrowser;
            if (control != null)
            {
                control.SetFolderSettingsFlag(FOLDERFLAGS.FWF_AUTOARRANGE, (bool)e.NewValue);
            }
        }

        #endregion

        #region CheckSelect Property

        public bool CheckSelect
        {
            get
            {
                return (bool)GetValue(CheckSelectProperty);
            }
            set
            {
                SetValue(CheckSelectProperty, value);
            }
        }

        public static readonly DependencyProperty CheckSelectProperty = DependencyProperty.Register(
            "CheckSelect", typeof(bool), typeof(ExplorerBrowser),
            new PropertyMetadata(false, OnCheckSelectPropertyChanged));

        private static void OnCheckSelectPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ExplorerBrowser;
            if (control != null)
            {
                control.SetFolderSettingsFlag(FOLDERFLAGS.FWF_CHECKSELECT, (bool)e.NewValue);
            }
        }

        #endregion

        #region ExtendedTitles Property

        public bool ExtendedTitles
        {
            get
            {
                return (bool)GetValue(ExtendedTitlesProperty);
            }
            set
            {
                SetValue(ExtendedTitlesProperty, value);
            }
        }

        public static readonly DependencyProperty ExtendedTitlesProperty = DependencyProperty.Register(
            "ExtendedTitles", typeof(bool), typeof(ExplorerBrowser),
            new PropertyMetadata(false, OnExtendedTitlesPropertyChanged));

        private static void OnExtendedTitlesPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ExplorerBrowser;
            if (control != null)
            {
                control.SetFolderSettingsFlag(FOLDERFLAGS.FWF_EXTENDEDTILES, (bool)e.NewValue);
            }
        }

        #endregion

        #region FullRowSelect Property

        public bool FullRowSelect
        {
            get
            {
                return (bool)GetValue(FullRowSelectProperty);
            }
            set
            {
                SetValue(FullRowSelectProperty, value);
            }
        }

        public static readonly DependencyProperty FullRowSelectProperty = DependencyProperty.Register(
            "FullRowSelect", typeof(bool), typeof(ExplorerBrowser),
            new PropertyMetadata(false, OnFullRowSelectPropertyChanged));

        private static void OnFullRowSelectPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ExplorerBrowser;
            if (control != null)
            {
                control.SetFolderSettingsFlag(FOLDERFLAGS.FWF_FULLROWSELECT, (bool)e.NewValue);
            }
        }

        #endregion

        #region ShowFileNames Property

        public bool ShowFileNames
        {
            get
            {
                return (bool)GetValue(ShowFileNamesProperty);
            }
            set
            {
                SetValue(ShowFileNamesProperty, value);
            }
        }

        public static readonly DependencyProperty ShowFileNamesProperty = DependencyProperty.Register(
            "ShowFileNames", typeof(bool), typeof(ExplorerBrowser),
            new PropertyMetadata(true, OnShowFileNamesPropertyChanged));

        private static void OnShowFileNamesPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ExplorerBrowser;
            if (control != null)
            {
                control.SetFolderSettingsFlag(FOLDERFLAGS.FWF_HIDEFILENAMES, !(bool)e.NewValue);
            }
        }

        #endregion

        #region SaveBrowserViewState Property

        public bool SaveBrowserViewState
        {
            get
            {
                return (bool)GetValue(SaveBrowserViewStateProperty);
            }
            set
            {
                SetValue(SaveBrowserViewStateProperty, value);
            }
        }

        public static readonly DependencyProperty SaveBrowserViewStateProperty = DependencyProperty.Register(
            "SaveBrowserViewState", typeof(bool), typeof(ExplorerBrowser),
            new PropertyMetadata(false, OnSaveBrowserViewStatePropertyChanged));

        private static void OnSaveBrowserViewStatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ExplorerBrowser;
            if (control != null)
            {
                control.SetFolderSettingsFlag(FOLDERFLAGS.FWF_NOBROWSERVIEWSTATE, !(bool)e.NewValue);
            }
        }

        #endregion

        #region ShowColumnHeader Property

        public bool ShowColumnHeader
        {
            get
            {
                return (bool)GetValue(ShowFileNamesProperty);
            }
            set
            {
                SetValue(ShowFileNamesProperty, value);
            }
        }

        public static readonly DependencyProperty ShowColumnHeaderProperty = DependencyProperty.Register(
            "ShowColumnHeader", typeof(bool), typeof(ExplorerBrowser),
            new PropertyMetadata(true, OnShowColumnHeaderPropertyChanged));

        private static void OnShowColumnHeaderPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ExplorerBrowser;
            if (control != null)
            {
                control.SetFolderSettingsFlag(FOLDERFLAGS.FWF_NOCOLUMNHEADER, !(bool)e.NewValue);
            }
        }

        #endregion

        #region ShowHeaderInAllViews Property

        public bool ShowHeaderInAllViews
        {
            get
            {
                return (bool)GetValue(ShowHeaderInAllViewsProperty);
            }
            set
            {
                SetValue(ShowFileNamesProperty, value);
            }
        }

        public static readonly DependencyProperty ShowHeaderInAllViewsProperty = DependencyProperty.Register(
            "ShowHeaderInAllViews", typeof(bool), typeof(ExplorerBrowser),
            new PropertyMetadata(true, OnShowHeaderInAllViewsPropertyChanged));

        private static void OnShowHeaderInAllViewsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ExplorerBrowser;
            if (control != null)
            {
                control.SetFolderSettingsFlag(FOLDERFLAGS.FWF_NOHEADERINALLVIEWS, !(bool)e.NewValue);
            }
        }

        #endregion

        #region ShowIcons Property

        public bool ShowIcons
        {
            get
            {
                return (bool)GetValue(ShowIconsProperty);
            }
            set
            {
                SetValue(ShowIconsProperty, value);
            }
        }

        public static readonly DependencyProperty ShowIconsProperty = DependencyProperty.Register(
            "ShowIcons", typeof(bool), typeof(ExplorerBrowser),
            new PropertyMetadata(true, OnShowItemsPropertyChanged));

        private static void OnShowItemsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ExplorerBrowser;
            if (control != null)
            {
                control.SetFolderSettingsFlag(FOLDERFLAGS.FWF_NOICONS, !(bool)e.NewValue);
            }
        }

        #endregion

        #region ShowSubFolders Property

        public bool ShowSubFolders
        {
            get
            {
                return (bool)GetValue(ShowSubFoldersProperty);
            }
            set
            {
                SetValue(ShowSubFoldersProperty, value);
            }
        }

        public static readonly DependencyProperty ShowSubFoldersProperty = DependencyProperty.Register(
            "ShowSubFolders", typeof(bool), typeof(ExplorerBrowser),
            new PropertyMetadata(true, OnShowSubFoldersPropertyChanged));

        private static void OnShowSubFoldersPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ExplorerBrowser;
            if (control != null)
            {
                control.SetFolderSettingsFlag(FOLDERFLAGS.FWF_NOSUBFOLDERS, !(bool)e.NewValue);
            }
        }

        #endregion

        #region SingleClickActivate Property

        public bool SingleClickActivate
        {
            get
            {
                return (bool)GetValue(SingleClickActivateProperty);
            }
            set
            {
                SetValue(SingleClickActivateProperty, value);
            }
        }

        public static readonly DependencyProperty SingleClickActivateProperty = DependencyProperty.Register(
            "SingleClickActivate", typeof(bool), typeof(ExplorerBrowser),
            new PropertyMetadata(false, OnSingleClickActivatePropertyChanged));

        private static void OnSingleClickActivatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ExplorerBrowser;
            if (control != null)
            {
                control.SetFolderSettingsFlag(FOLDERFLAGS.FWF_SINGLECLICKACTIVATE, (bool)e.NewValue);
            }
        }

        #endregion

        #region SingleSelection Property

        public bool SingleSelection
        {
            get
            {
                return (bool)GetValue(SingleSelectionProperty);
            }
            set
            {
                SetValue(SingleSelectionProperty, value);
            }
        }

        public static readonly DependencyProperty SingleSelectionProperty = DependencyProperty.Register(
            "SingleSelection", typeof(bool), typeof(ExplorerBrowser),
            new PropertyMetadata(false, OnSingleSelectionPropertyChanged));

        private static void OnSingleSelectionPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ExplorerBrowser;
            if (control != null)
            {
                control.SetFolderSettingsFlag(FOLDERFLAGS.FWF_SINGLESEL, (bool)e.NewValue);
            }
        }

        #endregion

        #region NavigateOnce Property

        public bool NavigateOnce
        {
            get
            {
                return (bool)GetValue(NavigateOnceProperty);
            }
            set
            {
                SetValue(NavigateOnceProperty, value);
            }
        }

        public static readonly DependencyProperty NavigateOnceProperty = DependencyProperty.Register(
            "NavigateOnce", typeof(bool), typeof(ExplorerBrowser),
            new PropertyMetadata(false, OnNavigateOncePropertyChanged));

        private static void OnNavigateOncePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ExplorerBrowser;
            if (control != null)
            {
                control.SetOptionFlag(EXPLORER_BROWSER_OPTIONS.EBO_NAVIGATEONCE, (bool)e.NewValue);
            }
        }

        #endregion

        #region AlwaysNavigate Property

        public bool AlwaysNavigate
        {
            get
            {
                return (bool)GetValue(AlwaysNavigateProperty);
            }
            set
            {
                SetValue(AlwaysNavigateProperty, value);
            }
        }

        public static readonly DependencyProperty AlwaysNavigateProperty = DependencyProperty.Register(
            "AlwaysNavigate", typeof(bool), typeof(ExplorerBrowser),
            new PropertyMetadata(false, OnAlwaysNavigatePropertyChanged));

        private static void OnAlwaysNavigatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ExplorerBrowser;
            if (control != null)
            {
                control.SetOptionFlag(EXPLORER_BROWSER_OPTIONS.EBO_ALWAYSNAVIGATE, (bool)e.NewValue);
            }
        }

        #endregion

        #region ThumbnailSize Property

        public int ThumbnailSize
        {
            get
            {
                return (int)GetValue(ThumbnailSizeProperty);
            }
            set
            {
                SetValue(ThumbnailSizeProperty, value);
            }
        }

        public static readonly DependencyProperty ThumbnailSizeProperty = DependencyProperty.Register(
            "ThumbnailSize", typeof(int), typeof(ExplorerBrowser),
            new PropertyMetadata(32, OnThumbnailSizePropertyChanged));

        private static void OnThumbnailSizePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ExplorerBrowser;
            if (control != null)
            {
                using (var folderView = FolderView.CreateInternal(control))
                {
                    folderView.IconSize = (int)e.NewValue;
                }
            }
        }

        #endregion

        #region NavigationPane Property

        public PaneVisibilityStates NavigationPane
        {
            get
            {
                return (PaneVisibilityStates)GetValue(NavigationPaneProperty);
            }
            set
            {
                SetValue(NavigationPaneProperty, value);
            }
        }

        public static readonly DependencyProperty NavigationPaneProperty = DependencyProperty.Register(
            "NavigationPane", typeof(PaneVisibilityStates), typeof(ExplorerBrowser),
            new PropertyMetadata(PaneVisibilityStates.DoNotCare, OnNavigationPanePropertyChanged));

        private static void OnNavigationPanePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ExplorerBrowser;
            if (control != null)
            {
                control.PaneVisibility.Navigation = (PaneVisibilityStates)e.NewValue;
            }
        }

        #endregion

        #region CommandsPane Property

        public PaneVisibilityStates CommandsPane
        {
            get
            {
                return (PaneVisibilityStates)GetValue(CommandsPaneProperty);
            }
            set
            {
                SetValue(CommandsPaneProperty, value);
            }
        }

        public static readonly DependencyProperty CommandsPaneProperty = DependencyProperty.Register(
            "CommandsPane", typeof(PaneVisibilityStates), typeof(ExplorerBrowser),
            new PropertyMetadata(PaneVisibilityStates.DoNotCare, OnCommandsPanePropertyChanged));

        private static void OnCommandsPanePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ExplorerBrowser;
            if (control != null)
            {
                control.PaneVisibility.Commands = (PaneVisibilityStates)e.NewValue;
            }
        }

        #endregion

        #region CommandsOrganizePane Property

        public PaneVisibilityStates CommandsOrganizePane
        {
            get
            {
                return (PaneVisibilityStates)GetValue(CommandsOrganizePaneProperty);
            }
            set
            {
                SetValue(CommandsOrganizePaneProperty, value);
            }
        }

        public static readonly DependencyProperty CommandsOrganizePaneProperty = DependencyProperty.Register(
            "CommandsOrganizePane", typeof(PaneVisibilityStates), typeof(ExplorerBrowser),
            new PropertyMetadata(PaneVisibilityStates.DoNotCare, OnCommandsOrganizePanePropertyChanged));

        private static void OnCommandsOrganizePanePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ExplorerBrowser;
            if (control != null)
            {
                control.PaneVisibility.CommandsOrganize = (PaneVisibilityStates)e.NewValue;
            }
        }

        #endregion

        #region CommandsViewPane Property

        public PaneVisibilityStates CommandsViewPane
        {
            get
            {
                return (PaneVisibilityStates)GetValue(CommandsViewPaneProperty);
            }
            set
            {
                SetValue(CommandsViewPaneProperty, value);
            }
        }

        public static readonly DependencyProperty CommandsViewPaneProperty = DependencyProperty.Register(
            "CommandsViewPane", typeof(PaneVisibilityStates), typeof(ExplorerBrowser),
            new PropertyMetadata(PaneVisibilityStates.DoNotCare, OnCommandsViewPanePropertyChanged));

        private static void OnCommandsViewPanePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ExplorerBrowser;
            if (control != null)
            {
                control.PaneVisibility.CommandsView = (PaneVisibilityStates)e.NewValue;
            }
        }

        #endregion

        #region DetailsPane Property

        public PaneVisibilityStates DetailsPane
        {
            get
            {
                return (PaneVisibilityStates)GetValue(DetailsPaneProperty);
            }
            set
            {
                SetValue(DetailsPaneProperty, value);
            }
        }

        public static readonly DependencyProperty DetailsPaneProperty = DependencyProperty.Register(
            "DetailsPane", typeof(PaneVisibilityStates), typeof(ExplorerBrowser),
            new PropertyMetadata(PaneVisibilityStates.DoNotCare, OnDetailsPanePropertyChanged));

        private static void OnDetailsPanePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ExplorerBrowser;
            if (control != null)
            {
                control.PaneVisibility.Details = (PaneVisibilityStates)e.NewValue;
            }
        }

        #endregion

        #region PreviewPane Property

        public PaneVisibilityStates PreviewPane
        {
            get
            {
                return (PaneVisibilityStates)GetValue(PreviewPaneProperty);
            }
            set
            {
                SetValue(PreviewPaneProperty, value);
            }
        }

        public static readonly DependencyProperty PreviewPaneProperty = DependencyProperty.Register(
            "PreviewPane", typeof(PaneVisibilityStates), typeof(ExplorerBrowser),
            new PropertyMetadata(PaneVisibilityStates.DoNotCare, PreviewPanePropertyChanged));

        private static void PreviewPanePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ExplorerBrowser;
            if (control != null)
            {
                control.PaneVisibility.Preview = (PaneVisibilityStates)e.NewValue;
            }
        }

        #endregion

        #region QueryPane Property

        public PaneVisibilityStates QueryPane
        {
            get
            {
                return (PaneVisibilityStates)GetValue(QueryPaneProperty);
            }
            set
            {
                SetValue(QueryPaneProperty, value);
            }
        }

        public static readonly DependencyProperty QueryPaneProperty = DependencyProperty.Register(
            "QueryPane", typeof(PaneVisibilityStates), typeof(ExplorerBrowser),
            new PropertyMetadata(PaneVisibilityStates.DoNotCare, QueryPanePropertyChanged));

        private static void QueryPanePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ExplorerBrowser;
            if (control != null)
            {
                control.PaneVisibility.Query = (PaneVisibilityStates)e.NewValue;
            }
        }

        #endregion

        #region AdvancedQueryPane Property

        public PaneVisibilityStates AdvancedQueryPane
        {
            get
            {
                return (PaneVisibilityStates)GetValue(AdvancedQueryPaneProperty);
            }
            set
            {
                SetValue(AdvancedQueryPaneProperty, value);
            }
        }

        public static readonly DependencyProperty AdvancedQueryPaneProperty = DependencyProperty.Register(
            "AdvancedQueryPane", typeof(PaneVisibilityStates), typeof(ExplorerBrowser),
            new PropertyMetadata(PaneVisibilityStates.DoNotCare, AdvancedQueryPanePropertyChanged));

        private static void AdvancedQueryPanePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ExplorerBrowser;
            if (control != null)
            {
                control.PaneVisibility.AdvancedQuery = (PaneVisibilityStates)e.NewValue;
            }
        }

        #endregion

        #region StatusBarPane Property

        public PaneVisibilityStates StatusBarPane
        {
            get
            {
                return (PaneVisibilityStates)GetValue(StatusBarPaneProperty);
            }
            set
            {
                SetValue(StatusBarPaneProperty, value);
            }
        }

        public static readonly DependencyProperty StatusBarPaneProperty = DependencyProperty.Register(
            "StatusBarPane", typeof(PaneVisibilityStates), typeof(ExplorerBrowser),
            new PropertyMetadata(PaneVisibilityStates.DoNotCare, StatusBarPanePropertyChanged));

        private static void StatusBarPanePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ExplorerBrowser;
            if (control != null)
            {
                control.PaneVisibility.StatusBar = (PaneVisibilityStates)e.NewValue;
            }
        }

        #endregion

        #region RibbonPane Property

        public PaneVisibilityStates RibbonPane
        {
            get
            {
                return (PaneVisibilityStates)GetValue(RibbonPaneProperty);
            }
            set
            {
                SetValue(RibbonPaneProperty, value);
            }
        }

        public static readonly DependencyProperty RibbonPaneProperty = DependencyProperty.Register(
            "RibbonPane", typeof(PaneVisibilityStates), typeof(ExplorerBrowser),
            new PropertyMetadata(PaneVisibilityStates.DoNotCare, RibbonPanePropertyChanged));

        private static void RibbonPanePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ExplorerBrowser;
            if (control != null)
            {
                control.PaneVisibility.Ribbon = (PaneVisibilityStates)e.NewValue;
            }
        }

        #endregion

        #endregion

        #region Events

        public event EventHandler<NavigationPendingEventArgs> NavigationPending;

        protected override void OnNavigationPending(NavigationPendingEventArgs args)
        {
            var handler = this.NavigationPending;
            if (handler != null)
            {
                handler(this, args);
            }
        }

        public event EventHandler<NavigationCompletedEventArgs> NavigationCompleted;

        protected override void OnNavigationCompleted(NavigationCompletedEventArgs args)
        {
            var handler = this.NavigationCompleted;
            if (handler != null)
            {
                handler(this, args);
            }
        }

        public event EventHandler<NavigationFailedEventArgs> NavigationFailed;

        protected override void OnNavigationFailed(NavigationFailedEventArgs args)
        {
            var handler = this.NavigationFailed;
            if (handler != null)
            {
                handler(this, args);
            }
        }

        #endregion

        protected override void OnInitialized(EventArgs e)
        {
            if (this.currentNavigationTarget != null)
            {
                Navigate(this.currentNavigationTarget);
            }
            else
            {
                Navigate(ShellKnownFolders.Desktop);
            }
        }

        public IEnumerable<ShellObject> GetItems()
        {
            using (var folderView = FolderView.CreateInternal(this))
            {
                return folderView.GetItems();
            }
        }

        public IEnumerable<ShellObject> GetSelectedItems()
        {
            using (var folderView = FolderView.CreateInternal(this))
            {
                return folderView.GetSelectedItems();
            }
        }

        public void Navigate(ShellObject shellObject)
        {
            Contract.Requires<ArgumentNullException>(shellObject != null);

            this.currentNavigationTarget = shellObject;
            if (this.ExplorerBrowserNative != null)
            {
                var hr = this.ExplorerBrowserNative.BrowseToObject(shellObject.ShellItem.ShellItemInterface, 0);
                if (HRESULT.Failed(hr))
                {
                    if (hr == COMErrorCodes.ResourceInUse || hr == COMErrorCodes.Cancelled)
                    {
                        var args = new NavigationFailedEventArgs(shellObject);
                        OnNavigationFailed(args);
                    }
                    else
                    {
                        throw new ExplorerControlException(ErrorMessages.ExplorerNavigationFailed);
                    }
                }
            }
        }
    }
}