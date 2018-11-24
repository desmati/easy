using System;
using System.Data;

namespace Microsoft.AspNetCore.Identity
{
	public class EasySignInToken<TKey, TKeyUser> : EasyStorableObject<TKey>
	{
		public EasySignInToken(TKeyUser UserId)
		{
			ExpiresAt = DateTime.Now.AddMinutes(15);
			IssuedAt = DateTime.Now;
			Key = EasyString.RandomString(96);
			this.UserId = UserId;
		}

		public string Key { get; set; }
		public DateTime IssuedAt { get; set; }
		public DateTime ExpiresAt { get; set; }
		public TKeyUser UserId { get; set; }
	}
}
