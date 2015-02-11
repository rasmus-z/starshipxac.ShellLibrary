using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Runtime.InteropServices;

using starshipxac.Shell.Interop;
using starshipxac.Shell.Interop.KnownFolder;
using starshipxac.Shell.Media.Imaging;
using starshipxac.Shell.Resources;

namespace starshipxac.Shell.Internal
{
	/// <summary>
	/// �W���t�H���_�[�̃v���p�e�B���`���܂��B
	/// </summary>
	internal class FolderProperties
	{
		/// <summary>
		/// <para>
		/// �W���t�H���_�[�C���^�[�t�F�C�X���w�肵�āA
		/// <see cref="FolderProperties"/>�N���X�̐V�����C���X�^���X�����������܂��B
		/// </para>
		/// </summary>
		/// <param name="knownFolderInterface">�W���t�H���_�[�C���^�[�t�F�C�X�B</param>
		internal FolderProperties(IKnownFolder knownFolderInterface)
		{
			Contract.Requires<ArgumentNullException>(knownFolderInterface != null);

			Initialize(knownFolderInterface);
		}

		[ContractInvariantMethod]
		private void ObjectInvariant()
		{
			Contract.Invariant(this.ParsingName != null);
			Contract.Invariant(this.CanonicalName != null);
			Contract.Invariant(this.Description != null);
			Contract.Invariant(this.RelativePath != null);
		}

		public string ParsingName { get; private set; }
		public string CanonicalName { get; private set; }
		public KnownFolderCategories Category { get; private set; }
		public string Description { get; private set; }
		public Guid ParentId { get; private set; }
		public string Parent { get; private set; }
		public string RelativePath { get; private set; }
		public StringReference ToolTipResource { get; private set; }
		public string ToolTip { get; private set; }
		public StringReference LocalizedNameResource { get; private set; }
		public string LocalizedName { get; private set; }
		public IconReference IconResource { get; private set; }
		public ShellIcon Icon { get; private set; }
		public FolderDefinitionFlags FolderDefinitionFlags { get; private set; }
		public FileAttributes FileAttributes { get; private set; }
		public Guid FolderTypeId { get; private set; }
		public string FolderType { get; private set; }
		public Guid FolderId { get; private set; }
		public string Path { get; private set; }
		public bool PathExists { get; private set; }
		public RedirectionCapability Redirection { get; private set; }
		public string Security { get; private set; }

		/// <summary>
		/// <see cref="KNOWNFOLDER_DEFINITION"/>���擾���A
		/// �e�v���p�e�B�����������܂��B
		/// </summary>
		/// <param name="knownFolderInterface"></param>
		private void Initialize(IKnownFolder knownFolderInterface)
		{
			Contract.Requires(knownFolderInterface != null);

			KNOWNFOLDER_DEFINITION knownFolderDefinition;
			knownFolderInterface.GetFolderDefinition(out knownFolderDefinition);

			try
			{
				this.ParsingName = PtrToString(knownFolderDefinition.pszParsingName);
				this.CanonicalName = PtrToString(knownFolderDefinition.pszName);
				this.Category = (KnownFolderCategories)knownFolderDefinition.category;
				this.Description = PtrToString(knownFolderDefinition.pszDescription);
				this.ParentId = knownFolderDefinition.fidParent;
				this.RelativePath = PtrToString(knownFolderDefinition.pszRelativePath);

				InitializeResourceProperties(knownFolderDefinition);

				this.FolderDefinitionFlags = (FolderDefinitionFlags)knownFolderDefinition.kfdFlags;
				this.FileAttributes = (FileAttributes)knownFolderDefinition.dwAttributes;
				this.FolderTypeId = knownFolderDefinition.ftidType;
				this.FolderType = String.Empty;
				this.FolderId = knownFolderInterface.GetId();

				InitializePath(knownFolderInterface);

				this.Redirection = (RedirectionCapability)knownFolderInterface.GetRedirectionCapabilities();
				this.Security = PtrToString(knownFolderDefinition.pszSecurity);
			}
			finally
			{
				ShellNativeMethods.FreeKnownFolderDefinitionFields(ref knownFolderDefinition);
			}
		}

		/// <summary>
		/// <see cref="KNOWNFOLDER_DEFINITION"/>���烊�\�[�X�����擾���A�v���p�e�B�����������܂��B
		/// </summary>
		/// <param name="knownFolderDefinition"></param>
		private void InitializeResourceProperties(KNOWNFOLDER_DEFINITION knownFolderDefinition)
		{
			if (knownFolderDefinition.pszTooltip != IntPtr.Zero)
			{
				this.ToolTipResource = new StringReference(PtrToString(knownFolderDefinition.pszTooltip));
				this.ToolTip = this.ToolTipResource.LoadString();
			}
			else
			{
				this.ToolTipResource = null;
				this.ToolTip = String.Empty;
			}

			if (knownFolderDefinition.pszLocalizedName != IntPtr.Zero)
			{
				this.LocalizedNameResource = new StringReference(PtrToString(knownFolderDefinition.pszLocalizedName));
				this.LocalizedName = this.LocalizedNameResource.LoadString();
			}
			else
			{
				this.LocalizedNameResource = null;
				this.LocalizedName = String.Empty;
			}

			if (knownFolderDefinition.pszIcon != IntPtr.Zero)
			{
				this.IconResource = new IconReference(PtrToString(knownFolderDefinition.pszIcon));
				this.Icon = this.IconResource.LoadIcon();
			}
			else
			{
				this.IconResource = null;
				this.Icon = null;
			}
		}

		/// <summary>
		/// �p�X�������������܂��B
		/// </summary>
		/// <param name="knownFolderInterface"></param>
		private void InitializePath(IKnownFolder knownFolderInterface)
		{
			Contract.Requires(knownFolderInterface != null);

			this.Path = String.Empty;

			if (this.Category == KnownFolderCategories.Virtual)
			{
				this.PathExists = false;
			}
			else
			{
				try
				{
					this.Path = knownFolderInterface.GetPath(0);
					this.PathExists = true;
				}
				catch (FileNotFoundException)
				{
					this.PathExists = false;
				}
				catch (DirectoryNotFoundException)
				{
					this.PathExists = false;
				}
			}
		}

		private static string PtrToString(IntPtr ptr)
		{
			return Marshal.PtrToStringUni(ptr) ?? String.Empty;
		}
	}
}