using System;
using Windows.UI.Xaml.Data;

namespace ChefsBook_UWP_App.Converters
{
    public class NullableIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (string.IsNullOrEmpty((string)value))
                return null;

            int intValue = -1;
            int.TryParse((string)value, out intValue);

            if (intValue != -1)
                return intValue;

            return null;
        }
    }
}
