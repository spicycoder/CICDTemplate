using CICDTemplate.Application.Abstractions.Clock;

namespace CICDTemplate.Infrastructure.Clock;

// <see cref="IDateTimeProvider"/>
public sealed class DateTimeProvider : IDateTimeProvider
{
    // <see cref="IDateTimeProvider"/>
    public DateTime Now => DateTime.UtcNow;
}
