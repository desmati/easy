using System;
using System.Collections.Generic;
using System.Data;

namespace Microsoft.AspNetCore.Identity
{
	public class ApplicationUser<TKey> : EasyStorableObject<TKey>
	{
		public DateTimeOffset? LockoutEnd { get; set; }
		public bool TwoFactorEnabled { get; set; }
		public bool PhoneNumberConfirmed { get; set; }
		public string PhoneNumber { get; set; }
		public string ConcurrencyStamp { get; set; }
		public string SecurityStamp { get; set; }
		public string PasswordHash { get; set; }
		public bool EmailConfirmed { get; set; }
		public string NormalizedEmail { get; set; }
		public string Email { get; set; }
		public string NormalizedUserName { get; set; }
		public string UserName { get; set; }
		public bool LockoutEnabled { get; set; }
		public int AccessFailedCount { get; set; }

		public List<string> Roles { get; set; }
		public string ConfirmPhoneNumberToken { get; set; }
		public DateTime ConfirmPhoneNumberTokenExpiresAt { get; set; }

		public string ConfirmPasswordToken { get; set; }
		public DateTime ConfirmPasswordTokenExpiresAt { get; set; }
	}
}
