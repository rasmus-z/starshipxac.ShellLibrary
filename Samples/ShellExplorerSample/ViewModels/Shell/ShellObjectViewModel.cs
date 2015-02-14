using System;
using System.Diagnostics.Contracts;
using Livet;
using starshipxac.Shell;
using starshipxac.Windows.Shell.Media.Imaging;

namespace ShellExplorerSample.ViewModels.Shell
{
	public class ShellObjectViewModel : ViewModel
	{
		private readonly string parsingName;
		private string displayName;
		private string itemTypeText;
		private DateTime? dateCreated;
		private DateTime? dateModified;

		public ShellObjectViewModel(ShellObject shellObject, ShellFolderViewModel parentFolder)
		{
			Contract.Requires<ArgumentNullException>(shellObject != null);
			Contract.Requires<ArgumentNullException>(parentFolder != null);

			this.ShellObject = shellObject;
			this.ParentFolder = parentFolder;
			this.ThumbnailFactory = this.ParentFolder.ThumbnailFactory;

			this.parsingName = this.ShellObject.ParsingName;
		}

		protected ShellObjectViewModel(ShellObject shellObject, ShellThumbnailFactory thumbnailFactory)
		{
			Contract.Requires<ArgumentNullException>(shellObject != null);

			this.ShellObject = shellObject;
			this.ParentFolder = null;
			this.ThumbnailFactory = thumbnailFactory;

			this.parsingName = this.ShellObject.ParsingName;
		}

		internal ShellObjectViewModel(ShellFolderViewModel parentFolder)
		{
			this.ShellObject = null;
			this.ParentFolder = parentFolder;
			this.ThumbnailFactory = parentFolder.ThumbnailFactory;

			this.parsingName = String.Empty;
			this.displayName = String.Empty;
			this.itemTypeText = String.Empty;
			this.dateCreated = DateTime.MinValue;
			this.dateModified = DateTime.MinValue;
		}

		[ContractInvariantMethod]
		private void ObjectInvariant()
		{
			Contract.Invariant(this.parsingName != null);
		}

		public ShellObject ShellObject { get; private set; }

		public ShellFolderViewModel ParentFolder { get; private set; }

		public ShellThumbnailFactory ThumbnailFactory { get; private set; }

		public virtual string ParsingName
		{
			get
			{
				return this.parsingName;
			}
		}

		public virtual string DisplayName
		{
			get
			{
				Contract.Ensures(Contract.Result<string>() != null);
				if (this.displayName == null)
				{
					this.displayName = this.ShellObject.DisplayName;
				}
				return this.displayName;
			}
		}

		public string ItemTypeText
		{
			get
			{
				Contract.Ensures(Contract.Result<string>() != null);
				if (this.itemTypeText == null)
				{
					var itemTypeTextProperty = this.ShellObject.Properties.Create<string>("System.ItemTypeText");
					this.itemTypeText = itemTypeTextProperty.Value;
				}
				return this.itemTypeText;
			}
		}

		public DateTime DateCreated
		{
			get
			{
				if (!this.dateCreated.HasValue)
				{
					this.dateCreated = this.ShellObject.DateCreated.ToLocalTime();
				}
				return this.dateCreated.Value;
			}
		}

		public DateTime DateModified
		{
			get
			{
				if (!this.dateModified.HasValue)
				{
					this.dateModified = this.ShellObject.DateModified.ToLocalTime();
				}
				return this.dateModified.Value;
			}
		}

		#region Thumbnail Property

		private ShellThumbnail thumbnail;

		public ShellThumbnail Thumbnail
		{
			get
			{
				if (this.ShellObject != null && this.thumbnail == null)
				{
					if (this.ThumbnailFactory != null)
					{
						this.thumbnail = new ShellThumbnail(this.ShellObject, this.ThumbnailFactory);
					}
				}
				return this.thumbnail;
			}
		}

		#endregion

		public override string ToString()
		{
			return String.Format("{0}: {{ ParsingName={1} }}",
				this.GetType().Name,
				this.ParsingName);
		}
	}
}