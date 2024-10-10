using FluentValidation;
using Library.Domain.Entities;

namespace Library.Domain.Validators
{
    internal class BookValidator : AbstractValidator<Book>
    {
        public BookValidator() 
        {
            RuleFor(b => b.Id).NotEmpty();
            RuleFor(b => b.Title).NotEmpty();
            RuleFor(b => b.Description).NotEmpty();
        }
    }
}
