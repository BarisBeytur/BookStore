using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiBookStore.Application.AuthorOperations.Commands.CreateAuthors;
using WebApiBookStore.DbOperations;
using WebApiBookStore.Entities;
using WebApiBookStore.UnitTests.TestSetup;
using Xunit;
using static WebApiBookStore.Application.AuthorOperations.Commands.CreateAuthors.CreateAuthorCommand;

namespace WebApiBookStore.UnitTests.Application.AuthorOperations.Commands.CreateAuthors
{
    public class CreateAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly IBookContext _context;
        private readonly IMapper _mapper;

        public CreateAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistAuthorNameIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            //arrange (hazırlık)
            var author = new Author { Name = "Hüseyim" };
            _context.Authors.Add(author);
            _context.SaveChanges();

            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            command.Model = new CreateAuthorModel
            {
                Name = author.Name,
            };

            //act && assert (çalıştırma && doğrulama)
            FluentActions
                .Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message
                .Should().Be("Bu yazar zaten ekli!");
        }
        //[Fact]
        //public void WhenAlreadyExistBookAuthor_InvalidOperationException_ShouldBeReturnError()
        //{
        //    //arrange (hazırlık)
        //    var author = new Author { BookID = 1 };
        //    _context.Authors.Add(author);
        //    _context.SaveChanges();

        //    CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
        //    command.Model = new CreateAuthorModel { BookID = author.BookID };

        //    //act && assert (çalıştırma && doğrulama)
        //    FluentActions
        //        .Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message
        //        .Should().Be("Bu kitabın zaten yazarı var");
        //}

        [Fact]
        public void WhenValidInputIsGiven_Author_ShouldBeCreated()
        {
            //arrange (hazırlama)
            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            command.Model = new CreateAuthorModel { Name = "Hüsem", SurName = "Özem", BookID = 4, DateOfBirth = 1992 };

            //act (çalıştırma)
            FluentActions
                .Invoking(() => command.Handle()).Invoke();

            //assert (doğrulama)
            var author = _context.Authors.SingleOrDefault(x => x.Name == command.Model.Name);
            author.Should().NotBeNull();
            author.SurName.Should().Be(command.Model.SurName);
            author.BookID.Should().Be(command.Model.BookID);
            author.DateOfBirth.Should().Be(command.Model.DateOfBirth);
        }
    }
}
