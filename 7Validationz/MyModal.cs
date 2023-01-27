using System.ComponentModel.DataAnnotations;

namespace _7Validationz
{
  public class MyModal : IValidatableObject
  {
    [Required]
    public int Id { get; set; }

    [Required]
    [MinLength(3)]
    //[CustomizeAttribute("M")]
    [Customize("M")]
    public string Name { get; set; }

    [Required]
    [MinLength(3)]
    [MaxLength(100)]
    public string Description { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    [Phone]
    public string Phone { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      if(string.IsNullOrEmpty(Email) && string.IsNullOrEmpty(Phone))
      {
        yield return new ValidationResult("Either Email Or Phone is required", new[] {nameof(Phone), nameof(Email) });
      }
      yield return ValidationResult.Success;
    }
  }
}
