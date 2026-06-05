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
            RuleFor(p => p.Name).MaximumLength(180).WithMessage("Name exceeds 180 characters");

            // Description validation
            RuleFor(p => p.Description).MaximumLength(360).WithMessage("Description exceeds 360 characters");

            // Cover art 
            RuleFor(p => p.CoverArtUrl)
            // Validate url unless user leaves null
            .Must(BeAValidUrl).When(p => !string.IsNullOrEmpty(p.CoverArtUrl)).WithMessage("Cover art must be a valid link.")
            .Must(BeAnImageUrl).When(p => !string.IsNullOrEmpty(p.CoverArtUrl)).WithMessage("Cover art URL must end with .jpg, .jpeg, .png or .webp");

        }

        private bool BeAValidUrl(string url)
        {
            // Tries to access url and returns bool based on success
            return Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out Uri? _);
        }

        private bool BeAnImageUrl(string url)
        {
            // Removes any query parameters after extension
            var cleanUrl = url.Split('?')[0];

            // Stores everything after and including '.'
            var extension = Path.GetExtension(cleanUrl).ToLower();
            
            var allowedExtensions = new[] {".jpg", ".jpeg", ".png", ".webp"};

            // Returns bool if url has any of allowed extensions
            return allowedExtensions.Contains(extension);
        }


    }
}