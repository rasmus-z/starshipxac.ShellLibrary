using System;
using starshipxac.Shell.Resources;

namespace starshipxac.Shell.PropertySystem
{
    /// <summary>
    ///     Define shell property interface.
    /// </summary>
    public interface IShellProperty
    {
        /// <summary>
        ///     Get the property key.
        /// </summary>
        ShellPropertyKey PropertyKey { get; }

        /// <summary>
        ///     Get the property description.
        /// </summary>
        ShellPropertyDescription Description { get; }

        /// <summary>
        ///     Get the canonical name.
        /// </summary>
        string CanonicalName { get; }

        /// <summary>
        ///     Get the value of the property's <see cref="ShellObject" /> type.
        /// </summary>
        object ValueAsObject { get; }

        /// <summary>
        ///     Get the property value type.
        /// </summary>
        Type ValueType { get; }

        /// <summary>
        ///     Get the property icon resource reference.
        /// </summary>
        IconReference IconReference { get; }

        /// <summary>
        ///     Clear property value.
        /// </summary>
        void ClearValue();

        /// <summary>
        ///     Get the display text.
        /// </summary>
        /// <param name="formatFlags">Format flag.</param>
        /// <returns>Display text.</returns>
        string GetDisplayText(PropertyDescriptionFormatFlags formatFlags);
    }
}