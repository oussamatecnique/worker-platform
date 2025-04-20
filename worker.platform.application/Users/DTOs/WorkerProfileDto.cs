namespace worker.platform.application.Users.DTOs;

public record WorkerProfileDto(int Id, string FirstName, string LastName, string CIN, string PhoneNumber, int UserId, string Address, int JobCategoryId, int LivingCityId);
