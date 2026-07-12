using Application.UseCases.Listings.Commands.Create;

namespace ApplicationTests.UseCases.Listings.Commands;

public class ValidatorTests
{
    private readonly Validator _validator = new();

    [Theory]
    [InlineData("Laptop Dell XPS 15", "High performance laptop", 1299.99)]
    [InlineData("iPhone 14 Pro", "Latest Apple smartphone", 999.00)]
    public async Task ValidCreateCommand_PassesValidation(string title, string description, decimal price)
    {
        // Arrange
        var model = new CreateCommand(title, description, price, Guid.NewGuid());
        
        // Act
        var result = await _validator.ValidateAsync(model);
        
        // Assert
        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData("", "Valid description", 100)] // Empty Title
    [InlineData("Valid title", "", 100)] // Empty Description
    [InlineData("Valid title", "Valid description", 0)] // Price too low
    [InlineData("Valid title", "Valid description", -10)] // Negative price
    [InlineData("Valid title", "Valid description", 10000)] // Price too high
    public async Task InvalidCreateCommand_FailsValidation(string title, string description, decimal price)
    {
        // Arrange
        var model = new CreateCommand(title, description, price, Guid.NewGuid());
        
        // Act
        var result = await _validator.ValidateAsync(model);
        
        // Assert
        Assert.False(result.IsValid);
    }
}