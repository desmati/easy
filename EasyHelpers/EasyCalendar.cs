using System.Globalization;
using System.Text;

namespace System
{
	public static class EasyCalendar
	{
		public static string ToPersianDate(this DateTime dateTime)
		{
			var shamsi = dateTime.ToPersianDateInfo();
			var pc = new PersianCalendar();
			StringBuilder sb = new StringBuilder();
			sb.Append(shamsi.Year.ToString("0000"));
			sb.Append("/");
			sb.Append(shamsi.Month.ToString("00"));
			sb.Append("/");
			sb.Append(shamsi.Day.ToString("00"));
			return sb.ToString();
		}
		public static EasyDate ToPersianDateInfo(this DateTime dateTime)
		{
			PersianCalendar pc = new PersianCalendar();
			var r = new EasyDate()
			{
				Year = pc.GetYear(dateTime),
				Month = pc.GetMonth(dateTime),
				Day = pc.GetDayOfMonth(dateTime),
				Hour = pc.GetHour(dateTime),
				Minute = pc.GetMinute(dateTime)
			};
			return r;
		}

		public static DateTime ToMiladiDateTime(this string s)
		{
			s = ("" + s).FixUnicodeCharacters();
			if (s.Length < 6)
			{
				return DateTime.MinValue;
			}

			var sal = "";
			var mah = "";
			var roz = "";
			var splitter = s.Contains("/") ? '/' : s.Contains("-") ? '-' : s.Contains(" ") ? ' ' : s.Contains(".") ? '.' : '?';
			var parts = new string[3];
			if (splitter != '?')
			{
				if (parts.Length != 3)
				{
					return DateTime.MinValue;
				}

				parts = s.Split(splitter);
				sal = parts[0];
				if (sal.Length == 2)
				{
					sal = "13" + sal;
				}

				mah = parts[1];
				roz = parts[2];
			}
			else
			{
				if (s.IsNumber())
				{
					switch (s.Length)
					{
						case 8://13641108
							sal = s.Substring(0, 4);
							mah = s.Substring(4, 2);
							roz = s.Substring(6, 2);
							break;
						case 6://641108
							sal = "13" + s.Substring(0, 2);
							mah = s.Substring(2, 2);
							roz = s.Substring(4, 2);
							break;
					}
				}
				else
				{
					return DateTime.MinValue;
				}
			}

			PersianCalendar pc = new PersianCalendar();
			var ret = pc.ToDateTime(Convert.ToInt32(sal), Convert.ToInt32(mah), Convert.ToInt32(roz), 0, 0, 0, 0);
			return ret;
		}
		public static EasyDate ToDateiInfo(this DateTime dt)
		{
			return new EasyDate()
			{
				Year = dt.Year,
				Month = dt.Month,
				Day = dt.Day,
				Hour = dt.Hour,
				Minute = dt.Minute
			};
		}

		public static EasyDate ToMiladiInfo(this EasyDate s)
		{
			return ToMiladiDateTime(s).ToDateiInfo();
		}

		public static string ToShamsiDateTimeString(this EasyDate s)
		{
			return s.ToShamsiDateString() + " - " + s.Hour + ":" + s.Minute;
		}

		public static string ToShamsiDateString(this EasyDate s)
		{
			return new DateTime(s.Year, s.Month, s.Day).ToPersianDate();
		}

		public static DateTime ToMiladiDateTime(this EasyDate s)
		{
			PersianCalendar pc = new PersianCalendar();
			var ret = pc.ToDateTime(s.Year, s.Month, s.Day, s.Hour ?? 0, s.Minute ?? 0, 0, 0);
			return ret;
		}

		public static string ToMiladi(this string s)
		{
			return ToMiladiDateTime(s).ToShortDateString();
		}
	}
}
