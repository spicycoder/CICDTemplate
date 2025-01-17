using CICDTemplate.ArchitectureTests.Infrastructure;

using Shouldly;

using FluentValidation;

using MediatR;

using NetArchTest.Rules;

namespace CICDTemplate.ArchitectureTests.Layers;

public class ApplicationLayerTests : BaseTest
{
    [Fact]
    public void Application_ShouldNotHaveDependencyOn_Infrastructure()
    {
        var result = Types.InAssembly(ApplicationAssembly)
            .Should()
            .NotHaveDependencyOn(InfrastructureAssembly.GetName().Name)
            .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }

    [Fact]
    public void Application_ShouldNotHaveDependencyOn_Presentation()
    {
        var result = Types.InAssembly(ApplicationAssembly)
            .Should()
            .NotHaveDependencyOn(PresentationAssembly.GetName().Name)
            .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }

    [Fact]
    public void Handler_ShouldHaveName_EndingWithHandler()
    {
        var result = Types.InAssembly(ApplicationAssembly)
            .That()
            .ImplementInterface(typeof(IRequestHandler<,>))
            .Should()
            .HaveNameEndingWith("Handler", StringComparison.InvariantCulture)
            .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }

    [Fact]
    public void Validator_ShouldHaveName_EndingWithValidator()
    {
        var result = Types.InAssembly(ApplicationAssembly)
            .That()
            .Inherit(typeof(AbstractValidator<>))
            .Should()
            .HaveNameEndingWith("Validator", StringComparison.InvariantCulture)
            .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }
}