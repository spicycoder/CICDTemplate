using CICDTemplate.Application.Products.Commands.PublishProduct;

using FluentAssertions;

namespace CICDTemplate.Application.UnitTests.Products.PublishProducts;

public class PublishProductCommandValidatorTests
{
    [Fact]
    public void Validate_HappyPath_Valid()
    {
        // arrange
        var validator = new PublishProductCommandValidator();
        var command = new PublishProductCommand("Cookies", "Yummy!");

        // act
        var result = validator.Validate(command);

        // assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_EmptyName_Invalid()
    {
        // arrange
        var validator = new PublishProductCommandValidator();
        var command = new PublishProductCommand(string.Empty, "Yummy!");

        // act
        var result = validator.Validate(command);

        // assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Validate_NameTooLong_Invalid()
    {
        // arrange
        var validator = new PublishProductCommandValidator();
        var command = new PublishProductCommand(string.Join(' ', Enumerable.Range(1, 11)), "Yummy!");

        // act
        var result = validator.Validate(command);

        // assert
        result.IsValid.Should().BeFalse();
    }



    [Fact]
    public void Validate_EmptyDescription_valid()
    {
        // arrange
        var validator = new PublishProductCommandValidator();
        var command = new PublishProductCommand("Cookies", string.Empty);

        // act
        var result = validator.Validate(command);

        // assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_DescriptionTooLong_Invalid()
    {
        // arrange
        var validator = new PublishProductCommandValidator();
        var command = new PublishProductCommand("Cookie", string.Join(' ', Enumerable.Range(1, 101)));

        // act
        var result = validator.Validate(command);

        // assert
        result.IsValid.Should().BeFalse();
    }
}
