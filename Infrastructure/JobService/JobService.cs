using Application.Abstractions.Jobs;
using Hangfire;
using JobService.Jobs;

namespace JobService;

internal class JobService(IBackgroundJobClient jobClient) : IJobService
{
    public Task ScheduleListingExpirationJob(Guid listingId, TimeSpan delay)
    {
        jobClient.Schedule<ListingExpirationJob>(
            job => job.Execute(listingId),
            delay
        );
        return Task.CompletedTask;
    }
}