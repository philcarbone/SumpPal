using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace SumpPal
{
    public class BooleanValueToBrushConverter : IValueConverter
    {
        #region Public Methods

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var boolValue = false;
            bool.TryParse(value.ToString(), out boolValue);
            return new SolidColorBrush(boolValue ? Colors.Green : Colors.Red);
        }

        #endregion Public Methods

        #region Public Methods

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        #endregion Public Methods
    }
}