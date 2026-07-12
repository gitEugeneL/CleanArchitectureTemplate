using Api.Abstractions;
using Api.Mappers;
using Application.UseCases.Listings;
using Contracts.Listings;
using Domain.Entities.Listings;
using MediatR;

namespace Api.Endpoints.Listings;

public class CreateEndpoint : IEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPost("listings", async (CreateListingRequest request, ISender sender, CancellationToken ct) =>
            {
                var result = await sender.Send(request.ToCreateCommand(), ct);
                return TypedResults.Created($"listings/{result.ListingId}", result);
            })
            .AllowAnonymous()
            .WithTags(nameof(Listing))
            .Produces<ListingResponse>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound);
    }
}