using System;
using starshipxac.Shell.Resources;

namespace starshipxac.Shell.PropertySystem
{
    /// <summary>
    ///     シェルプロパティインターフェイスを定義します。
    /// </summary>
    public interface IShellProperty
    {
        /// <summary>
        ///     プロパティキーを取得します。
        /// </summary>
        ShellPropertyKey PropertyKey { get; }

        /// <summary>
        ///     プロパティの説明を取得します。
        /// </summary>
        ShellPropertyDescription Description { get; }

        /// <summary>
        ///     プロパティの標準的な名前を取得します。
        /// </summary>
        string CanonicalName { get; }

        /// <summary>
        ///     プロパティの<see cref="Object" />型の値を取得します。
        /// </summary>
        object ValueAsObject { get; }

        /// <summary>
        ///     プロパティの値の型を取得します。
        /// </summary>
        Type ValueType { get; }

        /// <summary>
        ///     プロパティのアイコンリソース参照を取得します。
        /// </summary>
        IconReference IconReference { get; }

        /// <summary>
        ///     プロパティ値をクリアします。
        /// </summary>
        void ClearValue();

        /// <summary>
        ///     書式化した文字列を取得します。
        /// </summary>
        /// <param name="formatFlags">書式オプション。</param>
        /// <returns>書式化した文字列。</returns>
        string GetDisplayText(PropertyDescriptionFormatFlags formatFlags);
    }
}