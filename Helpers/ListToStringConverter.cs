using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace LibManager.Helpers
{
    internal class ListToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            List<string> liValue = (List<string>)value;
            string display = String.Join("|", liValue);
            return display;

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool invert = parameter != null && parameter.ToString().Equals("Inverse", StringComparison.OrdinalIgnoreCase);
            System.Windows.Visibility visibility = (System.Windows.Visibility)value;
            return invert ? (visibility != Visibility.Visible) : (visibility == Visibility.Visible);
        }
    }
}