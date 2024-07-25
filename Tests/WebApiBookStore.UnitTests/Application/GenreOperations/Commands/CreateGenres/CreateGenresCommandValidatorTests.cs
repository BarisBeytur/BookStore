using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiBookStore.Application.GenreOperations.Commands.CreateGenres;
using WebApiBookStore.UnitTests.TestSetup;
using Xunit;
using static WebApiBookStore.Application.GenreOperations.Commands.CreateGenres.CreateGenresCommand;

namespace WebApiBookStore.UnitTests.Application.GenreOperations.Commands.CreateGenres
{
    public class CreateGenresCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("")]
        [InlineData("a")]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(string genrename)
        {
            //arrange
            CreateGenresCommand command = new CreateGenresCommand(null, null);
            command.Model = new CreateGenreModel
            {
                GenreName = genrename
            };

            //act (çalıştırma)
            CreateGenresValidator validator = new CreateGenresValidator();
            var result = validator.Validate(command);

            //assert (doğrulama)
            result.Errors.Count().Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnErrors()
        {
            //arrange (hazırlık)
            CreateGenresCommand command = new CreateGenresCommand(null, null);
            CreateGenreModel model = new CreateGenreModel()
            {
                GenreName = "TestGenreValidator"
            };

            command.Model = model;

            //act (çalıştırma)
            CreateGenresValidator validator = new CreateGenresValidator();
            var result = validator.Validate(command);

            //assert (doğrulama)
            result.Errors.Count().Should().Be(0);
        }
    }
}
