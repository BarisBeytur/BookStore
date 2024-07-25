using FluentValidation;

namespace WebApiBookStore.Application.GenreOperations.Commands.CreateGenres
{
    public class CreateGenresValidator : AbstractValidator<CreateGenresCommand>
    {
        public CreateGenresValidator()
        {
            
            RuleFor(commond => commond.Model.GenreName).MinimumLength(2);
        }
    }
}
