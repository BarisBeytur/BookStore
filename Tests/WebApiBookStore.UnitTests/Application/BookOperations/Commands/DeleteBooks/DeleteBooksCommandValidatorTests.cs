using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiBookStore.Application.BookOperations.Commands.DeleteBooks;
using WebApiBookStore.UnitTests.TestSetup;
using Xunit;

namespace WebApiBookStore.UnitTests.Application.BookOperations.Commands.DeleteBooks
{
    public class DeleteBooksCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnError(int id)
        {
            //arrange (hazırlık)
            DeleteBooksQuery command = new DeleteBooksQuery(null);
            command.BookId = id;

            //arc (çalıştırma)
            DeleteBooksValidator validator = new DeleteBooksValidator();
            var result = validator.Validate(command);

            //assert (doğrulama)
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]

        [InlineData(2323)]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError(int id)
        {
            //arrange (hazırlık)
            DeleteBooksQuery command = new DeleteBooksQuery(null);
            command.BookId = id;

            //act (çalıştırma)
            DeleteBooksValidator validator = new DeleteBooksValidator();
            var result = validator.Validate(command);

            //assert (doğrulama)
            result.Errors.Count.Should().Be(0);

        }
    }
}
