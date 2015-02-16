using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using System.Text;
using starshipxac.Shell.Media.Imaging;

namespace starshipxac.Shell.Resources
{
    /// <summary>
    /// リソースDLLからリソースを取得するメソッドを定義します。
    /// </summary>
    public sealed class ResourceLibrary
    {
        private IntPtr instanceHandle = IntPtr.Zero;

        /// <summary>
        /// リソースDLLのパスを指定して、<see cref="ResourceLibrary"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="libraryPath">リソースDLLのパス。</param>
        public ResourceLibrary(string libraryPath)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(libraryPath));

            this.LibraryPath = libraryPath;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(!String.IsNullOrWhiteSpace(this.LibraryPath));
        }

        /// <summary>
        /// リソースDLLのパスを取得します。
        /// </summary>
        public string LibraryPath { get; private set; }

        /// <summary>
        /// リソースから文字列を取得します。
        /// </summary>
        /// <param name="resourceId">取得する文字列のリソースID。</param>
        /// <returns>取得した文字列。</returns>
        /// <exception cref="Win32Exception">リソースDLLを読み込めませんでした。</exception>
        public string LoadString(int resourceId)
        {
            Contract.Ensures(Contract.Result<string>() != null);

            var hInstance = GetInstanceHandle();

            var result = new StringBuilder(255);
            LoadString(hInstance, resourceId, result, result.Capacity);

            return result.ToString();
        }

        /// <summary>
        /// リソースからアイコンを取得します。
        /// </summary>
        /// <param name="resourceId">取得するアイコンのリソースID。</param>
        /// <param name="size">アイコンのサイズ。</param>
        /// <returns>取得したアイコン。</returns>
        /// <exception cref="Win32Exception">
        /// <para>リソースDLLを読み込めませんでした。</para>
        /// <para>または</para>
        /// <para>指定したリソースIDのアイコンを取得できませんでした。</para>
        /// </exception>
        public ShellIcon LoadIcon(int resourceId, int size = 0)
        {
            Contract.Ensures(Contract.Result<ShellIcon>() != null);

            var hInstance = GetInstanceHandle();

            var resourceName = String.Format("#{0}", resourceId);
            var hicon = LoadIconImage(hInstance, resourceName, size, size, LR_DEFAULTCOLOR);
            if (hicon == IntPtr.Zero)
            {
                throw new Win32Exception();
            }
            return new ShellIcon(hicon);
        }

        /// <summary>
        /// リソースDLLのハンドルを取得します。
        /// </summary>
        /// <returns>リソースDLLのインスタンスハンドル。</returns>
        /// <exception cref="Win32Exception">リソースDLLを読み込めませんでした。</exception>
        private IntPtr GetInstanceHandle()
        {
            if (this.instanceHandle == IntPtr.Zero)
            {
                this.instanceHandle = LoadLibraryEx(this.LibraryPath, LOAD_LIBRARY_AS_DATAFILE);
                if (this.instanceHandle == IntPtr.Zero)
                {
                    throw new Win32Exception();
                }
            }
            return this.instanceHandle;
        }

        #region Native Methods

        private const UInt32 DONT_RESOLVE_DLL_REFERENCES = 0x00000001;
        private const UInt32 LOAD_IGNORE_CODE_AUTHZ_LEVEL = 0x00000010;
        private const UInt32 LOAD_LIBRARY_AS_DATAFILE = 0x00000002;
        private const UInt32 LOAD_LIBRARY_AS_DATAFILE_EXCLUSIVE = 0x00000040;
        private const UInt32 LOAD_LIBRARY_AS_IMAGE_RESOURCE = 0x00000020;
        private const UInt32 LOAD_WITH_ALTERED_SEARCH_PATH = 0x00000008;

        private const UInt32 IMAGE_BITMAP = 0;
        private const UInt32 IMAGE_ICON = 1;
        private const UInt32 IMAGE_CURSOR = 2;

        private const UInt32 LR_CREATEDIBSECTION = 0x00002000;
        private const UInt32 LR_DEFAULTCOLOR = 0x00000000;
        private const UInt32 LR_DEFAULTSIZE = 0x00000040;
        private const UInt32 LR_LOADFROMFILE = 0x00000010;
        private const UInt32 LR_LOADMAP3DCOLORS = 0x00001000;
        private const UInt32 LR_LOADTRANSPARENT = 0x00000020;
        private const UInt32 LR_MONOCHROME = 0x00000001;
        private const UInt32 LR_SHARED = 0x00008000;
        private const UInt32 LR_VGACOLOR = 0x00000080;

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr LoadLibraryEx(string lpFileName, IntPtr hReservedNull, UInt32 dwFlags);

        private static IntPtr LoadLibraryEx(string lpFileName, UInt32 dwFlags)
        {
            return LoadLibraryEx(lpFileName, IntPtr.Zero, dwFlags);
        }

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern int LoadString(IntPtr instanceHandle, int id, StringBuilder buffer, int bufferSize);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern IntPtr LoadImage(IntPtr hInstance, string lpszName, UInt32 uType,
            int cxDesired, int cyDesired, UInt32 fuLoad);

        private static IntPtr LoadIconImage(IntPtr hInstance, string lpszName, int cxDesired, int cyDesired, uint fuLoad)
        {
            return LoadImage(hInstance, lpszName, IMAGE_ICON, cxDesired, cyDesired, fuLoad);
        }

        #endregion
    }
}