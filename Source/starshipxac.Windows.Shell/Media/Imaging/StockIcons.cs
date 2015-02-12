﻿using System;
using System.Collections.Generic;
using System.Linq;
using starshipxac.Windows.Shell.Media.Imaging.Interop;

namespace starshipxac.Windows.Shell.Media.Imaging
{
	/// <summary>
	/// ストックアイコンを取得します。
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
		/// ストックアイコンの規定サイズを取得または設定します。
		/// </summary>
		public static StockIconSize DefaultSize { get; set; }

		/// <summary>
		/// オーバーレイアイコンを取得するかどうかを判定する値を取得または設定します。
		/// </summary>
		public static bool DefaultLinkOverlay { get; set; }

		/// <summary>
		/// セレクト状態アイコンを取得するかどうかを判定する値を取得または設定します。
		/// </summary>
		public static bool DefaultSelectedState { get; set; }

		/// <summary>
		/// すべてのストックアイコンを列挙します。
		/// </summary>
		public static IEnumerable<StockIcon> AllStockIcons
		{
			get
			{
				return Enum.GetValues(typeof(SHSTOCKICONID)).OfType<SHSTOCKICONID>().Select(GetStockIcon);
			}
		}

		#region Stock Icons

		public static StockIcon NoAssociatedDocument
		{
			get
			{
				return GetStockIcon(SHSTOCKICONID.SIID_DOCNOASSOC);
			}
		}

		public static StockIcon AssociatedDocument
		{
			get
			{
				return GetStockIcon(SHSTOCKICONID.SIID_DOCASSOC);
			}
		}

		public static StockIcon Application
		{
			get
			{
				return GetStockIcon(SHSTOCKICONID.SIID_APPLICATION);
			}
		}

		public static StockIcon Folder
		{
			get
			{
				return GetStockIcon(SHSTOCKICONID.SIID_FOLDER);
			}
		}

		public static StockIcon FolderOpen
		{
			get
			{
				return GetStockIcon(SHSTOCKICONID.SIID_FOLDEROPEN);
			}
		}

		public static StockIcon Floppy525Drive
		{
			get
			{
				return GetStockIcon(SHSTOCKICONID.SIID_DRIVE525);
			}
		}

		public static StockIcon Floppy35Drive
		{
			get
			{
				return GetStockIcon(SHSTOCKICONID.SIID_DRIVE35);
			}
		}

		#endregion

		private static StockIcon GetStockIcon(SHSTOCKICONID stockIconId)
		{
			return new StockIcon(stockIconId, DefaultSize, DefaultLinkOverlay, DefaultSelectedState);
		}
	}
}