using FluentValidation;

namespace WebApiBookStore.Application.AuthorOperations.Commands.DeleteAuthors
{
    public class DeleteAuthorValidator : AbstractValidator<DeleteAuthorsCommand>
    {
        public DeleteAuthorValidator()
        {
            RuleFor(command => command.ID).GreaterThan(0);
        }
    }
}
