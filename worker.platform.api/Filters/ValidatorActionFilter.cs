using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Validot;

namespace worker.platform.Filters;

public class ValidatorActionFilter: IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        foreach (var arg in context.ActionArguments)
        {
            var model = arg.Value;
            if (model == null)
            {
                continue;
            }

            var modelType = model.GetType();
            var holder = Validator.Factory
                .FetchHolders(Assembly.GetExecutingAssembly())
                .SingleOrDefault(h => h.SpecifiedType == modelType);

            if (holder == null) continue;

            var validatorType = typeof(Validator<>).MakeGenericType(modelType);

            var validator = holder.CreateValidator();

            var validateMethod = validatorType.GetMethod("Validate", new[] { modelType });
            var result = validateMethod.Invoke(validator, new[] { model });

            var anyErrorsProperty = result.GetType().GetProperty("AnyErrors");
            bool anyErrors = (bool)anyErrorsProperty.GetValue(result);

            if (anyErrors)
            {
                context.Result = new BadRequestObjectResult(result.ToString());
                return;
            }

        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {

    }
}
