using System;
using System.Diagnostics.Contracts;

namespace starshipxac.Shell.Resources
{
    /// <summary>
    ///     Define resource reference class.
    /// </summary>
    public abstract class ResourceReference : IEquatable<ResourceReference>
    {
        /// <summary>
        ///     Initialize a new instance of the <see cref="ResourceReference" /> class
        ///     to the specified library name and resource ID.
        /// </summary>
        /// <param name="libraryName">Library name of executable file or DLL file, icon file.</param>
        /// <param name="resourceId">The index of the icon.</param>
        protected ResourceReference(string libraryName, int resourceId)
        {
            this.LibraryPath = libraryName;
            this.ResourceId = resourceId;
            this.ReferencePath = GetReferencePathInternal();
        }

        /// <summary>
        ///     Initialize a new instance of the <see cref="ResourceReference" /> class
        ///     with a comma-separated library name and resource ID.
        /// </summary>
        /// <param name="referencePath">A comma-separated library name and resource ID.</param>
        protected ResourceReference(string referencePath)
        {
            this.ReferencePath = referencePath;
            ParseReferencePathInternal();
        }

        /// <summary>
        ///     Get the path name of the executable file or DLL file.
        /// </summary>
        public string LibraryPath { get; private set; }

        /// <summary>
        ///     Get the resource ID.
        /// </summary>
        public int ResourceId { get; private set; }

        /// <summary>
        ///     Get reference path.
        /// </summary>
        /// <remarks>
        ///     Resource path is a string consisting of a library name and resource ID combined by a comma.
        /// </remarks>
        public string ReferencePath { get; }

        private string GetReferencePathInternal()
        {
            Contract.Ensures(Contract.Result<string>() != null);
            return GetReferencePath();
        }

        private void ParseReferencePathInternal()
        {
            string libraryName;
            int resourceId;
            ParseReferencePath(out libraryName, out resourceId);
            this.LibraryPath = libraryName;
            this.ResourceId = resourceId;
        }

        /// <summary>
        ///     Get reference path.
        /// </summary>
        /// <returns></returns>
        protected abstract string GetReferencePath();

        /// <summary>
        ///     Parse reference path.
        /// </summary>
        /// <param name="libraryPath"></param>
        /// <param name="resourceId"></param>
        protected abstract void ParseReferencePath(out string libraryPath, out int resourceId);

        /// <summary>
        ///     Compare the two <see cref="ResourceReference" /> to determine if they are equal.
        /// </summary>
        /// <param name="left">The first <see cref="ResourceReference" />.</param>
        /// <param name="right">The second <see cref="ResourceReference" />.</param>
        /// <returns>
        ///     If the two <see cref="ResourceReference" /> are equal, it returns <c>true</c>.
        ///     Otherwise returns <c>false</c>.
        /// </returns>
        public static bool operator ==(ResourceReference left, ResourceReference right)
        {
            return Equals(left, right);
        }

        /// <summary>
        ///     Compare the two <see cref="ResourceReference" /> to determine if they are not equal.
        /// </summary>
        /// <param name="left">The first <see cref="ResourceReference" />.</param>
        /// <param name="right">The second <see cref="ResourceReference" />.</param>
        /// <returns>
        ///     If the two <see cref="ResourceReference" /> are not equal, it returns <c>true</c>.
        ///     Otherwise returns <c>false</c>.
        /// </returns>
        public static bool operator !=(ResourceReference left, ResourceReference right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        ///     Determines whether the value of the specified <see cref="ResourceReference" /> is equal
        ///     to the current <see cref="ResourceReference" />.
        /// </summary>
        /// <param name="other">Compare with the current <see cref="ResourceReference" /> <see cref="ResourceReference" />.</param>
        /// <returns>
        ///     If <paramref name="other" /> is equal to the current <see cref="ResourceReference" />, it returns <c>true</c>.
        ///     Otherwise <c>false</c>.
        /// </returns>
        public bool Equals(ResourceReference other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return this.LibraryPath.Equals(other.LibraryPath, StringComparison.InvariantCultureIgnoreCase) &&
                   this.ResourceId.Equals(other.ResourceId);
        }

        /// <summary>
        ///     Determines whether the value of the specified object is equal to the current <see cref="ResourceReference" />.
        /// </summary>
        /// <param name="obj">The object to be compared with the current <see cref="ResourceReference" />.</param>
        /// <returns>
        ///     If the current <see cref="ResourceReference" /> is equal to <paramref name="obj" />, it returns <c>true</c>.
        ///     Otherwise <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            return this.Equals(obj as ResourceReference);
        }

        /// <summary>
        ///     Get a hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            return this.ReferencePath.GetHashCode();
        }

        /// <summary>
        ///     Get a string representation of this instance.
        /// </summary>
        /// <returns>A string representation of this instance.</returns>
        public override string ToString()
        {
            return $"{{ReferencePath: {ReferencePath}}}";
        }
    }
}