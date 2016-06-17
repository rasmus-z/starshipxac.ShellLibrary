using System;
using System.Globalization;
using System.Windows.Data;
using starshipxac.Shell.PropertySystem;

namespace ShellFileDialogSample.Views
{
    public class ShellPropertyValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IShellProperty)
            {
                var property = (IShellProperty)value;

                if (targetType == typeof(string))
                {
                    if (property.ValueAsObject == null)
                    {
                        return String.Empty;
                    }
                    else if (property.ValueType == typeof(string[]))
                    {
                        var strings = (string[])property.ValueAsObject;
                        return $"{{ {String.Join(", ", strings)} }}";
                    }
                    else
                    {
                        return property.ValueAsObject.ToString();
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
