using Domain.Entities.Categories;
using Domain.Entities.Common;
using Domain.Enums;
using Domain.Exceptions.Listing;

namespace Domain.Entities.Listings;

public class Listing : BaseEntity
{
    private Listing()
    {
    }
    
    public Listing(string title, string description, decimal price, Guid categoryId)
    {
        Title = title;
        Description = description;
        Price = price;
        CategoryId = categoryId;
    }

    public string Title { get; private set; } = null!;
    
    public string Description { get; private set; } = null!;
    
    public decimal Price { get; private set; }
    
    public Status Status { get; private set; } = Status.Open;
    
    public DateTime? ClosedDateTime { get; private set; }
    
    /*** Relations ***/
    public Guid CategoryId { get; private set; }
    public Category Category { get; private set; } = null!;

    public void Close()
    {
        if (Status == Status.Closed)
            throw new ListingAlreadyClosedException(Id);
        
        Status = Status.Closed;
        ClosedDateTime = DateTime.UtcNow;
    }
}