using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
	public static class EasyString
	{
		public static string Right(this string input, int count)
		{
			return string.Join("", (input + "").ToCharArray().Reverse().Take(count).Reverse());
		}

		public static string Left(this string input, int count)
		{
			return string.Join("", (input + "").ToCharArray().Take(count));
		}

		public static bool IsNumber(this string str)
		{
			if (string.IsNullOrEmpty(str))
			{
				return false;
			}

			var numbers = new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
			for (var i = 0; i < str.Length; i++)
			{
				var c = str.Substring(i, 1);
				if (!byte.TryParse(c, out byte n))
				{
					return false;
				}

				if (!numbers.Contains(n))
				{
					return false;
				}
			}
			return true;
		}


		public static string RandomNumericalString(int length = 5)
		{
			var First = RandomString(1, "123456789");
			var Other = RandomString(length - 1, "0123456789");
			return First + Other;
		}

		public static string RandomString(int length = 32, string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789")
		{
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", "length cannot be less than zero.");
			}

			if (string.IsNullOrEmpty(allowedChars))
			{
				throw new ArgumentException("allowedChars may not be empty.");
			}

			const int byteSize = 0x100;
			var allowedCharSet = new HashSet<char>(allowedChars).ToArray();
			if (byteSize < allowedCharSet.Length)
			{
				throw new ArgumentException(string.Format("allowedChars may contain no more than {0} characters.", byteSize));
			}

			// Guid.NewGuid and System.Random are not particularly random. By using a
			// cryptographically-secure random number generator, the caller is always
			// protected, regardless of use.
			using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
			{
				var result = new StringBuilder();
				var buf = new byte[128];
				while (result.Length < length)
				{
					rng.GetBytes(buf);
					for (var i = 0; i < buf.Length && result.Length < length; ++i)
					{
						// Divide the byte into allowedCharSet-sized groups. If the
						// random value falls into the last group and the last group is
						// too small to choose from the entire allowedCharSet, ignore
						// the value in order to avoid biasing the result.
						var outOfRangeStart = byteSize - (byteSize % allowedCharSet.Length);
						if (outOfRangeStart <= buf[i])
						{
							continue;
						}

						result.Append(allowedCharSet[buf[i] % allowedCharSet.Length]);
					}
				}
				return result.ToString();
			}
		}

		public static string FixUnicodeCharacters(this string text, bool keepZeroJoiner = false, bool trimOutput = true)
		{
			var result = string.IsNullOrWhiteSpace(text)
				? ""
				: text.Replace(Environment.NewLine, " ").Replace("\t", " ")
					.Replace("ك", "ک").Replace("ك", "ک").Replace("ک", "ک")
					.Replace("ى", "ی").Replace("ی", "ی").Replace("ی", "ی").Replace("ي", "ی")
					.Replace("ـ", "").Replace("‌", keepZeroJoiner ? "‌" : " ").Replace("‌", keepZeroJoiner ? "‌" : " ") //نيم فاصله
					.Replace("إ", "ا").Replace("أ", "ا")
					.Replace("ۀ", "ه ").Replace("ة", "ه ")
					.Replace("ؤ", "و").Replace("ؤ", "و")
					.Replace("۱", "1").Replace("۲", "2").Replace("۳", "3")
					.Replace("۴", "4").Replace("۵", "5").Replace("۶", "6")
					.Replace("۷", "7").Replace("۸", "8").Replace("۹", "9")
					.Replace("۰", "0");
			return trimOutput
				? result.Trim()
				: result;
		}

		public static bool IsNullOrEmpty(this string str)
		{
			return str.IsNullOrEmpty();
		}
	}
}
