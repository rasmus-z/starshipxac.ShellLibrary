using System;
using System.Runtime.InteropServices;

namespace starshipxac.Shell.PropertySystem.Interop
{
	/// <summary>
	/// �v���p�e�B�L�[���`���܂��B
	/// </summary>
	/// <remarks>
	/// http://msdn.microsoft.com/en-us/library/windows/desktop/bb773381(v=vs.85).aspx
	/// </remarks>
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct PROPERTYKEY : IEquatable<PROPERTYKEY>
	{
		/// <summary>
		/// <see cref="PROPERTYKEY"/>�̐V�����C���X�^���X�����������܂��B
		/// </summary>
		/// <param name="formatId">�v���p�e�B��<see cref="Guid"/>�B</param>
		/// <param name="propId">�v���p�e�BID(PID)�B</param>
		public PROPERTYKEY(Guid formatId, UInt32 propId)
			: this()
		{
			this.fmtid = formatId;
			this.pid = propId;
		}

		/// <summary>
		/// �v���p�e�B��<see cref="Guid"/>���擾���܂��B
		/// </summary>
		public Guid fmtid { get; private set; }

		/// <summary>
		///  �v���p�e�BID���擾���܂��B
		/// </summary>
		public UInt32 pid { get; private set; }

		/// <summary>
		/// �w�肵��<see cref="PROPERTYKEY"/>�̒l���A���݂�<see cref="PROPERTYKEY"/>�Ɠ��������ǂ����𔻒肵�܂��B
		/// </summary>
		/// <param name="other">���݂�<see cref="PROPERTYKEY"/>�Ɣ�r����<see cref="PROPERTYKEY"/>�B</param>
		/// <returns>
		/// <paramref name="other"/>�ƌ��݂�<see cref="PROPERTYKEY"/>���������ꍇ��<c>true</c>�B
		/// ����ȊO�̏ꍇ��<c>false</c>�B
		/// </returns>
		public bool Equals(PROPERTYKEY other)
		{
			return this.fmtid.Equals(other.fmtid) && (this.pid == other.pid);
		}

		/// <summary>
		/// �w�肵���I�u�W�F�N�g�̒l���A���݂�<see cref="PROPERTYKEY"/>�Ɠ��������ǂ����𔻒肵�܂��B
		/// </summary>
		/// <param name="obj">���݂�<see cref="PROPERTYKEY"/>�Ɣ�r����I�u�W�F�N�g�B</param>
		/// <returns>
		/// <paramref name="obj"/>�ƌ��݂�<see cref="PROPERTYKEY"/>���������ꍇ��<c>true</c>�B
		/// ����ȊO�̏ꍇ��<c>false</c>�B
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
		/// 2��<see cref="PROPERTYKEY"/>���r���āA���������ǂ����𔻒肵�܂��B
		/// </summary>
		/// <param name="left">1�߂�<see cref="PROPERTYKEY"/>�B</param>
		/// <param name="right">2�߂�<see cref="PROPERTYKEY"/>�B</param>
		/// <returns>
		/// 2��<see cref="PROPERTYKEY"/>���������ꍇ��<c>true</c>�B
		/// ����ȊO�̏ꍇ��<c>false</c>�B
		/// </returns>
		public static bool operator ==(PROPERTYKEY left, PROPERTYKEY right)
		{
			return left.Equals(right);
		}

		/// <summary>
		/// 2��<see cref="PROPERTYKEY"/>���r���āA�������Ȃ����ǂ����𔻒肵�܂��B
		/// </summary>
		/// <param name="left">1�߂�<see cref="PROPERTYKEY"/>�B</param>
		/// <param name="right">2�߂�<see cref="PROPERTYKEY"/>�B</param>
		/// <returns>
		/// 2��<see cref="PROPERTYKEY"/>���������ꍇ��<c>true</c>�B
		/// ����ȊO�̏ꍇ��<c>false</c>�B
		/// </returns>
		public static bool operator !=(PROPERTYKEY left, PROPERTYKEY right)
		{
			return !(left == right);
		}

		/// <summary>
		/// ���̃C���X�^���X�̃n�b�V���R�[�h���擾���܂��B
		/// </summary>
		/// <returns>�n�b�V���R�[�h�B</returns>
		public override int GetHashCode()
		{
			return this.fmtid.GetHashCode() ^ this.pid.GetHashCode();
		}

		/// <summary>
		/// ���̃C���X�^���X�̕�����\�����擾���܂��B
		/// </summary>
		/// <returns>���̃C���X�^���X�̕�����\���B</returns>
		public override string ToString()
		{
			return string.Format(System.Globalization.CultureInfo.InvariantCulture,
				"PROPERTYKEY: {{fmtid={0}, pid={1}}}",
				this.fmtid.ToString("B"), this.pid);
		}
	}
}