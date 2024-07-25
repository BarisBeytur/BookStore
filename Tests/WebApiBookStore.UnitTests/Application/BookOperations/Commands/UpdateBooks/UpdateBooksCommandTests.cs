using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiBookStore.Application.BookOperations.Commands.UpdateBooks;
using WebApiBookStore.DbOperations;
using WebApiBookStore.UnitTests.TestSetup;
using Xunit;
using static WebApiBookStore.Application.BookOperations.Commands.UpdateBooks.UpdateBooksQuery;

namespace WebApiBookStore.UnitTests.Application.BookOperations.Commands.UpdateBooks
{
    public class UpdateBooksCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookContext _context;


        public UpdateBooksCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;

        }

        [Fact]
        public void WhenNonExistBookIdIsGiven_Update_ShouldBeReturnError()
        {
            //arrange (hazırlık)
            UpdateBooksQuery command = new UpdateBooksQuery(_context);
            command.BookId = 999;

            //act && assert (çalıştırma && doğrulama)
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("bu id ile bir kitap yok");
        }

        [Fact]
        public void WhenValidInputAreGiven_Update_ShouldNotBeReturnError()
        {
            //arrange (hazırlık)
            UpdateBooksQuery command = new UpdateBooksQuery(_context);
            UpdateBookViewModel model = new UpdateBookViewModel { Title = "UpdateTest", GenreId = 1 };

            command.Model = model;
            command.BookId = 1;

            //act
            FluentActions
                .Invoking(() => command.Handle()).Invoke();

            //assert
            var book = _context.Books.SingleOrDefault(x => x.Title == model.Title);
            book.Should().NotBeNull();
            book.GenreId.Should().Be(model.GenreId);


        }


    }
}
