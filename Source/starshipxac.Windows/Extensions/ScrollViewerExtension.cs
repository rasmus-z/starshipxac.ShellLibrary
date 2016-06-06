using System;
using System.Diagnostics.Contracts;
using System.Windows.Controls;
using System.Windows.Media;

namespace starshipxac.Windows.Extensions
{
    /// <summary>
    ///     <see cref="ScrollViewer" />の拡張メソッドを定義します。
    /// </summary>
    public static class ScrollViewerExtension
    {
        public static ScrollViewer GetScrollViewer(this Visual visual)
        {
            Contract.Requires(visual != null);
            return visual.GetDescendantByType(typeof(ScrollViewer)) as ScrollViewer;
        }

        public static double GetScrollHorizontalOffset(this Visual visual)
        {
            Contract.Requires<ArgumentNullException>(visual != null);

            var scrollViewer = GetScrollViewer(visual);
            if (scrollViewer == null)
            {
                throw new InvalidOperationException();
            }

            return scrollViewer.VerticalOffset;
        }

        public static void SetScrollHorizontalOffset(this Visual visual, double offset)
        {
            Contract.Requires<ArgumentNullException>(visual != null);

            var scrollViewer = GetScrollViewer(visual);
            if (scrollViewer == null)
            {
                throw new InvalidOperationException();
            }

            scrollViewer.ScrollToHorizontalOffset(offset);
        }

        public static double GetScrollVerticalOffset(this Visual visual)
        {
            Contract.Requires<ArgumentNullException>(visual != null);

            var scrollViewer = GetScrollViewer(visual);
            if (scrollViewer == null)
            {
                throw new InvalidOperationException();
            }

            return scrollViewer.VerticalOffset;
        }

        public static void SetScrollVerticalOffset(this Visual visual, double offset)
        {
            Contract.Requires<ArgumentNullException>(visual != null);

            var scrollViewer = GetScrollViewer(visual);
            if (scrollViewer == null)
            {
                throw new InvalidOperationException();
            }

            scrollViewer.ScrollToVerticalOffset(offset);
        }
    }
}