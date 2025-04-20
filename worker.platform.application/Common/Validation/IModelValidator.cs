namespace worker.platform.application.Common.Validation;

public interface IModelValidator
{
    public (bool isValid, string? message) Validate<T>(T model);
}
