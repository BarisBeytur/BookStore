using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiBookStore.Application.BookOperations.Commands.CreateBooks;
using WebApiBookStore.UnitTests.TestSetup;
using Xunit;
using static WebApiBookStore.Application.BookOperations.Commands.CreateBooks.CreateBooksQuery;

namespace WebApiBookStore.UnitTests.Application.BookOperations.Commands.CreateBooks
{
    public class CreateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {

        [Theory]
        [InlineData("", 0, 0)]
        [InlineData("Lor", 0, 0)]
        [InlineData("Lor", 1, 0)]
        [InlineData("Lor", 0, 1)]
        [InlineData("Lord", 0, 0)]
        [InlineData("Lord", 1, 0)]
        [InlineData("Lord", 0, 1)]
        [InlineData("Lord", 100, 0)]
        [InlineData("", 1, 0)]
        [InlineData("", 0, 1)]

        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, int pageCount, int genreId)
        {
            //arrange
            CreateBooksQuery command = new CreateBooksQuery(null, null);
            command.Model = new CreateBookModel()
            {
                Title = title,
                PageCount = pageCount,
                GenreId = genreId,
                PublishDate = DateTime.Now.Date.AddYears(-1)
            };

            //act
            CreateBooksValidator validator = new CreateBooksValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {

            //arrange
            CreateBooksQuery command = new CreateBooksQuery(null, null);
            command.Model = new CreateBookModel()
            {
                Title = "Bukre",
                GenreId = 1,
                PageCount = 200,
                PublishDate = DateTime.Now.Date
            };

            //act
            CreateBooksValidator validator = new CreateBooksValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);

        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {

            //arrange
            CreateBooksQuery command = new CreateBooksQuery(null, null);
            command.Model = new CreateBookModel()
            {
                Title = "Bukre",
                GenreId = 1,
                PageCount = 200,
                PublishDate = DateTime.Now.Date.AddYears(-1)
            };

            //act
            CreateBooksValidator validator = new CreateBooksValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);
        }
    }

}
