using CICDTemplate.Application.States.Commands.SaveState;

using FluentAssertions;

namespace CICDTemplate.Application.UnitTests.SaveState;

public class SaveStateCommandValidatorTests
{
    [Fact]
    public void Validate_HappyPath_Valid()
    {
        // Arrange
        var validator = new SaveStateCommandValidator();
        var command = new SaveStateCommand("Cookies", "Yummy!");

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_EmptyName_Invalid()
    {
        // Arrange
        var validator = new SaveStateCommandValidator();
        var command = new SaveStateCommand(string.Empty, "Yummy!");

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Validate_NameTooLong_Invalid()
    {
        // Arrange
        var validator = new SaveStateCommandValidator();
        var command = new SaveStateCommand(string.Join(' ', Enumerable.Range(1, 11)), "Yummy!");

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
    }



    [Fact]
    public void Validate_EmptyDescription_valid()
    {
        // Arrange
        var validator = new SaveStateCommandValidator();
        var command = new SaveStateCommand("Cookies", string.Empty);

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_DescriptionTooLong_Invalid()
    {
        // Arrange
        var validator = new SaveStateCommandValidator();
        var command = new SaveStateCommand("Cookie", string.Join(' ', Enumerable.Range(1, 101)));

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
    }
}