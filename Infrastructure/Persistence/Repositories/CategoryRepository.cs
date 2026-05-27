using Domain.Abstractions.Repositories;
using Domain.Entities.Categories;

namespace Persistence.Repositories;

internal class CategoryRepository(DataContext dataContext) : ICategoryRepository
{
    public Task<Category> AddAsync(Category category, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsByIdAsync(Guid categoryId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<Category?> GetByIdAsync(Guid categoryId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<Category>> GetAllAsync(CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}