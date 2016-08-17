using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using starshipxac.Shell.Media.Imaging;
using starshipxac.Windows.Shell.Media.Imaging;

namespace ShellExplorerSample.Views
{
    public class ShellThumbnailImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var thumbnail = value as ShellThumbnail;
            if (value is ShellThumbnail)
            {
                if (targetType == typeof(ImageSource))
                {
                    return new ShellImageSource(thumbnail);
                }
            }

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
