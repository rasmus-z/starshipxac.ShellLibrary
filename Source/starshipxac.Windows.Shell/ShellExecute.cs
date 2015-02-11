using System;
using System.Diagnostics.Contracts;
using starshipxac.Shell;
using starshipxac.Windows.Shell.Interop;

namespace starshipxac.Windows.Shell
{
	public static class ShellExecute
	{
		private static readonly IntPtr HWND_DESKTOP = (IntPtr)0;
		private const int SW_HIDE = 0;

		/// <summary>
		/// 指定したファイルまたはフォルダーのプロパティダイアログを表示します。
		/// </summary>
		/// <param name="path">ファイルまたはフォルダーのパス。</param>
		public static void ShowProperty(string path)
		{
			Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(path));

			var shei = SHELLEXECUTEINFO.Create(HWND_DESKTOP);
			shei.fMask = SEE_MASK.SEE_MASK_NOCLOSEPROCESS | SEE_MASK.SEE_MASK_INVOKEIDLIST | SEE_MASK.SEE_MASK_FLAG_NO_UI;
			shei.lpFile = path;
			shei.lpVerb = "properties";
			shei.nShow = SW_HIDE;

			WindowsShellNativeMethods.ShellExecuteEx(ref shei);
		}

		/// <summary>
		/// 指定した<see cref="ShellObject"/>のプロパティダイアログを表示します。
		/// </summary>
		/// <param name="shellObject"><see cref="ShellObject"/>。</param>
		public static void ShowProperty(ShellObject shellObject)
		{
			Contract.Requires<ArgumentNullException>(shellObject != null);
			Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(shellObject.ParsingName));

			ShowProperty(shellObject.ParsingName);
		}
	}
}