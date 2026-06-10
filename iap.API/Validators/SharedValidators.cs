using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace iap.API.Validators
{
    public static class SharedValidators
    {
        public static IRuleBuilderOptions<T, string> MustBeValidName<T>(
            this IRuleBuilder<T, string> ruleBuilder)
        {
            // Name playlist validation
            return ruleBuilder
                .MaximumLength(180).WithMessage("Name exceeds 180 characters");
        }

        public static IRuleBuilderOptions<T, string> MustBeValidImage<T>(
            this IRuleBuilder<T, string> ruleBuilder)
        {
            // Name playlist validation
            return ruleBuilder
                .Must(BeAValidUrl).WithMessage("Must be a valid link.")
                .Must(BeAnImageUrl).WithMessage("Image URL must end with .jpg, .jpeg, .png or .webp");
        }


        // TODO: Try combine into one function
        private static bool BeAValidUrl(string url)
        {
            // Tries to access url and returns bool based on success
            return Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out Uri? _);
        }

        private static bool BeAnImageUrl(string url)
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