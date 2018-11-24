using System.Data;

namespace Microsoft.AspNetCore.Identity
{
	public class ApplicationRole<TKey> : EasyStorableObject<TKey>
	{
		public string Name { get; set; }
		public string NormalizedName { get; set; }
		public string ConcurrencyStamp { get; set; }
	}
}
