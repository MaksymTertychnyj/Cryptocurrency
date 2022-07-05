using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Cryptocurrency.Helper.Converter
{
    public class PriceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string stringPrice = value.ToString()!.Replace('.', ',');

            if (value != null && double.TryParse(stringPrice, out double price))
            {
                return String.Format("{0:0.000}", price);
            }
            else
            {
                return null!;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
