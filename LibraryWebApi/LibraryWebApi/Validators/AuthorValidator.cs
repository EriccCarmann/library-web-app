using FluentValidation;
using Library.Domain.Entities;

namespace LibraryWebApi.Validators
{
    public class AuthorValidator : AbstractValidator<Author>
    {
        public AuthorValidator()
        {
            RuleFor(a => a.FirstName).NotEmpty();
            RuleFor(a => a.LastName).NotEmpty();
            RuleFor(a => a.DateOfBirth).NotEmpty();
            RuleFor(a => a.Country).NotEmpty();
        }
    }
}
