using CICDTemplate.ArchitectureTests.Infrastructure;

using FluentAssertions;

using NetArchTest.Rules;

namespace CICDTemplate.ArchitectureTests.Layers;

public class InfrastructureTests : BaseTest
{
    [Fact]
    public void Infrastructure_ShouldNotHaveDependencyOn_Presentation()
    {
        var result = Types.InAssembly(InfrastructureAssembly)
            .Should()
            .NotHaveDependencyOn(PresentationAssembly.GetName().Name)
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }
}
