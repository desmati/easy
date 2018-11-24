//namespace Microsoft.AspNetCore.Identity
//{
//	public class EasySignInResult
//	{
//		public static EasySignInResult Success
//		{
//			get
//			{
//				var r = new EasySignInResult() { Succeeded = true };
//				return r;
//			}
//		}

//		public static EasySignInResult Failed => new EasySignInResult() { Succeeded = false };

//		public static EasySignInResult LockedOut => new EasySignInResult() { Succeeded = false, IsLockedOut = true };

//		public static EasySignInResult NotAllowed => new EasySignInResult() { Succeeded = false, IsNotAllowed = true };

//		//public static EasySignInResult TwoFactorRequired => new EasySignInResult() { Succeeded = false };

//		public bool Succeeded { get; protected set; }

//		public bool IsLockedOut { get; protected set; }

//		public bool IsNotAllowed { get; protected set; }
//		public EasySignInToken Token { get; set; }

//		//public bool RequiresTwoFactor { get; protected set; }
//	}
//}
