using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiBookStore.Application.AuthorOperations.Commands.DeleteAuthors;
using WebApiBookStore.DbOperations;
using WebApiBookStore.Entities;
using WebApiBookStore.UnitTests.TestSetup;
using Xunit;

namespace WebApiBookStore.UnitTests.Application.AuthorOperations.Commands.DeleteAuthors
{
    public class DeleteAuthorsCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly IBookContext _context;

        public DeleteAuthorsCommandTests (CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenInvalidAuthorIdIsGiven_InvalidOperationException_ShouldBeReturnError()
        {

            //arrange (hazırlık)
            DeleteAuthorsCommand command = new DeleteAuthorsCommand(_context);
            command.ID = 99;

            //act && assert (çalıştırma && doğrulama)
            FluentActions
                .Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message
                .Should().Be("Böyle bir yazar yok");
        }

        [Fact]
        public void WhenValidAuthorIdIsGiven_Author_ShouldBeDeleted()
        {

            //arrange
            var author = new Author { Name = "Hem", SurName = "Fam" };
            _context.Authors.Add(author);
            _context.SaveChanges();


            DeleteAuthorsCommand command = new DeleteAuthorsCommand(_context);
            command.ID = author.AuthorID;

            //act
            FluentActions
                .Invoking(() => command.Handle()).Invoke();

            //assert
            author = _context.Authors.SingleOrDefault(x => x.AuthorID == command.ID);
            author.Should().BeNull();

        }
    }
}
