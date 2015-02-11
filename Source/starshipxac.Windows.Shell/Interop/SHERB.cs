using System;

namespace starshipxac.Windows.Shell.Interop
{
	/// <summary>
	/// ごみ箱を空にするフラグを定義します。
	/// </summary>
	/// <remarks>
	/// http://msdn.microsoft.com/en-us/library/windows/desktop/bb762160(v=vs.85).aspx
	/// </remarks>
	internal static class SHERB
	{
		/// <summary>
		///問い合わせダイアログを表示しません。
		/// </summary>
		public const UInt32 SHERB_NOCONFIRMATION = 0x00000001;

		/// <summary>
		/// 進捗ダイアログを表示しません。
		/// </summary>
		public const UInt32 SHERB_NOPROGRESSUI = 0x00000002;

		/// <summary>
		/// 操作が完了したときの音を再生しません。
		/// </summary>
		public const UInt32 SHERB_NOSOUND = 0x00000004;
	}
}