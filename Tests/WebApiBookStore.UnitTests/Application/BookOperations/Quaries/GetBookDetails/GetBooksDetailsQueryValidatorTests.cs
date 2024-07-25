using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiBookStore.Application.BookOperations.Quaries.GetBookDetails;
using WebApiBookStore.UnitTests.TestSetup;
using Xunit;

namespace WebApiBookStore.UnitTests.Application.BookOperations.Quaries.GetBookDetails
{
    public class GetBooksDetailsQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnError(int id)
        {
            //arrange (hazırlık)
            GetBookDetailQuery query = new GetBookDetailQuery(null, null);
            query.BookId = id;

            //act (çalıştırma)
            GetBooksDetalValidator validator = new GetBooksDetalValidator();
            var result = validator.Validate(query);

            //assert
            result.Errors.Count().Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            //arrange (hazırlık)
            GetBookDetailQuery query = new GetBookDetailQuery(null, null);
            query.BookId = 1;

            //act (çalıştırma)
            GetBooksDetalValidator validator = new GetBooksDetalValidator();
            var result = validator.Validate(query);

            //assert
            result.Errors.Count().Should().Be(0);
        }
    }
}
