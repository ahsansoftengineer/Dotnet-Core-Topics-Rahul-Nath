## Layering

### What is Layering in ASP .NET CORE?
- Layering is a design pattern in which an application is divided into layers, each with a specific responsibility. In .NET Core, layering can be used to organize the code in a web application, making it easier to maintain and understand.
- There are several different ways to implement layering in a .NET Core application. Some common layers include:
- By separating the code into different layers, it is easier to change or maintain one layer without affecting the other layers. It also makes it easier to test the application, as each layer can be tested independently.

1. Presentation layer: This layer contains the user interface of the application, including views, controllers, and view models.

2. Business logic layer: This layer contains the business logic of the application, including any rules or calculations that need to be performed.

3. Data access layer: This layer is responsible for interacting with the database or other data stores, including querying and updating data.

4. Infrastructure layer: This layer contains any utility or infrastructure code that is required by the *other layers*, such as logging, caching, or messaging.

### Files and Folder Structure of ASP .Net Core?
#### 1. Core
- Entities
- Interfaces
- Specifications
- ValueObjects
- Exceptions
#### 2. Application
- Interfaces
- Services
- Dtos
- Mapper
- Exceptions
#### 3. Infrastructure
- Logging
- Exceptions
- Messaging
#### 4. Data Access Layer
- Data
- Repository
- Services
- Migrations
#### 5. Web (Presentation Layer)
- Interfaces
- Services
- Pages
- ViewModels
- Extensions
- Mapper