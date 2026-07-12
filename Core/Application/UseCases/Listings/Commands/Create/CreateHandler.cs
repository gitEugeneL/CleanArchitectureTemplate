using Domain.Abstractions;
using Domain.Abstractions.Repositories;
using Domain.Exceptions.Category;
using FluentValidation;
using MediatR;

namespace Application.UseCases.Listings.Commands.Create;

internal class CreateHandler(
    ICategoryRepository categoryRepository,
    IListingRepository listingRepository,
    IUnitOfWork unitOfWork,
    IValidator<CreateCommand> validator
) : IRequestHandler<CreateCommand, ListingResponse>
{
    public async Task<ListingResponse> Handle(CreateCommand command, CancellationToken ct)
    {
        await validator.ValidateAndThrowAsync(command, ct);
        
        // TODO Check if category exists (cache service)
        
        var categoryExists = await categoryRepository.ExistsByIdAsync(command.CategoryId, ct);
        if (!categoryExists)
            throw new CategoryNotFoundException(command.CategoryId);

        var listing = await listingRepository.AddAsync(command.ToListing(), ct);
        await unitOfWork.SaveChangesAsync(ct);
        
        // TODO clear listings cache
        
        return listing.ToListingResponse();
    }
}