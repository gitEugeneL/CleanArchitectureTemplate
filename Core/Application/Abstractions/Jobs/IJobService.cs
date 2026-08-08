namespace Application.Abstractions.Jobs;

public interface IJobService
{
    Task ScheduleListingExpirationJob(Guid listingId, TimeSpan delay);
}