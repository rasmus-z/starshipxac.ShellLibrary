using System;
using starshipxac.Windows.Shell.Controls.Explorers.Interop;

namespace starshipxac.Windows.Shell.Controls.Explorers
{
    public class ExplorerBrowserPaneVisibility
    {
        public PaneVisibilityStates Navigation { get; set; }

        public PaneVisibilityStates Commands { get; set; }

        public PaneVisibilityStates CommandsOrganize { get; set; }

        public PaneVisibilityStates CommandsView { get; set; }

        public PaneVisibilityStates Details { get; set; }

        public PaneVisibilityStates Preview { get; set; }

        public PaneVisibilityStates Query { get; set; }

        public PaneVisibilityStates AdvancedQuery { get; set; }

        /// <summary>
        /// ステータスバー(Windows8)
        /// </summary>
        public PaneVisibilityStates StatusBar { get; set; }

        /// <summary>
        /// リボン(Windows8)
        /// </summary>
        public PaneVisibilityStates Ribbon { get; set; }

        internal PaneVisibilityStates GetState(Guid paneGuid)
        {
            System.Diagnostics.Debug.WriteLine("GetState: " + paneGuid);
            if (paneGuid == ExplorerPaneGuid.Navigation)
            {
                return this.Navigation;
            }
            else if (paneGuid == ExplorerPaneGuid.Commands)
            {
                return this.Commands;
            }
            else if (paneGuid == ExplorerPaneGuid.CommandsOrganize)
            {
                return this.CommandsOrganize;
            }
            else if (paneGuid == ExplorerPaneGuid.CommandsView)
            {
                return this.CommandsView;
            }
            else if (paneGuid == ExplorerPaneGuid.Details)
            {
                return this.Details;
            }
            else if (paneGuid == ExplorerPaneGuid.Preview)
            {
                return this.Preview;
            }
            else if (paneGuid == ExplorerPaneGuid.Query)
            {
                return this.Query;
            }
            else if (paneGuid == ExplorerPaneGuid.AdvancedQuery)
            {
                return this.AdvancedQuery;
            }
            else if (paneGuid == ExplorerPaneGuid.StatusBar)
            {
                return this.StatusBar;
            }
            else if (paneGuid == ExplorerPaneGuid.Ribbon)
            {
                return this.Ribbon;
            }
            return PaneVisibilityStates.Show;
        }

        internal EXPLORERPANESTATE GetPaneState(Guid paneGuid)
        {
            return ToPaneState(GetState(paneGuid));
        }

        internal static EXPLORERPANESTATE ToPaneState(PaneVisibilityStates state)
        {
            switch (state)
            {
                case PaneVisibilityStates.DoNotCare:
                    return EXPLORERPANESTATE.EPS_DONTCARE;

                case PaneVisibilityStates.Hide:
                    return EXPLORERPANESTATE.EPS_DEFAULT_OFF | EXPLORERPANESTATE.EPS_FORCE;

                case PaneVisibilityStates.Show:
                    return EXPLORERPANESTATE.EPS_DEFAULT_ON | EXPLORERPANESTATE.EPS_FORCE;

                default:
                    throw new ArgumentException("undefined PaneVisibilityState.");
            }
        }
    }
}