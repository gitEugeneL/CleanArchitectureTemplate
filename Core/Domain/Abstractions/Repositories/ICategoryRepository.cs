using Domain.Entities.Categories;

namespace Domain.Abstractions.Repositories;

public interface ICategoryRepository
{
    Task<Category> AddAsync(Category category, CancellationToken ct);
    
    Task<bool> ExistsByIdAsync(Guid categoryId, CancellationToken ct);
    
    Task<Category?> GetByIdAsync(Guid categoryId, CancellationToken ct);
    
    Task<IReadOnlyList<Category>> GetAllAsync(CancellationToken ct);
}