- Book to Author many to one.
- Book to BookDetails one to one.
- Book to Category many to many.
-----------------------------------------------------------------------------
API Endpoints:

* Author
- get list of paged authors ordered by author Id.
- get author by id
- get author by name paged
- post author
- put author
- patch author
- delete author

* Book
- get list of paged books ordered by date added, title.
- get book by id
- get book by title
- post book
- put book
- patch book
- delete book

* Category
- get list of paged categories ordered by date added, name.
- get category by id
- get category by name
- post category
- put category
- patch category
- delete category

* Common
- list of paged book(title,dateadded), authors, categories
-----------------------------------------------------------------------------
* Use JWT
* Use Generic pattern
* Use Repository and UOW pattern
* API versioning
* Middleware
	create that store the request URL and endpoint version.
* Use Custom Attributes
* Use Fluent Validation


-----------------------------------------------------------------------------
* Install packages to use Entitiy Framework Core
Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.Tools
Microsoft.EntityFrameworkCore.Design
Microsoft.EntityFrameworkCore.SqlServer
-----------------------------------------------------------------------------
* Entity Framework Commands
//Create migration
add-migration migrationname

//get list of existing migrations
Get-Migration

//generates a SQL script from a blank database to the latest migration
Script-Migration
// generates a SQL script from the given migration to the latest migration
Script-Migration migrationname
//generates a SQL script from the specified from migration to the specified to migration
Script-Migration migrationnameFrom migrationnameTo

//updates your database to the latest migration
update-database -verbose
// updates your database to a given migration
//Note that this can be used to roll back to an earlier migration as well
Update-Database migrationname
//If the migration has already been applied to the database, you need to 
//first roll back the database to the state before the migration by using the Update-Database command:
Update-Database -Migration:0
//Remove the last (most recent) migration
Remove-Migration

-----------------------------------------------------------------------------
Need packages  to use JWT:
Install-Package Microsoft.AspNetCore.Authentication.JwtBearer
Install-Package Microsoft.AspNetCore.Identity.EntityFrameworkCore
Install-Package Microsoft.VisualStudio.Web.CodeGeneration.Design
Install-Package System.IdentityModel.Tokens.Jwt

Generate Key: https://8gwifi.org/jwsgen.jsp
-----------------------------------------------------------------------------
AddTransient, AddScoped and AddSingleton Services Differences
https://stackoverflow.com/questions/38138100/addtransient-addscoped-and-addsingleton-services-differences#:~:text=AddTransient()%20%2D%20This%20method%20creates,per%20request%20within%20the%20scope.
- Transient objects are always different; a new instance is provided to every controller and every service.
- Scoped objects are the same within a request, but different across different requests
- Singleton objects are the same for every object and every request (regardless of whether an instance is provided in ConfigureServices)
-----------------------------------------------------------------------------
Solid Principles
https://enlabsoftware.com/development/how-to-apply-solid-principles-with-practical-examples-in-c-sharp.html
1-Single responsibility
2-Open close 
3-Liskov substitution
4-Interface segregation
5-Dependency inversion

SOLID principles tell us how to write good programs and to make our program clean.

Single responsibility
Class should be designed for single responsibility and there should not be more than one reason to make changes in this class. The responsibility of this class should be completely tied/encapsulated by the class.

Example1:
Create one class for Migrate data, one class that gets, processes, and migrates the data.
Example2:
invoice: company invoice, personal invoice, and  storage invoice …

Open Close
Open for extension and closed for modification.
Example1:
invoice: company invoice, personal invoice, and  storage invoice the create invoice method.

Liskov Substitution Principle
You should be able to use any derived class instead of a parent class and have it behave in the same manner without modification.
Example1:
invoice: company invoice, personal invoice, and  storage invoice the export  invoice method.

Interface Segregation Principle
Clients should not be forced to implement interfaces they don't use. Instead of one fat interface, many small interfaces are preferred based on groups of functions, each one serving one submodule.
Example1: IPayment Interface, pay from bank and its loan or pay by e-wallet.

Dependency Inversion Principle
High-level modules should not depend on low-level modules. Both should depend on abstractions (interface).
Abstractions should not depend upon details. Details should depend on abstractions
In high-level modules, we could use classes from low-level modules by injecting their interface via DI. The DI would create instances and provide them to the current class. This keeps BasketController and other similar classes from depending on low-level modules but just only relying on abstractions (interface).
-----------------------------------------------------------------------------
The most common ways to implement API versioning are:
1-URL versioning: https://localhost:5001/api/v1/workouts
2-Header versioning: https://localhost:5001/api/workouts -H 'api-version: 1'
3-Query parameter versioning: https://localhost:5001/api/workouts?api-version=1
install packages
Asp.Versioning.Http
Asp.Versioning.Mvc.ApiExplorer
-----------------------------------------------------------------------------

