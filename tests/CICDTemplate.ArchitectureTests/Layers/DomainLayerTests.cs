using System.Reflection;

using CICDTemplate.ArchitectureTests.Infrastructure;
using CICDTemplate.Domain.Entities;

using Shouldly;

using NetArchTest.Rules;

namespace CICDTemplate.ArchitectureTests.Layers;

public class DomainLayerTests : BaseTest
{
    [Fact]
    public void Domain_ShouldNotHaveDependencyOn_Application()
    {
        var result = Types.InAssembly(DomainAssembly)
            .Should()
            .NotHaveDependencyOn(ApplicationAssembly.GetName().Name)
            .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }

    [Fact]
    public void Domain_ShouldNotHaveDependencyOn_Infrastructure()
    {
        var result = Types.InAssembly(DomainAssembly)
            .Should()
            .NotHaveDependencyOn(InfrastructureAssembly.GetName().Name)
            .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }

    [Fact]
    public void Domain_ShouldNotHaveDependencyOn_Presentation()
    {
        var result = Types.InAssembly(DomainAssembly)
            .Should()
            .NotHaveDependencyOn(PresentationAssembly.GetName().Name)
            .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }

    [Fact]
    public void Entities_ShouldHave_PrivateParameterlessConstructor()
    {
        var entityTypes = Types.InAssembly(DomainAssembly)
            .That()
            .Inherit(typeof(Entity))
            .GetTypes()
            .ToList();

        var violatingEntities = entityTypes
            .SelectMany(x => x.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance))
            .Any(y => !y.IsPrivate || y.GetParameters().Length > 0);

        violatingEntities.ShouldBeFalse();
    }
}