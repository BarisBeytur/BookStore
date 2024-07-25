using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiBookStore.Application.BookOperations.Commands.DeleteBooks;
using WebApiBookStore.DbOperations;
using WebApiBookStore.Entities;
using WebApiBookStore.UnitTests.TestSetup;
using Xunit;

namespace WebApiBookStore.UnitTests.Application.BookOperations.Commands.DeleteBooks
{
    public class DeleteBooksCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookContext _context;
        

        public DeleteBooksCommandTests(CommonTestFixture testFixture)
        {
            _context= testFixture.Context;
            
        }

        [Fact]
        public void WhenAlreadyExistNonBookIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange (hazırlık)
            DeleteBooksQuery command = new DeleteBooksQuery(_context);
            command.BookId = 3213;

            //act && assert (calıştırma && doğrulama)
            FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Böyle bir kitap yok");
        }

        [Fact]
        public void WhenValidInputAreGiven_Book_ShouldBeDeleted()
        {
            //arrange (hazırlık)
            var book = new Book { Title = "DeleteTest", GenreId = 1, PageCount = 122, PublishDate = DateTime.Now.Date.AddYears(-12)};
            new Book { Title = "DeleteTest", GenreId = 1, PageCount = 122, PublishDate = DateTime.Now.Date.AddYears(-12)};
            new Book { Title = "DeleteTest", GenreId = 1, PageCount = 122, PublishDate = DateTime.Now.Date.AddYears(-12)};
            new Book { Title = "DeleteTest", GenreId = 1, PageCount = 122, PublishDate = DateTime.Now.Date.AddYears(-12)};

            _context.Add(book);
            _context.SaveChanges();

            DeleteBooksQuery command = new DeleteBooksQuery(_context);
            command.BookId = book.Id;


            //act (çalıştırma)
            FluentActions.Invoking(() => command.Handle()).Invoke();


            //assert (doğrulama)
            book = _context.Books.SingleOrDefault(x => x.Id== book.Id);
            book.Should().BeNull();
        }
    }
}
