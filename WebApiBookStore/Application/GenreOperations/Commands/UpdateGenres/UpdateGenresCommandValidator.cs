using FluentValidation;

namespace WebApiBookStore.Application.GenreOperations.Commands.UpdateGenres
{
    public class UpdateGenresCommandValidator : AbstractValidator<UpdateGenresCommand>
    {
        public UpdateGenresCommandValidator()
        {
            RuleFor(command => command.Model.GenreName).MinimumLength(2)/*.When(x => x.Model.GenreName.Trim() != string.Empty)*/;
            
        }
    }
}
