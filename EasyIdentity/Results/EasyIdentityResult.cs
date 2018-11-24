using System.Collections.Generic;

namespace Microsoft.AspNetCore.Identity
{
	public class EasyIdentityResult
	{
		public static EasyIdentityResult Success =>
			new EasyIdentityResult()
			{
				Succeeded = true
			};

		public bool Succeeded { get; protected set; }

		public IEnumerable<IdentityError> Errors { get; protected set; }

		public static EasyIdentityResult Failed(params IdentityError[] errors)
		{
			return new EasyIdentityResult()
			{
				Succeeded = false,
				Errors = errors
			};
		}
		public static EasyIdentityResult Failed(string Message, string Code = "")
		{
			var r = Failed(new IdentityError[]{
				new IdentityError()
				{
					Code=Code,
					Description=Message
				}
			});

			return r;
		}

		public static IdentityResult FailedIdentityResult(string Message, string Code = "")
		{
			return IdentityResult.Failed(new IdentityError[]{
				new IdentityError()
				{
					Code=Code,
					Description=Message
				}
			});
		}
	}
}
