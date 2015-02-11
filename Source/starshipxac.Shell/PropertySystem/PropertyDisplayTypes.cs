using System;
using starshipxac.Shell.PropertySystem.Interop;

namespace starshipxac.Shell.PropertySystem
{
	/// <summary>
	/// プロパティの表示種別を定義します。
	/// </summary>
	public enum PropertyDisplayTypes
	{
		/// <summary>
		/// 文字列。
		/// </summary>
		String = PROPDESC_DISPLAYTYPE.PDDT_STRING,

		/// <summary>
		/// 数値。
		/// </summary>
		Number = PROPDESC_DISPLAYTYPE.PDDT_NUMBER,

		/// <summary>
		/// 真理値。
		/// </summary>
		Boolean = PROPDESC_DISPLAYTYPE.PDDT_BOOLEAN,

		/// <summary>
		/// 時刻。
		/// </summary>
		DateTime = PROPDESC_DISPLAYTYPE.PDDT_DATETIME,

		/// <summary>
		/// 列挙値。
		/// </summary>
		Enumerated = PROPDESC_DISPLAYTYPE.PDDT_ENUMERATED
	}
}