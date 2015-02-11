using System;
using System.Runtime.InteropServices;

namespace starshipxac.Shell.PropertySystem.Interop
{
	/// <summary>
	/// プロパティキーを定義します。
	/// </summary>
	/// <remarks>
	/// http://msdn.microsoft.com/en-us/library/windows/desktop/bb773381(v=vs.85).aspx
	/// </remarks>
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct PROPERTYKEY : IEquatable<PROPERTYKEY>
	{
		/// <summary>
		/// <see cref="PROPERTYKEY"/>の新しいインスタンスを初期化します。
		/// </summary>
		/// <param name="formatId">プロパティの<see cref="Guid"/>。</param>
		/// <param name="propId">プロパティID(PID)。</param>
		public PROPERTYKEY(Guid formatId, UInt32 propId)
			: this()
		{
			this.fmtid = formatId;
			this.pid = propId;
		}

		/// <summary>
		/// プロパティの<see cref="Guid"/>を取得します。
		/// </summary>
		public Guid fmtid { get; private set; }

		/// <summary>
		///  プロパティIDを取得します。
		/// </summary>
		public UInt32 pid { get; private set; }

		/// <summary>
		/// 指定した<see cref="PROPERTYKEY"/>の値が、現在の<see cref="PROPERTYKEY"/>と等しいかどうかを判定します。
		/// </summary>
		/// <param name="other">現在の<see cref="PROPERTYKEY"/>と比較する<see cref="PROPERTYKEY"/>。</param>
		/// <returns>
		/// <paramref name="other"/>と現在の<see cref="PROPERTYKEY"/>が等しい場合は<c>true</c>。
		/// それ以外の場合は<c>false</c>。
		/// </returns>
		public bool Equals(PROPERTYKEY other)
		{
			return this.fmtid.Equals(other.fmtid) && (this.pid == other.pid);
		}

		/// <summary>
		/// 指定したオブジェクトの値が、現在の<see cref="PROPERTYKEY"/>と等しいかどうかを判定します。
		/// </summary>
		/// <param name="obj">現在の<see cref="PROPERTYKEY"/>と比較するオブジェクト。</param>
		/// <returns>
		/// <paramref name="obj"/>と現在の<see cref="PROPERTYKEY"/>が等しい場合は<c>true</c>。
		/// それ以外の場合は<c>false</c>。
		/// </returns>
		public override bool Equals(object obj)
		{
			var result = false;
			if (obj is PROPERTYKEY)
			{
				var other = (PROPERTYKEY)obj;
				result = this.Equals(other);
			}
			return result;
		}

		/// <summary>
		/// 2つの<see cref="PROPERTYKEY"/>を比較して、等しいかどうかを判定します。
		/// </summary>
		/// <param name="left">1つめの<see cref="PROPERTYKEY"/>。</param>
		/// <param name="right">2つめの<see cref="PROPERTYKEY"/>。</param>
		/// <returns>
		/// 2つの<see cref="PROPERTYKEY"/>が等しい場合は<c>true</c>。
		/// それ以外の場合は<c>false</c>。
		/// </returns>
		public static bool operator ==(PROPERTYKEY left, PROPERTYKEY right)
		{
			return left.Equals(right);
		}

		/// <summary>
		/// 2つの<see cref="PROPERTYKEY"/>を比較して、等しくないかどうかを判定します。
		/// </summary>
		/// <param name="left">1つめの<see cref="PROPERTYKEY"/>。</param>
		/// <param name="right">2つめの<see cref="PROPERTYKEY"/>。</param>
		/// <returns>
		/// 2つの<see cref="PROPERTYKEY"/>が等しい場合は<c>true</c>。
		/// それ以外の場合は<c>false</c>。
		/// </returns>
		public static bool operator !=(PROPERTYKEY left, PROPERTYKEY right)
		{
			return !(left == right);
		}

		/// <summary>
		/// このインスタンスのハッシュコードを取得します。
		/// </summary>
		/// <returns>ハッシュコード。</returns>
		public override int GetHashCode()
		{
			return this.fmtid.GetHashCode() ^ this.pid.GetHashCode();
		}

		/// <summary>
		/// このインスタンスの文字列表現を取得します。
		/// </summary>
		/// <returns>このインスタンスの文字列表現。</returns>
		public override string ToString()
		{
			return string.Format(System.Globalization.CultureInfo.InvariantCulture,
				"PROPERTYKEY: {{fmtid={0}, pid={1}}}",
				this.fmtid.ToString("B"), this.pid);
		}
	}
}