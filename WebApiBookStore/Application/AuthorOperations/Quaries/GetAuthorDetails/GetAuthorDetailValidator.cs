using FluentValidation;


namespace WebApiBookStore.Application.AuthorOperations.Quaries.GetAuthorDetails
{
    public class GetAuthorDetailValidator : AbstractValidator<GetAuthorDetailQuery>
    {
        public GetAuthorDetailValidator()
        {
            RuleFor(query => query.ID).NotNull().GreaterThan(0);
            
        }
    }
}
