using Microsoft.AspNetCore.Http;

namespace System.Web
{
	public static class CurrentContext
	{
		static IServiceProvider services = null;

		/// <summary>
		/// Provides static access to the framework's services provider
		/// </summary>
		public static IServiceProvider Services
		{
			get => services;
			set
			{
				if (services != null)
				{
					throw new Exception("Can't set once a value has already been set.");
				}
				services = value;
			}
		}

		/// <summary>
		/// Provides static access to the current HttpContext
		/// </summary>
		public static HttpContext Current
		{
			get
			{
				IHttpContextAccessor httpContextAccessor = services.GetService(typeof(IHttpContextAccessor)) as IHttpContextAccessor;
				return httpContextAccessor?.HttpContext;
			}
		}

		public static T GetFromSession<T>(string key)
		{
			try
			{
				return Current.Session.Get(key).FromBytes().FromJson<T>();
			}
			catch
			{
				return default(T);
			}
		}

		public static void SetInSession(string key, object value)
		{
			Current.Session.Set(key, value.ToJson().ToBytes());
		}

	}
}