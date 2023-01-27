## Validation

### What is ApiController?
1. In the context of ASP.NET Core, an ApiController is a controller that handles HTTP requests specifically for APIs. It is a subclass of ControllerBase and is designed to be used in conjunction with the [ApiController] attribute.

2. The ApiController class provides a number of features that are useful for building APIs, such as automatic model validation and automatic HTTP 400 responses if validation fails. It also includes convenience methods for generating HTTP responses, such as Ok and BadRequest.

### API Controller vs MVC Controller
1. One key difference between ApiController and Controller is that ApiController disables several features by default that are not generally needed in an API, such as automatically generating JSON responses and validating request headers. This can make ApiController a more lightweight base class for building APIs, as it reduces the amount of unnecessary processing that occurs for each request.

### Validation Built-in attributes
- Here are some of the built-in validation attributes:

- [ValidateNever]: Indicates that a property or parameter should be excluded from validation.
- [CreditCard]:  Requires jQuery Validation Additional Methods.
- [Compare]: Validates that two properties in a model match.
- [EmailAddress]: Validates that the property has an email format.
- [Phone]: Validates that the property has a telephone number format.
- [Range]: Validates that the property value falls within a specified range.
- [RegularExpression]: Validates that the property value matches a specified regular expression.
- [Required]: Validates that the field isn't null
- [StringLength]: Length of Characters
- [Url]: Proper URL
- [Remote]: Validates input on the client by calling an action method on the server. See [Remote] attribute for details about this attribute's behavior.

### Validation Maximum errors
1. Validation stops when the maximum number of errors is reached (200 by default). You can configure this number with the following code in Program.cs:
```c#
builder.Services.AddRazorPages()
    .AddMvcOptions(options =>
    {
        options.MaxModelValidationErrors = 50;
        options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(
            _ => "The field is required.");
    });

builder.Services.AddSingleton
    <IValidationAttributeAdapterProvider, CustomValidationAttributeAdapterProvider>();
```

### What is Top Level Node Validation?
- Model-bound top-level nodes are validated in addition to validating model properties. In the following example from the sample app, the VerifyPhone method uses the RegularExpressionAttribute to validate the phone action parameter:
- Top-level nodes include:

1. Action parameters
2. Controller properties
3. Page handler parameters
4. Page model properties

```c#

```

### How to Disable Model Validation in ASP .Net Core?
- By Default Dotnet Core shift with Model Validation based on Data Types and Optional Variables "?"
- To Disable this we have to manually configure that in Program.cs
```c#
// Disable Model Validation
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
  options.SuppressModelStateInvalidFilter = true;
});
```

### How to Set the Default Behavior of Type Checking?
1. Customizing Default Behavior of Type Checking on Program.cs
```c#
builder.Services.AddControllers(options  =>
{
  options.SuppressAsyncSuffixInActionNames = true;
  // Option To Check Non Nullable Reference Type Required
  options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
  options.SuppressInputFormatterBuffering= true;
  options.SuppressOutputFormatterBuffering= true;
});

// Disable Model Validation
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
  options.SuppressModelStateInvalidFilter = true;
});
```
2. Customizing Default Behavior of Type Checking on appsettings.json
```xml
<Project Sdk="Microsoft.NET.Sdk.Web">
  <PropertyGroup>
    <TargetFramework>net6.0</TargetFramework>
    <!-- Disabling Validation Checking on Reference Type -->
    <Nullable>enable</Nullable>
  </PropertyGroup>
</Project
```
### Adding CustomValidation?
1. Creating Custom Validation
```c#
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
    }
  }
```
2. Modal Implementation
```c#
public class MyModal
{
    [Required]
    public int Id { get; set; }

    [Required]
    [MinLength(3)]
    //[CustomizeAttribute("M")]
    [Customize("M")]
    public string Name { get; set; }
}
```
### Custom Modal Validation Extending
- This could be used for Class Level Validation (Group Validation)
```c#
public class MyModal : IValidatableObject
{
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
```