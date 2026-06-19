using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using iap.API.Dtos;
using iap.API.Models;

namespace iap.API.Validators.Track
{
public class CreateTrackValidator : AbstractValidator<CreateTrackRequestDto>
{
    public CreateTrackValidator()
    {
        // Title required
        RuleFor(x => x.Title)
            .NotEmpty()
            .MustBeValidLength();

        RuleFor(x => x.Artist!)
            .MustBeValidLength()
            .When(x => !string.IsNullOrEmpty(x.Artist));

        RuleFor(x => x.AlbumName!)
            .MustBeValidLength()
            .When(x => !string.IsNullOrEmpty(x.AlbumName));

        // Duration must be positive
        RuleFor(x => x.DurationSeconds)
            .GreaterThan(0)
            .WithMessage("Duration must be greater than 0.");

        // BlobUrl required
        RuleFor(x => x.BlobUrl)
            .NotEmpty()
            .WithMessage("Audio file URL is required.")
            .MustBeValidUrl()
            .WithMessage("BlobUrl must be a valid URL.");

        // Cover art optional but must be valid image if provided
        RuleFor(x => x.CoverArtUrl!)
            .MustBeValidImage()
            .When(x => !string.IsNullOrEmpty(x.CoverArtUrl));

        // TODO: Track number optional but must be positive if provided
        // RuleFor(x => x.AlbumTrackNumber)
        //     .GreaterThan(0)
        //     .WithMessage("Track number must be greater than 0.")
        //     .When(x => x.AlbumTrackNumber.HasValue);
    }
}
}