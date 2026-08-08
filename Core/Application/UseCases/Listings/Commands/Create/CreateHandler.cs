using Application.Abstractions.Cache;
using Application.Abstractions.Jobs;
using Application.UseCases.Categories;
using Domain.Abstractions;
using Domain.Abstractions.Repositories;
using Domain.Exceptions.Category;
using FluentValidation;
using MediatR;

namespace Application.UseCases.Listings.Commands.Create;

internal sealed class CreateHandler(
    ICategoryRepository categoryRepository,
    IListingRepository listingRepository,
    ICacheService cacheService,
    IJobService jobService,
    IUnitOfWork unitOfWork,
    IValidator<CreateCommand> validator
) : IRequestHandler<CreateCommand, ListingResponse>
{
    public async Task<ListingResponse> Handle(CreateCommand command, CancellationToken ct)
    {
        await validator.ValidateAndThrowAsync(command, ct);
        
        var cachedCategories = await cacheService.GetAsync<IReadOnlyList<CategoryResponse>>(CacheKeys.CategoriesAll);
        
        var categoryExists = cachedCategories?.Any(c => c.CategoryId == command.CategoryId)
                             ?? await categoryRepository.ExistsByIdAsync(command.CategoryId, ct);

        if (!categoryExists)
            throw new CategoryNotFoundException(command.CategoryId);

        var listing = await listingRepository.AddAsync(command.ToListing(), ct);
        await unitOfWork.SaveChangesAsync(ct);

        await cacheService.RemoveByPrefixAsync(CacheKeys.ListingsByCategoryId(command.CategoryId));

        await jobService.ScheduleListingExpirationJob(listing.Id, TimeSpan.FromMinutes(1));
        
        return listing.ToListingResponse();
    }
}