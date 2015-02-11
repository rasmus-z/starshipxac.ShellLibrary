using System;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using starshipxac.Windows.Shell.Interop;

namespace starshipxac.Windows.Shell.Controls
{
	public sealed class MenuItemInfo
	{
		private IntPtr subMenu;

		public MenuItemInfo(int id)
		{
			this.Id = id;
		}

		internal static MenuItemInfo Create(MENUITEMINFO menuiteminfo)
		{
			var result = new MenuItemInfo((int)menuiteminfo.wID)
			{
				subMenu = menuiteminfo.hSubMenu,
				Image = CreateImageSource(menuiteminfo.hbmpItem),
			};

			return result;
		}

		private static ImageSource CreateImageSource(IntPtr hbitmap)
		{
			if (hbitmap == IntPtr.Zero)
			{
				return null;
			}

			return Imaging.CreateBitmapSourceFromHBitmap(hbitmap, IntPtr.Zero,
				new Int32Rect(0, 0, 16, 16),
				BitmapSizeOptions.FromEmptyOptions());
		}

		public int Id { get; private set; }

		public bool HasSubMenu
		{
			get
			{
				return this.subMenu != IntPtr.Zero;
			}
		}

		public ImageSource Image { get; private set; }
	}
}