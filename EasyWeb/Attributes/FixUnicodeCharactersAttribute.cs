namespace System.ComponentModel.DataAnnotations
{
	public class FixUnicodeCharactersAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			try
			{
				validationContext
				.ObjectType
				.GetProperty(validationContext.MemberName)
				.SetValue(validationContext.ObjectInstance, value.ToString().FixUnicodeCharacters(), null);
			}
			catch (System.Exception)
			{
			}
			return ValidationResult.Success;
		}
	}
}
