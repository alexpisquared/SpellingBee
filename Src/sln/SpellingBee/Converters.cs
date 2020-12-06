using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace SpellingBee
{
	public class TextToPromptConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return (value.ToString().Length>0) ?
				"Submit" :"Re-Read";
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string strValue = value as string;
			DateTime resultDateTime;
			if (DateTime.TryParse(strValue, out resultDateTime))
			{
				return resultDateTime;
			}
			return DependencyProperty.UnsetValue;
		}

	}

	public class TextToBoolConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value.ToString().Length > 0;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string strValue = value as string;
			DateTime resultDateTime;
			if (DateTime.TryParse(strValue, out resultDateTime))
			{
				return resultDateTime;
			}
			return DependencyProperty.UnsetValue;
		}

	}
}
