﻿### How to Setup N-Tier Architecture?
- The Dependency Hierarchy also goes in the same order
- The Higher Level Layer Depends on the Implementation of Lower Level Layer
- PL > BLL > DAL


#### Note : Dependency Inversion Principle says the higher level should not depends on Lower Level Layer and both of them depends on abstractions
- Client will own the interface
- DI also says that they cannot depends each other but here PL > BLL > DAL it also a type of dependency
- To Achieve DI we have inverted the Depended No DAL > BLL 
- Previously PL < BLL but now it also depends on PL < DAL 
- DAL Depends on BLL
- BLL has no Dependency
- PL Depends on BLL & DAL
- This kind of architecture is known as Clean Architecture Layers (Onion View)

### Onion View
- User Interface -> |PL|  Controllers, View Models
- Domain Service -> |BLL| Domain Services, Interface Entities, Application Core
- Infrastructure -> |DAL| Repository, Impl. Servicies

### What do we called to this Clean Layers Architecture?
- Clean Architecture
- Hexagonal Architecture
- Onion Architecture
- Ports and Adapters

### What is N-tier Architecture?
1. N-tier architecture is a software design pattern in which the presentation layer (e.g., user interface), the business logic layer, and the data access layer (e.g., database) are separated into distinct tiers or layers. The idea behind this pattern is to modularize the different concerns of an application, such as UI, business logic, and data persistence, into separate layers that can be developed, tested, and maintained independently.

2. In an N-tier architecture, the presentation layer communicates with the business logic layer via a set of well-defined interfaces, and the business logic layer communicates with the data access layer in a similar way. This separation of concerns allows the different layers to be developed, tested, and maintained independently, and it makes it easier to modify or replace one layer without affecting the others.

3. There are many variations of the N-tier architecture, and the specific number of tiers can vary depending on the needs of the application. For example, a three-tier architecture might have a presentation layer, a business logic layer, and a data access layer, while a four-tier architecture might add an additional layer for security or caching.

### What is Domain Driven Design?
1. Domain-driven design (DDD) is a software design approach that focuses on modeling the core business concepts and processes of an application in the design of the software itself. It is based on the idea that the structure and behavior of the software should be closely aligned with the business domain, and that a clear understanding of the business domain is essential for effective software design.

2. In DDD, the business domain is represented using a set of model classes that represent the core concepts and processes of the business. These classes are often referred to as the "domain model." The domain model is used to drive the design of the rest of the application, including the user interface and the data access layer.

3. DDD involves a number of specific design techniques, such as the use of domain-specific languages, the identification of bounded contexts, and the use of aggregates and entities to model complex business concepts. It is often used in conjunction with object-oriented design principles and agile software development methodologies.

### Domain Driven Design VS n-Tier Architecture
1. Domain-driven design (DDD) and N-tier architecture are two separate software design approaches that can be used independently or in combination with each other.

2. DDD is a design approach that focuses on modeling the core business concepts and processes of an application in the design of the software itself. It involves creating a domain model that represents the business domain and using that model to drive the design of the rest of the application.

3. On the other hand, N-tier architecture is a design pattern that involves separating the different concerns of an application, such as the user interface, business logic, and data persistence, into distinct layers or tiers. The idea is to modularize the application in a way that allows the different layers to be developed, tested, and maintained independently.

4. While DDD and N-tier architecture are often used together, they are not mutually exclusive. It is possible to design an application using DDD principles and implement it using an N-tier architecture, or vice versa. It is also possible to use DDD or N-tier architecture independently, depending on the specific needs of the application.

### What is Domain Driven Design?
- Create Layered Application with ASP.NET Core Web Application Project
1. N-Layer Hexagonal architecture (Core, Application, Infrastructure and Presentation Layers)
2. Domain Driven Design (Entities, Repositories, Domain/Application Services, DTO’s…)
3. Clean Architecture, with applying SOLID principles
- Best practices like loosely-coupled, dependency-inverted architecture and using design patterns such as Dependency Injection, logging, validation, exception handling, localization and so on.
- Repository and Specification Design Pattern
- Test Driven Development (TDD) technics

### Dotnet Solution inside Project?
1. No, it is not possible to have a Visual Studio solution inside a project. A solution can contain multiple projects, but a project cannot contain a solution.

2. A Visual Studio solution is a container for one or more projects, and is used to manage the build, deployment, and debugging of those projects. A project, on the other hand, is a container for the source code, resources, and other files that are needed to build and run a specific application or component.

3. If you have multiple related solutions that you want to manage together, you can use a solution folder to group them within a single parent solution. This allows you to manage the solutions as a single entity, and to specify build order and other solution-level settings. However, each solution will still be a separate entity, and will need to be built and deployed independently.

### Dotnet Solution inside Another Solution
1. Yes, it is possible to have one Visual Studio solution contain another solution as a project. This is called a "solution folder," and it allows you to organize and manage multiple related projects within a single solution.

2. To create a solution folder in Visual Studio, right-click on the solution in the Solution Explorer, and select "Add" > "New Solution Folder." You can then add existing projects to the solution folder by dragging them from the Solution Explorer into the solution folder, or by right-clicking on the solution folder and selecting "Add" > "Existing Project."

3. Solution folders can be useful for organizing and grouping related projects within a solution, especially if the solution contains a large number of projects. They can also be used to manage dependencies between projects, and to specify build order and other project-level settings.


### What is the purpose of Infrastructure Layer
1. The infrastructure layer is responsible for providing the underlying services and support for the other layers in an application. Some of the specific responsibilities of the infrastructure layer might include:

- - Providing access to shared resources, such as databases, message queues, or other external systems
- - Handling low-level technical tasks, such as network communication, security, or error handling
- - Implementing infrastructure-level policies and procedures, such as authentication, authorization, or data validation
- - Providing support for cross-cutting concerns, such as logging, caching, or monitoring
2. The infrastructure layer is typically implemented as a set of reusable components or libraries that can be used by multiple applications or modules. It should be designed to be modular, flexible, and easy to maintain, so that it can evolve as the needs of the application change over time.

### InfrastrIcture Layer VS Data Access Layer
1. It is important to keep these two layers separated because it helps to maintain a clear separation of concerns. The infrastructure layer should not be concerned with data access, and the business logic should not be concerned with the technical details of how data is stored and retrieved. This allows for easier maintenance and scalability of the application over time.




