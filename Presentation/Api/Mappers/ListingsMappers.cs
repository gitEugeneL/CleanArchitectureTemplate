using Application.UseCases.Listings.Commands.Create;
using Application.UseCases.Listings.Queries.GetAllByCategoryId;
using Contracts.Listings;

namespace Api.Mappers;

public static class ListingsMappers
{
    public static CreateCommand ToCreateCommand(this CreateListingRequest request)
    {
        return new CreateCommand(request.Title, request.Description, request.Price, request.CategoryId);
    }

    public static GetAllByCategoryIdQuery ToGetAllByCategoryIdQuery(
        this GetAllByCategoryIdRequest request,
        Guid categoryId)
    {
        return new GetAllByCategoryIdQuery(categoryId, request.PageNumber, request.PageSize);
    }
}