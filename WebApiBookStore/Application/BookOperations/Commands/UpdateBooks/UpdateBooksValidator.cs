using FluentValidation;

namespace WebApiBookStore.Application.BookOperations.Commands.UpdateBooks
{
    public class UpdateBooksValidator : AbstractValidator<UpdateBooksQuery>
    {
        public UpdateBooksValidator()
        {
            RuleFor(common => common.Model.Title).MinimumLength(2);
            RuleFor(common => common.Model.GenreId).GreaterThan(0);
        }
    }
}
