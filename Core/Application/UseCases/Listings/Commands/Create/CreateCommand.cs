using MediatR;

namespace Application.UseCases.Listings.Commands.Create;

public record CreateCommand(
    string Title,
    string Description,
    decimal Price,
    Guid CategoryId
) : IRequest<ListingResponse>;