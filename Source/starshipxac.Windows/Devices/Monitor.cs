using System;
using System.Diagnostics.Contracts;
using System.Windows;

namespace starshipxac.Windows.Devices
{
    /// <summary>
    /// ���j�^�[����ێ����܂��B
    /// </summary>
    public class Monitor : IEquatable<Monitor>
    {
        /// <summary>
        /// ���j�^�[�n���h�����w�肵�āA
        /// <see cref="Monitor"/>�N���X�̐V�����C���X�^���X�����������܂��B
        /// </summary>
        /// <param name="hMonitor">���j�^�[�n���h���B</param>
        internal Monitor(IntPtr hMonitor)
        {
            Contract.Requires<ArgumentNullException>(hMonitor != IntPtr.Zero);

            this.Handle = hMonitor;
        }

        /// <summary>
        /// ���j�^�[�n���h�����擾���܂��B
        /// </summary>
        internal IntPtr Handle { get; private set; }

        /// <summary>
        /// �f�o�C�X�����擾���܂��B
        /// </summary>
        public string DeviceName { get; internal set; }

        /// <summary>
        /// ��ꃂ�j�^�[���ǂ����𔻒肷��l���擾���܂��B
        /// </summary>
        public bool IsPrimary { get; internal set; }

        /// <summary>
        /// ���j�^�[�̃T�C�Y���擾���܂��B
        /// </summary>
        public Rect Bounds { get; internal set; }

        /// <summary>
        /// ���j�^�[���̃A�v���P�[�V��������̈�̃T�C�Y���擾���܂��B
        /// </summary>
        public Rect WorkingArea { get; internal set; }

        public static bool operator ==(Monitor x, Monitor y)
        {
            return Equals(x, y);
        }

        public static bool operator !=(Monitor x, Monitor y)
        {
            return !Equals(x, y);
        }

        public bool Equals(Monitor other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return this.Handle.Equals(other.Handle);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (this.GetType() != obj.GetType())
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            return Equals((Monitor)obj);
        }

        public override int GetHashCode()
        {
            return this.Handle.GetHashCode();
        }

        public override string ToString()
        {
            return String.Format("Monitor: {{DeviceName={0}}}", this.DeviceName);
        }
    }
}