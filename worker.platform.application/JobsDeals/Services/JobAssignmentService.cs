using worker.platform.application.Common;
using worker.platform.application.JobsDeals.Mappers;
using worker.platform.application.JobsDeals.Repositories;
using worker.platform.application.JobsDeals.Validators;

namespace worker.platform.application.JobsDeals.Services;

public class JobAssignmentService : IJobAssignmentService
{
    private readonly IJobRequestRepository _jobRequestRepository;
    private readonly IJobAssignmentMapper _jobAssignmentMapper;
    private readonly IJobResponseRepository _jobResponseRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJobAssignmentValidator _jobAssignmentValidator;

    public JobAssignmentService(IJobRequestRepository jobRequestRepository,
        IJobAssignmentMapper jobAssignmentMapper, IJobResponseRepository jobResponseRepository, IUnitOfWork unitOfWork, IJobAssignmentValidator jobAssignmentValidator
    )
    {
        _jobRequestRepository = jobRequestRepository ?? throw new ArgumentNullException(nameof(jobRequestRepository));
        _jobAssignmentMapper = jobAssignmentMapper ?? throw new ArgumentNullException(nameof(jobAssignmentMapper));
        _jobResponseRepository =
            jobResponseRepository ?? throw new ArgumentNullException(nameof(jobResponseRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _jobAssignmentValidator = jobAssignmentValidator ?? throw new ArgumentNullException(nameof(jobAssignmentValidator));
    }

    public async Task<bool> AddJobRequestAsync(AddJobRequestDto addJobRequestDto, CancellationToken cancellationToken)
    {
        var (isValid, message) = await _jobAssignmentValidator.ValidateJobRequestAsync(addJobRequestDto);
        if (!isValid)
        {
            throw new InvalidOperationException(message);
        }
        var jobRequest = _jobAssignmentMapper.MapAddRequestDto(addJobRequestDto);
        _jobRequestRepository.Add(jobRequest);

        return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> ApplyForJobRequestAsync(JobResponseDto jobResponseDtoDto,
        CancellationToken cancellationToken
    )
    {
        var (isValid, message) = await _jobAssignmentValidator.ValidateJobResponseAsync(jobResponseDtoDto);
        if (!isValid)
        {
            throw new InvalidOperationException(message);
        }

        var jobResponse = _jobAssignmentMapper.MapJobApplicationToResponse(jobResponseDtoDto);
        _jobResponseRepository.Add(jobResponse);

        return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
    }
}
