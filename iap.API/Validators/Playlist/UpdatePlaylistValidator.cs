using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using iap.API.Dtos;

namespace iap.API.Validators
{
    public class UpdatePlaylistValidator : AbstractValidator<UpdatePlaylistRequestDto>
    {
        public UpdatePlaylistValidator()
        {
            RuleFor(p => p.Name!)
            // Does not allow default name when updating
            // .NotEmpty().WithMessage("Playlist name required.").When(x => x.Name != null)
            .MustBeValidLength();

            RuleFor(p => p.Description!)
            .MaximumLength(360).WithMessage("Description exceeds 360 characters");

            RuleFor(p => p.CoverArtUrl!)
            // Validate url unless user leaves null
            .MustBeValidImage()
            .When(p => !string.IsNullOrEmpty(p.CoverArtUrl));
        }
    }
}