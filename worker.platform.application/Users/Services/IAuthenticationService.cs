using worker.platform.application.Users.DTOs;

namespace worker.platform.application.Users.Services;

public interface IAuthenticationService
{
    public Task<string> SignInAsync(string email, string password);

    Task<User?> GetUserByIdAsync(int userId);

    public Task<bool> SignUpAsync(SignUpUserDto userDto);

    public Task<bool> SetupWorkerProfile(WorkerProfileDto workerProfileDto);
}
