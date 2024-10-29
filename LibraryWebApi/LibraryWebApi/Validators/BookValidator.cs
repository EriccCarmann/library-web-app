﻿using FluentValidation;
using Library.Domain.Entities;

namespace LibraryWebApi.Validators
{
    public class BookValidator : AbstractValidator<Book>
    {
        public BookValidator() 
        {
            RuleFor(b => b.Title).NotEmpty();
            RuleFor(b => b.Description).NotEmpty();
            RuleFor(b => b.Genre).NotEmpty();
            RuleFor(b => b.AuthorId).NotEmpty();
            RuleFor(b => b.ISBN).NotEmpty().MinimumLength(13).MaximumLength(13);
        }
    }
}