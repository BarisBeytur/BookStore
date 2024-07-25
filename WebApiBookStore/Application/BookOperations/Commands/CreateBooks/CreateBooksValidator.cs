using FluentValidation;
using System;

namespace WebApiBookStore.Application.BookOperations.Commands.CreateBooks
{
    public class CreateBooksValidator : AbstractValidator<CreateBooksQuery>
    {
        public CreateBooksValidator()
        {
            RuleFor(command => command.Model.GenreId).GreaterThan(0);
            RuleFor(command => command.Model.PageCount).GreaterThan(0);
            RuleFor(command => command.Model.PublishDate.Date).NotEmpty().LessThan(DateTime.Now.Date);
            RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(2);
        }
    }
}