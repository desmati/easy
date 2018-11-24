namespace System.IO
{
	public static class EasyFiles
	{
		public static string SaveFileFromBase64String(this string fileData, string path, string fileName = null)
		{
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
			//data:image/png;base64,kbsdnlfkdfn
			var parts = fileData.Split(',');
			var infoPart = parts[0];
			var dataPart = parts[1];
			var mimeType = infoPart.Split(';')[0].Split(':')[1];
			fileName = (fileName ?? Guid.NewGuid().ToString()) + "." + mimeType.GetExtension();
			string filePath = Path.Combine(path, fileName);
			byte[] fileBytes = Convert.FromBase64String(dataPart);
			File.WriteAllBytes(filePath, fileBytes);
			return fileName;
		}

		public static void SaveAsJson(this object obj, string Path)
		{
			var json = obj.ToJson();
			File.WriteAllText(Path, json);
		}

		public static T JsonFileToObject<T>(this string Path)
		{
			if (string.IsNullOrEmpty(Path) || !File.Exists(Path))
			{
				return default(T);
			}

			var json = File.ReadAllText(Path);
			if (string.IsNullOrEmpty(json))
			{
				return default(T);
			}

			try
			{
				return json.FromJson<T>();
			}
			catch
			{
				return default(T);
			}
		}
	}
}
