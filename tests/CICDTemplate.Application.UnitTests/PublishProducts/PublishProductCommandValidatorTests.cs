﻿using CICDTemplate.Application.PubSub.Commands.PublishProduct;

using Shouldly;

namespace CICDTemplate.Application.UnitTests.PublishProducts;

public class PublishProductCommandValidatorTests
{
    [Fact]
    public void Validate_HappyPath_Valid()
    {
        // Arrange
        var validator = new PublishProductCommandValidator();
        var command = new PublishProductCommand("Cookies", "Yummy!");

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.ShouldBeTrue();
    }

    [Fact]
    public void Validate_EmptyName_Invalid()
    {
        // Arrange
        var validator = new PublishProductCommandValidator();
        var command = new PublishProductCommand(string.Empty, "Yummy!");

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.ShouldBeFalse();
    }

    [Fact]
    public void Validate_NameTooLong_Invalid()
    {
        // Arrange
        var validator = new PublishProductCommandValidator();
        var command = new PublishProductCommand(string.Join(' ', Enumerable.Range(1, 11)), "Yummy!");

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.ShouldBeFalse();
    }

    [Fact]
    public void Validate_EmptyDescription_valid()
    {
        // Arrange
        var validator = new PublishProductCommandValidator();
        var command = new PublishProductCommand("Cookies", string.Empty);

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.ShouldBeTrue();
    }

    [Fact]
    public void Validate_DescriptionTooLong_Invalid()
    {
        // Arrange
        var validator = new PublishProductCommandValidator();
        var command = new PublishProductCommand("Cookie", string.Join(' ', Enumerable.Range(1, 101)));

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.ShouldBeFalse();
    }
}