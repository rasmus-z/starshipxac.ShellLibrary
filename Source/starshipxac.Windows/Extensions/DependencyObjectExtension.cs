using System;
using System.Diagnostics.Contracts;
using System.Windows;
using System.Windows.Media;

namespace starshipxac.Windows.Extensions
{
    /// <summary>
    ///     <see cref="DependencyObject" />拡張メソッドを定義します。
    /// </summary>
    public static class DependencyObjectExtension
    {
        public static T FindVisualAncestor<T>(this DependencyObject target) where T : DependencyObject
        {
            Contract.Requires<ArgumentNullException>(target != null);

            var parent = VisualTreeHelper.GetParent(target);
            if (parent == null)
            {
                return null;
            }
            if (parent is T)
            {
                return (T)parent;
            }
            return parent.FindVisualAncestor<T>();
        }
    }
}