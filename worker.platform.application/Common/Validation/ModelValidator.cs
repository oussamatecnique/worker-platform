using System.Reflection;
using Validot;

namespace worker.platform.application.Common.Validation;

public class ModelValidator: IModelValidator
{
    public (bool isValid, string? message) Validate<T>(T model)
    {
        var holder = Validator.Factory.FetchHolders(Assembly.GetExecutingAssembly()).Single(h => h.SpecifiedType == typeof(T));

        var validator = (Validator<T>)holder.CreateValidator();

        var result = validator.Validate(model);

        if (result.AnyErrors)
        {
            return (false, result.ToString());
        }

        return (true, null);
    }
}
