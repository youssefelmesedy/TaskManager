namespace TaskManager.Attributes
{
    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime date && date <= DateTime.Now)
            {
                return new ValidationResult("يجب أن يكون تاريخ التسليم في المستقبل.");
            }
            return ValidationResult.Success;
        }
    }
}
