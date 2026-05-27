using Domain.Entities.Common;
using Domain.Entities.Listings;

namespace Domain.Entities.Categories;

public class Category : BaseEntity
{
    private Category()
    {
    }
    
    public Category(string name, string? description = null)
    {
        Name = name;
        Description = description;
    }

    public string Name { get; private set; } = null!;
    public string? Description { get; private set;}
    
    /*** Relations ***/
    public ICollection<Listing> Listings { get; private set; } = new HashSet<Listing>();
}