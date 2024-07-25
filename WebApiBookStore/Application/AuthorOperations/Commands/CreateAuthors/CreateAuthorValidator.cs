using FluentValidation;

namespace WebApiBookStore.Application.AuthorOperations.Commands.CreateAuthors
{
    public class CreateAuthorValidator : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorValidator()
        {
            RuleFor(command => command.Model.Name).MinimumLength(2);
            RuleFor(command => command.Model.SurName).MinimumLength(2);
            RuleFor(commond => commond.Model.DateOfBirth).GreaterThan(0);
            
        }
    }
}
