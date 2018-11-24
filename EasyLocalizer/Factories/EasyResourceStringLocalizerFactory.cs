using Microsoft.Extensions.Localization;

namespace System.Globalization
{
	public class EasyResourceStringLocalizerFactory : IStringLocalizerFactory
	{
		public EasyResourceStringLocalizerFactory()
		{
		}
		public IStringLocalizer Create(Type resourceSource)
		{
			return new EasyResourceStringLocalizer();
		}

		public IStringLocalizer Create(string baseName, string location)
		{
			return new EasyResourceStringLocalizer();
		}
	}
}
