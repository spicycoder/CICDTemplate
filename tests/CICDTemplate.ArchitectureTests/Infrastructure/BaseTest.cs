using System.Reflection;

using CICDTemplate.Application.Abstractions.Clock;
using CICDTemplate.Domain.Entities;
using CICDTemplate.Infrastructure;

namespace CICDTemplate.ArchitectureTests.Infrastructure;

public abstract class BaseTest
{
    protected static readonly Assembly DomainAssembly = typeof(Entity).Assembly;
    protected static readonly Assembly ApplicationAssembly = typeof(IDateTimeProvider).Assembly;
    protected static readonly Assembly InfrastructureAssembly = typeof(ApplicationDbContext).Assembly;
    protected static readonly Assembly PresentationAssembly = typeof(Program).Assembly;
}