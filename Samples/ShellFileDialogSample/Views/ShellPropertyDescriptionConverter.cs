using System;
using System.Globalization;
using System.Windows.Data;
using starshipxac.Shell.PropertySystem;

namespace ShellFileDialogSample.Views
{
    public class ShellPropertyDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IShellProperty)
            {
                var property = (IShellProperty)value;

                if (targetType == typeof(string))
                {
                    return property.GetDisplayText(PropertyDescriptionFormatFlags.Default);
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