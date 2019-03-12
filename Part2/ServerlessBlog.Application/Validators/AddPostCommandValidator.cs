using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using ServerlessBlog.Commands;

namespace ServerlessBlog.Application.Validators
{
    internal class AddPostCommandValidator : AbstractValidator<AddPostCommand>
    {
        public AddPostCommandValidator()
        {
            RuleFor(cmd => cmd.Post).NotNull().SetValidator(new NewPostValidator());
        }
    }
}
