using System;
using System.Globalization;
using Xamarin.Forms;

namespace FormsGroupingExample.Converters
{
	public class GroupHeightConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (Device.RuntimePlatform == Device.iOS)
				if (value.ToString().Length <= 24)
					return (Double)45;
				else
					return (Double)55;
			else  // Android      
				if (value.ToString().Length <= 24)
				return (Double)30;
			else
				return (Double)60;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value;	
		}
	}
}
