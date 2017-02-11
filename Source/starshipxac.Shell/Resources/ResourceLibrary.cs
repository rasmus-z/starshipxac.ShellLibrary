using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Text;
using starshipxac.Shell.Media.Imaging;
using starshipxac.Shell.Resources.Interop;

namespace starshipxac.Shell.Resources
{
    /// <summary>
    ///     Define resource library class.
    /// </summary>
    public sealed class ResourceLibrary
    {
        private IntPtr instanceHandle = IntPtr.Zero;

        /// <summary>
        ///     Initialize a new instance of the <see cref="ResourceLibrary" /> class
        ///     to the specified resource DLL path.
        /// </summary>
        /// <param name="libraryPath">Resource DLL path.</param>
        public ResourceLibrary(string libraryPath)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(libraryPath));

            this.LibraryPath = libraryPath;
        }

        /// <summary>
        ///     Get the resource DLL path.
        /// </summary>
        public string LibraryPath { get; }

        /// <summary>
        ///     Get the string from resource.
        /// </summary>
        /// <param name="resourceId">String resource ID.</param>
        /// <returns>String.</returns>
        /// <exception cref="Win32Exception">Could not load resource DLL.</exception>
        public string LoadString(int resourceId)
        {
            Contract.Ensures(Contract.Result<string>() != null);

            var hInstance = GetInstanceHandle();

            var result = new StringBuilder(255);
            ResourceMethods.LoadString(hInstance, resourceId, result, result.Capacity);

            return result.ToString();
        }

        /// <summary>
        ///     Get the icon from resource.
        /// </summary>
        /// <param name="resourceId">Icon resource ID.</param>
        /// <param name="size">Icon size.</param>
        /// <returns>Icon.</returns>
        /// <exception cref="Win32Exception">
        ///     <para>Could not load resource DLL.</para>
        ///     or
        ///     <para>Could not acquire the icon of the specified resource ID.</para>
        /// </exception>
        public ShellIcon LoadIcon(int resourceId, int size = 0)
        {
            Contract.Ensures(Contract.Result<ShellIcon>() != null);

            var hInstance = GetInstanceHandle();

            var resourceName = $"#{resourceId}";
            var hicon = ResourceMethods.LoadIconImage(hInstance, resourceName, size, size, ResourceMethods.LR_DEFAULTCOLOR);
            if (hicon == IntPtr.Zero)
            {
                throw new Win32Exception();
            }
            return new ShellIcon(hicon);
        }

        /// <summary>
        ///     Get the handle of resource DLL.
        /// </summary>
        /// <returns>Instance handle for the resource DLL.</returns>
        /// <exception cref="Win32Exception">Could not load resource DLL.</exception>
        private IntPtr GetInstanceHandle()
        {
            if (this.instanceHandle == IntPtr.Zero)
            {
                this.instanceHandle = ResourceMethods.LoadLibraryEx(this.LibraryPath, ResourceMethods.LOAD_LIBRARY_AS_DATAFILE);
                if (this.instanceHandle == IntPtr.Zero)
                {
                    throw new Win32Exception();
                }
            }
            return this.instanceHandle;
        }

        public override string ToString()
        {
            return $"{{ LibraryPath: {LibraryPath} }}";
        }
    }
}