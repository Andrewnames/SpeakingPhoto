using System;
using System.Globalization;
using System.IO;
using Xamarin.Forms;

namespace AppXF.Services
{
    public class ImageNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                string name = (string)value;

                if (name.StartsWith("/9"))
                {
                    return LoadImage(name);
                }
                else
                {
                   // var uri = new Uri(name, UriKind.Absolute);
                    return name;
                }
                //return uri.AbsoluteUri;// "icon.png";// new Uri(name);
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        private ImageSource LoadImage(string base64String)
        {
            byte[] imageBytes = System.Convert.FromBase64String(base64String);

            ImageSource image = ImageSource.FromStream(() => new MemoryStream(imageBytes));
            return image;
        }
    }
}
