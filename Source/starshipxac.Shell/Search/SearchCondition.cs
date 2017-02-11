using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using starshipxac.Shell.Interop;
using starshipxac.Shell.PropertySystem;
using starshipxac.Shell.Runtime.Interop;
using starshipxac.Shell.Search.Interop;

namespace starshipxac.Shell.Search
{
    /// <summary>
    ///     Define search condition class.
    /// </summary>
    public class SearchCondition : IDisposable
    {
        internal SearchCondition(ICondition searchConditionNative)
        {
            Contract.Requires<ArgumentNullException>(searchConditionNative != null);

            this.SearchConditionNative = searchConditionNative;

            ConditionOperation = SearchConditionOperation.Implicit;

            this.PropertyKey = ShellPropertyKey.FromCanonicalName(this.PropertyCanonicalName);
        }

        ~SearchCondition()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(false);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.SearchConditionNative != null)
            {
                Marshal.ReleaseComObject(this.SearchConditionNative);
                this.SearchConditionNative = null;
            }
        }

        internal ICondition SearchConditionNative { get; set; }

        public string PropertyCanonicalName { get; } = null;

        public ShellPropertyKey PropertyKey { get; private set; }

        public string PropertyValue { get; internal set; }

        public SearchConditionOperation ConditionOperation { get; }

        public SearchConditionType ConditionType => SearchConditionType.Leaf;

        public IEnumerable<SearchCondition> GetSubConditions()
        {
            var result = new List<SearchCondition>();

            object subConditionObject;
            var guid = new Guid(ComIID.IEnumUnknown);
            var hr = this.SearchConditionNative.GetSubConditions(ref guid, out subConditionObject);
            if (HRESULT.Failed(hr))
            {
                throw ShellException.FromHRESULT(hr);
            }

            if (subConditionObject != null)
            {
                var enumUnknown = subConditionObject as IEnumUnknown;
                while (enumUnknown != null && hr == COMErrorCodes.S_OK)
                {
                    var buffer = IntPtr.Zero;
                    var fetched = 0U;
                    hr = enumUnknown.Next(1, ref buffer, ref fetched);
                    if (HRESULT.Succeeded(hr) && fetched == 1)
                    {
                        result.Add(new SearchCondition((ICondition)Marshal.GetObjectForIUnknown(buffer)));
                    }
                }
            }

            return result;
        }
    }
}