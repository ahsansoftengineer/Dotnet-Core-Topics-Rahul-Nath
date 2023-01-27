### What is dotnet Core Identity?
1. ASP.NET Core Identity is a membership system that adds login functionality to ASP.NET Core web applications. It allows users to register, login, and manage their account information. The library provides a set of APIs for managing user accounts, including creating and updating user profiles, authenticating users, and managing passwords. Additionally, it supports external authentication providers, such as Google, Facebook, and Microsoft. It is built on top of the ASP.NET Core framework and can be easily integrated into new or existing web applications.

### What is Azure AD?
1. Azure Active Directory (Azure AD) is a cloud-based identity and access management service provided by Microsoft. It is used to manage and authenticate users, devices, and applications for an organization. Azure AD provides a centralized directory of users and groups, which can be used to manage access to resources both on-premises and in the cloud.

#### With Azure AD, organizations can:
1. Authenticate users with their existing on-premises Active Directory, or with cloud-based identity providers such as Microsoft, Google, Facebook, and Twitter.
2. Control access to resources based on user roles and group membership.
3. Manage and secure access to applications, including Microsoft's own software as well as third-party SaaS applications.
4. Integrate with other Azure services, such as Azure AD B2C and Azure AD B2B, to enable external users to securely access resources.
Multi-factor Authentication for added security
5. Azure AD is a fully managed service that is highly available, scalable, and secure. It can be used to provide authentication and authorization for a wide range of applications and services, including web applications, mobile apps, desktop apps, and APIs.

### What is the Fastest way of Configuring Authentication in Dotnet Core Application?
1. Dot Provide Ready template for SPA, Web, and Android apps to work with Authentication.

### Dotnet JSON Web Token (JWT)
1. JSON Web Token (JWT) is an open standard for creating and representing claims securely between parties. It is often used to authenticate and authorize users in web applications and APIs. In a JWT-based authentication system, the server creates a JSON object, also known as a "claim," that contains information about the authenticated user, such as their name, email address, and roles. This claim is then encoded into a JWT and sent to the client as a token. The client then sends this token in the headers of subsequent requests to the server in order to authenticate and authorize the user's actions.

2. In .NET framework, there is a library called "System.IdentityModel.Tokens.Jwt" which provides support for creating and validating JWT tokens. It can be used to create and validate tokens for authentication and authorization in .NET Core web applications and APIs. Additionally, it has options for encryption and signing of the tokens to secure it from tampering.

3. In case of .NET Core you have other libraries such as "Microsoft.AspNetCore.Authentication.JwtBearer" which provide the implementation for JWT tokens in ASP.NET Core web application and APIs . It allow to configure middleware that will validate and authenticate JWT tokens present in the request headers and authorize the user's actions based on the claims in the tokens.

### What is Dotnet Single Sign-On (SSO)?
1. Single Sign-On (SSO) is a mechanism that allows users to authenticate once and gain access to multiple applications or services without having to enter their credentials multiple times. With SSO, users only need to log in once, and they will be automatically logged in to all the other applications or services they have been granted access to.
2. In .NET framework, you can achieve SSO by using different libraries such as "Microsoft.IdentityModel.Protocols.WSFederation" or "Microsoft.IdentityModel.Protocols.OpenIdConnect" to implement protocols such as WS-Federation and OpenID Connect respectively. These protocols allows you to establish trust between different applications and services so that a user's identity can be seamlessly carried across different domains.
3. In .NET Core, you can use "Microsoft.AspNetCore.Authentication" which allows you to configure middleware for the SSO in your application. The library provides authentication handlers for different protocols like OpenID Connect, WS-Federation, and SAML that allows you to implement SSO using the same protocol across your different application and services.
4. Additionally you can use Azure AD or other identity providers to handle the SSO functionality.

### What is Two Factor Authentications?
1. Two-factor authentication (2FA) is a method of confirming a user's claimed identity by utilizing a combination of two different factors: something they know, something they have, or something they are. This is in contrast to a single-factor authentication, which only relies on one factor -- typically a password or passphrase. 
2. By using a second factor, it becomes more difficult for an attacker to gain unauthorized access to a user's account or device. 
3. Examples of 2FA include using a one-time code sent to a user's phone, or using a physical token, such as a security key, to verify a user's identity.

### What is Conditional Access?
1. Conditional access can be implemented in a .NET application using Azure Active Directory (Azure AD) and the Azure AD Authentication Library (ADAL) or the Microsoft Authentication Library (MSAL) .NET.

2. When using Azure AD, the application can be configured to require a user to authenticate with a specific identity provider, such as a Microsoft account or an organizational account, before allowing access to the application's resources. Additionally, the application can be configured to require multi-factor authentication (MFA) or to only allow access from specific IP addresses or networks.

3. To use conditional access in a .NET application, you'll need to register the application in Azure AD and configure the authentication settings for the application. After this, you can use the ADAL or MSAL .NET library to handle the authentication process and check the results of the authentication against your conditional access policy before granting access to the resources.

4. Additionally, you can use Azure AD Conditional Access policies to enforce these controls for your application. Once the policies are defined, it automatically applies the controls to all your clients that use Azure AD for authentication.

5. It is important to note that Conditional Access policies will only work if the user accessing the application is authenticated with Azure AD and the application is also registered with Azure AD.

### How to use Azure AD Identity in Web Application?
1. In Azure Search for "App Registration"
2. Follow the Instruction Provided in Images
3. Install Nuget Package "Microsoft.Identity.Web"
4. Update appsettings.json 
- Instance = Endpoints URL
- ClientId = ObjectID
- TenantId = Directory (Tenant) ID
- Audience = As per Our Current Package we use ObjectID here as well
```json
{
  "AzureAD": {
    "Instance": "https://login.microsoftonline.com/",
    "ClientId": "cebf3225-27fa-4123-b2c9-30ac8e70c6e9",
    "TenantId": "906ea004-6163-4a73-a490-5e29e361c443",
    "Audience": "cebf3225-27fa-4123-b2c9-30ac8e70c6e9"
  }
}
```
5. Add The Service
```c#
builder.Services.AddMicrosoftIdentityWebApiAuthentication(builder.Configuration);
```
6. Add the Middleware
```c#
app.UseRouting();
app.UseAuthentication();
app.Use(async (context, next) =>
{
  if (!context.User.Identity?.IsAuthenticated ?? false)
  {
    context.Response.StatusCode = 401;
    await context.Response.WriteAsync("Not Authenticated");
  }
  else await next();
});
```

### Dotnet Implicit Flows Full Description 
1. [O Auth Flows](https://learn.microsoft.com/en-us/azure/active-directory/develop/authentication-flows-app-scenarios)
2. [Auth 2 Implicit Grant Flow](https://learn.microsoft.com/en-us/azure/active-directory/develop/v2-oauth2-implicit-grant-flow)
3. In the Current Project we are using "No 2" Flow 
4. Query Parameters that are required to work with Identity in Dotnet Core Just Remove the Line Breaks
```json
// My Working Example
- https://login.microsoftonline.com/e98528c1-7875-4d12-aaa4-3b7b1760f132/oauth2/authorize
- ?client_id=e3801eb3-4543-46e9-978d-52ec18b8fdbd
- &response_type=token
- &redirect_uri=https://localhost:7171/weatherforecast
- &resource=e3801eb3-4543-46e9-978d-52ec18b8fdbd
- &response_mode=fragment
- &state=12345
- &nonce=678910

https://login.microsoftonline.com/e98528c1-7875-4d12-aaa4-3b7b1760f132/oauth2/authorize?client_id=cebf3225-27fa-4123-b2c9-30ac8e70c6e9&response_type=token&redirect_uri=https://localhost:7171/&resource=cebf3225-27fa-4123-b2c9-30ac8e70c6e9&response_mode=fragment&state=12345&nonce=678910

```
5. Token Given by Azure AD
```json
https://localhost:7171/access_token=eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6Ii1LSTNROW5OUjdiUm9meG1lWm9YcWJIWkdldyIsImtpZCI6Ii1LSTNROW5OUjdiUm9meG1lWm9YcWJIWkdldyJ9.eyJhdWQiOiJjZWJmMzIyNS0yN2ZhLTQxMjMtYjJjOS0zMGFjOGU3MGM2ZTkiLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC9lOTg1MjhjMS03ODc1LTRkMTItYWFhNC0zYjdiMTc2MGYxMzIvIiwiaWF0IjoxNjczNTE1NzAwLCJuYmYiOjE2NzM1MTU3MDAsImV4cCI6MTY3MzUyMDY2MiwiYWNyIjoiMSIsImFpbyI6IkFhUUFXLzhUQUFBQUJ4eFlzVUpNZ2hCajlZN0ZUMURyOFRIRXJmTXl6NUM4ZHlKSG5LZ1MrRld0NCthVzR4MmoxYjVoYldSRmJ2ZlR3TVBKQWF2dXdsUE4zZ1NmT1FqVnhrYmppR1ZnRXBhamhVcGpxZnIrVVNjOHhGNUNlWEhXUkhpSk83bWRBaGNBUzNScVlDT3RqN0UrNU9LR3RUUERXRDlramVxOVRnWFZnN2Q3YS9CZGVHQ0dHSXlnR1ZZMjJkZ2o3V3d4V2c0QXRIRlkvS1RNbUI3K0hsbFNFWnEyckE9PSIsImFtciI6WyJwd2QiLCJtZmEiXSwiYXBwaWQiOiJjZWJmMzIyNS0yN2ZhLTQxMjMtYjJjOS0zMGFjOGU3MGM2ZTkiLCJhcHBpZGFjciI6IjAiLCJlbWFpbCI6ImFoc2Fuc29mdGVuZ2luZWVyQG91dGxvb2suY29tIiwiZmFtaWx5X25hbWUiOiJBaHNhbiIsImdpdmVuX25hbWUiOiJNIiwiaWRwIjoibGl2ZS5jb20iLCJpcGFkZHIiOiIyMDIuMTY1LjIzNi4xNTYiLCJuYW1lIjoiTSBBaHNhbiIsIm9pZCI6IjFlYWNhYzJmLWM5MTItNDFjMS05NDAxLTM0NDhmODRiYWYyZiIsInJoIjoiMC5BVXNBd1NpRjZYVjRFazJxcER0N0YyRHhNaVV5djg3Nkp5TkJzc2t3ckk1d3h1bExBRlEuIiwic2NwIjoiVXNlci5SZWFkIiwic3ViIjoiQTI4dmpYRHNacTFtWFNIRXNvTVdKeEpMdFdYemQ3SlVFWjV4dTJCcS00RSIsInRpZCI6ImU5ODUyOGMxLTc4NzUtNGQxMi1hYWE0LTNiN2IxNzYwZjEzMiIsInVuaXF1ZV9uYW1lIjoibGl2ZS5jb20jYWhzYW5zb2Z0ZW5naW5lZXJAb3V0bG9vay5jb20iLCJ1dGkiOiJzeFFTZlZaak8wU0VvTHNUS2ZYZUFRIiwidmVyIjoiMS4wIn0.JqPMo-4fsaUkBiLHeqib5_bYS86M8_KihMtrmL42IgY-Uq5Yp3rAM3QjtL8DG0gZf2NBa_KsHqrauvsY3Ac3xP6faRbi_aTSk0uGujRU-Zu4OZIbsRL2WrjGVfjGVCpv8D0R9H7jTrFJUyxFbhAyOKV3Ko-UXRRFqQqzgRYXP9P61M4TLLmyQSTONLPil2IZknNvoqSKZZ05nHslDD0G1arkUmRKPHOMGuRXZhzdHdztc7YotvBVMw14JODFZUSqMGWSV7UWgU0EqosNU-HtosNWab4o0PzTyIqDCoAF9DUjI46NvToQQMwDP6-2GDt6_-oDGiCNbx6A5Dhb3-gM-A&token_type=Bearer&expires_in=4661&state=12345&session_state=1b706c21-ada5-49f7-8dac-12ff82ea6cce


&token_type=Bearer
&expires_in=4400
&state=12345
&session_state=98480b77-52c3-4418-9b89-d5f4e6184a61
```
6. Under the Hood Asp .net Core hit the Identity Provider to Validate the Token provided by Client.
