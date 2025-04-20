using worker.platform.application.BackOffice.DTOs;
using worker.platform.application.Users.DTOs;

namespace worker.platform.application.Users.Mappers;

public interface IUserMapper
{
    public User SignupUserDtoToUser(SignUpUserDto userDto);

    public Worker WorkerProfileDtoToWorker(WorkerProfileDto workerProfileDto);

    public UserDto UserDtoToUserDto(User user);
}
