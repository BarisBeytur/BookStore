using FluentValidation;

namespace WebApiBookStore.Application.GenreOperations.Commands.DeleteGenres
{
    public class DeleteGenresCommandValidator : AbstractValidator<DeleteGenresCommand>
    {
        public DeleteGenresCommandValidator()
        {
            RuleFor(command => command.ID).GreaterThan(0);
        }
    }
}
