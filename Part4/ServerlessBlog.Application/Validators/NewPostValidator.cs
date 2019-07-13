using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using ServerlessBlog.Commands.Models;

namespace ServerlessBlog.Application.Validators
{
    internal class NewPostValidator : AbstractValidator<NewPost>
    {
        public NewPostValidator()
        {
            RuleFor(post => post.Body).NotEmpty();
            RuleFor(post => post.Title).NotEmpty().MaximumLength(128);
        }
    }
}
