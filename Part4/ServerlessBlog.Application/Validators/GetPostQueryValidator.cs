using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using ServerlessBlog.Commands;

namespace ServerlessBlog.Application.Validators
{
    internal class GetPostQueryValidator : AbstractValidator<GetPostQuery>
    {
        public GetPostQueryValidator()
        {
            RuleFor(query => query.PostId).NotEmpty();
        }
    }
}
