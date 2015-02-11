﻿using System;

namespace starshipxac.Shell.Interop.Library
{
	/// <summary>
	/// ライブラリ管理ダイアログオプションを定義します。
	/// </summary>
	/// <remarks>
	/// http://msdn.microsoft.com/en-us/library/windows/desktop/dd378449(v=vs.85).aspx
	/// </remarks>
	internal enum LIBRARYMANAGEDIALOGOPTIONS
	{
		LMD_DEFAULT = 0,
		LMD_ALLOWUNINDEXABLENETWORKLOCATIONS = 0x1
	}
}