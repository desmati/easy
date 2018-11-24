using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace System.Web
{
	public static class EasyWeb
	{
		public static async Task<TResult> PostAsync<TResult>(string url, object data, string TokenHeader = "", params EasyPair[] Headers)
		{
			var jsonData = data?.ToJson() ?? "";
			var client = new HttpClient();
			var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
			content.Headers.Add("Authorization", "Bearer " + TokenHeader);
			if (Headers?.Count() > 0)
			{
				foreach (var item in Headers)
				{
					content.Headers.Add(item.Key, item.Value);
				}
			}

			var json = await client.PostAsync(url, content)?.Result?.Content?.ReadAsStringAsync();
			return (json ?? "").FromJson<TResult>();
		}

		public static Uri AddOrUpdateParameter(this Uri url, string paramName, string paramValue)
		{
			var uriBuilder = new UriBuilder(url);
			var query = HttpUtility.ParseQueryString(uriBuilder.Query);

			if (query.AllKeys.Contains(paramName))
			{
				query[paramName] = paramValue;
			}
			else
			{
				query.Add(paramName, paramValue);
			}

			uriBuilder.Query = query.ToString();
			return uriBuilder.Uri;
		}
	}
}
