using worker.platform.application.BackOffice.DTOs;
using worker.platform.application.BackOffice.Repositories;
using worker.platform.application.Common;
using worker.platform.application.Common.Helpers;
using worker.platform.application.Common.Models;
using worker.platform.application.JobsDeals.Repositories;
using worker.platform.application.Users.Helpers;
using worker.platform.application.Users.Mappers;
using worker.platform.application.Users.Repositories;

namespace worker.platform.application.BackOffice;

public class BoService: IBoService
{
    private readonly ITaskTypeParamDefinitionRepository _taskTypeParamDefinitionRepository;
    private readonly ITaskTypeRepository _taskTypeRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserMapper _userMapper;

    public BoService(ITaskTypeParamDefinitionRepository iTaskTypeParamDefinitionRepository, IUnitOfWork unitOfWork, IUserRepository userRepository, IUserMapper userMapper, ITaskTypeRepository taskTypeRepository)
    {
        _taskTypeParamDefinitionRepository = iTaskTypeParamDefinitionRepository ?? throw new ArgumentNullException(nameof(iTaskTypeParamDefinitionRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _userMapper = userMapper ?? throw new ArgumentNullException(nameof(userMapper));
        _taskTypeRepository = taskTypeRepository ?? throw new ArgumentNullException(nameof(taskTypeRepository));
    }

    public async Task<int> CreateTaskParamsDefinitionAsync(AddTaskTypeParamsDefinitionDto addTaskTypeParamsDefinitionDto, CancellationToken cancellationToken)
    {
        var jobCategoryPramsDefinitions = new List<TaskTypeParamsDefinition>();
        foreach (var attributeDefinition in addTaskTypeParamsDefinitionDto.AttributeDefinitionDtos)
        {
            jobCategoryPramsDefinitions.Add(new TaskTypeParamsDefinition
            {
                AttributeName = attributeDefinition.AttributeName,
                AttributeType = attributeDefinition.AttributeType,
                TaskTypeId = addTaskTypeParamsDefinitionDto.taskTypeId,
            });
        }

        await _taskTypeParamDefinitionRepository.AddRangeAsync(jobCategoryPramsDefinitions, cancellationToken);

        return await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<PagedResultModel<UserDto>> GetPagedUsersAsync(GetPagedUsersDto query,
        CancellationToken cancellationToken = default
    )
    {
        var resultPaged = await _userRepository.GetPagedUsersAsync(query);


        return new PagedResultModel<UserDto>
        {
            Data = resultPaged.Data.Select(x => _userMapper.UserDtoToUserDto(x)).ToList(),
            CountTotal = resultPaged.CountTotal
        };
    }

    public async Task<bool> AddAdminUserAsync(AddAdminUser data, CancellationToken cancellationToken = default)
    {
        var password = GlobalHelper.GeneratePassword(data.Email);
        var dbUser = new User
        {
            Email = data.Email,
            PasswordHash = PasswordHasher.HashPassword(password),
            RoleId = Role.Constants.AdminKey,
        };
        _userRepository.Add(dbUser);

        return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> CreateTaskTypeAsync(AddTaskTypeDto addTaskTypeDto, CancellationToken cancellationToken)
    {
        var boTaskType = new TaskType
        {
            Id = 0,
            Description = addTaskTypeDto.Description,
            JobCategoryId = addTaskTypeDto.jobCategoryId,
            Logo = null
        };
        _taskTypeRepository.Add(boTaskType);

        return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
    }
}
