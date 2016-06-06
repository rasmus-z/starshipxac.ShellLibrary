using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Text;
using starshipxac.Shell.Interop;
using starshipxac.Shell.Properties;

namespace starshipxac.Shell.Resources
{
    /// <summary>
    ///     文字列リソースを取得します。
    /// </summary>
    public class StringReference : ResourceReference
    {
        private static readonly char[] Separator = {','};

        public StringReference(string libraryName, int resourceId)
            : base(libraryName, resourceId)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(libraryName));
        }

        public StringReference(string referencePath)
            : base(referencePath.Replace("shell32,dll", "shell32.dll"))
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(referencePath));
        }

        protected override string GetReferencePath()
        {
            return String.Format(CultureInfo.InstalledUICulture, "{0},{1}", this.LibraryPath, this.ResourceId);
        }

        protected override void ParseReferencePath(out string libraryPath, out int resourceId)
        {
            var refParams = this.ReferencePath.Split(Separator);

            if (refParams.Length != 2 || String.IsNullOrWhiteSpace(refParams[0]) || String.IsNullOrWhiteSpace(refParams[1]))
            {
                throw new InvalidOperationException(ErrorMessages.InvalidReferencePath);
            }

            // ライブラリ名
            libraryPath = Environment.ExpandEnvironmentVariables(refParams[0].Replace(@"@", String.Empty));
            // リソースID
            resourceId = Int32.Parse(refParams[1], CultureInfo.InstalledUICulture);
        }

        /// <summary>
        ///     リソースから文字列を取得します。
        /// </summary>
        /// <returns></returns>
        public string LoadString()
        {
            Contract.Ensures(Contract.Result<string>() != null);

            var buffer = new StringBuilder(1024);
            ShellNativeMethods.SHLoadIndirectString(this.ReferencePath, buffer, buffer.Capacity, IntPtr.Zero);

            return buffer.ToString();
        }
    }
}