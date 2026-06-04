using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using iap.API.Dtos;

namespace iap.API.Validators
{
    public class CreatePlaylistValidator : AbstractValidator<CreatePlaylistRequestDto>
    {
        public CreatePlaylistValidator()
        {
            // Name validation
            RuleFor(p => p.Name).MustBeValidName();
        }
    }
}