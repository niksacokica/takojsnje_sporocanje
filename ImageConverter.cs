using System;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace takojsnje_sporocanje{
    public class ImageConverter : IValueConverter{
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture){
            MessageBox.Show(value.ToString());

            if(File.Exists((string)value))
                return new ImageSourceConverter().ConvertFromString((string)value) as ImageSource;
            
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture){
            throw new NotImplementedException();
        }
    }
}
