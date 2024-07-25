using FluentValidation;

namespace WebApiBookStore.Application.AuthorOperations.Commands.UpdateAuthors
{
    public class UpdateAuthorsValidator : AbstractValidator<UpdateAuthorsCommand>
    {
        public UpdateAuthorsValidator()
        {
            RuleFor(command => command.Model.Name).MinimumLength(2);
            RuleFor(command => command.Model.SurName).MinimumLength(2);
            RuleFor(command => command.Model.DateOfBirth).GreaterThan(0);
        }
    }
}
