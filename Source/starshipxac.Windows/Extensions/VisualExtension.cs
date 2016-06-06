using System;
using System.Windows;
using System.Windows.Media;

namespace starshipxac.Windows.Extensions
{
    /// <summary>
    ///     <see cref="Visual" />の拡張メソッドを定義します。
    /// </summary>
    public static class VisualExtension
    {
        public static Visual GetDescendantByType(this Visual element, Type type)
        {
            if (element == null)
            {
                return null;
            }

            if (element.GetType() == type)
            {
                return element;
            }

            (element as FrameworkElement)?.ApplyTemplate();

            Visual result = null;
            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(element); i++)
            {
                var visual = VisualTreeHelper.GetChild(element, i) as Visual;
                result = GetDescendantByType(visual, type);
                if (result != null)
                {
                    break;
                }
            }

            return result;
        }
    }
}