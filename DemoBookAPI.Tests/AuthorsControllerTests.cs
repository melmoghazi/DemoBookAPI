using DemoBookAPI.Controllers.V3;
using DemoBookAPI.Core.Consts;
using DemoBookAPI.Core.Interfaces;
using DemoBookAPI.Domain;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;

namespace DemoBookAPI.Tests
{
    public class AuthorsControllerTests
    {
        [Fact]
        public async Task GetAuthors_Returns_TheCorrectNumberOfAuthors()
        {
            //Arrange 
            var uow = A.Fake<IUnitOfWork>();
            int count = 5;
            int pageNumber = 1;
            int pageSize = 2;
            var fakeAuthors = A.CollectionOfDummy<Author>(count).AsEnumerable();

            A.CallTo(() => uow.Authors.FindAllAsync(a => a.IsActive, pageSize, pageNumber, a => a.AuthorId, OrderBy.Ascending))
                .Returns(Task.FromResult(fakeAuthors));
            var controller = new AuthorsController(uow);
            
            //Act
            var actionResult= await controller.GetAuthors(pageNumber, pageSize);

            //Assert
            var result = actionResult.Result as OkObjectResult;
            var authorsList = result.Value as IEnumerable<Author>;

            Assert.Equal(count, authorsList.Count());
        }
    }
}