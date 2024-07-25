using FluentValidation;

namespace WebApiBookStore.Application.BookOperations.Quaries.GetBookDetails
{
    public class GetBooksDetalValidator : AbstractValidator<GetBookDetailQuery>
    {
        public GetBooksDetalValidator()
        {
            RuleFor(common => common.BookId).GreaterThan(0);
        }
    }
}
