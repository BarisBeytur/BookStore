using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiBookStore.Application.AuthorOperations.Commands.DeleteAuthors;
using WebApiBookStore.UnitTests.TestSetup;
using Xunit;

namespace WebApiBookStore.UnitTests.Application.AuthorOperations.Commands.DeleteAuthors
{
    public class DeleteAuthorsCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnError(int id)
        {
            //arrange
            DeleteAuthorsCommand command = new DeleteAuthorsCommand(null);
            command.ID = id;

            //act
            DeleteAuthorValidator validator = new DeleteAuthorValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count().Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputIsGiven_Validator_ShouldNotBeReturnError()
        {
            //arrange
            DeleteAuthorsCommand command = new DeleteAuthorsCommand(null);
            command.ID = 1;

            //act
            DeleteAuthorValidator validator = new DeleteAuthorValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count().Should().Be(0);
        }
    }
}
