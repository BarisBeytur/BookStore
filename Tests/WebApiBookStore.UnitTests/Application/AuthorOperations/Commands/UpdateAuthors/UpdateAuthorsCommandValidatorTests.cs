using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiBookStore.Application.AuthorOperations.Commands.UpdateAuthors;
using WebApiBookStore.UnitTests.TestSetup;
using Xunit;
using static WebApiBookStore.Application.AuthorOperations.Commands.UpdateAuthors.UpdateAuthorsCommand;

namespace WebApiBookStore.UnitTests.Application.AuthorOperations.Commands.UpdateAuthors
{
    public class UpdateAuthorsCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("","",0)]
        [InlineData("a","",0)]
        [InlineData("a","a",0)]
        [InlineData("a","a",1)]
        [InlineData("","",1)]
        [InlineData("a","",1)]
        [InlineData("","a",1)]
        [InlineData("aa","a",1)]
        [InlineData("aa","a",0)]
        [InlineData("a","aa",1)]
        [InlineData("a","aa",0)]
        [InlineData("aa","",0)]
        [InlineData("aa","",1)]
        [InlineData("","aa",1)]
        [InlineData("","aa",0)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name, string surname, int dateofbirth)
        {
            //arrange
            UpdateAuthorsCommand command = new UpdateAuthorsCommand(null);
            UpdateAuthorsModel model = new UpdateAuthorsModel { Name = name, SurName = surname, DateOfBirth = dateofbirth };

            command.ID = 1;
            command.Model = model;

            //act
            UpdateAuthorsValidator validator = new UpdateAuthorsValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count().Should().BeGreaterThan(0);
        }
    }
}
