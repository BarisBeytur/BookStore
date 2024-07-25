using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiBookStore.Application.GenreOperations.Quaries.GetGenreDetails;
using WebApiBookStore.DbOperations;
using WebApiBookStore.UnitTests.TestSetup;
using Xunit;

namespace WebApiBookStore.UnitTests.Application.GenreOperations.Quaries.GetBookDetails
{


    public class GetGenreDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly IBookContext _context;
        private readonly IMapper _mapper;

        public GetGenreDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenInvalidGenreIdIsGiven_InvalidOperationException_ShouldBeReturnError()
        {

            //arrange (hazırlık)
            GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);
            query.ID = 999;

            //act && assert (çalıştırma && doğrulama)
            FluentActions
                .Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>().And.Message
                .Should().Be("Bulunamadı");
        }

        [Fact]
        public void WhenValidGenreIdIsGiven_Genre_ShouldNotBeReturnError()
        {
            //arrange
            GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);
            query.ID = 1;

            //act
            FluentActions
                .Invoking(() => query.Handle()).Invoke();

            //assert
            var genre = _context.Genres.SingleOrDefault(x => x.GenreID == query.ID);
            genre.Should().NotBeNull();
        }
    }
}
