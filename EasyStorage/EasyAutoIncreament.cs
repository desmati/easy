using System.IO;

namespace System.Data
{
	public static class EasyAutoIncreament
	{
		private static readonly object Lock = new object();

		public static long NextId(string keyType, string storagePath)
		{
			lock (Lock)
			{
				long nextId = 1;
				var directory = Path.Combine(storagePath, "Keys");
				var filePath = Path.Combine(storagePath, "Keys", keyType + ".txt");

				if (!Directory.Exists(directory))
				{
					Directory.CreateDirectory(directory);
				}

				if (File.Exists(filePath))
				{
					nextId = long.Parse(File.ReadAllText(filePath)) + 1;
				}

				File.WriteAllText(filePath, nextId.ToString());

				return nextId;
			}
		}
	}
}