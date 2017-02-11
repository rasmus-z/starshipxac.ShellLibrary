using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Text;
using starshipxac.Shell.Interop;
using starshipxac.Shell.Properties;

namespace starshipxac.Shell.Resources
{
    /// <summary>
    ///     Define string resource class.
    /// </summary>
    public class StringReference : ResourceReference
    {
        private static readonly char[] Separator = {','};

        /// <summary>
        ///     Initialize a instance of the <see cref="StringReference" /> class
        ///     to the specified library name and resource ID.
        /// </summary>
        /// <param name="libraryName">Library name of executable file or DLL file.</param>
        /// <param name="resourceId">The index of the string.</param>
        public StringReference(string libraryName, int resourceId)
            : base(libraryName, resourceId)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(libraryName));
        }

        /// <summary>
        ///     Initialize a new instance of the <see cref="StringReference" /> class
        ///     to the specified reference path.
        /// </summary>
        /// <param name="referencePath">A comma-separated library name and resource ID.</param>
        public StringReference(string referencePath)
            : base(referencePath.Replace("shell32,dll", "shell32.dll"))
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(referencePath));
        }

        /// <summary>
        ///     Get the reference path.
        /// </summary>
        /// <returns></returns>
        protected override string GetReferencePath()
        {
            return String.Format(CultureInfo.InstalledUICulture, "{0},{1}", this.LibraryPath, this.ResourceId);
        }

        /// <summary>
        ///     Parse reference path.
        /// </summary>
        /// <param name="libraryPath">Library name of executable file or DLL file.</param>
        /// <param name="resourceId">Resource ID.</param>
        protected override void ParseReferencePath(out string libraryPath, out int resourceId)
        {
            var refParams = this.ReferencePath.Split(Separator);

            if (refParams.Length != 2 || String.IsNullOrWhiteSpace(refParams[0]) || String.IsNullOrWhiteSpace(refParams[1]))
            {
                throw new InvalidOperationException(ErrorMessages.InvalidReferencePath);
            }

            // Library path
            libraryPath = Environment.ExpandEnvironmentVariables(refParams[0].Replace(@"@", String.Empty));
            // Resource ID.
            resourceId = Int32.Parse(refParams[1], CultureInfo.InstalledUICulture);
        }

        /// <summary>
        ///     Load string from resource.
        /// </summary>
        /// <returns></returns>
        public string LoadString()
        {
            Contract.Ensures(Contract.Result<string>() != null);

            var buffer = new StringBuilder(1024);
            ShellLightwaightNativeMethods.SHLoadIndirectString(this.ReferencePath, buffer, buffer.Capacity, IntPtr.Zero);

            return buffer.ToString();
        }
    }
}