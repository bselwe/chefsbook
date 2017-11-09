using System;
using Windows.UI.Xaml.Data;

namespace ChefsBook_UWP_App.Converters
{
    public class NullableTimeSpanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (string.IsNullOrEmpty((string)value))
                return null;

            TimeSpan timeSpanValue = TimeSpan.FromDays(7);
            TimeSpan.TryParse((string)value, out timeSpanValue);

            if (timeSpanValue != TimeSpan.FromDays(7))
                return timeSpanValue;

            return null;
        }
    }
}
