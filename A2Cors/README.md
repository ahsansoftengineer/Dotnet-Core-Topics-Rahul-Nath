## CORS (Cross-Origin Resource Sharing)
### What is CORS?
1. CORS (Cross-Origin Resource Sharing) is a security feature implemented by web browsers that blocks web pages from making requests to a different domain than the one that served the web page. It is a mechanism that is used to prevent malicious websites from making requests to your site and potentially stealing sensitive information or performing other harmful actions.

2. CORS works by adding HTTP headers to the server response that specify which domains are allowed to make requests to the server. When a client (such as a web browser) makes a request to a server, the server can include these headers in its response to indicate which domains are allowed to access the resources on the server. The client will then check the headers to see if the domain of the current web page is included in the list of allowed domains. If it is not, the client will block the request and prevent the resources from being accessed.

3. To enable CORS for a server, you will need to add the appropriate headers to the server response. The exact headers and values will depend on the resources you want to allow access to and the domains that you want to allow access from. You can find more information about CORS and how to enable it on your server in the CORS documentation.

### What is the Range of Port Number?
1. Port numbers range from 0 to 65536, but only ports numbers 0 to 1024 are reserved for privileged services and designated as well-known ports. 
2. This list of well-known port numbers specifies the port used by the server process as its contact port.

### What are the three ranges of port numbers?
- The port numbers are divided into three ranges:
1. Well-known ports. The well known ports are those from 0 - 1,023. ...
2. Registered ports. The registered ports are those from 1,024 - 49,151. ...
3. Dynamic and/or private ports. The dynamic and/or private ports are those from 49,152 - 65,535.
4. Is there a limit to port numbers?
The highest TCP port number is 65,535. The TCP protocol provides 16 bits for the port number,

### What are the CORS policy options?
- Set the allowed origins.
- Set the allowed HTTP methods.
- Set the allowed request headers.
- Set the exposed response headers.
- Credentials in cross-origin requests.
- Set the preflight expiration time.

### Same origin
1. Two URLs have the same origin if they have identical schemes, hosts, and ports (RFC 6454).
- These two URLs have the same origin:
- https://example.com/foo.html
- https://example.com/bar.html

2. These URLs have different origins than the previous two URLs:

- https://example.net: Different domain
- https://www.example.com/foo.html: Different subdomain
- http://example.com/foo.html: Different scheme
- https://example.com:9000/foo.html: Different port

### How Enable CORS for specific Route / Controllers?
1. We can use this attributes at Class / Action level
```c#
[DisableCors] // Not Required for this Path
[EnableCors]  // Required for this path


[HttpDelete("{action}/{id}")]
public IActionResult MyDelete2(int id) =>
    ControllerContext.MyDisplayRouteInfo(id);
```
### Preflighted Requests
1. A preflight request is an HTTP request that is sent by a browser before making an actual request for a resource. It is used to check whether the actual request is safe to send and to determine whether the server is willing to accept the request. Preflight requests are sent with an HTTP OPTIONS method and include a number of headers that describe the actual request that the browser is about to make.

2. Preflight requests are used when the actual request includes headers that are not considered "simple" by the browser, or when the request method is not a "simple" method. According to the CORS specification, simple methods are defined as GET, HEAD, and POST, and simple headers are those that are defined as "simple" by the specification. All other headers and methods are considered non-simple and may trigger a preflight request.

3. The purpose of a preflight request is to allow the server to decide whether it is willing to accept the actual request based on the headers and method that will be used. If the server determines that the request is safe and acceptable, it can include the appropriate headers in the response to indicate that the actual request can be sent. If the server does not allow the request, it can include the appropriate headers in the response to indicate that the request should be blocked.

### How to make Preflight Requests?
1. If you add headers to a request from client script then the browser will make first a preflight request then make an actual request.
```javascript

```

### How to Setup CORS in DOT NET CORE?
1. In Program.cs make the following changes
```c#
builder.Services.AddCors(options =>
{
  options.AddDefaultPolicy(build =>
  {
    // 1. Provide Here the External Route those will communicate with API
    // build.WithOrigins("http://localhost:3000");

    // 2. Using Configuration
    var appSettings = builder.Configuration.GetSection("AllowedOrigins").Get<AppSettings>();
    build.WithOrigins(appSettings.AllowedOrigins);
  });
});
```

### How to Setup CORS with Methods and Origins?
```c#
builder.Services.AddCors(options =>
{
  options.AddDefaultPolicy(build =>
  {
    // Provide Here the External Route those will communicate with API
    //build.WithOrigins("http://localhost:3000");

    // Adding CORS with Configuration, Origins, Headers
    var appSettings = builder.Configuration.GetSection("AllowedOrigins").Get<AppSettings>();
    build
      .WithOrigins(appSettings.AllowedOrigins)
      .WithHeaders("Authorization")
      .WithMethods("GET", "POST", "PUT", "DELETE", "PATCH");
  });
});
```

### How to Setup Naming Policy?
```c#


builder.Services.AddCors(options =>
{
  // To make a Naming of Origin Policy to be used
  options.AddPolicy("SpecificOrigin", build =>
  {
    build.WithOrigins("http://local:2020")
    .WithHeaders("Authorization");
  });
});
```