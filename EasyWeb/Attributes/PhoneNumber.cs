using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Collections.Generic;
using System.Linq;

namespace System.ComponentModel.DataAnnotations
{
	public class PhoneNumberAttribute : Attribute, IModelValidator
	{
		public string ErrorMessage { get; set; }
		public string CountryCode { get; set; } = "98";
		public byte MaxLength { get; set; } = 11;

		IEnumerable<ModelValidationResult> IModelValidator.Validate(ModelValidationContext context)
		{
			var value = context.Model + "";
			var phoneNumber = value.FixPhoneNumber(CountryCode, MaxLength);

			if (phoneNumber.IsNullOrEmpty())
			{
				return new List<ModelValidationResult>
				{
					new ModelValidationResult("", ErrorMessage)
				};
			}

			return Enumerable.Empty<ModelValidationResult>();
		}
	}
}
