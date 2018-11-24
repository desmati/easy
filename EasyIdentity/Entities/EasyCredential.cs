using System;
using System.Data;

namespace Microsoft.AspNetCore.Identity
{
	public class Credential : EasyStorableObject<Guid>
	{
		public string UserId { get; set; }
		public string CredentialTypeId { get; set; }
		public string Identifier { get; set; }
		public string Secret { get; set; }
	}
}
