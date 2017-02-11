using System;
using System.Diagnostics.Contracts;
using starshipxac.Shell.PropertySystem;

namespace starshipxac.Shell
{
    /// <summary>
    ///     Define shell link file class.
    /// </summary>
    public class ShellLink : ShellFile
    {
        private ShellProperty<string> titleProperty;
        private ShellProperty<string> targetLocation;
        private ShellProperty<string> linkArgumentsProperty;
        private ShellProperty<string> linkCommentProperty;
        private ShellProperty<string> commentProperty;

        internal static readonly string FileExtension = ".lnk";

        /// <summary>
        ///     Initialize a new instance of the <see cref="ShellLink" /> class.
        /// </summary>
        /// <param name="shellItem"><see cref="ShellItem" />.</param>
        internal ShellLink(ShellItem shellItem)
            : base(shellItem)
        {
            Contract.Requires<ArgumentNullException>(shellItem != null);
        }

        /// <summary>
        ///     Get or set target path.
        /// </summary>
        public string TargetLocation
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                if (this.targetLocation == null)
                {
                    this.targetLocation = new ShellProperty<string>(this, new Guid("{B9B4B3FC-2B51-4A42-B5D8-324146AFCF25}"), 2);
                }
                return this.targetLocation.Value;
            }
            set
            {
                Contract.Requires<ArgumentNullException>(value != null);
                if (this.targetLocation == null)
                {
                    this.targetLocation = new ShellProperty<string>(this, new Guid("{B9B4B3FC-2B51-4A42-B5D8-324146AFCF25}"), 2);
                }
                this.targetLocation.Value = value;
            }
        }

        /// <summary>
        ///     Get the target <see cref="ShellObject" />.
        /// </summary>
        public ShellObject Target
        {
            get
            {
                Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(this.TargetLocation));
                Contract.Ensures(Contract.Result<ShellObject>() != null);
                return ShellFactory.FromParsingName(this.TargetLocation);
            }
        }

        /// <summary>
        ///     Get the link file title.
        /// </summary>
        public string Title
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                if (this.titleProperty == null)
                {
                    this.titleProperty = new ShellProperty<string>(this, new Guid("{F29F85E0-4FF9-1068-AB91-08002B27B3D9}"), 2);
                }
                return this.titleProperty.GetValue(String.Empty);
            }
        }

        /// <summary>
        ///     Get the arguments set in the shell link file.
        /// </summary>
        public string Arguments
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                if (this.linkArgumentsProperty == null)
                {
                    this.linkArgumentsProperty = new ShellProperty<string>(this, new Guid("{436F2667-14E2-4FEB-B30A-146C53B5B674}"), 100);
                }
                return this.linkArgumentsProperty.GetValue(String.Empty);
            }
        }

        /// <summary>
        ///     Get the comment set in the shell link file.
        /// </summary>
        public string Comment
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                if (this.linkCommentProperty == null)
                {
                    this.linkCommentProperty = new ShellProperty<string>(this, new Guid("{B9B4B3FC-2B51-4A42-B5D8-324146AFCF25}"), 5);
                }
                if (this.linkCommentProperty.Value != null)
                {
                    return this.linkCommentProperty.Value;
                }

                if (this.commentProperty == null)
                {
                    this.commentProperty = new ShellProperty<string>(this, new Guid("{F29F85E0-4FF9-1068-AB91-08002B27B3D9}"), 6);
                }
                return this.commentProperty.GetValue(String.Empty);
            }
        }
    }
}