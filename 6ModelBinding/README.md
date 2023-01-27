## Model Binding

### What is Model Binding
1. Model binding allows controller actions to work directly with model types (passed in as method arguments), rather than HTTP requests. 
2. Mapping between incoming request data and application models is handled by model binders.

### What is FormaTters in ASP NET CORE?
1. Use a custom formatter to add support for a content type that isn't handled by the built-in formatters
2. For serializing data sent to the client, create an output formatter class.
3. For deserializing data received from the client, create an input formatter class.
4. Add instances of formatter classes to the InputFormatters and OutputFormatters collections in MvcOptions.
5. The framework provides built-in input and output formatters for JSON and XML. It provides a built-in output formatter for plain text, but doesn't provide an input formatter for plain text.

### Model Binding Sequence
- By Default modal binding gets data in the form of key-value pairs from the following sources in an HTTP request.

1. Form Fields
2. The Request body 
3. Route data
4. Query showing 
5. Uploaded files

### Attributes to bind data
If the default source is not correct use one of the following attributes to specify the source:
- [FromQuery] query string
- [FromRoute] route data.
- [FromForm] Gets values from posted from fields.
- [FromBody] Gets values from the request body.
- [FromHeader] Gets values from HTTP headers.
- [FromServices] It has Nothing to do with Model Binding. It is for Binding Services.

### How to Pass Array / Objects in Query Parameters?
1. https://localhost:7083/api/test/complex?Id=101&Name=Ahsan&Marks=1&Marks=2
2. Marks[0]=1&Marks[1]=2

### Note: 
1. Query Parameters are Case Insensitive abc=234 ABc=234 are same
2. Query Parameters can be mapped with Arrays and Objects
3. Implicitly Query Parameters Mapped to Endpoints Args
4. To Explicitly Map Query Parameters into an Object Use *[FromQuery] MyComplexType mct*

### How to Bind a Property from Query Parameter at Class Level?
1. If you need a QueryParameter that needs within whole class Function then you can bind it at class level
2. So you can access within every function of class without declaring in every function
```c#
    [BindProperty(SupportsGet =true, Name = "bindPropertyNameOverride")]
    public bool bindPropertyName { get; set; }
```

### JSON DATA
```json
{
    "Id":101,
    "Name":"Ahsan",
    "Description":"Description of mine",
    "Marks": [
        "101", "202"
    ]
}
```
### XML
```xml
<MyComplexType>
    <Id>102</Id>
    <Name>Furqan Majid</Name>
    <Description> XML Description </Description>
    <Marks >
        <string>666</string>
        <string>555</string>
    </Marks>
</MyComplexType>
```