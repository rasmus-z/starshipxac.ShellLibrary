using System;
using System.Runtime.InteropServices;
using starshipxac.Shell.Components.Interop;
using starshipxac.Shell.Interop;

namespace starshipxac.Shell.Components.Internal
{
	/// <summary>
	/// �V�F���ύX�ʒm����ێ����܂��B
	/// </summary>
	internal class ShellChangeNotify
	{
		/// <summary>
		/// <see cref="ShellChangeNotify"/>�N���X�̐V�����C���X�^���X�����������܂��B
		/// </summary>
		/// <param name="wParam"><c>WPARAM</c>�B</param>
		/// <param name="lParam"><c>LPARAM</c>�B</param>
		internal ShellChangeNotify(IntPtr wParam, IntPtr lParam)
		{
			var hwnd = wParam;
			var processId = (UInt32)lParam.ToInt64();
			IntPtr pidl;
			uint lEvent;
			var lockId = ShellWatcherNativeMethods.SHChangeNotification_Lock(hwnd, processId, out pidl, out lEvent);
			try
			{
				this.ChangeType = (ShellChangeTypes)lEvent;
				var notifyStruct = (ShellNotifyStruct)Marshal.PtrToStructure(pidl, typeof(ShellNotifyStruct));

				var guid = new Guid(ShellIID.IShellItem2);

				// dwItem1
				if (notifyStruct.item1 != IntPtr.Zero &&
				    (this.ChangeType & ShellChangeTypes.SystemImageUpdate) == ShellChangeTypes.None)
				{
					var shellObject = CreateShellObject(notifyStruct.item1, ref guid);
					if (shellObject != null)
					{
						this.ShellObject = shellObject;
					}
				}
				else
				{
					ImageIndex = notifyStruct.item1.ToInt32();
				}

				// dwItem2
				if (notifyStruct.item2 != IntPtr.Zero)
				{
					var shellObject = CreateShellObject(notifyStruct.item2, ref guid);
					if (shellObject != null)
					{
						this.ShellObject2 = shellObject;
					}
				}
			}
			finally
			{
				if (lockId != IntPtr.Zero)
				{
					ShellWatcherNativeMethods.SHChangeNotification_Unlock(lockId);
				}
			}
		}

		/// <summary>
		/// �V�F���ύX�ʒm��ʂ��擾���܂��B
		/// </summary>
		public ShellChangeTypes ChangeType { get; private set; }

		/// <summary>
		/// ���������C�x���g���V�X�e���C�x���g���ǂ����𔻒肷��l���擾���܂��B
		/// </summary>
		public bool FromSystemInterrupt
		{
			get
			{
				return (this.ChangeType & ShellChangeTypes.FromInterrupt) != ShellChangeTypes.None;
			}
		}

		/// <summary>
		/// <see cref="ShellObject"/>���擾���܂��B
		/// </summary>
		public ShellObject ShellObject { get; private set; }

		/// <summary>
		/// 2�߂�<see cref="ShellObject"/>���擾���܂��B
		/// </summary>
		public ShellObject ShellObject2 { get; private set; }

		/// <summary>
		/// �C���[�W�̃C���f�b�N�X���擾���܂��B
		/// </summary>
		public int ImageIndex { get; private set; }

		/// <summary>
		/// <see cref="ShellObject"/>���쐬���܂��B
		/// </summary>
		/// <param name="pidl"><c>PIDL</c>�B</param>
		/// <param name="riid"><c>GUID</c>�B</param>
		/// <returns></returns>
		private static ShellObject CreateShellObject(IntPtr pidl, ref Guid riid)
		{
			IShellItem2 shellItem2;
			var code = ShellNativeMethods.SHCreateItemFromIDList(pidl, ref riid, out shellItem2);
			if (HRESULT.Failed(code))
			{
				return null;
			}

			return ShellFactory.Create(shellItem2);
		}
	}
}