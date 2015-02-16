using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using starshipxac.Shell.Interop;
using starshipxac.Shell.Properties;
using starshipxac.Shell.PropertySystem;
using starshipxac.Shell.PropertySystem.Interop;
using starshipxac.Shell.Runtime;
using starshipxac.Shell.Runtime.Interop;
using starshipxac.Shell.Search.Interop;

namespace starshipxac.Shell.Search
{
    public static class SearchConditionFactory
    {
        public static SearchCondition CreateLeafCondition(string propertyName, string value, SearchConditionOperation operation)
        {
            using (var propVar = PropVariant.FromObject(value))
            {
                return CreateLeafCondition(propertyName, propVar, null, operation);
            }
        }

        public static SearchCondition CreateLeafCondition(string propertyName, DateTime value, SearchConditionOperation operation)
        {
            using (var propVar = PropVariant.FromObject(value))
            {
                return CreateLeafCondition(propertyName, propVar, "System.StructuredQuery.CustomProperty.DateTime", operation);
            }
        }

        public static SearchCondition CreateLeafCondition(string propertyName, int value, SearchConditionOperation operation)
        {
            using (var propVar = PropVariant.FromObject(value))
            {
                return CreateLeafCondition(propertyName, propVar, "System.StructuredQuery.CustomProperty.Integer", operation);
            }
        }

        public static SearchCondition CreateLeafCondition(string propertyName, bool value, SearchConditionOperation operation)
        {
            using (var propVar = PropVariant.FromObject(value))
            {
                return CreateLeafCondition(propertyName, propVar, "System.StructuredQuery.CustomProperty.Boolean", operation);
            }
        }

        public static SearchCondition CreateLeafCondition(string propertyName, double value, SearchConditionOperation operation)
        {
            using (var propVar = PropVariant.FromObject(value))
            {
                return CreateLeafCondition(propertyName, propVar, "System.StructuredQuery.CustomProperty.FloatingPoint", operation);
            }
        }

        private static SearchCondition CreateLeafCondition(string propertyName, PropVariant propVar, string valueType,
            SearchConditionOperation operation)
        {
            IConditionFactory nativeConditionFactory = null;
            SearchCondition condition = null;

            try
            {
                nativeConditionFactory = (IConditionFactory)new ConditionFactoryCoClass();

                ICondition nativeCondition = null;

                if (string.IsNullOrEmpty(propertyName) || propertyName.ToUpperInvariant() == "SYSTEM.NULL")
                {
                    propertyName = null;
                }

                var hr = nativeConditionFactory.MakeLeaf(propertyName, (CONDITION_OPERATION)operation, valueType,
                    propVar, null, null, null, false, out nativeCondition);
                if (HRESULT.Failed(hr))
                {
                    throw ShellException.FromHRESULT(hr);
                }

                condition = new SearchCondition(nativeCondition);
            }
            finally
            {
                if (nativeConditionFactory != null)
                {
                    Marshal.ReleaseComObject(nativeConditionFactory);
                }
            }

            return condition;
        }

        public static SearchCondition CreateLeafCondition(ShellPropertyKey propertyKey, string value,
            SearchConditionOperation operation)
        {
            Contract.Requires<ArgumentNullException>(propertyKey != null);

            string canonicalName;
            var key = propertyKey.PropertyKeyNative;
            PropertySystemNativeMethods.PSGetNameFromPropertyKey(ref key, out canonicalName);
            if (string.IsNullOrEmpty(canonicalName))
            {
                throw new ArgumentException(ErrorMessages.SearchConditionFactoryInvalidProperty, "propertyKey");
            }

            return CreateLeafCondition(canonicalName, value, operation);
        }

        public static SearchCondition CreateLeafCondition(ShellPropertyKey propertyKey, DateTime value,
            SearchConditionOperation operation)
        {
            Contract.Requires<ArgumentNullException>(propertyKey != null);

            string canonicalName;
            var key = propertyKey.PropertyKeyNative;
            PropertySystemNativeMethods.PSGetNameFromPropertyKey(ref key, out canonicalName);
            if (string.IsNullOrEmpty(canonicalName))
            {
                throw new ArgumentException(ErrorMessages.SearchConditionFactoryInvalidProperty, "propertyKey");
            }

            return CreateLeafCondition(canonicalName, value, operation);
        }

        public static SearchCondition CreateLeafCondition(ShellPropertyKey propertyKey, bool value,
            SearchConditionOperation operation)
        {
            Contract.Requires<ArgumentNullException>(propertyKey != null);

            var canonicalName = propertyKey.Name;
            if (string.IsNullOrEmpty(canonicalName))
            {
                throw new ArgumentException(ErrorMessages.SearchConditionFactoryInvalidProperty, "propertyKey");
            }

            return CreateLeafCondition(canonicalName, value, operation);
        }

        public static SearchCondition CreateLeafCondition(ShellPropertyKey propertyKey, double value,
            SearchConditionOperation operation)
        {
            Contract.Requires<ArgumentNullException>(propertyKey != null);

            var canonicalName = propertyKey.Name;
            if (string.IsNullOrEmpty(canonicalName))
            {
                throw new ArgumentException(ErrorMessages.SearchConditionFactoryInvalidProperty, "propertyKey");
            }

            return CreateLeafCondition(canonicalName, value, operation);
        }

        public static SearchCondition CreateLeafCondition(ShellPropertyKey propertyKey, int value,
            SearchConditionOperation operation)
        {
            Contract.Requires<ArgumentNullException>(propertyKey != null);

            var canonicalName = propertyKey.Name;
            if (string.IsNullOrEmpty(canonicalName))
            {
                throw new ArgumentException(ErrorMessages.SearchConditionFactoryInvalidProperty, "propertyKey");
            }

            return CreateLeafCondition(canonicalName, value, operation);
        }

        public static SearchCondition CreateAndOrCondition(SearchConditionType conditionType, bool simplify,
            params SearchCondition[] conditionNodes)
        {
            ICondition result = null;

            var nativeConditionFactory = (IConditionFactory)new ConditionFactoryCoClass();
            try
            {
                var conditions = new List<ICondition>();
                if (conditionNodes != null)
                {
                    conditions.AddRange(conditionNodes.Select(c => c.SearchConditionNative));
                }
                IEnumUnknown subConditions = new EnumUnknown<ICondition>(conditions);

                var hr = nativeConditionFactory.MakeAndOr((CONDITION_TYPE)conditionType, subConditions, simplify, out result);
                if (HRESULT.Failed(hr))
                {
                    throw ShellException.FromHRESULT(hr);
                }
            }
            finally
            {
                Marshal.ReleaseComObject(nativeConditionFactory);
            }

            return new SearchCondition(result);
        }

        public static SearchCondition CreateNotCondition(SearchCondition conditionToBeNegated, bool simplify)
        {
            Contract.Requires<ArgumentNullException>(conditionToBeNegated != null);

            ICondition result;

            var nativeConditionFactory = (IConditionFactory)new ConditionFactoryCoClass();
            try
            {
                var hr = nativeConditionFactory.MakeNot(conditionToBeNegated.SearchConditionNative, simplify, out result);
                if (HRESULT.Failed(hr))
                {
                    throw ShellException.FromHRESULT(hr);
                }
            }
            finally
            {
                Marshal.ReleaseComObject(nativeConditionFactory);
            }

            return new SearchCondition(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        /// <remarks>
        /// <para>http://msdn.microsoft.com/en-us/library/bb233500.aspx</para>
        /// <para>http://www.microsoft.com/windows/products/winfamily/desktopsearch/technicalresources/advquery.mspx</para>
        /// </remarks>
        public static SearchCondition ParseStructuredQuery(string query)
        {
            return ParseStructuredQuery(query, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cultureInfo"></param>
        /// <returns></returns>
        /// <remarks>
        /// <para>http://msdn.microsoft.com/en-us/library/bb233500.aspx</para>
        /// <para>http://www.microsoft.com/windows/products/winfamily/desktopsearch/technicalresources/advquery.mspx</para>
        /// </remarks>
        public static SearchCondition ParseStructuredQuery(string query, CultureInfo cultureInfo)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrEmpty(query));

            ICondition result = null;

            var nativeQueryParserManager = (IQueryParserManager)new QueryParserManagerCoClass();
            IQueryParser queryParser = null;
            IQuerySolution querySolution = null;

            IEntity mainType = null;
            SearchCondition searchCondition = null;
            try
            {
                // IQueryParser作成
                var guid = new Guid(SearchIID.IQueryParser);
                var hr = nativeQueryParserManager.CreateLoadedParser(
                    "SystemIndex",
                    cultureInfo == null ? (ushort)0 : (ushort)cultureInfo.LCID,
                    ref guid,
                    out queryParser);
                if (HRESULT.Failed(hr))
                {
                    throw ShellException.FromHRESULT(hr);
                }

                if (queryParser != null)
                {
                    using (var optionValue = PropVariant.FromObject(true))
                    {
                        hr = queryParser.SetOption(STRUCTURED_QUERY_SINGLE_OPTION.SQSO_NATURAL_SYNTAX, optionValue);
                        if (HRESULT.Failed(hr))
                        {
                            throw ShellException.FromHRESULT(hr);
                        }
                    }

                    hr = queryParser.Parse(query, null, out querySolution);
                    if (HRESULT.Failed(hr))
                    {
                        throw ShellException.FromHRESULT(hr);
                    }

                    if (querySolution != null)
                    {
                        hr = querySolution.GetQuery(out result, out mainType);
                        if (HRESULT.Failed(hr))
                        {
                            throw ShellException.FromHRESULT(hr);
                        }
                    }
                }

                searchCondition = new SearchCondition(result);
            }
            catch
            {
                if (searchCondition != null)
                {
                    searchCondition.Dispose();
                }
                throw;
            }
            finally
            {
                Marshal.ReleaseComObject(nativeQueryParserManager);

                if (queryParser != null)
                {
                    Marshal.ReleaseComObject(queryParser);
                }

                if (querySolution != null)
                {
                    Marshal.ReleaseComObject(querySolution);
                }

                if (mainType != null)
                {
                    Marshal.ReleaseComObject(mainType);
                }
            }

            return searchCondition;
        }
    }
}