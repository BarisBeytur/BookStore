using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiBookStore.Application.AuthorOperations.Commands.CreateAuthors;
using WebApiBookStore.Application.AuthorOperations.Quaries.GetAuthorDetails;
using WebApiBookStore.DbOperations;
using WebApiBookStore.UnitTests.TestSetup;
using Xunit;
using static WebApiBookStore.Application.AuthorOperations.Commands.CreateAuthors.CreateAuthorCommand;

namespace WebApiBookStore.UnitTests.Application.AuthorOperations.Quaries.GetAuthorDetails
{
    public class GetAuthorDetailsQueryTests: IClassFixture<CommonTestFixture>
    {
        private readonly IBookContext _context;
        private readonly IMapper _mapper;

        public GetAuthorDetailsQueryTests (CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenInvalidAuthorIdIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            //arrange
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context, _mapper);
            query.ID = 999;

            //act && assert
            FluentActions
                .Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>().And.Message
                .Should().Be("Öyle Bir Yazar Yok!");
        }

        [Fact]
        public void WhenValidAuthorIdIsGiven_Author_ShouldBeGetDetails()
        {
            //arrange

            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context, _mapper);
            query.ID = 1;

            //act
            FluentActions
                .Invoking(() => query.Handle()).Invoke();

            //assert
            var author = _context.Authors.SingleOrDefault(x => x.AuthorID == query.ID);
            author.Should().NotBeNull();
        }
    }
}
