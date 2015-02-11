using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Runtime.InteropServices;
using starshipxac.Shell.Interop;

namespace starshipxac.Shell.Internal
{
	/// <summary>
	/// �t�H���_�[���̃A�C�e���𔽕��������܂��B
	/// </summary>
	internal class ShellFolderEnumerator : IEnumerator<ShellObject>
	{
		private IEnumIDList enumIdList;

		/// <summary>
		/// <see cref="ShellFolderEnumerator"/>�N���X�̐V�����C���X�^���X�����������܂��B
		/// </summary>
		/// <param name="parentFolder">�e�t�H���_�[�B</param>
		/// <param name="options">�t�H���_�[���������I�v�V�����B </param>
		/// <remarks>
		/// <para>
		/// <see cref="System.IO.FileNotFoundException"/>����������ꍇ�́A
		/// �v���W�F�N�g�̃v���p�e�B -> �r���h�́u32�r�b�g��D��v�̃`�F�b�N���O���B
		/// </para>
		/// </remarks>
		internal ShellFolderEnumerator(ShellFolder parentFolder, SHCONTF options)
		{
			Contract.Requires<ArgumentNullException>(parentFolder != null);

			this.Parent = parentFolder;

			var hr = this.Parent.ShellFolderInterface.EnumObjects(
				IntPtr.Zero,
				options,
				out this.enumIdList);
			if (hr == COMErrorCodes.Cancelled)
			{
				var inner = Marshal.GetExceptionForHR(hr);
				throw new DirectoryNotFoundException(inner.Message, inner);
			}
			else if (HRESULT.Failed(hr))
			{
				throw ShellException.FromHRESULT(hr);
			}
			// hr == S_FALSE�̏ꍇ�́A�q�����݂��Ȃ��B(enumIdList = null)
		}

		/// <summary>
		/// <see cref="ShellFolderEnumerator"/>�N���X�̐V�����C���X�^���X�����������܂��B
		/// </summary>
		/// <param name="parentFolder">�e�t�H���_�[�B</param>
		internal ShellFolderEnumerator(ShellFolder parentFolder)
			: this(parentFolder, SHCONTF.SHCONTF_FOLDERS | SHCONTF.SHCONTF_NONFOLDERS)
		{
			Contract.Requires<ArgumentNullException>(parentFolder != null);
		}

		public void Dispose()
		{
			if (this.enumIdList != null)
			{
				Marshal.ReleaseComObject(this.enumIdList);
				this.enumIdList = null;
			}
		}

		[ContractInvariantMethod]
		private void ObjectInvaliant()
		{
			Contract.Invariant(this.Parent != null);
		}

		/// <summary>
		/// �e�t�H���_�[���擾���܂��B
		/// </summary>
		public ShellFolder Parent { get; private set; }

		public ShellObject Current { get; private set; }

		object System.Collections.IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		public bool MoveNext()
		{
			if (this.enumIdList == null)
			{
				return false;
			}

			const uint itemsRequested = 1;
			IntPtr item;
			uint numItemsReturned;
			var hr = this.enumIdList.Next(itemsRequested, out item, out numItemsReturned);
			if (HRESULT.Failed(hr) || numItemsReturned < itemsRequested)
			{
				return false;
			}

			this.Current = ShellFactory.Instance.Create(ShellItem.FromIdList(item, this.Parent.ShellFolderInterface));
			return true;
		}

		public void Reset()
		{
			if (this.enumIdList != null)
			{
				this.enumIdList.Reset();
			}
		}
	}
}