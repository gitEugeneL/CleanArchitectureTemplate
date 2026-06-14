using Domain.Abstractions.Repositories;
using Domain.Entities.Categories;
using Microsoft.EntityFrameworkCore;

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

    public async Task<IReadOnlyList<Category>> GetAllAsync(CancellationToken ct)
    {
        return await dataContext
            .Categories
            .AsNoTracking()
            .OrderBy(category => category.Name)
            .ToListAsync(ct);
    }
}