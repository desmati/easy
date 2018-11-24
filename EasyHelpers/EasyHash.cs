using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace System
{
	public static class EasyHash
	{
		public static byte[] Md5HashBytes(this string inputString)
		{
			return MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(inputString));
		}

		public static IEnumerable<string> Md5Hash(this string inputString)
		{
			foreach (byte b in Md5HashBytes(inputString))
			{
				yield return (b.ToString("X2"));
			}
		}

		public static string Md5(this string input, bool isFile = false, bool toUpperCase = false)
		{
			if (isFile)
			{
				return string.Join("", Md5Hash(input));
			}

			var fileChecksum = "";
			using (var md5 = MD5.Create())
			using (var stream = File.OpenRead(input))
			{
				fileChecksum = BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "");
			}
			return toUpperCase
				? fileChecksum.ToUpper()
				: fileChecksum.ToLower();
		}

		public static string PasswordHash(this string input)
		{
			return string.Join("", Md5Hash(input + "sdghfhGyhgs65$542gHFdhFDgfd345"));
		}
	}
}
