using Domain.Exceptions.Common;

namespace Domain.Exceptions.Category;

public sealed class CategoryNotFoundException(Guid categoryId) 
    : NotFoundException($"Category with id: {categoryId} not found.");