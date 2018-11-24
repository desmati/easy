using System.Collections.Generic;

namespace System
{
	public class EasyDate
	{
		public int Year { get; set; }
		public int Month { get; set; }
		public int Day { get; set; }
		public int? Hour { get; set; }
		public int? Minute { get; set; }

		public string ToTimeString()
		{
			return ("00" + Hour).Right(2) + ":" + ("00" + Minute).Right(2);
		}
		public string ToDateString()
		{
			return ("0000" + Year).Right(4) + "/" + ("00" + Month).Right(2) + "/" + ("00" + Day).Right(2);
		}

		public DateTime ParseToMiladiDateTime()
		{
			return new DateTime(Year, Month, Day, Hour ?? 0, Minute ?? 0, 0);
		}

		public string ToDateTimeString()
		{
			return ToDateString() + " - " + ToTimeString();
		}


		public static bool operator !=(EasyDate obj1, EasyDate obj2)
		{
			return !(obj1 == obj2);
		}

		public static bool operator ==(EasyDate obj1, EasyDate obj2)
		{
			if (ReferenceEquals(obj1, obj2))
			{
				return true;
			}

			if (ReferenceEquals(obj1, null) || ReferenceEquals(obj2, null))
			{
				return false;
			}

			return (obj1.Year == obj2.Year
					&& obj1.Month == obj2.Month
					&& obj1.Day == obj2.Day
					&& obj1.Hour == obj2.Hour
					&& obj1.Minute == obj2.Minute);
		}

		public override bool Equals(object obj)
		{
			var info = obj as EasyDate;
			return info != null &&
				   Year == info.Year &&
				   Month == info.Month &&
				   Day == info.Day &&
				   EqualityComparer<int?>.Default.Equals(Hour, info.Hour) &&
				   EqualityComparer<int?>.Default.Equals(Minute, info.Minute);
		}

		public override int GetHashCode()
		{
			var hashCode = 1264244598;
			hashCode = hashCode * -1521134295 + Year.GetHashCode();
			hashCode = hashCode * -1521134295 + Month.GetHashCode();
			hashCode = hashCode * -1521134295 + Day.GetHashCode();
			hashCode = hashCode * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(Hour);
			hashCode = hashCode * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(Minute);
			return hashCode;
		}
	}
}
