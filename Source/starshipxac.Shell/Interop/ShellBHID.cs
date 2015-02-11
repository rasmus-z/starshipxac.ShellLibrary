using System;

namespace starshipxac.Shell.Interop
{
	/// <summary>
	/// ハンドラー作成IDを定義します。
	/// </summary>
	/// <remarks>
	/// http://msdn.microsoft.com/en-us/library/windows/desktop/bb761134(v=vs.85).aspx
	/// </remarks>
	internal static class ShellBHID
	{
		/// <summary>
		/// <pre>{3981e224-f559-11d3-8e3a-00c04f6837d5}</pre>
		/// </summary>
		public static readonly Guid BHID_SFObject = new Guid(0x3981e224, 0xf559, 0x11d3, 0x8e, 0x3a, 0x00, 0xc0, 0x4f, 0x68, 0x37, 0xd5);

		/// <summary>
		/// </summary>
		public static readonly Guid BHID_SFUIObject = new Guid(0x3981e225, 0xf559, 0x11d3, 0x8e, 0x3a, 0x00, 0xc0, 0x4f, 0x68, 0x37, 0xd5);

		/// <summary>
		/// <pre>{3981e226-f559-11d3-8e3a-00c04f6837d5}</pre>
		/// </summary>
		public static readonly Guid BHID_SFViewObject = new Guid(0x3981e226, 0xf559, 0x11d3, 0x8e, 0x3a, 0x00, 0xc0, 0x4f, 0x68, 0x37, 0xd5);

		/// <summary>
		/// <pre>{3981e227-f559-11d3-8e3a-00c04f6837d5}</pre>
		/// </summary>
		public static readonly Guid BHID_Storage = new Guid(0x3981e227, 0xf559, 0x11d3, 0x8e, 0x3a, 0x00, 0xc0, 0x4f, 0x68, 0x37, 0xd5);

		/// <summary>
		/// <pre>{1CEBB3AB-7C10-499a-A417-92CA16C4CB83}</pre>
		/// </summary>
		public static readonly Guid BHID_Stream = new Guid(0x1cebb3ab, 0x7c10, 0x499a, 0xa4, 0x17, 0x92, 0xca, 0x16, 0xc4, 0xcb, 0x83);

		/// <summary>
		/// <pre>Object: IRandomAccessStream</pre>
		/// <pre>{f16fc93b-77ae-4cfe-bda7-a866eea6878d}</pre>
		/// </summary>
		public static readonly Guid BHID_RandomAccessStream = new Guid(0xf16fc93b, 0x77ae, 0x4cfe, 0xbd, 0xa7, 0xa8, 0x66, 0xee, 0xa6, 0x87, 0x8d);

		/// <summary>
		/// <pre>{3981e228-f559-11d3-8e3a-00c04f6837d5}</pre>
		/// </summary>
		public static readonly Guid BHID_LinkTargetItem = new Guid(0x3981e228, 0xf559, 0x11d3, 0x8e, 0x3a, 0x00, 0xc0, 0x4f, 0x68, 0x37, 0xd5);

		/// <summary>
		/// <pre>{4621A4E3-F0D6-4773-8A9C-46E77B174840}</pre>
		/// </summary>
		public static readonly Guid BHID_StorageEnum = new Guid(0x4621a4e3, 0xf0d6, 0x4773, 0x8a, 0x9c, 0x46, 0xe7, 0x7b, 0x17, 0x48, 0x40);

		/// <summary>
		/// <pre>{5D080304-FE2C-48fc-84CE-CF620B0F3C53}</pre>
		/// </summary>
		public static readonly Guid BHID_Transfer = new Guid(0xd5e346a1, 0xf753, 0x4932, 0xb4, 0x3, 0x45, 0x74, 0x80, 0xe, 0x24, 0x98);

		/// <summary>
		/// <pre>{0384e1a4-1523-439c-a4c8-ab911052f586}</pre>
		/// </summary>
		public static readonly Guid BHID_PropertyStore = new Guid(0x0384e1a4, 0x1523, 0x439c, 0xa4, 0xc8, 0xab, 0x91, 0x10, 0x52, 0xf5, 0x86);

		/// <summary>
		/// <pre>{7b2e650a-8e20-4f4a-b09e-6597afc72fb0}</pre>
		/// </summary>
		public static readonly Guid BHID_ThumbnailHandler = new Guid(0x7b2e650a, 0x8e20, 0x4f4a, 0xb0, 0x9e, 0x65, 0x97, 0xaf, 0xc7, 0x2f, 0xb0);

		/// <summary>
		/// <pre>{94f60519-2850-4924-aa5a-d15e84868039}</pre>
		/// </summary>
		public static readonly Guid BHID_EnumItems = new Guid(0x94f60519, 0x2850, 0x4924, 0xaa, 0x5a, 0xd1, 0x5e, 0x84, 0x86, 0x80, 0x39);

		/// <summary>
		/// <pre>{B8C0BD9F-ED24-455c-83E6-D5390C4FE8C4}</pre>
		/// </summary>
		public static readonly Guid BHID_DataObject = new Guid(0xb8c0bd9f, 0xed24, 0x455c, 0x83, 0xe6, 0xd5, 0x39, 0xc, 0x4f, 0xe8, 0xc4);

		/// <summary>
		/// <pre>{bea9ef17-82f1-4f60-9284-4f8db75c3be9}</pre>
		/// </summary>
		public static readonly Guid BHID_AssociationArray = new Guid(0xbea9ef17, 0x82f1, 0x4f60, 0x92, 0x84, 0x4f, 0x8d, 0xb7, 0x5c, 0x3b, 0xe9);

		/// <summary>
		/// <pre>{38d08778-f557-4690-9ebf-ba54706ad8f7}</pre>
		/// </summary>
		public static readonly Guid BHID_Filter = new Guid(0x38d08778, 0xf557, 0x4690, 0x9e, 0xbf, 0xba, 0x54, 0x70, 0x6a, 0xd8, 0xf7);

		/// <summary>
		/// <pre>{b8ab0b9c-c2ec-4f7a-918d-314900e6280a}</pre>
		/// </summary>
		public static readonly Guid BHID_EnumAssocHandlers = new Guid(0xb8ab0b9c, 0xc2ec, 0x4f7a, 0x91, 0x8d, 0x31, 0x49, 0x00, 0xe6, 0x28, 0x0a);

		/// <summary>
		/// {8677DCEB-AAE0-4005-8D3D-547FA852F825}
		/// </summary>
		public static readonly Guid BHID_FilePlaceholder = new Guid(0x8677dceb, 0xaae0, 0x4005, 0x8d, 0x3d, 0x54, 0x7f, 0xa8, 0x52, 0xf8, 0x25);
	}
}