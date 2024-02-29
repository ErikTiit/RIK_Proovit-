<h1>RIK Proovitöö</h1>

This project is a full-stack application utilizing Blazor Server application built with .NET 8.0. It uses Microsoft SQL Server for data storage and MSTest and Moq for testing.


<h1>Architecture Overview</h1>

<h2>Repositories</h2>

The Repository is a part of the data access layer in the application, specifically handling data operations. It interacts with the database context, to perform various operations.

<h2>Services</h2>
This class is part of the service layer in the application. It interacts with an external API using the HttpClient to perform various operations.
The service can retrieve information about a object from the API and can also update the details of a object. These operations are performed asynchronously, meaning they are designed to be non-blocking and to improve the performance of the application when dealing with I/O operations, such as network requests.
The service uses the IConfiguration to access the API endpoints, which are stored in the application's configuration. This allows the service to construct the correct URLs for the API requests.

<h2>Controllers</h2>
The Controller class in this application is a RESTful API controller that manages operations. It uses the routing and model binding features of ASP.NET Core to map incoming HTTP requests to action methods.
This controller provides endpoints for clients to perform CRUD (Create, Read, Update, Delete) operations on objects. Clients can retrieve a list of all objects or a specific object by its ID, create a new object, update an existing object, or delete an object.
The controller interacts with a repository to perform these operations. The repository abstracts the underlying data access, allowing the controller to focus on handling HTTP requests and responses.
The controller methods return appropriate HTTP status codes and data. For example, they return a 200 OK status for successful operations, a 404 Not Found status when an object is not found, and a 500 Internal Server Error status when an unexpected error occurs.
All operations are performed asynchronously for better performance and scalability.

These three layers work together to create a scalable and maintainable application. By separating concerns into different layers, each part of the application can be developed and tested independently, which makes the application easier to understand, modify and test.

<h1>Prerequisites</h1>

Before you begin, ensure you have met the following requirements:

•	.NET 8.0 SDK: The .NET 8.0 SDK includes everything you need to develop and run .NET applications. It includes the .NET runtime, the .NET CLI, and the .NET libraries.
•	Microsoft SQL Server: Microsoft SQL Server is a relational database management system developed by Microsoft. This project uses Microsoft SQL Server for data storage.
•	Visual Studio 2022 or Visual Studio Code: These are the recommended IDEs for .NET development. They provide features like IntelliSense, debugging, and built-in Git commands.

<h1>Setup and Installation</h1>

1.	Make sure to install all the prerequisites mentioned in the README file.
2.	Clone the repository: Use Git to clone the project repository to your local machine.
3.	Open the project with Visual Studio 2022 or visual Studio Code
4.	Navigate to the repository
5.	run the following command :  dotnet ef database update
6.	and then : dotnet run 


<h1>Running tests </h1>

1.	Open the terminal.
2.	Navigate to the RIK_Proovitöö_Testing directory
3.	run the following command:  dotnet test

Currently the unit tests cover the Services and Controllers.


<h1>Basic Diagram of the database</h1>

![image](https://github.com/ErikTiit/RIK_Proovit-/assets/97980114/6fa26b84-a36b-45f2-88e6-04004ce5d341)

