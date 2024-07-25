using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiBookStore.Application.GenreOperations.Commands.DeleteGenres;
using WebApiBookStore.UnitTests.TestSetup;
using Xunit;

namespace WebApiBookStore.UnitTests.Application.GenreOperations.Commands.DeleteGenres
{
    public class DeleteGenresCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldeBeReturnError(int id)
        {

            //arrange (hazırlık)
            DeleteGenresCommand command = new DeleteGenresCommand(null);
            command.ID = id;

            //act (çalıştırma)
            DeleteGenresCommandValidator validator = new DeleteGenresCommandValidator();
            var result = validator.Validate(command);

            //assert (doğrulama)
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenInvalidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {

            //arrange (hazırlık)
            DeleteGenresCommand command = new DeleteGenresCommand(null);
            command.ID = 1;

            //act (çalıştırma)
            DeleteGenresCommandValidator validator = new DeleteGenresCommandValidator();
            var result = validator.Validate(command);

            //assert (doğrulama)
            result.Errors.Count.Should().Be(0);

        }
    }
}
