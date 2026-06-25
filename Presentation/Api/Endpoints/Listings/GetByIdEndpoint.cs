using Api.Abstractions;
using Application.UseCases.Listings;
using Application.UseCases.Listings.Queries.GetById;
using Domain.Entities.Listings;
using MediatR;

namespace Api.Endpoints.Listings;

internal class GetByIdEndpoint : IEndpoint 
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("listings/{listingsId:guid}", async (
            Guid listingsId,
            ISender sender,
            CancellationToken ct) =>
        {
            var query = new GetByIdQuery(listingsId);
            var result = await sender.Send(query, ct);
            return TypedResults.Ok(result);
        })
        .AllowAnonymous()
        .WithTags(nameof(Listing))
        .Produces<ListingResponse>()
        .Produces(StatusCodes.Status404NotFound);
    }
}