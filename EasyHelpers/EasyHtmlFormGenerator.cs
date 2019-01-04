using System.Collections.Specialized;

namespace System.Web
{
	public class EasyHtmlFormGenerator
	{
		private NameValueCollection inputs = new NameValueCollection();

		public EasyHtmlFormGenerator(string url, string method = "POST", string formName = "form1")
		{
			Method = method;
			FormName = formName;
			Url = url;
		}

		public string Create()
		{
			var r = "";
			r += (string.Format("<form name=\"{0}\" method=\"{1}\" action=\"{2}\" >", FormName, Method, Url));
			for (int i = 0; i < inputs.Keys.Count; i++)
			{
				r += (string.Format("<input name=\"{0}\" type=\"hidden\" value=\"{1}\">", inputs.Keys[i], inputs[inputs.Keys[i]]));
			}

			r += ("</form>");
			return r;
		}

		public void AddKey(string name, object value)
		{
			inputs.Add(name, value.ToString());
		}

		public string Method { get; set; } = "post";

		public string FormName { get; set; } = "form1";

		public string Url { get; set; } = "";

	}
}
