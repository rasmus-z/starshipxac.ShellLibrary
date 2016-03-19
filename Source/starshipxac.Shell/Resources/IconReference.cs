using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Text;
using starshipxac.Shell.Interop;
using starshipxac.Shell.Media.Imaging;

namespace starshipxac.Shell.Resources
{
    /// <summary>
    ///     アイコンリソース参照を取得します。
    /// </summary>
    public class IconReference : ResourceReference
    {
        public IconReference(string libraryName, int resourceId)
            : base(libraryName, resourceId)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(libraryName));
        }

        public IconReference(string referencePath)
            : base(referencePath)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(referencePath));
        }

        protected override string GetReferencePath()
        {
            return String.Format(CultureInfo.InvariantCulture, "{0},{1}", this.LibraryPath, this.ReferencePath);
        }

        protected override void ParseReferencePath(out string libraryPath, out int resourceId)
        {
            var path = new StringBuilder(this.ReferencePath, 256);
            resourceId = ShellNativeMethods.PathParseIconLocation(path);
            libraryPath = Environment.ExpandEnvironmentVariables(path.ToString());
        }

        /// <summary>
        ///     リソースからアイコンを取得します。
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public ShellIcon LoadIcon(int size = 0)
        {
            Contract.Ensures(Contract.Result<ShellIcon>() != null);

            var smallIcons = new[] { IntPtr.Zero };
            var largeIcons = new[] { IntPtr.Zero };
            ShellNativeMethods.ExtractIconEx(this.LibraryPath, this.ResourceId, largeIcons, smallIcons, 1);
            var hicon = largeIcons[0];
            if (hicon == IntPtr.Zero)
            {
                throw new Win32Exception();
            }
            return new ShellIcon(hicon);
        }
    }
}