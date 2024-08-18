Book to Author many to one.
Book to BookDetails one to one.
Book to Category many to many.
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

* API versioning
* Middleware
	create that store the request URL and endpoint version.
* Use Generic pattern
* Use Custom Attributes
* Use JWT
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