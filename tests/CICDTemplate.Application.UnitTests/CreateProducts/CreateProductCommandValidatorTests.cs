using CICDTemplate.Application.Products.Commands.CreateProduct;

using Shouldly;

namespace CICDTemplate.Application.UnitTests.CreateProducts;

public class CreateProductCommandValidatorTests
{
    [Fact]
    public void Validate_HappyPath_Valid()
    {
        // Arrange
        var validator = new CreateProductCommandValidator();
        var command = new CreateProductCommand("Cookies", "Yummy!");

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.ShouldBeTrue();
    }

    [Fact]
    public void Validate_EmptyName_Invalid()
    {
        // Arrange
        var validator = new CreateProductCommandValidator();
        var command = new CreateProductCommand(string.Empty, "Yummy!");

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.ShouldBeFalse();
    }

    [Fact]
    public void Validate_NameTooLong_Invalid()
    {
        // Arrange
        var validator = new CreateProductCommandValidator();
        var command = new CreateProductCommand(string.Join(' ', Enumerable.Range(1, 11)), "Yummy!");

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.ShouldBeFalse();
    }

    [Fact]
    public void Validate_EmptyDescription_valid()
    {
        // Arrange
        var validator = new CreateProductCommandValidator();
        var command = new CreateProductCommand("Cookies", string.Empty);

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.ShouldBeTrue();
    }

    [Fact]
    public void Validate_DescriptionTooLong_Invalid()
    {
        // Arrange
        var validator = new CreateProductCommandValidator();
        var command = new CreateProductCommand("Cookie", string.Join(' ', Enumerable.Range(1, 101)));

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.ShouldBeFalse();
    }
}