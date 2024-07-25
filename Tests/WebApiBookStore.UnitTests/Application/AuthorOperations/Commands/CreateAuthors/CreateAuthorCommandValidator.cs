using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiBookStore.Application.AuthorOperations.Commands.CreateAuthors;
using WebApiBookStore.UnitTests.TestSetup;
using Xunit;
using static WebApiBookStore.Application.AuthorOperations.Commands.CreateAuthors.CreateAuthorCommand;

namespace WebApiBookStore.UnitTests.Application.AuthorOperations.Commands.CreateAuthors
{
    public class CreateAuthorCommandValidator : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("","",0)]
        [InlineData("","",1)]
        [InlineData("a","",0)]
        [InlineData("a","",1)]
        [InlineData("","a",0)]
        [InlineData("","a",1)]
        [InlineData("aa","",0)]
        [InlineData("aa","",1)]
        [InlineData("","aa",0)]
        [InlineData("","aa",1)]
        [InlineData("a","a",0)]
        [InlineData("a","a",1)]
        [InlineData("aa","a",0)]
        [InlineData("aa","a",1)]
        [InlineData("a","aa",0)]
        [InlineData("a","aa",1)]
        [InlineData("aaa","a",0)]
        [InlineData("aaa","a",1)]
        [InlineData("a","aaa",0)]
        [InlineData("a","aaa",1)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name, string surname, int dateofbirth)
        {
            //arrange (hazırlık)
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            command.Model = new CreateAuthorModel
            {
                Name = name,
                SurName = surname,
                DateOfBirth = dateofbirth
            };

            //act (çalıştırma)
            CreateAuthorValidator validator = new CreateAuthorValidator();
            var result = validator.Validate(command);

            //assert (doğrulama)
            result.Errors.Count().Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
        {
            //arrange
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            command.Model = new CreateAuthorModel
            {
                Name = "aa",
                SurName = "aa",
                DateOfBirth = 1
            };

            //act
            CreateAuthorValidator validator = new CreateAuthorValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count().Should().Be(0);
        }
    }
}
