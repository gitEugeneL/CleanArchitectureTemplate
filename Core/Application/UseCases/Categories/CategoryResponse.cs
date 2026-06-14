namespace Application.UseCases.Categories;

public sealed record CategoryResponse(
    Guid CategoryId, 
    string Name
);
