using iTube.ViewModel;
using Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace iTube.Converter
{
    class RateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            RateEnum rate = (RateEnum)value;
            if ((RateEnum)Enum.Parse(typeof(RateEnum), parameter.ToString()) == rate)
            {
                return new SolidColorBrush(Colors.Red);
            }
            else
                return new SolidColorBrush(Colors.DimGray);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
