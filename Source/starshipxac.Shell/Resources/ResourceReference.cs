using System;
using System.Diagnostics.Contracts;

namespace starshipxac.Shell.Resources
{
    /// <summary>
    ///     リソース参照情報を保持します。
    /// </summary>
    [ContractClass(typeof(ResourceReferenceContract))]
    public abstract class ResourceReference : IEquatable<ResourceReference>
    {
        /// <summary>
        ///     ライブラリ名とリソースIDを指定して、<see cref="ResourceReference" />クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="libraryName">実行ファイルまたは DLLファイル、アイコンファイルのライブラリ名。</param>
        /// <param name="resourceId">アイコンのインデックス。</param>
        protected ResourceReference(string libraryName, int resourceId)
        {
            this.LibraryPath = libraryName;
            this.ResourceId = resourceId;
            this.ReferencePath = GetReferencePathInternal();
        }

        /// <summary>
        ///     カンマで区切られたライブラリ名とリソースIDを指定して、<see cref="ResourceReference" />クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="referencePath">カンマで区切られたライブラリ名とリソースID。</param>
        protected ResourceReference(string referencePath)
        {
            this.ReferencePath = referencePath;
            ParseReferencePathInternal();
        }

        /// <summary>
        ///     実行ファイルまたは DLLファイルのパス名を取得します。
        /// </summary>
        public string LibraryPath { get; private set; }

        /// <summary>
        ///     リソースIDを取得します。
        /// </summary>
        public int ResourceId { get; private set; }

        /// <summary>
        ///     リソース参照情報を取得します。
        /// </summary>
        /// <remarks>
        ///     リソース参照情報は、ライブラリ名とリソースIDをカンマで結合した文字列です。
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

        protected abstract string GetReferencePath();

        protected abstract void ParseReferencePath(out string libraryPath, out int resourceId);

        /// <summary>
        ///     2つの<see cref="ResourceReference" />を比較して、等しいかどうかを判定します。
        /// </summary>
        /// <param name="left">1つめの<see cref="ResourceReference" />。</param>
        /// <param name="right">2つめの<see cref="ResourceReference" />。</param>
        /// <returns>
        ///     2つの<see cref="ResourceReference" />が等しい場合は<c>true</c>。それ以外の場合は<c>false</c>。
        /// </returns>
        public static bool operator ==(ResourceReference left, ResourceReference right)
        {
            return Equals(left, right);
        }

        /// <summary>
        ///     2つの<see cref="ResourceReference" />を比較して、等しくないかどうかを判定します。
        /// </summary>
        /// <param name="left">1つめの<see cref="ResourceReference" />。</param>
        /// <param name="right">2つめの<see cref="ResourceReference" />。</param>
        /// <returns>
        ///     2つの<see cref="ResourceReference" />が等しくない場合は<c>true</c>。それ以外の場合は<c>false</c>。
        /// </returns>
        public static bool operator !=(ResourceReference left, ResourceReference right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        ///     指定した<see cref="ResourceReference" />の値が、現在の<see cref="ResourceReference" />と等しいかどうかを判定します。
        /// </summary>
        /// <param name="other">現在の<see cref="ResourceReference" />と比較する<see cref="ResourceReference" />。</param>
        /// <returns>
        ///     <paramref name="other" />と現在の<see cref="ResourceReference" />が等しい場合は<c>true</c>。
        ///     それ以外の場合は<c>false</c>。
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
        ///     指定したオブジェクトの値が、現在の<see cref="ResourceReference" />と等しいかどうかを判定します。
        /// </summary>
        /// <param name="obj">現在の<see cref="ResourceReference" />と比較するオブジェクト。</param>
        /// <returns>
        ///     <paramref name="obj" />と現在の<see cref="ResourceReference" />が等しい場合は<c>true</c>。
        ///     それ以外の場合は<c>false</c>。
        /// </returns>
        public override bool Equals(object obj)
        {
            return this.Equals(obj as ResourceReference);
        }

        /// <summary>
        ///     このインスタンスのハッシュコードを取得します。
        /// </summary>
        /// <returns>ハッシュコード。</returns>
        public override int GetHashCode()
        {
            return this.ReferencePath.GetHashCode();
        }

        /// <summary>
        ///     このインスタンスの文字列表現を取得します。
        /// </summary>
        /// <returns>このインスタンスの文字列表現。</returns>
        public override string ToString()
        {
            return $"{{ReferencePath: {ReferencePath}}}";
        }
    }

    [ContractClassFor(typeof(ResourceReference))]
    internal abstract class ResourceReferenceContract : ResourceReference
    {
        protected ResourceReferenceContract(string libraryName, int resourceId)
            : base(libraryName, resourceId)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(libraryName));
        }

        protected ResourceReferenceContract(string referencePath)
            : base(referencePath)
        {
            Contract.Requires<ArgumentException>(!String.IsNullOrWhiteSpace(referencePath));
        }

        [ContractInvariantMethod]
        private void ObjectInvarinat()
        {
            Contract.Invariant(this.LibraryPath != null);
            Contract.Invariant(this.ReferencePath != null);
        }

        protected override string GetReferencePath()
        {
            Contract.Ensures(Contract.Result<string>() != null);
            return default(string);
        }

        protected override void ParseReferencePath(out string libraryPath, out int resourceId)
        {
            Contract.Ensures(Contract.ValueAtReturn(out libraryPath) != null);
            libraryPath = String.Empty;
            resourceId = 0;
        }
    }
}