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
	}
}
