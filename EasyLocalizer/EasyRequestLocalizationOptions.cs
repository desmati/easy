using Microsoft.AspNetCore.Builder;

namespace Microsoft.Extensions.Localization
{
	public class EasyRequestLocalizationOptions : RequestLocalizationOptions
	{
		public string ResourcesPath { get; set; }
	}
}
