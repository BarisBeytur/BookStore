using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiBookStore.Application.GenreOperations.Commands.UpdateGenres;
using WebApiBookStore.UnitTests.TestSetup;
using Xunit;
using static WebApiBookStore.Application.GenreOperations.Commands.UpdateGenres.UpdateGenresCommand;

namespace WebApiBookStore.UnitTests.Application.GenreOperations.Commands.UpdateGenres
{
    public class UpdateGenresCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("a")]
        
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string genrename)
        {

            //arrange (hazırlık)
            UpdateGenresCommand command = new UpdateGenresCommand(null);
            command.Model = new UpdateGenreModel
            {
                GenreName = genrename,

            };

            //act (çalıştırma)
            UpdateGenresCommandValidator validator = new UpdateGenresCommandValidator();
            var result = validator.Validate(command);

            //assert (doğrulama)
            result.Errors.Count().Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
        {
            //arrange (hazırlık)
            UpdateGenresCommand command = new UpdateGenresCommand(null);
            command.Model = new UpdateGenreModel
            {
                GenreName = "UpdateGenreValidatorTest"
            };

            //act (çalıştırma)
            UpdateGenresCommandValidator validator = new UpdateGenresCommandValidator();
            var result = validator.Validate(command);

            //arrange (doğrulama)
            result.Errors.Count().Should().Be(0);
        }
    }
}
