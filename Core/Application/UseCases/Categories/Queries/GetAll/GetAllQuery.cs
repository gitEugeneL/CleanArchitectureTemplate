using MediatR;

namespace Application.UseCases.Categories.Queries.GetAll;

public sealed record GetAllQuery : IRequest<IReadOnlyList<CategoryResponse>>;