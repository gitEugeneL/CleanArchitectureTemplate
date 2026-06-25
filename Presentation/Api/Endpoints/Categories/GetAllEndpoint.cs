using Api.Abstractions;
using Application.UseCases.Categories;
using Application.UseCases.Categories.Queries.GetAll;
using Domain.Entities.Categories;
using MediatR;

namespace Api.Endpoints.Categories;

internal class GetAllEndpoint : IEndpoint  
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("categories", async (ISender sender, CancellationToken ct) =>
        {
            var query = new GetAllQuery();
            var result = await sender.Send(query, ct);
            return TypedResults.Ok(result);
        })
        .AllowAnonymous()
        .WithTags(nameof(Category))
        .Produces<IReadOnlyList<CategoryResponse>>();
    }
}