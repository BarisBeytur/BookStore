using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiBookStore.Application.AuthorOperations.Commands.UpdateAuthors;
using WebApiBookStore.DbOperations;
using WebApiBookStore.Entities;
using WebApiBookStore.UnitTests.TestSetup;
using Xunit;
using static WebApiBookStore.Application.AuthorOperations.Commands.UpdateAuthors.UpdateAuthorsCommand;

namespace WebApiBookStore.UnitTests.Application.AuthorOperations.Commands.UpdateAuthors
{
    public class UpdateAuthorsCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly IBookContext _context;

        public UpdateAuthorsCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenInvalidAuthorIdIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            //arrange
            UpdateAuthorsCommand command = new UpdateAuthorsCommand(_context);
            command.ID = 999;

            //act && assert 
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message
                .Should().Be("Böyle bir yazar yok");

        }

        [Fact]
        public void WhenInvalidAuthorNameIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            //arrange
            var authord = new Author() { Name = "TestUpdateAuthor", SurName = "DEneeme", DateOfBirth = 1990 };
            _context.Authors.Add(authord);
            _context.SaveChanges();

            UpdateAuthorsCommand command = new UpdateAuthorsCommand(_context);
            command.ID = 1;
            command.Model = new UpdateAuthorsModel
            {
                Name = authord.Name,
                SurName = authord.SurName,
                DateOfBirth = authord.DateOfBirth
            };

            //act
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message
                .Should().Be("Aynı isimde yazar var");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeUpdate()
        {
            //arrange
            UpdateAuthorsCommand command = new UpdateAuthorsCommand(_context);
            UpdateAuthorsModel model = new UpdateAuthorsModel { Name = "deneme", SurName = "falan", DateOfBirth = 2002 };

            command.Model = model;
            command.ID = 1;


            //act
            FluentActions
                .Invoking(() => command.Handle()).Invoke();

            //assert
            var author = _context.Authors.SingleOrDefault(x => x.AuthorID == command.ID);
            author.Should().NotBeNull();

        }
    }
}
