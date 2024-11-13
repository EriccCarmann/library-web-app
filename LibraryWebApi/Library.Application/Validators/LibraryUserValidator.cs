using FluentValidation;
using Library.Domain.Entities;

namespace Library.Application.Validators
{
    public class LibraryUserValidator : AbstractValidator<LibraryUser>
    {
        public LibraryUserValidator()
        {
            RuleFor(x => x.UserName).NotNull().Length(0, 50);
            RuleFor(x => x.Email).EmailAddress().Length(0, 100);
        }
    }
}