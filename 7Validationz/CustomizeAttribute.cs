using System.ComponentModel.DataAnnotations;

namespace _7Validationz
{
  public class CustomizeAttribute : ValidationAttribute
  {
    private readonly string _startswith;

    public CustomizeAttribute(string startswith)
    {
      _startswith= startswith;
    }

    protected override ValidationResult IsValid(object value, ValidationContext valCont)
    {
      if(value is string valueString && !valueString.StartsWith(_startswith)) {
        return 
          new ValidationResult($"{valCont.MemberName} does not start with {_startswith}");
      }
      return ValidationResult.Success;
      //return base.IsValid(value, valCont);
    }
  }
}
