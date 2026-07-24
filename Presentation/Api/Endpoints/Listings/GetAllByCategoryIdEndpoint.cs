using Api.Abstractions;
using Api.Mappers;
using Application.UseCases.Common;
using Application.UseCases.Listings;
using Contracts.Listings;
using Domain.Entities.Listings;
using MediatR;

namespace Api.Endpoints.Listings;

public class GetAllByCategoryIdEndpoint : IEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("categories/{categoryId:guid}/listings", 
                async (Guid categoryId,
                    [AsParameters] GetAllByCategoryIdRequest request,
                    ISender sender, 
                    CancellationToken ct) =>
                {
                    var query = request.ToGetAllByCategoryIdQuery(categoryId);
                    var result = await sender.Send(query, ct);
                    return TypedResults.Ok(result);
                })
        .AllowAnonymous()
        .WithTags(nameof(Listing))
        .Produces<PaginationResult<ListingResponse>>();
    }
}