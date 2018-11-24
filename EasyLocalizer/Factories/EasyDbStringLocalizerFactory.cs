using Microsoft.Extensions.Localization;

namespace System.Globalization
{
	public class EasyDbStringLocalizerFactory<TDbContext> : IStringLocalizerFactory
	{
		private readonly TDbContext _db;

		public EasyDbStringLocalizerFactory(TDbContext db)
		{
			_db = db;
		}
		public IStringLocalizer Create(Type resourceSource)
		{
			return new EasyDbStringLocalizer<TDbContext>(_db);
		}

		public IStringLocalizer Create(string baseName, string location)
		{
			return new EasyDbStringLocalizer<TDbContext>(_db);
		}
	}
}
