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
        const double high_temp = 25;//граница высокой температуры
        const double low_temp = 10;//граница низкой температуры
        const double avg_high_temp = 20;//верхняя граница средней температуры
        const double avg_low_temp = 15;//нижняя граница средней температуры
        public object Convert(object value, Type targetType, object paramet, CultureInfo culture)
        {
            if (value == null)
                return new SolidColorBrush(Colors.Gray);
            if (value is double value_)
            {
                if (value_ > high_temp)
                {
                    return new SolidColorBrush(Colors.Red);
                }else if (value_ < low_temp)
                {
                    return new SolidColorBrush(Colors.Aqua);
                }else if (value_ <= avg_high_temp && value_ >= avg_low_temp)
                {
                    return new SolidColorBrush(Colors.White);
                }else if (value_ < avg_low_temp)
                {
                    byte r = (byte)(255 * (value_ - low_temp) / (avg_low_temp - low_temp));
                    return new SolidColorBrush(Color.FromRgb(r, 255, 255));
                }
                else
                {
                    byte gb = (byte)(255 * (high_temp - value_) / (high_temp - avg_high_temp));
                    return new SolidColorBrush(Color.FromRgb(255, gb, gb));
                }
            }
            return new SolidColorBrush(Colors.Black);
        }
        public object ConvertBack(object value,Type targetType, object parametr,CultureInfo culture)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
