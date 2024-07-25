using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiBookStore.Application.BookOperations.Quaries.GetBookDetails;
using WebApiBookStore.DbOperations;
using WebApiBookStore.UnitTests.TestSetup;
using Xunit;

namespace WebApiBookStore.UnitTests.Application.BookOperations.Quaries.GetBookDetails
{
    public class GetBookDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookContext _context;
        private readonly IMapper _mapper;

        public GetBookDetailQueryTests(CommonTestFixture textFixture)
        {
            _context = textFixture.Context;
            _mapper = textFixture.Mapper;
        }

        [Fact]
        public void WhenNonExistBookIdIsGiven_Get_ShouldBeReturnError()
        {
            //arrange (hazırlık)
            GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
            query.BookId = 999;

            //act && assert (çalıştırma && doğrulama)
            FluentActions
                .Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Bulunamadı");
        }

        [Fact]
        public void WhenValidBookIdIsGiven_Get_ShouldNotBeReturnError()
        {
            //arrange
            GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
            query.BookId = 1;

            //act
            FluentActions
                .Invoking(() => query.Handle()).Invoke();

            //assert (çalıştırma)
            var book = _context.Books.SingleOrDefault(x => x.Id == query.BookId);
            book.Should().NotBeNull();
        }
    }
}
