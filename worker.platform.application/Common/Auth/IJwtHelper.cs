namespace worker.platform.application.Common.Auth;

public interface IJwtHelper
{
    string GenerateJwtToken(int id, string email, string role);
}
