using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using starshipxac.Shell.PropertySystem;
using starshipxac.Windows.Shell.Media.Imaging;

namespace ShellFileDialogSample.Views
{
    public class ShellPropertyIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IShellProperty)
            {
                if (targetType == typeof(ImageSource))
                {
                    var property = (IShellProperty)value;
                    try
                    {
                        return ShellIconFactory.CreateBitmapSource(property.IconReference.LoadIcon());
                    }
                    catch
                    {
                        return null;
                    }
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
