using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiBookStore.Application.BookOperations.Commands.UpdateBooks;
using WebApiBookStore.UnitTests.TestSetup;
using Xunit;
using static WebApiBookStore.Application.BookOperations.Commands.UpdateBooks.UpdateBooksQuery;

namespace WebApiBookStore.UnitTests.Application.BookOperations.Commands.UpdateBooks
{
    public class UpdateBooksCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("",0)]
        [InlineData("",1)]
        [InlineData("A",0)]
        [InlineData("A",1)]
        [InlineData("Aa",0)]
        [InlineData("AAA",0)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, int genreid)
        {
            //arrange (hazırlık)
            UpdateBooksQuery command = new UpdateBooksQuery(null);
            command.Model = new UpdateBookViewModel
            {
                Title = title,
                GenreId = genreid
            };

            //act (çalıştırma)
            UpdateBooksValidator validator = new UpdateBooksValidator();
            var result = validator.Validate(command);

            //assert (doğrulama)
            result.Errors.Count().Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
        {
            //arrange (hazırlık)
            UpdateBooksQuery command = new UpdateBooksQuery(null);
            command.Model = new UpdateBookViewModel
            {
                Title = "Aa",
                GenreId = 1
            };

            //act (çalıştırma)
            UpdateBooksValidator validator = new UpdateBooksValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count().Should().Be(0);

        }
    }
}
