- Book to Author many to one.
- Book to BookDetails one to one.
- Book to Category many to many.
-----------------------------------------------------------------------------
API Endpoints:
Author
- get list of paged authors ordered by author Id.
- get author by id
- get author by name paged
- post author
- put author
- patch author
- delete author

Book
- get list of paged books ordered by date added, title.
- get book by id
- get book by title
- post book
- put book
- patch book
- delete book

Category
- get list of paged categories ordered by date added, name.
- get category by id
- get category by name
- post category
- put category
- patch category
- delete category

Common
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
Install packages to use Entitiy Framework Core
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Tools
- Microsoft.EntityFrameworkCore.Design
- Microsoft.EntityFrameworkCore.SqlServer
-----------------------------------------------------------------------------
Need packages  to use JWT:
- Install-Package Microsoft.AspNetCore.Authentication.JwtBearer
- Install-Package Microsoft.AspNetCore.Identity.EntityFrameworkCore
- Install-Package Microsoft.VisualStudio.Web.CodeGeneration.Design
- Install-Package System.IdentityModel.Tokens.Jwt

Generate Key: https://8gwifi.org/jwsgen.jsp
-----------------------------------------------------------------------------
