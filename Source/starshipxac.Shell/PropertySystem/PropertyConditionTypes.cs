using System;
using starshipxac.Shell.PropertySystem.Interop;

namespace starshipxac.Shell.PropertySystem
{
	/// <summary>
	/// クエリビルダーのユーザーインターフェイスでプロパティを表示するときに使用する条件タイプを定義します。
	/// </summary>
	public enum PropertyConditionTypes
	{
		/// <summary>
		/// デフォルトの条件タイプ
		/// </summary>
		None = PROPDESC_CONDITION_TYPE.PDCOT_NONE,

		/// <summary>
		/// 文字列
		/// </summary>
		String = PROPDESC_CONDITION_TYPE.PDCOT_STRING,

		/// <summary>
		/// サイズ
		/// </summary>
		Size = PROPDESC_CONDITION_TYPE.PDCOT_SIZE,

		/// <summary>
		/// 時刻
		/// </summary>
		DateTime = PROPDESC_CONDITION_TYPE.PDCOT_DATETIME,

		/// <summary>
		/// 真理値
		/// </summary>
		Boolean = PROPDESC_CONDITION_TYPE.PDCOT_BOOLEAN,

		/// <summary>
		/// 数字
		/// </summary>
		Number = PROPDESC_CONDITION_TYPE.PDCOT_NUMBER,
	}
}