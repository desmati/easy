using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace System.Globalization
{
    public class EasyDbStringLocalizer<TDbContext> : IStringLocalizer
    {
        private readonly TDbContext _db;
        private string _cultureName;

        public EasyDbStringLocalizer(TDbContext db) : this(db, CultureInfo.CurrentUICulture) { }

        public EasyDbStringLocalizer(TDbContext db, CultureInfo cultureInfo)
        {
            _db = db;
            _cultureName = cultureInfo.Name;
        }

        public LocalizedString this[string name]
        {
            get
            {
                var value = GetString(name);
                return new LocalizedString(name, value ?? name, resourceNotFound: value == null);
            }
        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                var format = GetString(name);
                var value = string.Format(format ?? name, arguments);
                return new LocalizedString(name, value, resourceNotFound: format == null);
            }
        }

        private string GetString(string name)
        {
            throw new NotImplementedException();
            //var query = _db.Localizations.Where(l => l.Culture == _cultureName);
            //var value = query.FirstOrDefault(l => l.Key == name);
            //return value?.Value;
        }

        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            return new EasyDbStringLocalizer<TDbContext>(_db, culture);
        }
        public IEnumerable<LocalizedString> GetAllStrings(bool includeAncestorCultures)
        {
            throw new NotImplementedException();
            //return _db.Localizations.Where(l => l.Culture == _cultureName)
            //    .Select(l => new LocalizedString(l.Key, l.Value, true));
        }
    }
}
