using System.Globalization;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using worker.platform.application.Common;
using worker.platform.application.Common.Auth;
using worker.platform.application.Common.Exceptions;
using worker.platform.application.Common.Validation;
using worker.platform.application.Users.DTOs;
using worker.platform.application.Users.Exceptions;
using worker.platform.application.Users.Helpers;
using worker.platform.application.Users.Mappers;
using worker.platform.application.Users.Repositories;

namespace worker.platform.application.Users.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserMapper _userMapper;
    private readonly IJwtHelper _jwtHelper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AuthenticationService> _logger;
    private readonly IModelValidator _modelValidator;

    public AuthenticationService(IUserRepository userRepository, IUserMapper userMapper, IUnitOfWork unitOfWork,
        IJwtHelper jwtHelper, IModelValidator modelValidator, ILogger<AuthenticationService> logger
    )
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _userMapper = userMapper ?? throw new ArgumentNullException(nameof(userMapper));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _jwtHelper = jwtHelper ?? throw new ArgumentNullException(nameof(jwtHelper));
        _modelValidator = modelValidator ?? throw new ArgumentNullException(nameof(modelValidator));
        _logger = logger;
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

    public async Task<User?> GetUserByIdAsync(int userId) => await _userRepository.GetUserById(userId);

    public async Task<bool> SignUpAsync(SignUpUserDto userDto)
    {
        var (isValid, errorMessage) = _modelValidator.Validate(userDto);

        if (!isValid)
        {
            throw new ValidationException(errorMessage);
        }

        var user = _userMapper.SignupUserDtoToUser(userDto);

        _userRepository.Add(user);

        return await _unitOfWork.SaveChangesAsync() > 0;
    }

    public async Task<bool> SetupWorkerProfile(WorkerProfileDto workerProfileDto)
    {
        var (isValid, errorMessage) = _modelValidator.Validate(workerProfileDto);
        if (!isValid)
        {
            throw new ValidationException(errorMessage);
        }
        var worker = _userMapper.WorkerProfileDtoToWorker(workerProfileDto);

        _userRepository.AddOrUpdateWorkerProfile(worker);

        var updated = await _unitOfWork.SaveChangesAsync() > 0;

        _logger.LogInformation($"Worker profile updated: {worker}", JsonSerializer.Serialize(worker));

        return updated;
    }
}
