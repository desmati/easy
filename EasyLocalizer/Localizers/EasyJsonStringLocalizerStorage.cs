using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.IO;

namespace System.Globalization
{

	public static class EasyJsonStringLocalizerStorage
	{
		public static Dictionary<string, List<EasyPair>> Items { get; set; } = new Dictionary<string, List<EasyPair>>();
		public static Dictionary<string, List<EasyPair>> NewItems { get; set; } = new Dictionary<string, List<EasyPair>>();

		public static EasyRequestLocalizationOptions Options;
		public static string PathItems;
		public static string ResourcesPath = "Resources";
		private static bool Inited = false;
		public static void Init()
		{
			if (Inited)
			{
				return;
			}

			Options = EasyServiceProvider.Current?.GetService<IOptions<EasyRequestLocalizationOptions>>()?.Value;
			if (!string.IsNullOrEmpty(Options?.ResourcesPath))
			{
				ResourcesPath = Options.ResourcesPath;
			}

			PathItems = Path.Combine(EasyConfiguration.WWWRootPath, ResourcesPath);
			if (!Directory.Exists(PathItems))
			{
				Directory.CreateDirectory(PathItems);
			}

			Inited = true;
		}
	}
}