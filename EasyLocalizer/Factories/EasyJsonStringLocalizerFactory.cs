using Microsoft.Extensions.Localization;

namespace System.Globalization
{
	public class EasyJsonStringLocalizerFactory : IStringLocalizerFactory
	{
		public EasyJsonStringLocalizerFactory()
		{
		}
		public IStringLocalizer Create(Type resourceSource)
		{
			return new EasyJsonStringLocalizer();
		}

		public IStringLocalizer Create(string baseName, string location)
		{
			return new EasyJsonStringLocalizer();
		}
	}
}
