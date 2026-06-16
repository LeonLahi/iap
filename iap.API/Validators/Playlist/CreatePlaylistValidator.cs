using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using iap.API.Dtos;
using iap.API.Models;

namespace iap.API.Validators
{
    public class CreatePlaylistValidator : AbstractValidator<CreatePlaylistRequestDto>
    {
        public CreatePlaylistValidator()
        {
            RuleFor(p => p.Name)
            .MustBeValidName();

            RuleFor(p => p.Type)
            .IsInEnum().WithMessage("Invalid playlist type.");

            RuleFor(p => p.Description!)
            .MaximumLength(360).WithMessage("Description exceeds 360 characters");

            RuleFor(p => p.CoverArtUrl!)
            // Validate url unless user leaves null
            .MustBeValidImage()
            .When(p => !string.IsNullOrEmpty(p.CoverArtUrl));
        }


    }
}