using System;
using System.Collections.Generic;
using System.Linq;
using starshipxac.Windows.Shell.Media.Imaging.Interop;

namespace starshipxac.Windows.Shell.Media.Imaging
{
    /// <summary>
    ///     Define stock icon factory class.
    /// </summary>
    public static class StockIcons
    {
        static StockIcons()
        {
            DefaultSize = StockIconSize.Large;
            DefaultLinkOverlay = false;
            DefaultSelectedState = false;
        }

        /// <summary>
        ///     Get or set default stock icon size.
        /// </summary>
        public static StockIconSize DefaultSize { get; set; }

        /// <summary>
        ///     Get or set default value that determines whether to acquire link overlay.
        /// </summary>
        public static bool DefaultLinkOverlay { get; set; }

        /// <summary>
        ///     Get or set default value that determines whether to acquire the selection status icon.
        /// </summary>
        public static bool DefaultSelectedState { get; set; }

        /// <summary>
        ///     Enumerate all stock icons.
        /// </summary>
        /// <returns>A collection of stock icons.</returns>
        public static IEnumerable<StockIcon> EnumerateAllIcons()
        {
            return Enum.GetValues(typeof(SHSTOCKICONID)).OfType<SHSTOCKICONID>().Select(GetStockIcon);
        }

        #region Stock Icons

        public static StockIcon NoAssociatedDocument => GetStockIcon(SHSTOCKICONID.SIID_DOCNOASSOC);

        public static StockIcon AssociatedDocument => GetStockIcon(SHSTOCKICONID.SIID_DOCASSOC);

        public static StockIcon Application => GetStockIcon(SHSTOCKICONID.SIID_APPLICATION);

        public static StockIcon Folder => GetStockIcon(SHSTOCKICONID.SIID_FOLDER);

        public static StockIcon FolderOpen => GetStockIcon(SHSTOCKICONID.SIID_FOLDEROPEN);

        public static StockIcon Floppy525Drive => GetStockIcon(SHSTOCKICONID.SIID_DRIVE525);

        public static StockIcon Floppy35Drive => GetStockIcon(SHSTOCKICONID.SIID_DRIVE35);

        #endregion

        /// <summary>
        ///     Get the <see cref="StockIcon" />.
        /// </summary>
        /// <param name="stockIconId">Stock icon ID.</param>
        /// <returns></returns>
        private static StockIcon GetStockIcon(SHSTOCKICONID stockIconId)
        {
            return new StockIcon(stockIconId, DefaultSize, DefaultLinkOverlay, DefaultSelectedState);
        }
    }
}