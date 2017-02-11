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
    ///     Define icon resource reference class.
    /// </summary>
    public class IconReference : ResourceReference
    {
        /// <summary>
        ///     Initialize a new instance of the <see cref="IconReference" /> class
        ///     to the specified library name and resource ID.
        /// </summary>
        /// <param name="libraryName">Library name of executable file or DLL file, icon file.</param>
        /// <param name="resourceId">The index of the icon.</param>
        public IconReference(string libraryName, int resourceId)
            : base(libraryName, resourceId)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(libraryName));
        }

        /// <summary>
        ///     Initialize a new instance of the <see cref="IconReference" /> class
        ///     to the specified reference path.
        /// </summary>
        /// <param name="referencePath">A comma-separated library name and resource ID.</param>
        public IconReference(string referencePath)
            : base(referencePath)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(referencePath));
        }

        /// <summary>
        ///     Get the reference path.
        /// </summary>
        /// <returns></returns>
        protected override string GetReferencePath()
        {
            return String.Format(CultureInfo.InvariantCulture, "{0},{1}", this.LibraryPath, this.ReferencePath);
        }

        /// <summary>
        ///     Parse reference path.
        /// </summary>
        /// <param name="libraryPath">Library path.</param>
        /// <param name="resourceId">Resource ID.</param>
        protected override void ParseReferencePath(out string libraryPath, out int resourceId)
        {
            var path = new StringBuilder(this.ReferencePath, 256);
            resourceId = ShellLightwaightNativeMethods.PathParseIconLocation(path);
            libraryPath = Environment.ExpandEnvironmentVariables(path.ToString());
        }

        /// <summary>
        ///     Load icon from resource.
        /// </summary>
        /// <param name="size">Icon size.</param>
        /// <returns><see cref="ShellIcon"/>.</returns>
        public ShellIcon LoadIcon(int size = 0)
        {
            Contract.Ensures(Contract.Result<ShellIcon>() != null);

            var smallIcons = new[] {IntPtr.Zero};
            var largeIcons = new[] {IntPtr.Zero};
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