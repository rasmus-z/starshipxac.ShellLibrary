using System;
using System.Diagnostics.Contracts;

namespace starshipxac.Windows.Shell.Dialogs.Controls
{
	/// <summary>
	/// �t�@�C���_�C�A���O�̃R���g���[�����N���X���`���܂��B
	/// </summary>
	[ContractClass(typeof(FileDialogControlContract))]
	public abstract class FileDialogControl : IEquatable<FileDialogControl>
	{
		public static readonly int MinDialogControlId = 16;

		private static int nextId = MinDialogControlId;

		/// <summary>
		/// �R���g���[�������w�肵�āA<see cref="FileDialogControl"/>�N���X�̐V�����C���X�^���X�����������܂��B
		/// </summary>
		/// <param name="name">�R���g���[�����B</param>
		protected FileDialogControl(string name)
		{
			Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(name));

			this.Id = GetNextId();
			this.Name = name;
		}

		[ContractInvariantMethod]
		private void ObjectInvariant()
		{
			Contract.Invariant(this.Name != null);
		}

		public FileDialogBase Dialog { get; private set; }

		/// <summary>
		/// �R���g���[�������擾���܂��B
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// �R���g���[��ID���擾���܂��B
		/// </summary>
		public int Id { get; private set; }

		/// <summary>
		/// �R���g���[�����L�����ǂ����𔻒肷��l���擾�܂��͐ݒ肵�܂��B
		/// </summary>
		public bool Enabled
		{
			get
			{
				ThrowIfNotInitialized();
				return this.Dialog.GetControlEnabled(this);
			}
			set
			{
				ThrowIfNotInitialized();
				this.Dialog.SetControlEnabled(this, value);
			}
		}

		/// <summary>
		/// �R���g���[����\�����邩�ǂ����𔻒肷��l���擾�܂��͐ݒ肵�܂��B
		/// </summary>
		public bool Visible
		{
			get
			{
				ThrowIfNotInitialized();
				return this.Dialog.GetControlVisible(this);
			}
			set
			{
				ThrowIfNotInitialized();
				this.Dialog.SetControlVisible(this, value);
			}
		}

		/// <summary>
		/// �R���g���[���e�L�X�g���擾�܂��͐ݒ肵�܂��B
		/// </summary>
		public abstract string Text { get; set; }

		/// <summary>
		/// ���̃R���g���[��ID���擾���܂��B
		/// </summary>
		/// <returns>�R���g���[��ID�B</returns>
		private static int GetNextId()
		{
			var result = nextId;
			if (nextId == Int32.MaxValue)
			{
				nextId = Int32.MinValue;
			}
			else
			{
				nextId += 1;
			}
			return result;
		}

		internal virtual void Attach(FileDialogBase dialog)
		{
			Contract.Requires<ArgumentNullException>(dialog != null);

			this.Dialog = dialog;
		}

		internal virtual void Detach()
		{
			Contract.Requires<InvalidOperationException>(this.Dialog != null);

			this.Dialog = null;
		}

		protected void ThrowIfNotInitialized()
		{
			if (this.Dialog == null)
			{
				throw new InvalidOperationException();
			}
		}

		public static bool operator ==(FileDialogControl left, FileDialogControl right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(FileDialogControl left, FileDialogControl right)
		{
			return !Equals(left, right);
		}

		public bool Equals(FileDialogControl other)
		{
			if (ReferenceEquals(null, other))
			{
				return false;
			}
			if (ReferenceEquals(this, other))
			{
				return true;
			}
			return this.Id == other.Id;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
			{
				return false;
			}
			if (ReferenceEquals(this, obj))
			{
				return true;
			}
			if (this.GetType() != obj.GetType())
			{
				return false;
			}
			return Equals((FileDialogControl)obj);
		}

		public override int GetHashCode()
		{
			return this.Id.GetHashCode();
		}

		public override string ToString()
		{
			return string.Format("FileDialogControl[Name={0}, Id={1}]", this.Name, this.Id);
		}
	}

	[ContractClassFor(typeof(FileDialogControl))]
	abstract class FileDialogControlContract : FileDialogControl
	{
		protected FileDialogControlContract(string name)
			: base( name)
		{
			Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(name));
		}

		public override string Text
		{
			get
			{
				Contract.Ensures(Contract.Result<string>() != null);
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}
	}
}