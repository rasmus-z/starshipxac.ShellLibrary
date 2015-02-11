using System;
using System.Runtime.InteropServices;

namespace starshipxac.Windows.Shell.Controls.Interop
{
	/// <summary>
	/// �C���[�W���X�g�`��p�����[�^�[��ێ����܂��B
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	internal struct IMAGELISTDRAWPARAMS
	{
		public UInt32 cbSize;
		public IntPtr himl;
		public int i;
		public IntPtr hdcDst;
		public int x;
		public int y;
		public int cx;
		public int cy;
		public int xBitmap;
		public int yBitmap;
		public int rgbBk;
		public int rgbFg;
		public UInt32 fStyle;
		public UInt32 dwRop;
		public UInt32 fState;
		public UInt32 Frame;
		public UInt32 crEffect;
	}
}