using System;
using System.Collections.Generic;
using System.Globalization;
using imPACt.Models;
using System.Text;
using Xamarin.Forms;

namespace imPACt.Models
{
    public class GetImg : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string url = (string)value;
            ImageSource i = new Uri(url);
            return i;
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
