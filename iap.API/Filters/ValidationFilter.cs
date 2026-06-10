using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace iap.API.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Find any argument that has a validator registered
            foreach (var argument in context.ActionArguments.Values)
            {
                if (argument == null) continue;

                // Look for a validator in the DI container for this specific type
                var validatorType = typeof(IValidator<>).MakeGenericType(argument.GetType());
                var validator = context.HttpContext.RequestServices.GetService(validatorType) as IValidator;

                if (validator != null)
                {
                    var result = await validator.ValidateAsync(new ValidationContext<object>(argument));
                    if (!result.IsValid)
                    {
                        // If invalid, stop the request and return the errors
                        context.Result = new BadRequestObjectResult(result.Errors);
                        return;
                    }
                }
            }

            await next(); // If valid, move to the Controller
        }
    }
            
}