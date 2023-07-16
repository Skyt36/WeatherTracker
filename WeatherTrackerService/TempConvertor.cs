using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace WeatherTracker.Views
{
    public class TempConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object paramet, CultureInfo culture)
        {

            return new SolidColorBrush(Colors.Red);
        }
        public object ConvertBack(object value,Type targetType, object parametr,CultureInfo culture)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
