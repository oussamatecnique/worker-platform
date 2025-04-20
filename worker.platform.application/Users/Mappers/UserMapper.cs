using Riok.Mapperly.Abstractions;
using worker.platform.application.BackOffice.DTOs;
using worker.platform.application.Users.DTOs;
using worker.platform.application.Users.Helpers;

namespace worker.platform.application.Users.Mappers;

[Mapper]
public partial class UserMapper : IUserMapper
{
    [MapProperty(nameof(SignUpUserDto.Password), nameof(User.PasswordHash), Use = nameof(MapPassword))]
    [MapProperty(nameof(SignUpUserDto.Email), nameof(User.Email))]
    [MapProperty(nameof(SignUpUserDto.RoleId), nameof(User.RoleId))]
    public partial User SignupUserDtoToUser(SignUpUserDto userDto);

    [UserMapping(Default = false)]
    private static string MapPassword(string password) => PasswordHasher.HashPassword(password);


    [MapProperty(nameof(workerProfileDto.LivingCityId), nameof(Worker.CityId))]
    public partial Worker WorkerProfileDtoToWorker(WorkerProfileDto workerProfileDto);

    [MapProperty(nameof(User.Email), nameof(UserDto.Email))]
    [MapProperty(nameof(User.Role.Name), nameof(UserDto.Role))]
    public partial UserDto UserDtoToUserDto(User user);
}
