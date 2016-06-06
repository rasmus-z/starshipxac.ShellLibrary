using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.InteropServices;
using starshipxac.Shell.Internal;
using starshipxac.Shell.Interop;
using starshipxac.Shell.Properties;
using starshipxac.Shell.PropertySystem;
using starshipxac.Shell.Search;

namespace starshipxac.Shell
{
    public class ShellSearchFolder : ShellSearchCollection
    {
        [SuppressMessage("ReSharper", "SuspiciousTypeConversion.Global")]
        static ShellSearchFolder()
        {
            SearchFolderItemFactory = (ISearchFolderItemFactory)new SearchFolderItemFactoryCoClass();
        }

        private ShellSearchFolder(ShellItem shellItem, SearchCondition searchCondition, params ShellFolder[] searchScopePaths)
            : base(shellItem)
        {
            Contract.Requires<ArgumentNullException>(shellItem != null);
            Contract.Requires<ArgumentNullException>(searchCondition != null);

            this.SearchCondition = searchCondition;
            if (searchScopePaths != null && searchScopePaths.Length > 0 && searchScopePaths[0] != null)
            {
                this.SearchScopePaths = searchScopePaths.Select(cont => cont.ParsingName).ToArray();
            }
            else
            {
                this.SearchScopePaths = new string[0];
            }
            Initialize();
        }

        private ShellSearchFolder(ShellItem shellItem, SearchCondition searchCondition, params string[] searchScopePaths)
            : base(shellItem)
        {
            Contract.Requires<ArgumentNullException>(shellItem != null);
            Contract.Requires<ArgumentNullException>(searchCondition != null);

            this.SearchCondition = searchCondition;
            if (searchScopePaths != null && searchScopePaths.Length > 0 && searchScopePaths[0] != null)
            {
                this.SearchScopePaths = searchScopePaths;
            }
            else
            {
                this.SearchScopePaths = new string[0];
            }
            Initialize();
        }

        public static ShellSearchFolder Create(SearchCondition searchCondition, params ShellFolder[] searchScopePaths)
        {
            Contract.Requires<ArgumentNullException>(searchCondition != null);

            var shellItem = CreateShellItemInfo();
            return new ShellSearchFolder(shellItem, searchCondition, searchScopePaths);
        }

        public static ShellSearchFolder Create(SearchCondition searchCondition, params string[] searchScopePaths)
        {
            Contract.Requires<ArgumentNullException>(searchCondition != null);

            var shellItem = CreateShellItemInfo();
            return new ShellSearchFolder(shellItem, searchCondition, searchScopePaths);
        }

        private static ShellItem CreateShellItemInfo()
        {
            Contract.Ensures(Contract.Result<ShellItem>() != null);

            var guid = new Guid(ShellIID.IShellItem);
            IShellItem shellItem;
            var hr = SearchFolderItemFactory.GetShellItem(ref guid, out shellItem);
            if (HRESULT.Failed(hr))
            {
                throw ShellException.FromHRESULT(hr);
            }

            return new ShellItem((IShellItem2)shellItem);
        }

        [ContractInvariantMethod]
        private void ShellSearchFolderInvariant()
        {
            Contract.Invariant(this.SearchCondition != null);
            Contract.Invariant(this.SearchScopePaths != null);
        }

        internal static ISearchFolderItemFactory SearchFolderItemFactory { get; set; }

        public SearchCondition SearchCondition { get; }

        public string[] SearchScopePaths { get; }

        /// <summary>
        ///     プロパティの初期設定を行います。
        /// </summary>
        private void Initialize()
        {
            // SearchCondition初期化
            SearchFolderItemFactory.SetCondition(this.SearchCondition.SearchConditionNative);

            // SearchScopePaths初期化
            var shellItems = new List<IShellItem>(this.SearchScopePaths.Length);
            var shellItemGuid = new Guid(ShellIID.IShellItem);

            foreach (var path in this.SearchScopePaths)
            {
                IShellItem scopeShellItem;
                var hr = ShellNativeMethods.SHCreateItemFromParsingName(path, IntPtr.Zero, ref shellItemGuid, out scopeShellItem);
                if (HRESULT.Succeeded(hr))
                {
                    shellItems.Add(scopeShellItem);
                }
            }

            var scopeShellItemArray = new ShellItemArray(shellItems.ToArray());
            var result = SearchFolderItemFactory.SetScope(scopeShellItemArray);
            if (HRESULT.Failed(result))
            {
                throw ShellException.FromHRESULT(result);
            }
        }

        public void SetStacks(params string[] canonicalNames)
        {
            Contract.Requires<ArgumentNullException>(canonicalNames != null);

            var propertyKeyList = canonicalNames.Select(ShellPropertyKey.FromCanonicalName).ToList();
            if (propertyKeyList.Count > 0)
            {
                SetStacks(propertyKeyList.ToArray());
            }
        }

        public void SetStacks(params ShellPropertyKey[] propertyKeys)
        {
            if (propertyKeys != null && propertyKeys.Length > 0)
            {
                var keys = propertyKeys.Select(x => x.PropertyKeyNative).ToArray();
                SearchFolderItemFactory.SetStacks((uint)propertyKeys.Length, keys);
            }
        }

        public void SetDisplayName(string displayName)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrEmpty(displayName));

            var hr = SearchFolderItemFactory.SetDisplayName(displayName);
            if (HRESULT.Failed(hr))
            {
                throw ShellException.FromHRESULT(hr);
            }
        }

        public void SetIconSize(int value)
        {
            var hr = SearchFolderItemFactory.SetIconSize(value);
            if (HRESULT.Failed(hr))
            {
                throw ShellException.FromHRESULT(hr);
            }
        }

        public void SetFolderTypeId(Guid value)
        {
            Contract.Requires<ArgumentNullException>(value != null);

            var hr = SearchFolderItemFactory.SetFolderTypeID(value);
            if (HRESULT.Failed(hr))
            {
                throw ShellException.FromHRESULT(hr);
            }
        }

        public void SetFolderLogicalViewMode(FolderLogicalViewMode mode)
        {
            var hr = SearchFolderItemFactory.SetFolderLogicalViewMode((FOLDERLOGICALVIEWMODE)mode);
            if (HRESULT.Failed(hr))
            {
                throw ShellException.FromHRESULT(hr);
            }
        }

        public void SetVisibleColumns(ShellPropertyKey[] propertyKeys)
        {
            Contract.Requires<ArgumentNullException>(propertyKeys != null);

            var values = propertyKeys.Select(x => x.PropertyKeyNative).ToArray();
            var hr = SearchFolderItemFactory.SetVisibleColumns((uint)values.Length, values);
            if (HRESULT.Failed(hr))
            {
                throw new ShellException(ErrorMessages.ShellSearchFolderUnableToSetVisibleColumns, Marshal.GetExceptionForHR(hr));
            }
        }

        public void SortColumns(SortColumn[] columns)
        {
            Contract.Requires<ArgumentNullException>(columns != null);

            var values = columns.Select(x => new SORTCOLUMN(x.PropertyKey.PropertyKeyNative, (SORTDIRECTION)x.Direction)).ToArray();
            var hr = SearchFolderItemFactory.SetSortColumns((uint)values.Length, values);
            if (HRESULT.Failed(hr))
            {
                throw new ShellException(ErrorMessages.ShellSearchFolderUnableToSetSortColumns, Marshal.GetExceptionForHR(hr));
            }
        }

        public void SetGroupColumn(ShellPropertyKey propertyKey)
        {
            Contract.Requires<ArgumentNullException>(propertyKey != null);

            var key = propertyKey.PropertyKeyNative;
            var hr = SearchFolderItemFactory.SetGroupColumn(ref key);
            if (HRESULT.Failed(hr))
            {
                throw ShellException.FromHRESULT(hr);
            }
        }
    }
}