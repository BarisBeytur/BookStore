using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiBookStore.Application.GenreOperations.Quaries.GetGenreDetails;
using WebApiBookStore.UnitTests.TestSetup;
using Xunit;

namespace WebApiBookStore.UnitTests.Application.GenreOperations.Quaries.GetGenreDetails
{
    public class GetGenreDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnError(int id)
        {

            //arrange (hazırlık)
            GetGenreDetailQuery query = new GetGenreDetailQuery(null, null);
            query.ID = id;

            //act (çalıştırma)
            GetGenresDetailValidator validator = new GetGenresDetailValidator();
            var result = validator.Validate(query);

            //assert (doğrulama)
            result.Errors.Count().Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputIsGiven_Validator_ShouldNotBeReturnError()
        {

            //arrange (hazırlık)
            GetGenreDetailQuery query = new GetGenreDetailQuery(null, null);
            query.ID = 1;

            //act (çalıştırma)
            GetGenresDetailValidator validator = new GetGenresDetailValidator();
            var result = validator.Validate(query);

            //act
            result.Errors.Count().Should().Be(0);

        }
    }
}
