using Domain.Entities.Listings;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration;

public class ListingConfiguration : IEntityTypeConfiguration<Listing>
{
    public void Configure(EntityTypeBuilder<Listing> builder)
    {
        builder.HasKey(l => l.Id);
        
        builder.Property(l => l.Title)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(l => l.Description)
            .HasMaxLength(1000);
        
        builder.Property(l => l.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(l => l.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasDefaultValue(Status.Open);

        builder.Property(l => l.ClosedDateTime)
            .IsRequired(false);
    }
}