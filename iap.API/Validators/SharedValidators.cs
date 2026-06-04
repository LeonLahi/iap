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
            // Name validation
            return ruleBuilder
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(180).WithMessage("Name exceeds 180 characters");
        }
    }
}