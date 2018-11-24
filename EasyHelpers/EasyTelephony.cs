namespace System
{
	public static class EasyTelephony
	{
		public static string FixPhoneNumber(this string CellphoneNumber, string countryCode = "98", byte maxLength = 11)
		{
			if (string.IsNullOrEmpty(CellphoneNumber))
			{
				return "";
			}

			CellphoneNumber = CellphoneNumber.FixUnicodeCharacters();

			if (CellphoneNumber.StartsWith("+" + countryCode))
			{
				CellphoneNumber = "0" + CellphoneNumber.Substring(3);
			}

			if (CellphoneNumber.StartsWith("00" + countryCode))
			{
				CellphoneNumber = "0" + CellphoneNumber.Substring(4);
			}

			if (CellphoneNumber.StartsWith(countryCode))
			{
				CellphoneNumber = "0" + CellphoneNumber.Substring(2);
			}

			if (CellphoneNumber.Substring(0, 1) != "0")
			{
				CellphoneNumber = "0" + CellphoneNumber;
			}

			if (CellphoneNumber.Length != maxLength)
			{
				return "";
			}

			if (!CellphoneNumber.IsNumber())
			{
				return "";
			}

			return CellphoneNumber;
		}
	}
}
