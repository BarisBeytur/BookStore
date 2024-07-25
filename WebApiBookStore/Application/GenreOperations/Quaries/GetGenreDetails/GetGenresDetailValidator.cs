using FluentValidation;

namespace WebApiBookStore.Application.GenreOperations.Quaries.GetGenreDetails
{
    public class GetGenresDetailValidator : AbstractValidator<GetGenreDetailQuery>
    {
        public GetGenresDetailValidator()
        {
            RuleFor(common => common.ID).GreaterThan(0);
        }
    }
}
