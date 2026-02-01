using System.Globalization;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using worker.platform.application.Common;
using worker.platform.application.Common.Auth;
using worker.platform.application.Common.Caching;
using worker.platform.application.Users.DTOs;
using worker.platform.application.Users.Exceptions;
using worker.platform.application.Users.Helpers;
using worker.platform.application.Users.Mappers;
using worker.platform.application.Users.Repositories;

namespace worker.platform.application.Users.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly ICacheRepository _cacheRepository;
    private readonly IUserMapper _userMapper;
    private readonly IJwtHelper _jwtHelper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AuthenticationService> _logger;

    public AuthenticationService(IUserRepository userRepository, IUserMapper userMapper, IUnitOfWork unitOfWork,
        IJwtHelper jwtHelper, ILogger<AuthenticationService> logger, ICacheRepository cacheRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _userMapper = userMapper ?? throw new ArgumentNullException(nameof(userMapper));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _jwtHelper = jwtHelper ?? throw new ArgumentNullException(nameof(jwtHelper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _cacheRepository = cacheRepository ??  throw new ArgumentNullException(nameof(cacheRepository));
    }

    public async Task<string> SignInAsync(string email, string password)
    {
        var user = await _userRepository.GetByEmailAsync(email);

        if (user is null)
        {
            throw new UserNotFoundException("User not found");
        }

        var isUserValid = PasswordHasher.VerifyPassword(password, user.PasswordHash);

        return isUserValid
            ? _jwtHelper.GenerateJwtToken(user.Id, user.Email, user.RoleId.ToString(CultureInfo.InvariantCulture))
            : throw new WrongPasswordException("Wrong password");
    }

    public async Task<User> GetUserAsync(string email)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        if (user is null)
        {
            throw new UserNotFoundException("User not found");
        }

        return user;
    }

    public async Task<User> GetUserByIdAsync(int userId)
    {
        var user = await _cacheRepository.GetOrSetAsync<User>($"user-{userId}", async (_) =>
            await _userRepository.GetUserById(userId));
        
        _ =  user ?? throw new UserNotFoundException("User not found");
        
        return user;
    } 

    public async Task<bool> SignUpAsync(SignUpUserDto userDto)
    {
        var user = _userMapper.SignupUserDtoToUser(userDto);

        _userRepository.Add(user);

        return await _unitOfWork.SaveChangesAsync() > 0;
    }

    public async Task<bool> SetupWorkerProfile(WorkerProfileDto workerProfileDto)
    {
        var worker = _userMapper.WorkerProfileDtoToWorker(workerProfileDto);

        _userRepository.AddOrUpdateWorkerProfile(worker);

        var updated = await _unitOfWork.SaveChangesAsync() > 0;

        _logger.LogInformation($"Worker profile updated: {worker}", JsonSerializer.Serialize(worker));

        return updated;
    }
}
