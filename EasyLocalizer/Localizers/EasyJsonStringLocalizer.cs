using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace System.Globalization
{

	public class EasyJsonStringLocalizer : IStringLocalizer
	{
		private readonly string _cultureName;

		private List<EasyPair> GetItems(string Culture)
		{
			if (EasyJsonStringLocalizerStorage.Items?.ContainsKey(Culture) == true)
			{
				return EasyJsonStringLocalizerStorage.Items[Culture];
			}

			var PathCulture = string.Format(Path.Combine(EasyJsonStringLocalizerStorage.PathItems, "{0}.json"), Culture);
			var JsonItems = File.Exists(PathCulture) ? File.ReadAllText(PathCulture) : "";
			EasyJsonStringLocalizerStorage.Items.Add(Culture, JsonItems.FromJson<List<EasyPair>>() ?? new List<EasyPair>());
			return EasyJsonStringLocalizerStorage.Items[Culture];
		}

		private void AddOrUpdateItem(string Culture, string Name, string Value)
		{

			var CultureFound = EasyJsonStringLocalizerStorage.Items?.ContainsKey(Culture) == true;
			var CultureItems = CultureFound ? EasyJsonStringLocalizerStorage.Items[Culture] : new List<EasyPair>();
			var ResourceFound = CultureItems.Contains(Name);
			CultureItems = CultureItems.AddOrUpdate(Name, Value);
			if (EasyJsonStringLocalizerStorage.Items.ContainsKey(Culture))
			{
				EasyJsonStringLocalizerStorage.Items[Culture] = CultureItems;
			}
			else
			{
				EasyJsonStringLocalizerStorage.Items.Add(Culture, CultureItems);
			}

			if (EasyConfiguration.IsDebug)
			{
				var NewCultureFound = EasyJsonStringLocalizerStorage.NewItems?.ContainsKey(Culture) == true;
				var NewCultureItems = NewCultureFound ? EasyJsonStringLocalizerStorage.NewItems[Culture] : new List<EasyPair>();
				var NewResourceFound = NewCultureItems.Contains(Name);
				NewCultureItems = NewCultureItems.AddOrUpdate(Name, Value);
				if (EasyJsonStringLocalizerStorage.NewItems.ContainsKey(Culture))
				{
					EasyJsonStringLocalizerStorage.NewItems[Culture] = NewCultureItems;
				}
				else
				{
					EasyJsonStringLocalizerStorage.NewItems.Add(Culture, NewCultureItems);
				}

				if (!NewCultureFound || !NewResourceFound)
				{
					NewCultureItems.SaveAsJson(string.Format(Path.Combine(EasyJsonStringLocalizerStorage.PathItems, "++{0}.json"), Culture));
				}
			}
		}

		public EasyJsonStringLocalizer() : this(CultureInfo.CurrentUICulture) { }

		public EasyJsonStringLocalizer(CultureInfo cultureInfo)
		{
			_cultureName = cultureInfo.Name;
			EasyJsonStringLocalizerStorage.Init();
		}

		public LocalizedString this[string name]
		{
			get
			{
				var value = GetString(name, out bool ResourceNotFound);
				return new LocalizedString(name, value ?? name, resourceNotFound: value == null);
			}
		}

		public LocalizedString this[string name, params object[] arguments]
		{
			get
			{
				var format = GetString(name, out bool ResourceNotFound);
				var value = string.Format(format ?? name, arguments);
				return new LocalizedString(name, value, resourceNotFound: ResourceNotFound);
			}
		}

		private string GetString(string name, out bool ResourceNotFound)
		{
			var CultureItems = GetItems(_cultureName);
			var Result = name;
			ResourceNotFound = !CultureItems.Contains(name);
			if (ResourceNotFound)
			{
				AddOrUpdateItem(_cultureName, name, name);
			}
			else
			{
				Result = CultureItems.TryGetValue(name);
			}

			return Result;
		}

		public IStringLocalizer WithCulture(CultureInfo culture)
		{
			return new EasyJsonStringLocalizer(culture);
		}
		public IEnumerable<LocalizedString> GetAllStrings(bool includeAncestorCultures)
		{
			return GetItems(_cultureName)
				.Select(l => new LocalizedString(l.Key, l.Value, true));
		}
	}
}
