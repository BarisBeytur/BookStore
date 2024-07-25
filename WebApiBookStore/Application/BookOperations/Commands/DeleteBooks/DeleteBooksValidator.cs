using FluentValidation;

namespace WebApiBookStore.Application.BookOperations.Commands.DeleteBooks
{
    public class DeleteBooksValidator : AbstractValidator<DeleteBooksQuery>
    {
        public DeleteBooksValidator()
        {
            RuleFor(commond => commond.BookId).GreaterThan(0);

        }
    }
}
