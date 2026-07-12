using Application.UseCases.Listings.Commands.Create;
using Contracts.Listings;

namespace Api.Mappers;

public static class ListingsMappers
{
    public static CreateCommand ToCreateCommand(this CreateListingRequest request)
    {
        return new CreateCommand(request.Title, request.Description, request.Price, request.CategoryId);
    }
}