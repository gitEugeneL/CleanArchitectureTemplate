using MediatR;

namespace Application.UseCases.Listings.Queries.GetById;

public sealed record GetByIdQuery(Guid ListingId) : IRequest<ListingResponse>;